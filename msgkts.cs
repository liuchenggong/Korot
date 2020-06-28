

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
  public class msgkts : Form
  {
    private IContainer components = (IContainer) null;
    private Label label1;
    internal Button btNo;
    internal Button btCancel;
    internal Button btYes;

    public msgkts(string title, string message)
    {
      this.InitializeComponent();
      this.Text = title;
      this.label1.Text = message;
    }

    private void btYes_Click(object sender, EventArgs e)
    {
      int num = (int) new frmUpdate().ShowDialog();
      this.Close();
    }

    private void btNo_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (msgkts));
      this.label1 = new Label();
      this.btNo = new Button();
      this.btCancel = new Button();
      this.btYes = new Button();
      this.SuspendLayout();
      this.label1.Location = new Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(345, 65);
      this.label1.TabIndex = 0;
      this.label1.Text = "<message>";
      this.btNo.FlatStyle = FlatStyle.Flat;
      this.btNo.ImeMode = ImeMode.NoControl;
      this.btNo.Location = new Point(213, 81);
      this.btNo.Name = "btNo";
      this.btNo.Size = new Size(75, 23);
      this.btNo.TabIndex = 8;
      this.btNo.Text = "No";
      this.btNo.UseVisualStyleBackColor = true;
      this.btNo.Click += new EventHandler(this.btNo_Click);
      this.btCancel.FlatStyle = FlatStyle.Flat;
      this.btCancel.ImeMode = ImeMode.NoControl;
      this.btCancel.Location = new Point(132, 81);
      this.btCancel.Name = "btCancel";
      this.btCancel.Size = new Size(75, 23);
      this.btCancel.TabIndex = 9;
      this.btCancel.Text = "Cancel";
      this.btCancel.UseVisualStyleBackColor = true;
      this.btCancel.Click += new EventHandler(this.btCancel_Click);
      this.btYes.FlatStyle = FlatStyle.Flat;
      this.btYes.ImeMode = ImeMode.NoControl;
      this.btYes.Location = new Point(51, 81);
      this.btYes.Name = "btYes";
      this.btYes.Size = new Size(75, 23);
      this.btYes.TabIndex = 10;
      this.btYes.Text = "Yes";
      this.btYes.UseVisualStyleBackColor = true;
      this.btYes.Click += new EventHandler(this.btYes_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(370, 114);
      this.Controls.Add((Control) this.btNo);
      this.Controls.Add((Control) this.btCancel);
      this.Controls.Add((Control) this.btYes);
      this.Controls.Add((Control) this.label1);
      this.ForeColor = Color.Black;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (msgkts);
      this.Text = "<title>";
      this.ResumeLayout(false);
    }
  }
}
