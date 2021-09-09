using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FontConv
{
    class GlyphSet
    {
        public List<Glyph> Glyphs = new List<Glyph>();
        public int Left;
        public int Right;
        public bool TwoBit;

        public bool DropExternal = false;

        public void Add(Font F, byte Symb, int width)
        {
            if (Glyphs.Count(G => G._Char == Symb) == 0)
            {
                Glyphs.Add(new Glyph(F, Symb, width, Left, Right, TwoBit));
            }
        }

        public int MinBefore => Glyphs.Min(G => G.LinesBefore);
        public int MinAfter => Glyphs.Min(G => G.LinesAfter);

        public int MaxHeight => Glyphs.Max(G => ((G._Char > 32) ? G.Height : 0));

        public InternalFontData ConvertToData()
        {
            var MB = MinBefore;
            var MA = MinAfter;

            var Res = new InternalFontData();
            Res.TwoBit = TwoBit;
            Res.MaxHeight = MaxHeight - MinBefore - MinAfter;

            for (int i = 0; i < 256; i++)
            {
                var G = GetGlyph(i);

                if (G != null)
                {
                    if (G.Width > 0)
                    {
                        var CharData = G.GetBitmapData(MB, MA);
                        
                        if (!DropExternal || ((Res.Data.Count + CharData.Length) < 0x10000))
                        {
                            int Offset = Res.Data.Count;
                            Res.Indexes[i] = Offset;
                            Res.Blocks.Add(new FontCharBlock(Convert.ToByte(i), Res.Data.Count, CharData.Length));
                            Res.Data.AddRange(CharData);

                            // Debug.WriteLine($"{i:x2}: O{Offset} L{CharData.Length} W{G.Width} H{G.Height}");
                        }
                    }
                }
            }

            return Res;
        }

        Glyph GetGlyph(int Char)
        {
            foreach(var G in Glyphs)
            {
                if (G._Char == Char)
                    return G;
            }
            return null;
        }
    }


    class InternalFontData
    {
        public int[] Indexes = new int[256];
        public List<FontCharBlock> Blocks = new List<FontCharBlock>();

        public List<byte> Data = new List<byte>();

        public int MaxHeight;
        public bool TwoBit;

        public InternalFontData()
        {
            for (int i = 0; i < Indexes.Length; i++)
                Indexes[i] = 0;
        }
    }

    class FontCharBlock { public byte Char; public int Index; public int Length; public FontCharBlock(byte Char, int Offset, int Len) { this.Char = Char; Index = Offset; Length = Len; } }

}
