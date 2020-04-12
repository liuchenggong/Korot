using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmEditCollection : Form
    {
        frmCEF cefform;
        Control editItem;
        frmCollection colman;
        public frmEditCollection(frmCEF _cefform,frmCollection _colman,Control item)
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
            }else if (editItem is CustomLinkLabel)
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
        public string outXML()
        {
            string item = "[";
            string closetxt = "/]";
            if (editItem is PictureBox) 
            { 
                item += "picture ";
                item += "ID=\"" + tbID.Text.Trim() + "\" ";
                item += "BackColor=\"" + Tools.ColorToHex(pbBack.BackColor) + "\" ";
                item += "Source=\"" + tbSource.Text + "\" ";
                item += "Width=\"" + nudW.Value + "\" ";
                item += "Height=\"" + nudH.Value + "\" ";
            }
            else if(editItem is CustomLinkLabel) 
            {
                item += "link ";
                item += "ID=\"" + tbID.Text.Trim() + "\" ";
                item += "BackColor=\"" + Tools.ColorToHex(pbBack.BackColor) + "\" ";
                item += "Text=\"" + tbText.Text + "\" ";
                item += "Font=\"" + tbFont.Text + "\" ";
                item += "FontSize=\"" + nudSize.Value + "\" ";
                item += "FontProperties=\"";
                if (rbRegular.Checked)
                {
                    item += "Regular\" ";
                }else if (rbBold.Checked)
                {
                    item += "Bold\" ";
                }
                else if (rbItalic.Checked)
                {
                    item += "Italic\" ";
                }
                else if (rbUnderline.Checked)
                {
                    item += "Underline\" ";
                }
                else if (rbStrikeout.Checked)
                {
                    item += "Strikeout\" ";
                }
                item += "Source=\"" + tbSource.Text + "\" ";
            }
            else if (editItem is Label)
            {
                item += "label ";
                item += "ID=\"" + tbID.Text.Trim() + "\" ";
                Console.WriteLine(Tools.ColorToHex(pbBack.BackColor));
                item += "BackColor=\"" + Tools.ColorToHex(pbBack.BackColor) + "\" ";
                item += "ForeColor=\"" + Tools.ColorToHex(pbFore.BackColor) + "\" ";
                item += "Text=\"" + tbText.Text + "\" ";
                item += "Font=\"" + tbFont.Text + "\" ";
                item += "FontSize=\"" + nudSize.Value + "\" ";
                item += "FontProperties=\"";
                if (rbRegular.Checked)
                {
                    item += "Regular\" ";
                }
                else if (rbBold.Checked)
                {
                    item += "Bold\" ";
                }
                else if (rbItalic.Checked)
                {
                    item += "Italic\" ";
                }
                else if (rbUnderline.Checked)
                {
                    item += "Underline\" ";
                }
                else if (rbStrikeout.Checked)
                {
                    item += "Strikeout\" ";
                }
            }
            return item + closetxt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isOK = true;
            this.Close();
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog() {Color = pbBack.BackColor, AllowFullOpen = true, AnyColor = true, FullOpen = true, };
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
        bool isOK = false;
        private void frmEditCollection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isOK)
            {
                DialogResult = DialogResult.OK;
                cefform.colManager.Collections = cefform.colManager.Collections.Replace(editItem.Tag.ToString(), outXML());
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
            this.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            groupBox1.BackColor = Properties.Settings.Default.BackColor;
            groupBox1.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            groupBox2.BackColor = Properties.Settings.Default.BackColor;
            groupBox2.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            groupBox3.BackColor = Properties.Settings.Default.BackColor;
            groupBox3.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            groupBox4.BackColor = Properties.Settings.Default.BackColor;
            groupBox4.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            flpProp.BackColor = Properties.Settings.Default.BackColor;
            flpProp.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            btDone.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor,20,false);
            btDone.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            bt3DOT.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            bt3DOT.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            tbID.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            tbID.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            tbText.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            tbText.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            tbSource.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            tbSource.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            tbFont.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            tbFont.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            nudSize.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            nudSize.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            nudW.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            nudW.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            nudH.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            nudH.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;

        }

        private void RefreshTranslations()
        {
            groupBox1.Text = cefform.catCommon;
            groupBox2.Text = cefform.catText;
            groupBox3.Text = cefform.catOnline;
            groupBox4.Text = cefform.catPicture;
            Text = cefform.TitleEditItem;
            btDone.Text = cefform.TitleDone;
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
