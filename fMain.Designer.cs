namespace FontConv
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gChars = new System.Windows.Forms.GroupBox();
            this.chRusLittle = new System.Windows.Forms.CheckBox();
            this.chRusBig = new System.Windows.Forms.CheckBox();
            this.chLatinLittle = new System.Windows.Forms.CheckBox();
            this.chLatinBig = new System.Windows.Forms.CheckBox();
            this.chSpecial = new System.Windows.Forms.CheckBox();
            this.chNumbers = new System.Windows.Forms.CheckBox();
            this.chAll = new System.Windows.Forms.CheckBox();
            this.dlFont = new System.Windows.Forms.FontDialog();
            this.dlSave = new System.Windows.Forms.SaveFileDialog();
            this.bFont = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.lSample = new System.Windows.Forms.Label();
            this.tName = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.ck2Bit = new System.Windows.Forms.CheckBox();
            this.ckImages = new System.Windows.Forms.CheckBox();
            this.lSize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bUpdate = new System.Windows.Forms.Button();
            this.ckDrop = new System.Windows.Forms.CheckBox();
            this.gChars.SuspendLayout();
            this.SuspendLayout();
            // 
            // gChars
            // 
            this.gChars.Controls.Add(this.chRusLittle);
            this.gChars.Controls.Add(this.chRusBig);
            this.gChars.Controls.Add(this.chLatinLittle);
            this.gChars.Controls.Add(this.chLatinBig);
            this.gChars.Controls.Add(this.chSpecial);
            this.gChars.Controls.Add(this.chNumbers);
            this.gChars.Location = new System.Drawing.Point(12, 12);
            this.gChars.Name = "gChars";
            this.gChars.Size = new System.Drawing.Size(200, 159);
            this.gChars.TabIndex = 0;
            this.gChars.TabStop = false;
            this.gChars.Text = "Набор символов";
            // 
            // chRusLittle
            // 
            this.chRusLittle.AutoSize = true;
            this.chRusLittle.Location = new System.Drawing.Point(6, 134);
            this.chRusLittle.Name = "chRusLittle";
            this.chRusLittle.Size = new System.Drawing.Size(127, 17);
            this.chRusLittle.TabIndex = 5;
            this.chRusLittle.Text = "Русские прописные";
            this.chRusLittle.UseVisualStyleBackColor = true;
            this.chRusLittle.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // chRusBig
            // 
            this.chRusBig.AutoSize = true;
            this.chRusBig.Location = new System.Drawing.Point(6, 111);
            this.chRusBig.Name = "chRusBig";
            this.chRusBig.Size = new System.Drawing.Size(126, 17);
            this.chRusBig.TabIndex = 4;
            this.chRusBig.Text = "Русские заглавные";
            this.chRusBig.UseVisualStyleBackColor = true;
            this.chRusBig.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // chLatinLittle
            // 
            this.chLatinLittle.AutoSize = true;
            this.chLatinLittle.Location = new System.Drawing.Point(6, 88);
            this.chLatinLittle.Name = "chLatinLittle";
            this.chLatinLittle.Size = new System.Drawing.Size(140, 17);
            this.chLatinLittle.TabIndex = 3;
            this.chLatinLittle.Text = "Латинские прописные";
            this.chLatinLittle.UseVisualStyleBackColor = true;
            this.chLatinLittle.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // chLatinBig
            // 
            this.chLatinBig.AutoSize = true;
            this.chLatinBig.Location = new System.Drawing.Point(6, 65);
            this.chLatinBig.Name = "chLatinBig";
            this.chLatinBig.Size = new System.Drawing.Size(139, 17);
            this.chLatinBig.TabIndex = 2;
            this.chLatinBig.Text = "Латинские заглавные";
            this.chLatinBig.UseVisualStyleBackColor = true;
            this.chLatinBig.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // chSpecial
            // 
            this.chSpecial.AutoSize = true;
            this.chSpecial.Checked = true;
            this.chSpecial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chSpecial.Location = new System.Drawing.Point(6, 42);
            this.chSpecial.Name = "chSpecial";
            this.chSpecial.Size = new System.Drawing.Size(120, 17);
            this.chSpecial.TabIndex = 1;
            this.chSpecial.Text = "Знаки препинания";
            this.chSpecial.UseVisualStyleBackColor = true;
            this.chSpecial.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // chNumbers
            // 
            this.chNumbers.AutoSize = true;
            this.chNumbers.Checked = true;
            this.chNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chNumbers.Location = new System.Drawing.Point(6, 19);
            this.chNumbers.Name = "chNumbers";
            this.chNumbers.Size = new System.Drawing.Size(62, 17);
            this.chNumbers.TabIndex = 0;
            this.chNumbers.Text = "Цифры";
            this.chNumbers.UseVisualStyleBackColor = true;
            this.chNumbers.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // chAll
            // 
            this.chAll.AutoSize = true;
            this.chAll.Location = new System.Drawing.Point(18, 176);
            this.chAll.Name = "chAll";
            this.chAll.Size = new System.Drawing.Size(45, 17);
            this.chAll.TabIndex = 6;
            this.chAll.Text = "Все";
            this.chAll.UseVisualStyleBackColor = true;
            this.chAll.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // dlSave
            // 
            this.dlSave.DefaultExt = "*.h";
            this.dlSave.Filter = "Заголовочные файлы C (*.h)|*.h|Любые файлы (*.*)|*.*";
            this.dlSave.RestoreDirectory = true;
            this.dlSave.Title = "Сохранить код";
            // 
            // bFont
            // 
            this.bFont.Location = new System.Drawing.Point(235, 12);
            this.bFont.Name = "bFont";
            this.bFont.Size = new System.Drawing.Size(113, 23);
            this.bFont.TabIndex = 1;
            this.bFont.Text = "Шрифт...";
            this.bFont.UseVisualStyleBackColor = true;
            this.bFont.Click += new System.EventHandler(this.bFont_Click);
            // 
            // bSave
            // 
            this.bSave.Enabled = false;
            this.bSave.Location = new System.Drawing.Point(235, 71);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(113, 23);
            this.bSave.TabIndex = 2;
            this.bSave.Text = "Сохранить...";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // lSample
            // 
            this.lSample.Location = new System.Drawing.Point(12, 197);
            this.lSample.Name = "lSample";
            this.lSample.Size = new System.Drawing.Size(336, 118);
            this.lSample.TabIndex = 3;
            this.lSample.Text = "Sample";
            this.lSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tName
            // 
            this.tName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tName.Location = new System.Drawing.Point(252, 173);
            this.tName.Name = "tName";
            this.tName.Size = new System.Drawing.Size(100, 20);
            this.tName.TabIndex = 7;
            // 
            // lName
            // 
            this.lName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(186, 176);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(60, 13);
            this.lName.TabIndex = 8;
            this.lName.Text = "Название:";
            // 
            // ck2Bit
            // 
            this.ck2Bit.AutoSize = true;
            this.ck2Bit.Location = new System.Drawing.Point(241, 100);
            this.ck2Bit.Name = "ck2Bit";
            this.ck2Bit.Size = new System.Drawing.Size(81, 17);
            this.ck2Bit.TabIndex = 9;
            this.ck2Bit.Text = "2 бита/пкс";
            this.ck2Bit.UseVisualStyleBackColor = true;
            this.ck2Bit.CheckedChanged += new System.EventHandler(this.chNumbers_CheckedChanged);
            // 
            // ckImages
            // 
            this.ckImages.AutoSize = true;
            this.ckImages.Location = new System.Drawing.Point(241, 138);
            this.ckImages.Name = "ckImages";
            this.ckImages.Size = new System.Drawing.Size(74, 17);
            this.ckImages.TabIndex = 10;
            this.ckImages.Text = "Картинки";
            this.ckImages.UseVisualStyleBackColor = true;
            // 
            // lSize
            // 
            this.lSize.AutoSize = true;
            this.lSize.Location = new System.Drawing.Point(271, 158);
            this.lSize.Name = "lSize";
            this.lSize.Size = new System.Drawing.Size(10, 13);
            this.lSize.TabIndex = 11;
            this.lSize.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Размер:";
            // 
            // bUpdate
            // 
            this.bUpdate.Location = new System.Drawing.Point(235, 42);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(113, 23);
            this.bUpdate.TabIndex = 13;
            this.bUpdate.Text = "Подготовить";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // ckDrop
            // 
            this.ckDrop.AutoSize = true;
            this.ckDrop.Checked = true;
            this.ckDrop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckDrop.Location = new System.Drawing.Point(241, 119);
            this.ckDrop.Name = "ckDrop";
            this.ckDrop.Size = new System.Drawing.Size(125, 17);
            this.ckDrop.TabIndex = 14;
            this.ckDrop.Text = "Убрать не влезшее";
            this.ckDrop.UseVisualStyleBackColor = true;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 324);
            this.Controls.Add(this.ckDrop);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lSize);
            this.Controls.Add(this.ckImages);
            this.Controls.Add(this.ck2Bit);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.tName);
            this.Controls.Add(this.chAll);
            this.Controls.Add(this.lSample);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bFont);
            this.Controls.Add(this.gChars);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "fMain";
            this.Text = "Font Converter";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.gChars.ResumeLayout(false);
            this.gChars.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gChars;
        private System.Windows.Forms.CheckBox chAll;
        private System.Windows.Forms.CheckBox chRusLittle;
        private System.Windows.Forms.CheckBox chRusBig;
        private System.Windows.Forms.CheckBox chLatinLittle;
        private System.Windows.Forms.CheckBox chLatinBig;
        private System.Windows.Forms.CheckBox chSpecial;
        private System.Windows.Forms.CheckBox chNumbers;
        private System.Windows.Forms.FontDialog dlFont;
        private System.Windows.Forms.SaveFileDialog dlSave;
        private System.Windows.Forms.Button bFont;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Label lSample;
        private System.Windows.Forms.TextBox tName;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.CheckBox ck2Bit;
        private System.Windows.Forms.CheckBox ckImages;
        private System.Windows.Forms.Label lSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.CheckBox ckDrop;
    }
}

