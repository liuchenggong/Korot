

using Korot.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Korot
{
  public class frmUpdate : Form
  {
    private string installLocation = "C:\\TheHaltroy Installer\\Installer.exe";
    private IContainer components = (IContainer) null;
    internal Label Label1;

    public frmUpdate()
    {
      this.InitializeComponent();
    }

    private void frmUpdate_Load(object sender, EventArgs e)
    {
      try
      {
        Process.Start(this.installLocation);
      }
      catch
      {
      }
      Thread.Sleep(3000);
      Settings.Default.Save();
      Application.Exit();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmUpdate));
      this.Label1 = new Label();
      this.SuspendLayout();
      this.Label1.AutoSize = true;
      this.Label1.Location = new Point(12, 9);
      this.Label1.Name = "Label1";
      this.Label1.Size = new Size(73, 13);
      this.Label1.TabIndex = 5;
      this.Label1.Text = "Please Wait...";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(426, 56);
      this.Controls.Add((Control) this.Label1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (frmUpdate);
      this.Text = "Updating Korot...";
      this.Load += new EventHandler(this.frmUpdate_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
