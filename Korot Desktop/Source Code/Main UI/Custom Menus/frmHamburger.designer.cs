namespace Korot
{
    partial class frmHamburger
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tsSearch = new System.Windows.Forms.TextBox();
            this.btFindNext = new HTAlt.WinForms.HTButton();
            this.lbFindStatus = new System.Windows.Forms.Label();
            this.btCaseSensitive = new HTAlt.WinForms.HTButton();
            this.htButton4 = new HTAlt.WinForms.HTButton();
            this.btNewWindow = new HTAlt.WinForms.HTButton();
            this.btNewIncWindow = new HTAlt.WinForms.HTButton();
            this.btScreenShot = new HTAlt.WinForms.HTButton();
            this.btSave = new HTAlt.WinForms.HTButton();
            this.btFullScreen = new HTAlt.WinForms.HTButton();
            this.btTabColor = new HTAlt.WinForms.HTButton();
            this.btMute = new HTAlt.WinForms.HTButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btResetZoom = new HTAlt.WinForms.HTButton();
            this.btZoomMinus = new HTAlt.WinForms.HTButton();
            this.btZoomPlus = new HTAlt.WinForms.HTButton();
            this.lbZoom = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pbcollections = new System.Windows.Forms.PictureBox();
            this.lbCollections = new System.Windows.Forms.Label();
            this.pbHistory = new System.Windows.Forms.PictureBox();
            this.lbHistory = new System.Windows.Forms.Label();
            this.lbDownloads = new System.Windows.Forms.Label();
            this.pbDownloads = new System.Windows.Forms.PictureBox();
            this.lbThemes = new System.Windows.Forms.Label();
            this.pbThemes = new System.Windows.Forms.PictureBox();
            this.lbSettings = new System.Windows.Forms.Label();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.lbAbout = new System.Windows.Forms.Label();
            this.pbABout = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.btRestore = new HTAlt.WinForms.HTButton();
            this.btDefaultProxy = new HTAlt.WinForms.HTButton();
            this.flpExtensions = new System.Windows.Forms.FlowLayoutPanel();
            this.btExtStore = new HTAlt.WinForms.HTButton();
            this.btExtFolder = new HTAlt.WinForms.HTButton();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcollections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDownloads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThemes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbABout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tsSearch
            // 
            this.tsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tsSearch.Location = new System.Drawing.Point(41, 57);
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(177, 21);
            this.tsSearch.TabIndex = 1;
            this.tsSearch.Click += new System.EventHandler(this.tsSearch_Click);
            this.tsSearch.TextChanged += new System.EventHandler(this.tsSearch_TextChanged);
            // 
            // btFindNext
            // 
            this.btFindNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindNext.ButtonImage = global::Korot.Properties.Resources.rightarrow;
            this.btFindNext.ButtonText = "";
            this.btFindNext.FlatAppearance.BorderSize = 0;
            this.btFindNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFindNext.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btFindNext.Location = new System.Drawing.Point(232, 55);
            this.btFindNext.Name = "btFindNext";
            this.btFindNext.Size = new System.Drawing.Size(30, 29);
            this.btFindNext.TabIndex = 2;
            this.btFindNext.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btFindNext.UseVisualStyleBackColor = true;
            this.btFindNext.Click += new System.EventHandler(this.btFindNext_Click);
            // 
            // lbFindStatus
            // 
            this.lbFindStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFindStatus.Location = new System.Drawing.Point(3, 84);
            this.lbFindStatus.Name = "lbFindStatus";
            this.lbFindStatus.Size = new System.Drawing.Size(259, 16);
            this.lbFindStatus.TabIndex = 4;
            this.lbFindStatus.Text = "Not searching";
            this.lbFindStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btCaseSensitive
            // 
            this.btCaseSensitive.ButtonText = "Aa";
            this.btCaseSensitive.FlatAppearance.BorderSize = 0;
            this.btCaseSensitive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCaseSensitive.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.btCaseSensitive.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btCaseSensitive.Location = new System.Drawing.Point(5, 52);
            this.btCaseSensitive.Name = "btCaseSensitive";
            this.btCaseSensitive.Size = new System.Drawing.Size(30, 29);
            this.btCaseSensitive.TabIndex = 5;
            this.btCaseSensitive.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btCaseSensitive.UseVisualStyleBackColor = true;
            this.btCaseSensitive.Click += new System.EventHandler(this.btCaseSensitive_Click);
            // 
            // htButton4
            // 
            this.htButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.htButton4.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.htButton4.ButtonText = "";
            this.htButton4.FlatAppearance.BorderSize = 0;
            this.htButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.htButton4.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.htButton4.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.htButton4.Location = new System.Drawing.Point(232, 7);
            this.htButton4.Name = "htButton4";
            this.htButton4.Size = new System.Drawing.Size(30, 29);
            this.htButton4.TabIndex = 6;
            this.htButton4.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.htButton4.UseVisualStyleBackColor = true;
            this.htButton4.Click += new System.EventHandler(this.htButton4_Click);
            // 
            // btNewWindow
            // 
            this.btNewWindow.ButtonImage = global::Korot.Properties.Resources.newwindow;
            this.btNewWindow.FlatAppearance.BorderSize = 0;
            this.btNewWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNewWindow.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btNewWindow.Location = new System.Drawing.Point(12, 12);
            this.btNewWindow.Name = "btNewWindow";
            this.btNewWindow.Size = new System.Drawing.Size(20, 20);
            this.btNewWindow.TabIndex = 7;
            this.btNewWindow.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btNewWindow.UseVisualStyleBackColor = true;
            this.btNewWindow.Click += new System.EventHandler(this.htButton5_Click);
            // 
            // btNewIncWindow
            // 
            this.btNewIncWindow.ButtonImage = global::Korot.Properties.Resources.inctab;
            this.btNewIncWindow.FlatAppearance.BorderSize = 0;
            this.btNewIncWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNewIncWindow.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btNewIncWindow.Location = new System.Drawing.Point(38, 12);
            this.btNewIncWindow.Name = "btNewIncWindow";
            this.btNewIncWindow.Size = new System.Drawing.Size(20, 20);
            this.btNewIncWindow.TabIndex = 7;
            this.btNewIncWindow.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btNewIncWindow.UseVisualStyleBackColor = true;
            this.btNewIncWindow.Click += new System.EventHandler(this.htButton6_Click);
            // 
            // btScreenShot
            // 
            this.btScreenShot.ButtonImage = global::Korot.Properties.Resources.screenshot;
            this.btScreenShot.ButtonText = "";
            this.btScreenShot.FlatAppearance.BorderSize = 0;
            this.btScreenShot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btScreenShot.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btScreenShot.Location = new System.Drawing.Point(64, 12);
            this.btScreenShot.Name = "btScreenShot";
            this.btScreenShot.Size = new System.Drawing.Size(20, 20);
            this.btScreenShot.TabIndex = 8;
            this.btScreenShot.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btScreenShot.UseVisualStyleBackColor = true;
            this.btScreenShot.Click += new System.EventHandler(this.btScreenShot_Click);
            // 
            // btSave
            // 
            this.btSave.ButtonImage = global::Korot.Properties.Resources.collection;
            this.btSave.ButtonText = "";
            this.btSave.FlatAppearance.BorderSize = 0;
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btSave.Location = new System.Drawing.Point(90, 12);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(20, 20);
            this.btSave.TabIndex = 8;
            this.btSave.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btFullScreen
            // 
            this.btFullScreen.ButtonImage = global::Korot.Properties.Resources.fullscreen;
            this.btFullScreen.ButtonText = "";
            this.btFullScreen.FlatAppearance.BorderSize = 0;
            this.btFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFullScreen.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btFullScreen.Location = new System.Drawing.Point(116, 12);
            this.btFullScreen.Name = "btFullScreen";
            this.btFullScreen.Size = new System.Drawing.Size(20, 20);
            this.btFullScreen.TabIndex = 8;
            this.btFullScreen.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btFullScreen.UseVisualStyleBackColor = true;
            this.btFullScreen.Click += new System.EventHandler(this.btFullScreen_Click);
            // 
            // btTabColor
            // 
            this.btTabColor.ButtonImage = global::Korot.Properties.Resources.tab;
            this.btTabColor.ButtonText = "";
            this.btTabColor.FlatAppearance.BorderSize = 0;
            this.btTabColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btTabColor.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btTabColor.Location = new System.Drawing.Point(142, 12);
            this.btTabColor.Name = "btTabColor";
            this.btTabColor.Size = new System.Drawing.Size(20, 20);
            this.btTabColor.TabIndex = 8;
            this.btTabColor.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btTabColor.UseVisualStyleBackColor = true;
            this.btTabColor.Click += new System.EventHandler(this.btTabColor_Click);
            // 
            // btMute
            // 
            this.btMute.ButtonImage = global::Korot.Properties.Resources.mute;
            this.btMute.ButtonText = "";
            this.btMute.FlatAppearance.BorderSize = 0;
            this.btMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMute.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btMute.Location = new System.Drawing.Point(168, 12);
            this.btMute.Name = "btMute";
            this.btMute.Size = new System.Drawing.Size(20, 20);
            this.btMute.TabIndex = 9;
            this.btMute.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btMute.UseVisualStyleBackColor = true;
            this.btMute.Click += new System.EventHandler(this.btMute_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(-5, 104);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(286, 1);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(-8, 43);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(286, 1);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // btResetZoom
            // 
            this.btResetZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btResetZoom.ButtonText = "Reset Zoom";
            this.btResetZoom.FlatAppearance.BorderSize = 0;
            this.btResetZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btResetZoom.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btResetZoom.Location = new System.Drawing.Point(12, 143);
            this.btResetZoom.Name = "btResetZoom";
            this.btResetZoom.Size = new System.Drawing.Size(250, 20);
            this.btResetZoom.TabIndex = 12;
            this.btResetZoom.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btResetZoom.UseVisualStyleBackColor = true;
            this.btResetZoom.Click += new System.EventHandler(this.btResetZoom_Click);
            // 
            // btZoomMinus
            // 
            this.btZoomMinus.ButtonText = "-";
            this.btZoomMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btZoomMinus.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.btZoomMinus.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btZoomMinus.Location = new System.Drawing.Point(12, 117);
            this.btZoomMinus.Name = "btZoomMinus";
            this.btZoomMinus.Size = new System.Drawing.Size(20, 20);
            this.btZoomMinus.TabIndex = 12;
            this.btZoomMinus.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btZoomMinus.UseVisualStyleBackColor = true;
            // 
            // btZoomPlus
            // 
            this.btZoomPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btZoomPlus.ButtonText = "+";
            this.btZoomPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btZoomPlus.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.btZoomPlus.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btZoomPlus.Location = new System.Drawing.Point(242, 117);
            this.btZoomPlus.Name = "btZoomPlus";
            this.btZoomPlus.Size = new System.Drawing.Size(20, 20);
            this.btZoomPlus.TabIndex = 12;
            this.btZoomPlus.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btZoomPlus.UseVisualStyleBackColor = true;
            // 
            // lbZoom
            // 
            this.lbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbZoom.Location = new System.Drawing.Point(38, 118);
            this.lbZoom.Name = "lbZoom";
            this.lbZoom.Size = new System.Drawing.Size(194, 16);
            this.lbZoom.TabIndex = 4;
            this.lbZoom.Text = "ZOOMLEVEL";
            this.lbZoom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Location = new System.Drawing.Point(-8, 169);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(286, 1);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // pbcollections
            // 
            this.pbcollections.Image = global::Korot.Properties.Resources.collections;
            this.pbcollections.Location = new System.Drawing.Point(12, 179);
            this.pbcollections.Name = "pbcollections";
            this.pbcollections.Size = new System.Drawing.Size(20, 20);
            this.pbcollections.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbcollections.TabIndex = 14;
            this.pbcollections.TabStop = false;
            this.pbcollections.Click += new System.EventHandler(this.lbCollections_Click);
            this.pbcollections.MouseEnter += new System.EventHandler(this.Collections_MouseEnter);
            this.pbcollections.MouseLeave += new System.EventHandler(this.Collections_MouseLeave);
            // 
            // lbCollections
            // 
            this.lbCollections.AutoSize = true;
            this.lbCollections.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbCollections.Location = new System.Drawing.Point(38, 179);
            this.lbCollections.Name = "lbCollections";
            this.lbCollections.Size = new System.Drawing.Size(87, 19);
            this.lbCollections.TabIndex = 15;
            this.lbCollections.Text = "Collections";
            this.lbCollections.Click += new System.EventHandler(this.lbCollections_Click);
            this.lbCollections.MouseEnter += new System.EventHandler(this.Collections_MouseEnter);
            this.lbCollections.MouseLeave += new System.EventHandler(this.Collections_MouseLeave);
            // 
            // pbHistory
            // 
            this.pbHistory.Image = global::Korot.Properties.Resources.history;
            this.pbHistory.Location = new System.Drawing.Point(12, 205);
            this.pbHistory.Name = "pbHistory";
            this.pbHistory.Size = new System.Drawing.Size(20, 20);
            this.pbHistory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHistory.TabIndex = 14;
            this.pbHistory.TabStop = false;
            this.pbHistory.Click += new System.EventHandler(this.lbHistory_Click);
            this.pbHistory.MouseEnter += new System.EventHandler(this.History_MouseEnter);
            this.pbHistory.MouseLeave += new System.EventHandler(this.History_MouseLeave);
            // 
            // lbHistory
            // 
            this.lbHistory.AutoSize = true;
            this.lbHistory.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbHistory.Location = new System.Drawing.Point(38, 205);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.Size = new System.Drawing.Size(58, 19);
            this.lbHistory.TabIndex = 15;
            this.lbHistory.Text = "History";
            this.lbHistory.Click += new System.EventHandler(this.lbHistory_Click);
            this.lbHistory.MouseEnter += new System.EventHandler(this.History_MouseEnter);
            this.lbHistory.MouseLeave += new System.EventHandler(this.History_MouseLeave);
            // 
            // lbDownloads
            // 
            this.lbDownloads.AutoSize = true;
            this.lbDownloads.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbDownloads.Location = new System.Drawing.Point(38, 231);
            this.lbDownloads.Name = "lbDownloads";
            this.lbDownloads.Size = new System.Drawing.Size(88, 19);
            this.lbDownloads.TabIndex = 17;
            this.lbDownloads.Text = "Downloads";
            this.lbDownloads.Click += new System.EventHandler(this.pbDownloads_Click);
            this.lbDownloads.MouseEnter += new System.EventHandler(this.Downloads_MouseEnter);
            this.lbDownloads.MouseLeave += new System.EventHandler(this.Downloads_MouseLeave);
            // 
            // pbDownloads
            // 
            this.pbDownloads.Image = global::Korot.Properties.Resources.download;
            this.pbDownloads.Location = new System.Drawing.Point(12, 231);
            this.pbDownloads.Name = "pbDownloads";
            this.pbDownloads.Size = new System.Drawing.Size(20, 20);
            this.pbDownloads.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDownloads.TabIndex = 16;
            this.pbDownloads.TabStop = false;
            this.pbDownloads.Click += new System.EventHandler(this.pbDownloads_Click);
            this.pbDownloads.MouseEnter += new System.EventHandler(this.Downloads_MouseEnter);
            this.pbDownloads.MouseLeave += new System.EventHandler(this.Downloads_MouseLeave);
            // 
            // lbThemes
            // 
            this.lbThemes.AutoSize = true;
            this.lbThemes.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbThemes.Location = new System.Drawing.Point(38, 257);
            this.lbThemes.Name = "lbThemes";
            this.lbThemes.Size = new System.Drawing.Size(65, 19);
            this.lbThemes.TabIndex = 19;
            this.lbThemes.Text = "Themes";
            this.lbThemes.Click += new System.EventHandler(this.pbThemes_Click);
            this.lbThemes.MouseEnter += new System.EventHandler(this.Themes_MouseEnter);
            this.lbThemes.MouseLeave += new System.EventHandler(this.Themes_MouseLeave);
            // 
            // pbThemes
            // 
            this.pbThemes.Image = global::Korot.Properties.Resources.theme;
            this.pbThemes.Location = new System.Drawing.Point(12, 257);
            this.pbThemes.Name = "pbThemes";
            this.pbThemes.Size = new System.Drawing.Size(20, 20);
            this.pbThemes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbThemes.TabIndex = 18;
            this.pbThemes.TabStop = false;
            this.pbThemes.Click += new System.EventHandler(this.pbThemes_Click);
            this.pbThemes.MouseEnter += new System.EventHandler(this.Themes_MouseEnter);
            this.pbThemes.MouseLeave += new System.EventHandler(this.Themes_MouseLeave);
            // 
            // lbSettings
            // 
            this.lbSettings.AutoSize = true;
            this.lbSettings.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbSettings.Location = new System.Drawing.Point(38, 283);
            this.lbSettings.Name = "lbSettings";
            this.lbSettings.Size = new System.Drawing.Size(66, 19);
            this.lbSettings.TabIndex = 21;
            this.lbSettings.Text = "Settings";
            this.lbSettings.Click += new System.EventHandler(this.pbSettings_Click);
            this.lbSettings.MouseEnter += new System.EventHandler(this.Settings_MouseEnter);
            this.lbSettings.MouseLeave += new System.EventHandler(this.Settings_MouseLeave);
            // 
            // pbSettings
            // 
            this.pbSettings.Image = global::Korot.Properties.Resources.Settings;
            this.pbSettings.Location = new System.Drawing.Point(12, 283);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(20, 20);
            this.pbSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSettings.TabIndex = 20;
            this.pbSettings.TabStop = false;
            this.pbSettings.Click += new System.EventHandler(this.pbSettings_Click);
            this.pbSettings.MouseEnter += new System.EventHandler(this.Settings_MouseEnter);
            this.pbSettings.MouseLeave += new System.EventHandler(this.Settings_MouseLeave);
            // 
            // lbAbout
            // 
            this.lbAbout.AutoSize = true;
            this.lbAbout.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbAbout.Location = new System.Drawing.Point(38, 309);
            this.lbAbout.Name = "lbAbout";
            this.lbAbout.Size = new System.Drawing.Size(51, 19);
            this.lbAbout.TabIndex = 23;
            this.lbAbout.Text = "About";
            this.lbAbout.Click += new System.EventHandler(this.pbABout_Click);
            this.lbAbout.MouseEnter += new System.EventHandler(this.About_MouseEnter);
            this.lbAbout.MouseLeave += new System.EventHandler(this.About_MouseLeave);
            // 
            // pbABout
            // 
            this.pbABout.Image = global::Korot.Properties.Resources.about;
            this.pbABout.Location = new System.Drawing.Point(12, 309);
            this.pbABout.Name = "pbABout";
            this.pbABout.Size = new System.Drawing.Size(20, 20);
            this.pbABout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbABout.TabIndex = 22;
            this.pbABout.TabStop = false;
            this.pbABout.Click += new System.EventHandler(this.pbABout_Click);
            this.pbABout.MouseEnter += new System.EventHandler(this.About_MouseEnter);
            this.pbABout.MouseLeave += new System.EventHandler(this.About_MouseLeave);
            // 
            // pictureBox10
            // 
            this.pictureBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox10.BackColor = System.Drawing.Color.Black;
            this.pictureBox10.Location = new System.Drawing.Point(-8, 335);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(286, 1);
            this.pictureBox10.TabIndex = 24;
            this.pictureBox10.TabStop = false;
            // 
            // btRestore
            // 
            this.btRestore.ButtonImage = global::Korot.Properties.Resources.restore;
            this.btRestore.ButtonText = "";
            this.btRestore.FlatAppearance.BorderSize = 0;
            this.btRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRestore.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btRestore.Location = new System.Drawing.Point(194, 12);
            this.btRestore.Name = "btRestore";
            this.btRestore.Size = new System.Drawing.Size(20, 20);
            this.btRestore.TabIndex = 9;
            this.btRestore.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btRestore.UseVisualStyleBackColor = true;
            this.btRestore.Click += new System.EventHandler(this.btRestore_Click);
            // 
            // btDefaultProxy
            // 
            this.btDefaultProxy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btDefaultProxy.ButtonText = "Restore to default proxy";
            this.btDefaultProxy.FlatAppearance.BorderSize = 0;
            this.btDefaultProxy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDefaultProxy.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btDefaultProxy.Location = new System.Drawing.Point(5, 342);
            this.btDefaultProxy.Name = "btDefaultProxy";
            this.btDefaultProxy.Size = new System.Drawing.Size(257, 20);
            this.btDefaultProxy.TabIndex = 28;
            this.btDefaultProxy.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btDefaultProxy.UseVisualStyleBackColor = true;
            this.btDefaultProxy.Click += new System.EventHandler(this.btDefaultProxy_Click);
            // 
            // flpExtensions
            // 
            this.flpExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpExtensions.AutoScroll = true;
            this.flpExtensions.Location = new System.Drawing.Point(5, 394);
            this.flpExtensions.Name = "flpExtensions";
            this.flpExtensions.Size = new System.Drawing.Size(257, 113);
            this.flpExtensions.TabIndex = 29;
            // 
            // btExtStore
            // 
            this.btExtStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btExtStore.ButtonImage = global::Korot.Properties.Resources.store;
            this.btExtStore.ButtonText = "";
            this.btExtStore.FlatAppearance.BorderSize = 0;
            this.btExtStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExtStore.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btExtStore.Location = new System.Drawing.Point(105, 368);
            this.btExtStore.Name = "btExtStore";
            this.btExtStore.Size = new System.Drawing.Size(20, 20);
            this.btExtStore.TabIndex = 31;
            this.btExtStore.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btExtStore.UseVisualStyleBackColor = true;
            this.btExtStore.Click += new System.EventHandler(this.btExtStore_Click);
            // 
            // btExtFolder
            // 
            this.btExtFolder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btExtFolder.ButtonImage = global::Korot.Properties.Resources.extfolder;
            this.btExtFolder.ButtonText = "";
            this.btExtFolder.FlatAppearance.BorderSize = 0;
            this.btExtFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExtFolder.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btExtFolder.Location = new System.Drawing.Point(131, 368);
            this.btExtFolder.Name = "btExtFolder";
            this.btExtFolder.Size = new System.Drawing.Size(20, 20);
            this.btExtFolder.TabIndex = 32;
            this.btExtFolder.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btExtFolder.UseVisualStyleBackColor = true;
            this.btExtFolder.Click += new System.EventHandler(this.btExtFolder_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(2, 515);
            this.pictureBox4.TabIndex = 33;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Black;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox5.Location = new System.Drawing.Point(268, 0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(2, 515);
            this.pictureBox5.TabIndex = 34;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Black;
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox6.Location = new System.Drawing.Point(2, 0);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(266, 2);
            this.pictureBox6.TabIndex = 35;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Black;
            this.pictureBox7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox7.Location = new System.Drawing.Point(2, 513);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(266, 2);
            this.pictureBox7.TabIndex = 36;
            this.pictureBox7.TabStop = false;
            // 
            // frmHamburger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(270, 515);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.btExtStore);
            this.Controls.Add(this.btExtFolder);
            this.Controls.Add(this.flpExtensions);
            this.Controls.Add(this.btDefaultProxy);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.lbAbout);
            this.Controls.Add(this.pbABout);
            this.Controls.Add(this.lbSettings);
            this.Controls.Add(this.pbSettings);
            this.Controls.Add(this.lbThemes);
            this.Controls.Add(this.pbThemes);
            this.Controls.Add(this.lbDownloads);
            this.Controls.Add(this.pbDownloads);
            this.Controls.Add(this.lbHistory);
            this.Controls.Add(this.pbHistory);
            this.Controls.Add(this.lbCollections);
            this.Controls.Add(this.pbcollections);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btZoomPlus);
            this.Controls.Add(this.btZoomMinus);
            this.Controls.Add(this.btResetZoom);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btRestore);
            this.Controls.Add(this.btMute);
            this.Controls.Add(this.btTabColor);
            this.Controls.Add(this.btFullScreen);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btScreenShot);
            this.Controls.Add(this.btNewIncWindow);
            this.Controls.Add(this.btNewWindow);
            this.Controls.Add(this.htButton4);
            this.Controls.Add(this.btCaseSensitive);
            this.Controls.Add(this.lbZoom);
            this.Controls.Add(this.lbFindStatus);
            this.Controls.Add(this.btFindNext);
            this.Controls.Add(this.tsSearch);
            this.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHamburger";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmHamburger";
            this.Leave += new System.EventHandler(this.frmHamburger_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcollections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDownloads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThemes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbABout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tsSearch;
        private HTAlt.WinForms.HTButton btFindNext;
        private System.Windows.Forms.Label lbFindStatus;
        private HTAlt.WinForms.HTButton btCaseSensitive;
        private HTAlt.WinForms.HTButton htButton4;
        private HTAlt.WinForms.HTButton btNewWindow;
        private HTAlt.WinForms.HTButton btNewIncWindow;
        private HTAlt.WinForms.HTButton btScreenShot;
        private HTAlt.WinForms.HTButton btSave;
        private HTAlt.WinForms.HTButton btFullScreen;
        private HTAlt.WinForms.HTButton btTabColor;
        private HTAlt.WinForms.HTButton btMute;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private HTAlt.WinForms.HTButton btResetZoom;
        private HTAlt.WinForms.HTButton btZoomMinus;
        private HTAlt.WinForms.HTButton btZoomPlus;
        private System.Windows.Forms.Label lbZoom;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pbcollections;
        private System.Windows.Forms.Label lbCollections;
        private System.Windows.Forms.PictureBox pbHistory;
        private System.Windows.Forms.Label lbHistory;
        private System.Windows.Forms.Label lbDownloads;
        private System.Windows.Forms.PictureBox pbDownloads;
        private System.Windows.Forms.Label lbThemes;
        private System.Windows.Forms.PictureBox pbThemes;
        private System.Windows.Forms.Label lbSettings;
        private System.Windows.Forms.PictureBox pbSettings;
        private System.Windows.Forms.Label lbAbout;
        private System.Windows.Forms.PictureBox pbABout;
        private System.Windows.Forms.PictureBox pictureBox10;
        private HTAlt.WinForms.HTButton btRestore;
        private HTAlt.WinForms.HTButton btDefaultProxy;
        private System.Windows.Forms.FlowLayoutPanel flpExtensions;
        private HTAlt.WinForms.HTButton btExtStore;
        private HTAlt.WinForms.HTButton btExtFolder;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
    }
}