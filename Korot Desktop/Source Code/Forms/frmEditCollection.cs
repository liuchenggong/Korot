//MIT License
//
//Copyright (c) 2020 Eren "Haltroy" Kanat
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmEditCollection : Form
    {
        private readonly frmCEF cefform;
        private readonly Control editItem;
        private readonly frmCollection colman;
        public frmEditCollection(frmCEF _cefform, frmCollection _colman, Control item)
        {
            cefform = _cefform;
            colman = _colman;
            editItem = item;
            InitializeComponent();
            if (editItem is PictureBox)
            {
                groupBox2.Enabled = false;
                tbID.Text = editItem.Name;
                pbBack.BackColor = editItem.BackColor;
                tbSource.Text = ((PictureBox)editItem).ImageLocation;
                nudW.Value = editItem.Width;
                nudH.Value = editItem.Height;
            }
            else if (editItem is CustomLinkLabel)
            {
                groupBox4.Enabled = false;
                tbID.Text = editItem.Name;
                pbBack.BackColor = editItem.BackColor;
                tbText.Text = editItem.Text;
                tbFont.Text = editItem.Font.FontFamily.Name;
                nudSize.Value = Convert.ToDecimal(editItem.Font.Size);
                rbRegular.Checked = (editItem.Font.Style == FontStyle.Regular);
                rbBold.Checked = (editItem.Font.Style == FontStyle.Bold);
                rbItalic.Checked = (editItem.Font.Style == FontStyle.Italic);
                rbUnderline.Checked = (editItem.Font.Style == FontStyle.Underline);
                rbStrikeout.Checked = (editItem.Font.Style == FontStyle.Strikeout);
                pbFore.BackColor = editItem.ForeColor;
                tbSource.Text = ((CustomLinkLabel)editItem).Url;
            }
            else if (editItem is Label)
            {
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                tbID.Text = editItem.Name;
                pbBack.BackColor = editItem.BackColor;
                tbText.Text = editItem.Text;
                tbFont.Text = editItem.Font.FontFamily.Name;
                nudSize.Value = Convert.ToDecimal(editItem.Font.Size);
                rbRegular.Checked = (editItem.Font.Style == FontStyle.Regular);
                rbBold.Checked = (editItem.Font.Style == FontStyle.Bold);
                rbItalic.Checked = (editItem.Font.Style == FontStyle.Italic);
                rbUnderline.Checked = (editItem.Font.Style == FontStyle.Underline);
                rbStrikeout.Checked = (editItem.Font.Style == FontStyle.Strikeout);
                pbFore.BackColor = editItem.ForeColor;
            }
            RefreshTranslations();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            isOK = true;
            Close();
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog() { Color = pbBack.BackColor, AllowFullOpen = true, AnyColor = true, FullOpen = true, };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pbBack.BackColor = dialog.Color;
            }
        }

        private void pbFore_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog() { Color = pbFore.BackColor, AllowFullOpen = true, AnyColor = true, FullOpen = true, };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pbFore.BackColor = dialog.Color;
            }
        }

        private void rbRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRegular.Checked)
            {
                rbBold.Checked = false;
                rbItalic.Checked = false;
                rbUnderline.Checked = false;
                rbStrikeout.Checked = false;
            }
        }

        private void rbBold_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBold.Checked)
            {
                rbRegular.Checked = false;
                rbItalic.Checked = false;
                rbUnderline.Checked = false;
                rbStrikeout.Checked = false;
            }
        }

        private void rbItalic_CheckedChanged(object sender, EventArgs e)
        {
            if (rbItalic.Checked)
            {
                rbBold.Checked = false;
                rbRegular.Checked = false;
                rbUnderline.Checked = false;
                rbStrikeout.Checked = false;
            }
        }

        private void rbUnderline_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUnderline.Checked)
            {
                rbBold.Checked = false;
                rbItalic.Checked = false;
                rbRegular.Checked = false;
                rbStrikeout.Checked = false;
            }
        }

        private void rbStrikeout_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStrikeout.Checked)
            {
                rbBold.Checked = false;
                rbItalic.Checked = false;
                rbUnderline.Checked = false;
                rbRegular.Checked = false;
            }
        }

        private bool isOK = false;
        private void frmEditCollection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isOK)
            {
                DialogResult = DialogResult.OK;
                CollectionItem mainitem = cefform.colManager.GetItemFromID(editItem.Name);
                if (editItem is CustomLinkLabel)
                {
                    LinkItem ti = mainitem as LinkItem;
                    ti.ID = tbID.Text;
                    ti.Text = tbText.Text;
                    ti.Source = tbSource.Text;
                    ti.BackColor = pbBack.BackColor;
                    ti.ForeColor = pbFore.BackColor;
                    ti.Font = tbFont.Text;
                    ti.FontSize = Convert.ToInt32(nudSize.Value);
                    if (rbRegular.Checked)
                    {
                        ti.FontProperties = FontType.Regular;
                    }
                    else if (rbBold.Checked)
                    {
                        ti.FontProperties = FontType.Bold;
                    }
                    else if (rbItalic.Checked)
                    {
                        ti.FontProperties = FontType.Italic;
                    }
                    else if (rbUnderline.Checked)
                    {
                        ti.FontProperties = FontType.Underline;
                    }
                    else if (rbStrikeout.Checked)
                    {
                        ti.FontProperties = FontType.Strikeout;
                    }
                }
                else if (editItem is Label)
                {
                    TextItem ti = mainitem as TextItem;
                    ti.ID = tbID.Text;
                    ti.Text = tbText.Text;
                    ti.BackColor = pbBack.BackColor;
                    ti.ForeColor = pbFore.BackColor;
                    ti.Font = tbFont.Text;
                    ti.FontSize = Convert.ToInt32(nudSize.Value);
                    if (rbRegular.Checked)
                    {
                        ti.FontProperties = FontType.Regular;
                    }
                    else if (rbBold.Checked)
                    {
                        ti.FontProperties = FontType.Bold;
                    }
                    else if (rbItalic.Checked)
                    {
                        ti.FontProperties = FontType.Italic;
                    }
                    else if (rbUnderline.Checked)
                    {
                        ti.FontProperties = FontType.Underline;
                    }
                    else if (rbStrikeout.Checked)
                    {
                        ti.FontProperties = FontType.Strikeout;
                    }
                }
                else if (editItem is PictureBox)
                {
                    ImageItem ti = mainitem as ImageItem;
                    ti.ID = tbID.Text;
                    ti.Width = Convert.ToInt32(nudW.Value);
                    ti.Height = Convert.ToInt32(nudH.Value);
                    ti.Source = tbSource.Text;
                    ti.BackColor = pbBack.BackColor;
                }
                colman.Invoke(new Action(() => colman.genColList()));
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog() { Font = editItem.Font, };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbFont.Text = dialog.Font.FontFamily.Name;
                nudSize.Value = Convert.ToDecimal(dialog.Font.Size);
                rbRegular.Checked = (dialog.Font.Style == FontStyle.Regular);
                rbBold.Checked = (dialog.Font.Style == FontStyle.Bold);
                rbItalic.Checked = (dialog.Font.Style == FontStyle.Italic);
                rbUnderline.Checked = (dialog.Font.Style == FontStyle.Underline);
                rbStrikeout.Checked = (dialog.Font.Style == FontStyle.Strikeout);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            groupBox1.BackColor = cefform.Settings.Theme.BackColor;
            groupBox1.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            groupBox2.BackColor = cefform.Settings.Theme.BackColor;
            groupBox2.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            groupBox3.BackColor = cefform.Settings.Theme.BackColor;
            groupBox3.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            groupBox4.BackColor = cefform.Settings.Theme.BackColor;
            groupBox4.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            flpProp.BackColor = cefform.Settings.Theme.BackColor;
            flpProp.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            btDone.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            btDone.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            bt3DOT.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            bt3DOT.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            tbID.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            tbID.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            tbText.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            tbText.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            tbSource.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            tbSource.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            tbFont.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            tbFont.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            nudSize.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            nudSize.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            nudW.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            nudW.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            nudH.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            nudH.ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;

        }

        private void RefreshTranslations()
        {
            groupBox1.Text = cefform.catCommon;
            groupBox2.Text = cefform.catText;
            groupBox3.Text = cefform.catOnline;
            groupBox4.Text = cefform.catPicture;
            Text = cefform.TitleEditItem;
            btDone.ButtonText = cefform.TitleDone;
            lbID.Text = cefform.TitleID;
            tbID.Location = new Point(lbID.Location.X + lbID.Width, tbID.Location.Y);
            tbID.Width = groupBox1.Width - (lbID.Location.X + lbID.Width + 5);
            lbText.Text = cefform.TitleText;
            tbText.Location = new Point(lbText.Location.X + lbText.Width, tbText.Location.Y);
            tbText.Width = groupBox2.Width - (lbText.Location.X + lbText.Width + 5);
            lbFont.Text = cefform.TitleFont;
            tbFont.Location = new Point(lbFont.Location.X + lbFont.Width, tbFont.Location.Y);
            tbFont.Width = groupBox2.Width - (lbFont.Location.X + lbFont.Width + 5 + bt3DOT.Width);
            lbSource.Text = cefform.TitleSource;
            tbSource.Location = new Point(lbSource.Location.X + lbSource.Width, tbSource.Location.Y);
            tbSource.Width = groupBox3.Width - (lbSource.Location.X + lbSource.Width + 5);
            lbW.Text = cefform.TitleWidth;
            nudW.Location = new Point(lbW.Location.X + lbW.Width, nudW.Location.Y);
            nudW.Width = groupBox4.Width - (lbW.Location.X + lbW.Width + 5);
            lbH.Text = cefform.TitleHeight;
            nudH.Location = new Point(lbH.Location.X + lbH.Width, nudH.Location.Y);
            nudH.Width = groupBox4.Width - (lbH.Location.X + lbH.Width + 5);
            lbSize.Text = cefform.TitleSize;
            nudSize.Location = new Point(lbSize.Location.X + lbSize.Width, nudSize.Location.Y);
            nudSize.Width = groupBox3.Width - (lbSize.Location.X + lbSize.Width + 5);
            lbProp.Text = cefform.TitleProp;
            flpProp.Location = new Point(lbProp.Location.X + lbProp.Width, flpProp.Location.Y);
            flpProp.Width = groupBox3.Width - (lbProp.Location.X + lbProp.Width + 5);
            lbBackColor.Text = cefform.TitleBackColor;
            pbBack.Location = new Point(lbBackColor.Location.X + lbBackColor.Width, pbBack.Location.Y);
            lbForeColor.Text = cefform.TitleForeColor;
            pbFore.Location = new Point(lbForeColor.Location.X + lbForeColor.Width, pbFore.Location.Y);
            rbRegular.Text = cefform.TitleRegular;
            rbBold.Text = cefform.TitleBold;
            rbItalic.Text = cefform.TitleItalic;
            rbUnderline.Text = cefform.TitleUnderline;
            rbStrikeout.Text = cefform.TitleStrikeout;
        }
    }
}
