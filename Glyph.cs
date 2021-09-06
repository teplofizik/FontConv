using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FontConv
{
    class Glyph
    {
        public readonly byte _Char;

        int _Left;
        int _Right;

        int _Width;
        int _MaxHeight;
        bool _2Bit;

        int _LinesBefore;
        int _LinesAfter;

        int _LinesLeft;
        int _LinesRight;

        Font _Font;

        Bitmap bmp;

        public int LinesBefore => _LinesBefore;
        public int LinesAfter => _LinesAfter;

        public Glyph(Font Font, byte symb, int width, int Left, int Right, bool TwoBit)
        {
            _Char = symb;
            _Left = Left;
            _Right = Right;
            _Font = Font;
            _2Bit = TwoBit;
            _Width = width;

            ConstructImage();
        }

        public int Width => bmp?.Width ?? 0;
        public int Height => bmp?.Height ?? 0;

        private void ConstructImage()
        {
            SizeF size = GetSymbolSize(false);

            bmp = new Bitmap(Convert.ToInt32(size.Width), Convert.ToInt32(size.Height));

            int w = bmp.Width;
            int h = bmp.Height;

            int cl = _Left;
            int cr = _Right;

            // TODO: change algorithm
            if (bmp.Width - (_Left + _Right) > 10)
            {
                w -= (cl + cr);
            }
            else
            {
                cl = 0;
                cr = 0;
            }

            Graphics surface = Graphics.FromImage(bmp);

            string symbol = W1251ToUnicode(_Char);
            surface.TextRenderingHint = (_2Bit) ? System.Drawing.Text.TextRenderingHint.AntiAlias : System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            surface.DrawString(symbol, _Font, new SolidBrush(Color.Black), new PointF(0, 0));

            if (_Char == 48) // '0'
            {
                //_MaxHeight = _MaxHeight;
            }

            int counter = 0;
            int lines_bef = 0;
            int lines_aft = 0;
            int lines_left = 0;
            int lines_right = 0;

            // Cut empty lines
            while (counter < h)
            {
                int calc_color = 0;
                for (int x = 0; x < w; x++)
                {
                    Color color = bmp.GetPixel(x, counter);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_bef = counter;
                    break;
                }
                counter++;
            }
            if (counter == h) lines_bef = h;

            counter = h - 1;
            while (counter >= 0)
            {
                int calc_color = 0;
                for (int x = 0; x < w; x++)
                {
                    Color color = bmp.GetPixel(x, counter);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_aft = h - counter - 1;
                    break;
                }
                counter--;
            }
            if (counter == -1) lines_aft = h - 1;

            counter = 0;
            while (counter < w)
            {
                int calc_color = 0;
                for (int y = 0; y < h; y++)
                {
                    Color color = bmp.GetPixel(counter, y);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_left = counter;
                    counter = 0;
                    break;
                }
                counter++;
            }
            if (counter > 0) lines_left = counter;

            counter = 0;
            while (counter < w)
            {
                int calc_color = 0;
                for (int y = 0; y < h; y++)
                {
                    Color color = bmp.GetPixel(w - counter - 1, y);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_right = counter;
                    counter = 0;
                    break;
                }
                counter++;
            }

            if (counter > 0) lines_right = counter;

            _LinesBefore = lines_bef;
            _LinesAfter = lines_aft;

            _LinesLeft = lines_left;
            _LinesRight = lines_right;
        }

        public byte[] GetBitmapData(int SubBefore, int SubAfter)
        {
            int w = bmp.Width;
            int h = bmp.Height - SubAfter - SubBefore;

            int lines_bef = _LinesBefore - SubBefore;
            int lines_aft = _LinesAfter - SubAfter;
            int lines_left = _LinesLeft;
            int lines_right = _LinesRight;

            if (lines_bef > 15) lines_bef = 15;
            if (lines_aft > 15) lines_aft = 15;

            if ((lines_aft + lines_bef) > h) { lines_aft = h - lines_bef; };
            if ((lines_left == w) && (lines_right == w)) { lines_right = w / 2; lines_left = w / 2; };

            if ((lines_left + lines_right) > w) { lines_right = w - lines_left; };

            int _w = w - lines_left - lines_right + _Left + _Right;
            if (_Width > 0)
            {
                int __w = _w;

                _w = _Width;
                if (_Width >= w)
                {
                    lines_left = _Left;
                    _w = w;
                }
                else //if (width >= _w) (width < _w)
                {
                    lines_left -= (_Width - __w) / 2;
                }

                if (lines_left < _Left) lines_left = _Left;
            }

            var Res = new List<byte>();

            Res.Add(Convert.ToByte(_w));   // w
            Res.Add(Convert.ToByte(h - lines_aft - lines_bef));   // h
            Res.Add(Convert.ToByte((lines_bef & 0x0F) + ((lines_aft << 4) & 0xF0))); // How many lines skip before textout

            byte temp = 0;
            byte cntr = 0;

            int counter = 0;

            var Height = h - lines_aft - lines_bef;
            while (counter < _w * Height)
            {
                int x = (counter % _w) + lines_left - _Left;
                int y = (counter / _w) + lines_bef + SubBefore;

                Color color = (x >= bmp.Width) ? Color.FromArgb(0) : bmp.GetPixel(x, y);

                if (_2Bit)
                {
                    int C = color.A >> 6; // Only 2 bit

                    temp |= Convert.ToByte(C << cntr);
                    if (cntr == 6) // byte is full
                    {
                        Res.Add(temp);
                        temp = 0;
                        cntr = 0;
                    }
                    else
                        cntr += 2;
                }
                else
                {
                    if (color.A > 50)
                    {
                        temp |= Convert.ToByte(1 << cntr);
                    }

                    if (cntr == 7) // byte is full
                    {
                        Res.Add(temp);
                        temp = 0;
                        cntr = 0;
                    }
                    else
                        cntr++;
                }

                counter++;
            }

            if (cntr > 0)
                Res.Add(temp);

            return Res.ToArray();
        }

        public static string W1251ToUnicode(byte src)
        {
            Encoding srcEncodingFormat = Encoding.GetEncoding("windows-1251");
            Encoding dstEncodingFormat = Encoding.Unicode;
            byte[] originalByteString = { src };
            byte[] convertedByteString = Encoding.Convert(srcEncodingFormat,
            dstEncodingFormat, originalByteString);
            return dstEncodingFormat.GetString(convertedByteString);
        }

        SizeF GetSymbolSize(bool truncate) => GetSymbolSize(_Font, _Char, _Left, _Right, truncate);

        public static SizeF GetSymbolSize(Font F, byte Symb, int Left, int Right, bool truncate)
        {
            RectangleF rect = GetSymbolRect(F, Symb, Left, Right, truncate);

            return new SizeF(rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        public static RectangleF GetSymbolRect(Font F, byte Symb, int Left, int Right, bool truncate)
        {
            Bitmap tempbmp = new Bitmap(10, 10);

            string symbol = W1251ToUnicode(Symb);
            Graphics surface = Graphics.FromImage(tempbmp);
            SizeF size = surface.MeasureString(symbol, F);

            surface.Dispose();
            tempbmp.Dispose();

            if (!truncate)
            {
                return new RectangleF(0, 0, size.Width, size.Height);
            }

            int h = Convert.ToInt32(size.Height);
            int w = Convert.ToInt32(size.Width);

            tempbmp = new Bitmap(w, h);
            surface = Graphics.FromImage(tempbmp);
            surface.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            surface.DrawString(symbol, F, new SolidBrush(Color.Black), new PointF(0, 0));

            int counter = 0;
            int lines_bef = 0;
            int lines_aft = 0;
            int lines_left = 0;
            int lines_right = 0;

            // Cut empty lines
            while (counter < h)
            {
                int calc_color = 0;
                for (int x = 0; x < w; x++)
                {
                    Color color = tempbmp.GetPixel(x, counter);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_bef = counter;
                    break;
                }
                counter++;
            }
            if (counter == h) lines_bef = h;

            counter = h - 1;
            while (counter >= 0)
            {
                int calc_color = 0;
                for (int x = 0; x < w; x++)
                {
                    Color color = tempbmp.GetPixel(x, counter);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_aft = h - counter - 1;
                    break;
                }
                counter--;
            }
            if (counter == -1) lines_aft = -1;

            counter = 0;
            while (counter < w)
            {
                int calc_color = 0;
                for (int y = 0; y < h; y++)
                {
                    Color color = tempbmp.GetPixel(counter, y);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_left = counter;
                    counter = 0;
                    break;
                }
                counter++;
            }
            if (counter > 0) lines_left = counter;

            counter = 0;
            while (counter < w)
            {
                int calc_color = 0;
                for (int y = 0; y < h; y++)
                {
                    Color color = tempbmp.GetPixel(w - counter - 1, y);
                    calc_color += color.A;
                }
                if (calc_color != 0) // not empty line
                {
                    lines_right = counter;
                    counter = 0;
                    break;
                }
                counter++;
            }
            if (counter > 0) lines_right = counter;

            if ((lines_left == w) && (lines_right == w)) { lines_right = w / 2; lines_left = w / 2; };
            if ((lines_left + lines_right) > w) { lines_right = w - lines_left; };
            int _w = w - lines_left - lines_right + Left + Right;

            tempbmp.Dispose();
            surface.Dispose();

            return new RectangleF(lines_left - Left, lines_bef, _w, h - lines_aft - lines_bef);
        }
    }
}
