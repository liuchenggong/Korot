/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
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
                CollectionItem mainitem = cefform.Settings.CollectionManager.GetItemFromID(editItem.Name);
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
            Color BackColor2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            groupBox1.BackColor = cefform.Settings.Theme.BackColor;
            groupBox1.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            groupBox2.BackColor = cefform.Settings.Theme.BackColor;
            groupBox2.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            groupBox3.BackColor = cefform.Settings.Theme.BackColor;
            groupBox3.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            groupBox4.BackColor = cefform.Settings.Theme.BackColor;
            groupBox4.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            flpProp.BackColor = cefform.Settings.Theme.BackColor;
            flpProp.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            btDone.BackColor = BackColor2;
            btDone.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            bt3DOT.BackColor = BackColor2;
            bt3DOT.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            tbID.BackColor = BackColor2;
            tbID.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            tbText.BackColor = BackColor2;
            tbText.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            tbSource.BackColor = BackColor2;
            tbSource.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            tbFont.BackColor = BackColor2;
            tbFont.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            nudSize.BackColor = BackColor2;
            nudSize.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            nudW.BackColor = BackColor2;
            nudW.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            nudH.BackColor = BackColor2;
            nudH.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
        }

        private void RefreshTranslations()
        {
            groupBox1.Text = cefform.anaform.catCommon;
            groupBox2.Text = cefform.anaform.catText;
            groupBox3.Text = cefform.anaform.catOnline;
            groupBox4.Text = cefform.anaform.catPicture;
            Text = cefform.anaform.TitleEditItem;
            btDone.Text = cefform.anaform.TitleDone;
            lbID.Text = cefform.anaform.TitleID;
            tbID.Location = new Point(lbID.Location.X + lbID.Width, tbID.Location.Y);
            tbID.Width = groupBox1.Width - (lbID.Location.X + lbID.Width + 5);
            lbText.Text = cefform.anaform.TitleText;
            tbText.Location = new Point(lbText.Location.X + lbText.Width, tbText.Location.Y);
            tbText.Width = groupBox2.Width - (lbText.Location.X + lbText.Width + 5);
            lbFont.Text = cefform.anaform.TitleFont;
            tbFont.Location = new Point(lbFont.Location.X + lbFont.Width, tbFont.Location.Y);
            tbFont.Width = groupBox2.Width - (lbFont.Location.X + lbFont.Width + 5 + bt3DOT.Width);
            lbSource.Text = cefform.anaform.TitleSource;
            tbSource.Location = new Point(lbSource.Location.X + lbSource.Width, tbSource.Location.Y);
            tbSource.Width = groupBox3.Width - (lbSource.Location.X + lbSource.Width + 5);
            lbW.Text = cefform.anaform.TitleWidth;
            nudW.Location = new Point(lbW.Location.X + lbW.Width, nudW.Location.Y);
            nudW.Width = groupBox4.Width - (lbW.Location.X + lbW.Width + 5);
            lbH.Text = cefform.anaform.TitleHeight;
            nudH.Location = new Point(lbH.Location.X + lbH.Width, nudH.Location.Y);
            nudH.Width = groupBox4.Width - (lbH.Location.X + lbH.Width + 5);
            lbSize.Text = cefform.anaform.TitleSize;
            nudSize.Location = new Point(lbSize.Location.X + lbSize.Width, nudSize.Location.Y);
            nudSize.Width = groupBox3.Width - (lbSize.Location.X + lbSize.Width + 5);
            lbProp.Text = cefform.anaform.TitleProp;
            flpProp.Location = new Point(lbProp.Location.X + lbProp.Width, flpProp.Location.Y);
            flpProp.Width = groupBox3.Width - (lbProp.Location.X + lbProp.Width + 5);
            lbBackColor.Text = cefform.anaform.TitleBackColor;
            pbBack.Location = new Point(lbBackColor.Location.X + lbBackColor.Width, pbBack.Location.Y);
            lbForeColor.Text = cefform.anaform.TitleForeColor;
            pbFore.Location = new Point(lbForeColor.Location.X + lbForeColor.Width, pbFore.Location.Y);
            rbRegular.Text = cefform.anaform.TitleRegular;
            rbBold.Text = cefform.anaform.TitleBold;
            rbItalic.Text = cefform.anaform.TitleItalic;
            rbUnderline.Text = cefform.anaform.TitleUnderline;
            rbStrikeout.Text = cefform.anaform.TitleStrikeout;
        }
    }
}