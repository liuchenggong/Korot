

using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using Korot.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
  public class tabform : Form
  {
    private bool isLoading = false;
    private string loaduri = (string) null;
    private bool _Incognito = false;
    private string tabstyle = (string) null;
    private IContainer components = (IContainer) null;
    public int tabID;
    private ChromiumWebBrowser chromeBrowser;
    public frmMain _frmMain;
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private TextBox textBox1;
    private Panel panel1;
    private Button button5;

    public tabform(frmMain rmmain, bool isIncognito, string loadurl)
    {
      this.loaduri = loadurl;
      this.InitializeComponent();
      this.InitializeChromium();
      this._frmMain = rmmain;
      this._Incognito = isIncognito;
      if (!isIncognito)
        return;
      this.tabstyle = "[IM]";
    }

    public void InitializeChromium()
    {
      CefSettings cefSettings = new CefSettings();
      cefSettings.UserAgent = "Mozilla/5.0 (" + (object) Environment.OSVersion + "; " + (object) Environment.OSVersion.Platform + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36 Korot/" + Application.ProductVersion.ToString();
      if (!Cef.IsInitialized)
        Cef.Initialize(cefSettings);
      this.chromeBrowser = new ChromiumWebBrowser(this.loaduri, (IRequestContext) null);
      this.chromeBrowser.Dock = DockStyle.Fill;
      this.chromeBrowser.AddressChanged += new EventHandler<AddressChangedEventArgs>(this.cef_AddressChanged);
      this.chromeBrowser.TitleChanged += new EventHandler<TitleChangedEventArgs>(this.cef_TitleChanged);
      this.chromeBrowser.KeyDown += new KeyEventHandler(this.tabform_KeyDown);
      this.chromeBrowser.MenuHandler = (IContextMenuHandler) new MyCustomMenuHandler();
      this.chromeBrowser.LifeSpanHandler = (ILifeSpanHandler) new BrowserLifeSpanHandler(this);
      this.chromeBrowser.DownloadHandler = (IDownloadHandler) new DownloadHandler();
      this.chromeBrowser.LoadingStateChanged += new EventHandler<LoadingStateChangedEventArgs>(this.loadingstatechanged);
    }

    private void loadingstatechanged(object sender, LoadingStateChangedEventArgs e)
    {
      if (e.IsLoading)
        this.button2.Image = (Image) Resources.Cancel_512;
      else
        this.button2.Image = (Image) Resources.refresh;
      this.button1.Invoke((Action) (() => this.button1.Enabled = e.CanGoBack));
      this.button3.Invoke((Action) (() => this.button3.Enabled = e.CanGoForward));
      this.isLoading = e.IsLoading;
    }

    public void NewTab(string url)
    {
      this._frmMain.Invoke((Action) (() => this._frmMain.NewTab(url)));
    }

    private void tabform_Load(object sender, EventArgs e)
    {
      this.panel1.Controls.Add((Control) this.chromeBrowser);
    }

    private void button4_Click(object sender, EventArgs e)
    {
      this.chromeBrowser.Load(this.textBox1.Text);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.chromeBrowser.Back();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.chromeBrowser.Forward();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.isLoading)
        this.chromeBrowser.Stop();
      else
        this.chromeBrowser.Reload();
    }

    private void cef_AddressChanged(object sender, AddressChangedEventArgs e)
    {
      this.InvokeOnUiThreadIfRequired((Action) (() => this.textBox1.Text = e.Address));
      if (this._Incognito)
        return;
      this.InvokeOnUiThreadIfRequired((Action) (() => Settings.Default.History.Add(e.Address)));
    }

    private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
    {
      this.InvokeOnUiThreadIfRequired((Action) (() => this.Text = e.Title));
      this._frmMain.TabText(this.tabID, this.tabstyle + this.Text);
    }

    private void button5_Click(object sender, EventArgs e)
    {
      this.chromeBrowser.Load(Settings.Default.homepage);
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != Keys.Return)
        return;
      this.button4_Click((object) null, (EventArgs) null);
    }

    private void tabform_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.BrowserBack)
        this.button1_Click((object) null, (EventArgs) null);
      else if (e.KeyData == Keys.BrowserForward)
        this.button3_Click((object) null, (EventArgs) null);
      else if (e.KeyData == Keys.BrowserRefresh)
        this.button2_Click((object) null, (EventArgs) null);
      else if (e.KeyData == Keys.BrowserStop)
      {
        this.button2_Click((object) null, (EventArgs) null);
      }
      else
      {
        if (e.KeyData != Keys.BrowserHome)
          return;
        this.button5_Click((object) null, (EventArgs) null);
      }
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
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
      this.textBox1 = new TextBox();
      this.panel1 = new Panel();
      this.button4 = new Button();
      this.button2 = new Button();
      this.button1 = new Button();
      this.button3 = new Button();
      this.button5 = new Button();
      this.SuspendLayout();
      this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox1.Location = new Point(115, 2);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(542, 20);
      this.textBox1.TabIndex = 1;
      this.textBox1.KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
      this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.panel1.Location = new Point(-5, 31);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(701, 425);
      this.panel1.TabIndex = 2;
      this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
      this.button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.button4.FlatAppearance.BorderSize = 0;
      this.button4.FlatStyle = FlatStyle.Flat;
      this.button4.Image = (Image) Resources.go;
      this.button4.Location = new Point(663, 0);
      this.button4.Name = "button4";
      this.button4.Size = new Size(27, 26);
      this.button4.TabIndex = 0;
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.button4.KeyDown += new KeyEventHandler(this.tabform_KeyDown);
      this.button2.FlatAppearance.BorderSize = 0;
      this.button2.FlatStyle = FlatStyle.Flat;
      this.button2.Image = (Image) Resources.refresh;
      this.button2.Location = new Point(28, 0);
      this.button2.Name = "button2";
      this.button2.Size = new Size(27, 25);
      this.button2.TabIndex = 0;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button2.KeyDown += new KeyEventHandler(this.tabform_KeyDown);
      this.button1.FlatAppearance.BorderSize = 0;
      this.button1.FlatStyle = FlatStyle.Flat;
      this.button1.Image = (Image) Resources.arrow2_left_512;
      this.button1.Location = new Point(1, 0);
      this.button1.Name = "button1";
      this.button1.Size = new Size(27, 25);
      this.button1.TabIndex = 0;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button1.KeyDown += new KeyEventHandler(this.tabform_KeyDown);
      this.button3.FlatAppearance.BorderSize = 0;
      this.button3.FlatStyle = FlatStyle.Flat;
      this.button3.Image = (Image) Resources.arrow2_right_512;
      this.button3.Location = new Point(82, 0);
      this.button3.Name = "button3";
      this.button3.Size = new Size(27, 25);
      this.button3.TabIndex = 0;
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button3.KeyDown += new KeyEventHandler(this.tabform_KeyDown);
      this.button5.FlatAppearance.BorderSize = 0;
      this.button5.FlatStyle = FlatStyle.Flat;
      this.button5.Image = (Image) Resources.home;
      this.button5.Location = new Point(55, 0);
      this.button5.Name = "button5";
      this.button5.Size = new Size(27, 25);
      this.button5.TabIndex = 4;
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new EventHandler(this.button5_Click);
      this.button5.KeyDown += new KeyEventHandler(this.tabform_KeyDown);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(691, 450);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.button4);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button5);
      this.ForeColor = Color.Black;
      this.FormBorderStyle = FormBorderStyle.None;
      this.KeyPreview = true;
      this.Name = nameof (tabform);
      this.Text = " ";
      this.Load += new EventHandler(this.tabform_Load);
      this.KeyDown += new KeyEventHandler(this.tabform_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
