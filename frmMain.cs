
using Korot.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Korot
{
  public class frmMain : Form
  {
    private bool isMouseDown = false;
    private string[] _args = (string[]) null;
    private string maname = " - Korot";
    private bool _isIncognito = false;
    private IContainer components = (IContainer) null;
    private Point mouseposition;
    private System.Windows.Forms.Timer timer1;
    public TabControl tabControl1;
    public TabPage tabPage1;
    public TabPage tabPage2;
    private TabControl tabControl2;
    private TabPage tpMain;
    private Label label2;
    private Label label7;
    private Label label6;
    private TabPage tpSettings;
    private TabPage tpAbout;
    private Label label8;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem removeSelectedToolStripMenuItem;
    private ToolStripMenuItem removeAllButSelectedToolStripMenuItem;
    private ToolStripMenuItem clearToolStripMenuItem;
    private Label label5;
    private CheckBox checkBox2;
    private CheckBox checkBox1;
    private TextBox textBox1;
    private Label label11;
    private ImageList ımageList1;
    private Label label3;
    private TextBox textBox2;
    private Label label15;
    private Label label14;
    private Label label13;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private ListBox listBox1;
    private Label label1;
    private Label label4;
    private PictureBox pictureBox3;

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style = 101646336;
        if (this.DesignMode)
          createParams.Style |= 1073741824;
        int num = 8;
        if (Environment.OSVersion.Version.Major * 10 + Environment.OSVersion.Version.Minor >= 51)
          num |= 131072;
        createParams.ClassStyle = num;
        return createParams;
      }
    }

    public frmMain(bool isIncognito, string[] args)
    {
      this.InitializeComponent();
      this._isIncognito = isIncognito;
      if (isIncognito)
        this.maname = " - Korot (Incognito)";
      this._args = args;
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      this.tabControl1.SelectedIndex = 1;
      foreach (string url in this._args)
      {
        if (!(url == Application.ExecutablePath))
          this.NewTab(url);
      }
      this.Location = new Point(Settings.Default.WindowPosX, Settings.Default.WindowPosY);
      this.Size = new Size(Settings.Default.WindowSizeW, Settings.Default.WindowSizeH);
    }

    public void TabText(int TabID, string TabText)
    {
      this.tabControl1.Invoke((Action) (() => this.tabControl1.TabPages[TabID].Text = TabText.ToString() + "         "));
    }

    public void NewTab(string url)
    {
      TabPage tab = new TabPage();
      tabform tabform = new tabform(this, this._isIncognito, url);
      tabform.TopMost = false;
      tabform.TopLevel = false;
      tabform.Visible = true;
      tab.Text = "New Tab";
      tabform.Dock = DockStyle.Fill;
      this.tabControl1.Invoke((Action) (() => this.tabControl1.TabPages.Insert(this.tabControl1.TabPages.Count - 1, tab)));
      tab.Controls.Add((Control) tabform);
      this.tabControl1.Invoke((Action) (() => this.tabControl1.SelectedTab = tab));
      tabform.tabID = this.tabControl1.TabPages.Count - 2;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (this.tabControl1.SelectedTab == this.tabPage2)
        this.NewTab(Settings.Default.homepage);
      else if (this.tabControl1.SelectedTab == this.tabPage1)
        this.Text = this.maname.Replace(" - ", (string) null);
      else
        this.Text = this.tabControl1.SelectedTab.Text.Replace("         ", (string) null) + this.maname;
      if (this.tabControl1.TabPages.Count != 2)
        return;
      this.Close();
    }

    private void tabControl1_MouseDown(object sender, MouseEventArgs e)
    {
      for (int index = 0; index < this.tabControl1.TabPages.Count; ++index)
      {
        Rectangle tabRect = this.tabControl1.GetTabRect(index);
        if (new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 9, 7).Contains(e.Location) && index != 0 && index != this.tabControl1.TabPages.Count - 1)
        {
          this.tabControl1.TabPages.RemoveAt(index);
          break;
        }
        if (e.Button == MouseButtons.Left)
        {
          this.mouseposition = new Point(-e.X, -e.Y);
          this.isMouseDown = true;
        }
      }
    }

    private void label4_Click(object sender, EventArgs e)
    {
      this.tabControl2.SelectedTab = this.tpMain;
    }

    private void label1_Click(object sender, EventArgs e)
    {
      new frmMain(false, this._args).Show();
    }

    private void label7_Click(object sender, EventArgs e)
    {
      new frmMain(true, this._args).Show();
    }

    private void label8_Click(object sender, EventArgs e)
    {
      TabPage tabPage = new TabPage();
      tabform tabform = new tabform(this, true, Settings.Default.homepage);
      tabform.TopMost = false;
      tabform.TopLevel = false;
      tabform.Visible = true;
      tabPage.Text = "New Tab";
      tabform.Dock = DockStyle.Fill;
      this.tabControl1.TabPages.Insert(this.tabControl1.TabPages.Count - 1, tabPage);
      tabPage.Controls.Add((Control) tabform);
      this.tabControl1.SelectedTab = tabPage;
      tabform.tabID = this.tabControl1.TabPages.Count - 2;
    }

    private void label1_Click_1(object sender, EventArgs e)
    {
    }

    private void label10_Click(object sender, EventArgs e)
    {
      this.tabControl2.SelectedTab = this.tpMain;
    }

    private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      this.NewTab(this.listBox1.SelectedItem.ToString());
    }

    private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Settings.Default.History.Remove(this.listBox1.SelectedItem.ToString());
      this.tpSettings_Paint((object) null, (PaintEventArgs) null);
      Settings.Default.Save();
    }

    private void removeAllButSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Settings.Default.History.Clear();
      Settings.Default.History.Add(this.listBox1.SelectedItem.ToString());
      this.tpSettings_Paint((object) null, (PaintEventArgs) null);
      Settings.Default.Save();
    }

    private void clearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Settings.Default.History.Clear();
      this.tpSettings_Paint((object) null, (PaintEventArgs) null);
      Settings.Default.Save();
    }

    private void label2_Click(object sender, EventArgs e)
    {
      this.tabControl2.SelectedTab = this.tpSettings;
    }

    private void label4_Click_1(object sender, EventArgs e)
    {
      this.tabControl2.SelectedTab = this.tpMain;
    }

    private void tpSettings_Paint(object sender, PaintEventArgs e)
    {
      this.listBox1.Items.Clear();
      foreach (object obj in Settings.Default.History)
        this.listBox1.Items.Add(obj);
      this.checkBox1.Checked = Settings.Default.CheckForUpdates;
      this.checkBox2.Checked = Settings.Default.Notify;
      this.checkBox2.Enabled = Settings.Default.CheckForUpdates;
      this.textBox1.Text = Settings.Default.homepage;
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      Settings.Default.homepage = this.textBox1.Text;
      Settings.Default.Save();
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      Settings.Default.CheckForUpdates = this.checkBox1.Checked;
      this.checkBox2.Enabled = this.checkBox1.Checked;
      Settings.Default.Save();
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      Settings.Default.Notify = this.checkBox2.Checked;
      Settings.Default.Save();
    }

    private void label14_Click(object sender, EventArgs e)
    {
      TabPage tabPage = new TabPage();
      tabform tabform = new tabform(this, this._isIncognito, "http://thehaltroy.rf.gd");
      tabform.TopMost = false;
      tabform.TopLevel = false;
      tabform.Visible = true;
      tabPage.Text = "New Tab";
      tabform.Dock = DockStyle.Fill;
      this.tabControl1.TabPages.Insert(this.tabControl1.TabPages.Count - 1, tabPage);
      tabPage.Controls.Add((Control) tabform);
      this.tabControl1.SelectedTab = tabPage;
      tabform.tabID = this.tabControl1.TabPages.Count - 2;
    }

    private void tpAbout_Paint(object sender, PaintEventArgs e)
    {
      this.label15.Text = Application.ProductVersion.ToString();
    }

    private void label3_Click(object sender, EventArgs e)
    {
      this.tabControl2.SelectedTab = this.tpAbout;
    }

    private void label12_Click(object sender, EventArgs e)
    {
      this.tabControl2.SelectedTab = this.tpMain;
    }

    private void tabControl1_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.isMouseDown)
        return;
      Point mousePosition = Control.MousePosition;
      mousePosition.Offset(this.mouseposition.X, this.mouseposition.Y);
      this.Location = mousePosition;
      this.WindowState = FormWindowState.Normal;
    }

    private void tabControl1_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.isMouseDown = false;
    }

    private void frmMain_DoubleClick(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Normal)
        this.WindowState = FormWindowState.Maximized;
      else
        this.WindowState = FormWindowState.Normal;
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.WindowPosX = this.Location.X;
      Settings.Default.WindowPosY = this.Location.Y;
      Settings.Default.WindowSizeH = this.Size.Height;
      Settings.Default.WindowSizeW = this.Size.Width;
      Settings.Default.Save();
    }

    private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index == 0)
      {
        Graphics graphics1 = e.Graphics;
        string text = this.tabControl1.TabPages[e.Index].Text;
        Font font = e.Font;
        Brush black = Brushes.Black;
        Rectangle bounds1 = e.Bounds;
        double num1 = (double) (bounds1.Left + 18);
        bounds1 = e.Bounds;
        double num2 = (double) (bounds1.Top + 4);
        graphics1.DrawString(text, font, black, (float) num1, (float) num2);
        Graphics graphics2 = e.Graphics;
        Image image = this.ımageList1.Images[0];
        Rectangle bounds2 = e.Bounds;
        int x = bounds2.Left + 6;
        bounds2 = e.Bounds;
        int y = bounds2.Top + 4;
        graphics2.DrawImage(image, x, y);
        e.DrawFocusRectangle();
      }
      else if (e.Index == this.tabControl1.TabPages.Count - 1)
      {
        Graphics graphics = e.Graphics;
        string text = this.tabControl1.TabPages[e.Index].Text;
        Font font = e.Font;
        Brush black = Brushes.Black;
        Rectangle bounds = e.Bounds;
        double num1 = (double) (bounds.Left + 12);
        bounds = e.Bounds;
        double num2 = (double) (bounds.Top + 4);
        graphics.DrawString(text, font, black, (float) num1, (float) num2);
        e.DrawFocusRectangle();
      }
      else
      {
        Graphics graphics1 = e.Graphics;
        Bitmap cancel512 = Resources.Cancel_512;
        Rectangle bounds = e.Bounds;
        int x = bounds.Right - 18;
        bounds = e.Bounds;
        int y = bounds.Top + 4;
        graphics1.DrawImage((Image) cancel512, x, y);
        Graphics graphics2 = e.Graphics;
        string text = this.tabControl1.TabPages[e.Index].Text;
        Font font = e.Font;
        Brush black = Brushes.Black;
        bounds = e.Bounds;
        double num1 = (double) (bounds.Left + 12);
        bounds = e.Bounds;
        double num2 = (double) (bounds.Top + 4);
        graphics2.DrawString(text, font, black, (float) num1, (float) num2);
        e.DrawFocusRectangle();
      }
    }

    private void listBox1_DoubleClick(object sender, EventArgs e)
    {
      this.NewTab(this.listBox1.SelectedItem.ToString());
    }

    private void tpSettings_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmMain));
      this.tabControl1 = new TabControl();
      this.tabPage1 = new TabPage();
      this.tabControl2 = new TabControl();
      this.tpMain = new TabPage();
      this.label2 = new Label();
      this.label8 = new Label();
      this.label7 = new Label();
      this.label6 = new Label();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.removeSelectedToolStripMenuItem = new ToolStripMenuItem();
      this.removeAllButSelectedToolStripMenuItem = new ToolStripMenuItem();
      this.clearToolStripMenuItem = new ToolStripMenuItem();
      this.tpSettings = new TabPage();
      this.checkBox2 = new CheckBox();
      this.checkBox1 = new CheckBox();
      this.textBox1 = new TextBox();
      this.label11 = new Label();
      this.label5 = new Label();
      this.tpAbout = new TabPage();
      this.tabPage2 = new TabPage();
      this.ımageList1 = new ImageList(this.components);
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.pictureBox1 = new PictureBox();
      this.label13 = new Label();
      this.label14 = new Label();
      this.label15 = new Label();
      this.textBox2 = new TextBox();
      this.label3 = new Label();
      this.listBox1 = new ListBox();
      this.label1 = new Label();
      this.pictureBox2 = new PictureBox();
      this.pictureBox3 = new PictureBox();
      this.label4 = new Label();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabControl2.SuspendLayout();
      this.tpMain.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.tpSettings.SuspendLayout();
      this.tpAbout.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox3).BeginInit();
      this.SuspendLayout();
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Dock = DockStyle.Fill;
      this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
      this.tabControl1.Location = new Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(624, 441);
      this.tabControl1.TabIndex = 0;
      this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabControl1_DrawItem);
      this.tabControl1.DoubleClick += new EventHandler(this.frmMain_DoubleClick);
      this.tabControl1.MouseDown += new MouseEventHandler(this.tabControl1_MouseDown);
      this.tabControl1.MouseMove += new MouseEventHandler(this.tabControl1_MouseMove);
      this.tabControl1.MouseUp += new MouseEventHandler(this.tabControl1_MouseUp);
      this.tabPage1.BackColor = Color.White;
      this.tabPage1.Controls.Add((Control) this.tabControl2);
      this.tabPage1.ForeColor = Color.Black;
      this.tabPage1.ImageIndex = 0;
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(616, 415);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Korot       ";
      this.tabControl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tabControl2.Controls.Add((Control) this.tpMain);
      this.tabControl2.Controls.Add((Control) this.tpSettings);
      this.tabControl2.Controls.Add((Control) this.tpAbout);
      this.tabControl2.Location = new Point(-5, -25);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new Size(632, 440);
      this.tabControl2.TabIndex = 0;
      this.tpMain.AutoScroll = true;
      this.tpMain.BorderStyle = BorderStyle.FixedSingle;
      this.tpMain.Controls.Add((Control) this.label3);
      this.tpMain.Controls.Add((Control) this.label2);
      this.tpMain.Controls.Add((Control) this.label8);
      this.tpMain.Controls.Add((Control) this.label7);
      this.tpMain.Controls.Add((Control) this.label6);
      this.tpMain.Location = new Point(4, 22);
      this.tpMain.Name = "tpMain";
      this.tpMain.Padding = new Padding(3);
      this.tpMain.Size = new Size(624, 414);
      this.tpMain.TabIndex = 0;
      this.tpMain.Text = "tabPage3";
      this.tpMain.UseVisualStyleBackColor = true;
      this.label2.BorderStyle = BorderStyle.FixedSingle;
      this.label2.Image = (Image) componentResourceManager.GetObject("label2.Image");
      this.label2.ImageAlign = ContentAlignment.TopCenter;
      this.label2.Location = new Point(390, 3);
      this.label2.Name = "label2";
      this.label2.Size = new Size(100, 118);
      this.label2.TabIndex = 0;
      this.label2.Text = "Settings";
      this.label2.TextAlign = ContentAlignment.BottomCenter;
      this.label2.Click += new EventHandler(this.label2_Click);
      this.label8.BorderStyle = BorderStyle.FixedSingle;
      this.label8.Image = (Image) componentResourceManager.GetObject("label8.Image");
      this.label8.ImageAlign = ContentAlignment.TopCenter;
      this.label8.Location = new Point(124, 2);
      this.label8.Name = "label8";
      this.label8.Size = new Size(120, 119);
      this.label8.TabIndex = 0;
      this.label8.Text = "New Incognito Tab";
      this.label8.TextAlign = ContentAlignment.BottomCenter;
      this.label8.Click += new EventHandler(this.label8_Click);
      this.label7.BorderStyle = BorderStyle.FixedSingle;
      this.label7.Image = (Image) componentResourceManager.GetObject("label7.Image");
      this.label7.ImageAlign = ContentAlignment.TopCenter;
      this.label7.Location = new Point(250, 3);
      this.label7.Name = "label7";
      this.label7.Size = new Size(134, 118);
      this.label7.TabIndex = 0;
      this.label7.Text = "New Incognito Window";
      this.label7.TextAlign = ContentAlignment.BottomCenter;
      this.label7.Click += new EventHandler(this.label7_Click);
      this.label6.BorderStyle = BorderStyle.FixedSingle;
      this.label6.Image = (Image) componentResourceManager.GetObject("label6.Image");
      this.label6.ImageAlign = ContentAlignment.TopCenter;
      this.label6.Location = new Point(6, 3);
      this.label6.Name = "label6";
      this.label6.Size = new Size(112, 118);
      this.label6.TabIndex = 0;
      this.label6.Text = "New Window";
      this.label6.TextAlign = ContentAlignment.BottomCenter;
      this.label6.Click += new EventHandler(this.label1_Click);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.removeSelectedToolStripMenuItem,
        (ToolStripItem) this.removeAllButSelectedToolStripMenuItem,
        (ToolStripItem) this.clearToolStripMenuItem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(201, 70);
      this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
      this.removeSelectedToolStripMenuItem.Size = new Size(200, 22);
      this.removeSelectedToolStripMenuItem.Text = "Remove Selected";
      this.removeSelectedToolStripMenuItem.Click += new EventHandler(this.removeSelectedToolStripMenuItem_Click);
      this.removeAllButSelectedToolStripMenuItem.Name = "removeAllButSelectedToolStripMenuItem";
      this.removeAllButSelectedToolStripMenuItem.Size = new Size(200, 22);
      this.removeAllButSelectedToolStripMenuItem.Text = "Remove all but Selected";
      this.removeAllButSelectedToolStripMenuItem.Click += new EventHandler(this.removeAllButSelectedToolStripMenuItem_Click);
      this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
      this.clearToolStripMenuItem.Size = new Size(200, 22);
      this.clearToolStripMenuItem.Text = "Clear";
      this.clearToolStripMenuItem.Click += new EventHandler(this.clearToolStripMenuItem_Click);
      this.tpSettings.Controls.Add((Control) this.pictureBox2);
      this.tpSettings.Controls.Add((Control) this.listBox1);
      this.tpSettings.Controls.Add((Control) this.label1);
      this.tpSettings.Controls.Add((Control) this.checkBox2);
      this.tpSettings.Controls.Add((Control) this.checkBox1);
      this.tpSettings.Controls.Add((Control) this.textBox1);
      this.tpSettings.Controls.Add((Control) this.label11);
      this.tpSettings.Controls.Add((Control) this.label5);
      this.tpSettings.Location = new Point(4, 22);
      this.tpSettings.Name = "tpSettings";
      this.tpSettings.Size = new Size(624, 414);
      this.tpSettings.TabIndex = 3;
      this.tpSettings.Text = "tabPage3";
      this.tpSettings.UseVisualStyleBackColor = true;
      this.tpSettings.Click += new EventHandler(this.tpSettings_Click);
      this.tpSettings.Paint += new PaintEventHandler(this.tpSettings_Paint);
      this.checkBox2.AutoSize = true;
      this.checkBox2.Location = new Point(28, 92);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new Size(172, 17);
      this.checkBox2.TabIndex = 6;
      this.checkBox2.Text = "Notify me when there is update";
      this.checkBox2.UseVisualStyleBackColor = true;
      this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(10, 69);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(115, 17);
      this.checkBox1.TabIndex = 6;
      this.checkBox1.Text = "Check for Updates";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
      this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox1.Location = new Point(75, 43);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(534, 20);
      this.textBox1.TabIndex = 5;
      this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
      this.label11.AutoSize = true;
      this.label11.Location = new Point(5, 43);
      this.label11.Name = "label11";
      this.label11.Size = new Size(69, 13);
      this.label11.TabIndex = 4;
      this.label11.Text = "Home Page :";
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 15f);
      this.label5.Location = new Point(35, 6);
      this.label5.Name = "label5";
      this.label5.Size = new Size(83, 25);
      this.label5.TabIndex = 2;
      this.label5.Text = "Settings";
      this.tpAbout.Controls.Add((Control) this.label4);
      this.tpAbout.Controls.Add((Control) this.pictureBox3);
      this.tpAbout.Controls.Add((Control) this.textBox2);
      this.tpAbout.Controls.Add((Control) this.label15);
      this.tpAbout.Controls.Add((Control) this.label14);
      this.tpAbout.Controls.Add((Control) this.label13);
      this.tpAbout.Controls.Add((Control) this.pictureBox1);
      this.tpAbout.Location = new Point(4, 22);
      this.tpAbout.Name = "tpAbout";
      this.tpAbout.Size = new Size(624, 414);
      this.tpAbout.TabIndex = 5;
      this.tpAbout.Text = "tabPage3";
      this.tpAbout.UseVisualStyleBackColor = true;
      this.tpAbout.Paint += new PaintEventHandler(this.tpAbout_Paint);
      this.tabPage2.BackColor = Color.White;
      this.tabPage2.ForeColor = Color.Black;
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(616, 415);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "+";
      this.ımageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ımageList1.ImageStream");
      this.ımageList1.TransparentColor = Color.Transparent;
      this.ımageList1.Images.SetKeyName(0, "Başlıksız-1.png");
      this.timer1.Enabled = true;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.pictureBox1.Image = (Image) Resources.Başlıksız_1;
      this.pictureBox1.Location = new Point(10, 36);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(134, 124);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 5;
      this.pictureBox1.TabStop = false;
      this.label13.AutoSize = true;
      this.label13.Font = new Font("Microsoft Sans Serif", 25f);
      this.label13.Location = new Point(146, 24);
      this.label13.Name = "label13";
      this.label13.Size = new Size(98, 39);
      this.label13.TabIndex = 6;
      this.label13.Text = "Korot";
      this.label14.AutoSize = true;
      this.label14.Font = new Font("Microsoft Sans Serif", 15f);
      this.label14.Location = new Point(146, 63);
      this.label14.Name = "label14";
      this.label14.Size = new Size(108, 25);
      this.label14.TabIndex = 6;
      this.label14.Text = "TheHaltroy";
      this.label14.Click += new EventHandler(this.label14_Click);
      this.label15.AutoSize = true;
      this.label15.Font = new Font("Microsoft Sans Serif", 12f);
      this.label15.Location = new Point(238, 24);
      this.label15.Name = "label15";
      this.label15.Size = new Size(77, 20);
      this.label15.TabIndex = 6;
      this.label15.Text = "<version>";
      this.textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox2.Location = new Point(150, 91);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new Size(464, 158);
      this.textBox2.TabIndex = 7;
      this.textBox2.Text = componentResourceManager.GetString("textBox2.Text");
      this.label3.BorderStyle = BorderStyle.FixedSingle;
      this.label3.Image = (Image) componentResourceManager.GetObject("label3.Image");
      this.label3.ImageAlign = ContentAlignment.TopCenter;
      this.label3.Location = new Point(496, 3);
      this.label3.Name = "label3";
      this.label3.Size = new Size(108, 118);
      this.label3.TabIndex = 0;
      this.label3.Text = "About";
      this.label3.TextAlign = ContentAlignment.BottomCenter;
      this.label3.Click += new EventHandler(this.label3_Click);
      this.listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new Point(10, 142);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new Size(599, 264);
      this.listBox1.TabIndex = 8;
      this.listBox1.DoubleClick += new EventHandler(this.listBox1_DoubleClick);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 15f);
      this.label1.Location = new Point(9, 112);
      this.label1.Name = "label1";
      this.label1.Size = new Size(72, 25);
      this.label1.TabIndex = 7;
      this.label1.Text = "History";
      this.pictureBox2.Image = (Image) Resources.arrow2_left_512;
      this.pictureBox2.Location = new Point(3, 6);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(26, 24);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox2.TabIndex = 9;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.Click += new EventHandler(this.label4_Click);
      this.pictureBox3.Image = (Image) Resources.arrow2_left_512;
      this.pictureBox3.Location = new Point(10, 6);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new Size(26, 24);
      this.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox3.TabIndex = 10;
      this.pictureBox3.TabStop = false;
      this.pictureBox3.Click += new EventHandler(this.label4_Click);
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 15f);
      this.label4.Location = new Point(41, 7);
      this.label4.Name = "label4";
      this.label4.Size = new Size(64, 25);
      this.label4.TabIndex = 11;
      this.label4.Text = "About";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(624, 441);
      this.Controls.Add((Control) this.tabControl1);
      this.ForeColor = Color.Black;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.KeyPreview = true;
      this.MinimumSize = new Size(640, 480);
      this.Name = nameof (frmMain);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Korot";
      this.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
      this.Load += new EventHandler(this.frmMain_Load);
      this.DoubleClick += new EventHandler(this.frmMain_DoubleClick);
      this.MouseDown += new MouseEventHandler(this.tabControl1_MouseDown);
      this.MouseMove += new MouseEventHandler(this.tabControl1_MouseMove);
      this.MouseUp += new MouseEventHandler(this.tabControl1_MouseUp);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabControl2.ResumeLayout(false);
      this.tpMain.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.tpSettings.ResumeLayout(false);
      this.tpSettings.PerformLayout();
      this.tpAbout.ResumeLayout(false);
      this.tpAbout.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox3).EndInit();
      this.ResumeLayout(false);
    }

    public static class CustomMessageBox
    {
      public static void Show(string title, string description)
      {
        using (msgkts msgkts = new msgkts(title, description))
        {
          int num = (int) msgkts.ShowDialog();
        }
      }
    }
  }
}
