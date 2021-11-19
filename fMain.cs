using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FontConv
{
    public partial class fMain : Form
    {
        FontToC2 ftc2 = null;

        public fMain()
        {
            InitializeComponent();

            dlSave.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            bUpdate.PerformClick();
        }

        private void bFont_Click(object sender, EventArgs e)
        {
            float FontSize = dlFont.Font.Size;
            if (dlFont.ShowDialog() == DialogResult.Cancel) return;

            lSample.Font = dlFont.Font;
            ResetGeneratedFont();

            if (FontSize != dlFont.Font.Size) dlSave.FileName = "";
        }

        private void chAll_CheckedChanged(object sender, EventArgs e)
        {
            gChars.Enabled = !chAll.Checked;
        }

        private string GetFontName()
        {
            if (tName.Text.CompareTo("") == 0)
            {
                return String.Format("f{0:d}", Convert.ToInt32(dlFont.Font.Size));
            }
            else
            {
                return tName.Text;
            }

        }

        private void ResetGeneratedFont()
        {
            ftc2 = null;
            bSave.Enabled = false;

            lSize.Text = $"-";
            lSize.ForeColor = Color.Black;
        }

        private void UpdateFont()
        {
            ftc2 = new FontToC2();

            ftc2.SelectedFont = dlFont.Font;

            string sym = " ";
            {
                if (chNumbers.Checked) sym += "0123456789" + ",.=+-%°";
                if (chSpecial.Checked) sym += ".,():;\"" + "?!><%\\/—";

                if (chLatinBig.Checked) sym += "@ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (chLatinLittle.Checked) sym += "abcdefghijklmnopqrstuvwxyz";

                if (chRusBig.Checked) sym += "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
                if (chRusLittle.Checked) sym += "абвгдеёжзийклмнопрстуфхцчшщъыьэжюя";
            }

            //  ftc.TwoBitMode = ck2Bit.Checked;
            //  ftc.Left = 1;
            //  ftc.Right = 1;
            ftc2.TwoBitMode = ck2Bit.Checked;
            ftc2.DropExternal = ckDrop.Checked;
            ftc2.Left = 1;
            ftc2.Right = 1;
            if (chAll.Checked)
            {
                // ftc.ConvertAll();
                ftc2.ConvertAll();
            }
            else
            {
                //ftc.ConvertString(sym);
                ftc2.ConvertString(sym);
            }

            lSize.Text = $"{ftc2.FontSize}";
            lSize.ForeColor = (ftc2.FontSize > 0xFFFF) ? Color.Red : Color.Black;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            dlSave.Filter = "Заголовочные файлы C (*.h)|*.h|Любые файлы (*.*)|*.*";
            dlSave.Title = "Сохранить код";
            if (dlSave.FileName.CompareTo("") == 0)
                dlSave.FileName = GetFontName() + ".h";
            else
                dlSave.FileName = Path.GetFileName(dlSave.FileName);
            if (dlSave.ShowDialog() == DialogResult.Cancel) return;

            //  FontToC ftc = new FontToC();
            //  ftc.SelectedFont = dlFont.Font;
            //  if (ckImages.Checked) ftc.TempFolder = "img//";
            if (ckImages.Checked)
            {
                ftc2.TempFolder = "img//";

                ftc2._Test();
            }
            //ftc.SaveFile(dlSave.FileName, string.Format("f{0:d}", GetFontName()));

            ftc2.SaveFile(dlSave.FileName, string.Format("f{0:d}", GetFontName()));

            Text = "Font Converter - готово.";
        }

        private void fMain_Load(object sender, EventArgs e)
        {

        }

        private void chNumbers_CheckedChanged(object sender, EventArgs e)
        {
            ResetGeneratedFont();
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            UpdateFont();

            bSave.Enabled = (ftc2 != null) && (ftc2.FontSize < 0x10000);
        }
    }
}
