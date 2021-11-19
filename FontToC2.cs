using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace FontConv
{
    class FontToC2
    {
        GlyphSet Set = new GlyphSet();

        string _TempFolder = "";

        Font _Font;

        bool _2Bit = false;

        int _Left;
        int _Right;

        InternalFontData FontData = null;

        public FontToC2()
        {
            _Font = new Font("Times New Roman", 10);
            _Left = 0;
            _Right = 0;

        }

        public bool TwoBitMode
        {
            get { return _2Bit; }
            set { _2Bit = value; }
        }

        public string TempFolder
        {
            get { return _TempFolder; }
            set { _TempFolder = value; }
        }

        public bool DropExternal = false;

        public Font SelectedFont
        {
            get
            {
                return _Font;
            }

            set
            {
                _Font = value;
            }
        }
        public int Left
        {
            get
            {
                return _Left;
            }

            set
            {
                _Left = value;
            }
        }

        public int Right
        {
            get
            {
                return _Right;
            }

            set
            {
                _Right = value;
            }
        }

        private int GetNumWidth()
        {
            int num_width = 0;

            for (int i = '0'; i <= '9'; i++) // numbers
                num_width = Math.Max(Convert.ToInt32(GetSymbolSize(i, true).Width), num_width);

            return num_width;
        }

        private SizeF GetSymbolSize(int Symb, bool Truncate = false) => Glyph.GetSymbolSize(_Font, Convert.ToByte(Symb), _Left, _Right, Truncate);

        private RectangleF GetSymbolRect(int Symb, bool Truncate = false) => Glyph.GetSymbolRect(_Font, Convert.ToByte(Symb), _Left, _Right, Truncate);

        public void ConvertString(string Text)
        {
            int num_width = GetNumWidth();

            Set.Left = _Left;
            Set.Right = _Right;
            Set.TwoBit = _2Bit;
            Set.DropExternal = DropExternal;

            PreConvertSymbol(Convert.ToByte(32), Convert.ToInt32(GetSymbolSize(32).Width)); // first symbol - space

            for (int i = 1; i < 256; i++)
            {
                if (Text.IndexOf(Glyph.W1251ToUnicode(Convert.ToByte(i))) >= 0)
                {
                    int width = 0;
                    if (i == 32)
                        width = Convert.ToInt32(GetSymbolSize(32).Width);

                    if ((i >= '0') && (i <= '9'))
                        width = num_width;

                    PreConvertSymbol(Convert.ToByte(i), width);
                }
            }

            Binarize();
        }

        public void ConvertAll()
        {
            int num_width = GetNumWidth();

            float mintop = float.MaxValue, maxbot = 0;

            for (int i = 0; i < 256; i++) // ANSI table
            {
                RectangleF rect = GetSymbolRect(i);
                mintop = Math.Min(rect.Top, mintop);
                maxbot = Math.Max(rect.Bottom, maxbot);
            }

            Set.Left = _Left;
            Set.Right = _Right;
            Set.TwoBit = _2Bit;
            Set.DropExternal = DropExternal;

            for (int i = 0; i < 256; i++) // ANSI table
            {
                int width = 0;
                if (i == 32)
                {
                    width = Convert.ToInt32(GetSymbolSize(32).Width);
                }
                if ((i >= '0') && (i <= '9'))
                {
                    width = num_width;
                }
                PreConvertSymbol(Convert.ToByte(i), width);
            }

            // Здесь весь нужный набор глифов загружен
            Binarize();
        }

        void PreConvertSymbol(byte Symb, int width)
        {
            Set.Add(_Font, Symb, width);
        }

        void Binarize()
        {
            FontData = Set.ConvertToData();

            // Есть массив данных и индексов, надо его превратить в текст
        }


        private string GetBytesLine(string spaces, int Offset, int Length, int CharIndex)
        {
            int counter = Length;
            string _text = "";
            const int maxcharsinline = 16;

            // 1. Header
            for (int i = 0; i < 3; i++)
            {
                if (i < 2)
                    _text += string.Format("{0}, ", FontData.Data[Offset + i]);
                else
                    _text += string.Format("0x{0:x2}, ", FontData.Data[Offset + i]);
            }
            _text += string.Format(" // 0x{0:x2}\r\n", CharIndex) + spaces;

            int charsinline = 0;

            for (int i = 3; i < Length; i++)
            {
                _text += string.Format("0x{0:x2}, ", FontData.Data[Offset + i]);
                charsinline++;

                if ((charsinline == maxcharsinline) && (i < Length - 1))
                {
                    _text += "\r\n" + spaces;
                    charsinline = 0;
                }
            }
            return _text;
        }

        public bool Available => FontData != null;

        public int FontSize => FontData?.Data.Count ?? 0;

        public void SaveFile(string filename, string fontname)
        {
            fontname = fontname.ToLower();

            string _text = "";

            //_text += "// Font data for graphical project \r\n";
            //_text += "// use: SetFont(<index>, &" + fontname + "); \r\n\r\n";
            //_text += "// use: SetFont(<index>, " +
            //         string.Format("(uint8_t *){0:s}_index, (uint8_t *){0:s}_data, {1:s}_CHAR_COUNT, {1:s}_MAX_HEIGHT",
            //                       fontname,
            //                       fontname.ToUpper()) + "); \r\n\r\n";

            _text += "#include <stdint.h>\r\n";

            //_text += "// Count of characters in a font \r\n";
            //_text += "#define " + fontname.ToUpper() + "_CHAR_COUNT\t" +
            //         Convert.ToString(FontData.Indexes.Length) + "\r\n";
            //_text += "// Max height of characters in a font \r\n";
            //_text += "#define " + fontname.ToUpper() + "_MAX_HEIGHT\t" +
            //         Convert.ToString(FontData.MaxHeight) + "\r\n\r\n";

            //_text += "// Character data offset from start of array (2 byte per character): \r\n";

            //_text += "// Character data: \r\n";
            string temp = string.Format("const __flash uint8_t {0:S}_data [{1:D}] = ", fontname, FontData.Data.Count) + "{" + "\r\n" + "   ";
            _text += temp;

            string spaces = "";
            for (int i = 0; i < temp.Length; i++)
            {
                spaces += " ";
            }

            int Test = 0;
            foreach (var B in FontData.Blocks)
            {
                Test += B.Length;
                _text += GetBytesLine("   ", B.Index, B.Length, B.Char) + "\r\n" + "   ";
            }

            if (Test != FontData.Data.Count)
                Debug.WriteLine("Invalid write!");

            _text += "};\r\n\r\n";
            temp = string.Format("const __flash uint8_t {0:S}_index [{1:D}] = ", fontname, FontData.Indexes.Length * 2) + "{";
            _text += temp;

            spaces = "";
            for (int i = 0; i < temp.Length; i++)
            {
                spaces += " ";
            }

            for (int i = 0; i < FontData.Indexes.Length; i++)
            {
                _text += string.Format("0x{0:x2}, 0x{1:x2}, // {2:0} \"{3:S}\"  ", // 
                                               (FontData.Indexes[i] & 0xFF00) >> 8,
                                               FontData.Indexes[i] & 0xFF,
                                               i,
                                               Glyph.W1251ToUnicode(Convert.ToByte((i < 32) ? 32 : i))) + "\r\n" + spaces;
            }
            _text += "};\r\n\r\n";

            //_text += "const sFontRec " + fontname + " = {" +
            //    string.Format(" {0:d}, (uint8_t*){1:s}_index, (uint8_t*){1:s}_data, {2:d}, {3:d} ", FontData.TwoBit ? 2 : 1, fontname, FontData.Indexes.Length, FontData.MaxHeight) + "};";

            if (File.Exists(filename))
                File.Delete(filename);

            TextWriter tw = new StreamWriter(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write),
                                             Encoding.GetEncoding(1251));
            tw.Write(_text);
            tw.Close();
        }

        public void _Test()
        {
            if (!((FontData.Data.Count > 0) && (FontData.Indexes.Length > 0)))
            {
                // no data
                return;
            }

            // Ok, data nya
            int index = 0; // index of symbol;
            int offset = 0; // data offset

            while (index < FontData.Indexes.Length)
            {
                if (offset >= FontData.Data.Count) break;
                //               int offsetx = _Index[int];
                int w = FontData.Data[offset];
                int h = FontData.Data[offset + 1];
                int bef = FontData.Data[offset + 2] & 0x0F;
                int aft = (FontData.Data[offset + 2] & 0xF0) >> 4;

                offset += 3;
                Bitmap bmp = new Bitmap(w, h + bef + aft);

                int bit_counter = 0;
                int temp = 0;
                int counter = 0;
                while (counter < w * h)
                {
                    int x = counter % w;
                    int y = counter / w + bef;
                    if (bit_counter == 0)
                    {
                        temp = FontData.Data[offset];
                        offset += 1;
                    }

                    if (_2Bit)
                    {
                        switch (temp & 3)
                        {
                            case 0: break;
                            case 1: bmp.SetPixel(x, y, Color.FromArgb(170, 170, 170)); break;
                            case 2: bmp.SetPixel(x, y, Color.FromArgb(85, 85, 85)); break;
                            case 3: bmp.SetPixel(x, y, Color.Black); break;
                        }

                        temp >>= 2;
                        bit_counter += 2;
                    }
                    else
                    {
                        if ((temp & 1) == 1)
                        {
                            bmp.SetPixel(x, y, Color.Black);
                        }

                        temp >>= 1;
                        bit_counter++;
                    }
                    counter++;
                    if (bit_counter == 8) bit_counter = 0;
                }

                if (_TempFolder != "")
                {
                    bmp.Save(_TempFolder + Convert.ToString(index) + "_unpack.png", System.Drawing.Imaging.ImageFormat.Png);
                }
                bmp.Dispose();
                index++;
            }
        }
    }
}
