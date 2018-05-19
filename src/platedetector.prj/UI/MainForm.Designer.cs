namespace Platedetector.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseAlgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evalAlgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDetect = new System.Windows.Forms.Button();
            this.tboxAlg = new System.Windows.Forms.TextBox();
            this.lblAlg = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.tboxFolder = new System.Windows.Forms.TextBox();
            this.chkBoxMarkup = new System.Windows.Forms.CheckBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnMoveNext = new System.Windows.Forms.Button();
            this.btnMoveBack = new System.Windows.Forms.Button();
            this.pictureBox = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.logView = new Platedetector.Utils.Logging.LogView();
            this.groupBoxDir = new System.Windows.Forms.GroupBox();
            this.groupBoxMarkup = new System.Windows.Forms.GroupBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBoxDir.SuspendLayout();
            this.groupBoxMarkup.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.algToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(963, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImgToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // loadImgToolStripMenuItem
            // 
            this.loadImgToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadImgToolStripMenuItem.Image")));
            this.loadImgToolStripMenuItem.Name = "loadImgToolStripMenuItem";
            this.loadImgToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.loadImgToolStripMenuItem.Text = "Загрузить изображение";
            this.loadImgToolStripMenuItem.Click += new System.EventHandler(this.OnLoadImgToolStripMenuItemClick);
            // 
            // algToolStripMenuItem
            // 
            this.algToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseAlgToolStripMenuItem,
            this.evalAlgToolStripMenuItem});
            this.algToolStripMenuItem.Name = "algToolStripMenuItem";
            this.algToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.algToolStripMenuItem.Text = "Алгоритм";
            // 
            // chooseAlgToolStripMenuItem
            // 
            this.chooseAlgToolStripMenuItem.Name = "chooseAlgToolStripMenuItem";
            this.chooseAlgToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.chooseAlgToolStripMenuItem.Text = "Выбрать";
            this.chooseAlgToolStripMenuItem.Click += new System.EventHandler(this.OnChooseAlgToolStripMenuItemClick);
            // 
            // evalAlgToolStripMenuItem
            // 
            this.evalAlgToolStripMenuItem.Name = "evalAlgToolStripMenuItem";
            this.evalAlgToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.evalAlgToolStripMenuItem.Text = "Оценить";
            this.evalAlgToolStripMenuItem.Click += new System.EventHandler(this.OnEvalAlgToolStripMenuItemClick);
            // 
            // btnDetect
            // 
            this.btnDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetect.Location = new System.Drawing.Point(733, 474);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(218, 116);
            this.btnDetect.TabIndex = 3;
            this.btnDetect.Text = "Локализовать";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.OnButtonDetectClick);
            // 
            // tboxAlg
            // 
            this.tboxAlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxAlg.Location = new System.Drawing.Point(9, 32);
            this.tboxAlg.Name = "tboxAlg";
            this.tboxAlg.ReadOnly = true;
            this.tboxAlg.Size = new System.Drawing.Size(203, 20);
            this.tboxAlg.TabIndex = 6;
            // 
            // lblAlg
            // 
            this.lblAlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlg.AutoSize = true;
            this.lblAlg.Location = new System.Drawing.Point(6, 16);
            this.lblAlg.Name = "lblAlg";
            this.lblAlg.Size = new System.Drawing.Size(106, 13);
            this.lblAlg.TabIndex = 7;
            this.lblAlg.Text = "Текущий алгоритм:";
            // 
            // lblFolder
            // 
            this.lblFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 63);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(98, 13);
            this.lblFolder.TabIndex = 8;
            this.lblFolder.Text = "Текущий каталог:";
            // 
            // tboxFolder
            // 
            this.tboxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxFolder.Location = new System.Drawing.Point(9, 79);
            this.tboxFolder.Name = "tboxFolder";
            this.tboxFolder.ReadOnly = true;
            this.tboxFolder.Size = new System.Drawing.Size(181, 20);
            this.tboxFolder.TabIndex = 9;
            // 
            // chkBoxMarkup
            // 
            this.chkBoxMarkup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBoxMarkup.Location = new System.Drawing.Point(8, 19);
            this.chkBoxMarkup.Name = "chkBoxMarkup";
            this.chkBoxMarkup.Size = new System.Drawing.Size(171, 17);
            this.chkBoxMarkup.TabIndex = 11;
            this.chkBoxMarkup.Text = "Синхронизация с разметкой";
            this.chkBoxMarkup.UseVisualStyleBackColor = true;
            this.chkBoxMarkup.CheckedChanged += new System.EventHandler(this.OnCheckboxMarkupCheckedChanged);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.BackgroundImage = global::Platedetector.Properties.Resources.doc;
            this.btnOpenFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOpenFolder.Location = new System.Drawing.Point(190, 78);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(22, 22);
            this.btnOpenFolder.TabIndex = 10;
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.OnButtonOpenFolderClick);
            // 
            // btnMoveNext
            // 
            this.btnMoveNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveNext.BackgroundImage = global::Platedetector.Properties.Resources.right;
            this.btnMoveNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMoveNext.Location = new System.Drawing.Point(842, 418);
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.Size = new System.Drawing.Size(109, 50);
            this.btnMoveNext.TabIndex = 5;
            this.btnMoveNext.UseVisualStyleBackColor = true;
            this.btnMoveNext.Click += new System.EventHandler(this.OnButtonMoveNextClick);
            // 
            // btnMoveBack
            // 
            this.btnMoveBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveBack.BackgroundImage = global::Platedetector.Properties.Resources.left;
            this.btnMoveBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMoveBack.Location = new System.Drawing.Point(733, 418);
            this.btnMoveBack.Name = "btnMoveBack";
            this.btnMoveBack.Size = new System.Drawing.Size(109, 50);
            this.btnMoveBack.TabIndex = 4;
            this.btnMoveBack.UseVisualStyleBackColor = true;
            this.btnMoveBack.Click += new System.EventHandler(this.OnButtonMoveBackClick);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox.Location = new System.Drawing.Point(12, 27);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(715, 441);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // logView
            // 
            this.logView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logView.ItemHeight = 18;
            this.logView.Location = new System.Drawing.Point(12, 474);
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(715, 116);
            this.logView.TabIndex = 12;
            // 
            // groupBoxDir
            // 
            this.groupBoxDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDir.Controls.Add(this.lblAlg);
            this.groupBoxDir.Controls.Add(this.tboxAlg);
            this.groupBoxDir.Controls.Add(this.lblFolder);
            this.groupBoxDir.Controls.Add(this.btnOpenFolder);
            this.groupBoxDir.Controls.Add(this.tboxFolder);
            this.groupBoxDir.Location = new System.Drawing.Point(733, 27);
            this.groupBoxDir.Name = "groupBoxDir";
            this.groupBoxDir.Size = new System.Drawing.Size(218, 111);
            this.groupBoxDir.TabIndex = 13;
            this.groupBoxDir.TabStop = false;
            this.groupBoxDir.Text = "Состояние";
            // 
            // groupBoxMarkup
            // 
            this.groupBoxMarkup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMarkup.Controls.Add(this.chkBoxMarkup);
            this.groupBoxMarkup.Location = new System.Drawing.Point(734, 145);
            this.groupBoxMarkup.Name = "groupBoxMarkup";
            this.groupBoxMarkup.Size = new System.Drawing.Size(217, 45);
            this.groupBoxMarkup.TabIndex = 14;
            this.groupBoxMarkup.TabStop = false;
            this.groupBoxMarkup.Text = "Разметка";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnDetect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 599);
            this.Controls.Add(this.groupBoxMarkup);
            this.Controls.Add(this.groupBoxDir);
            this.Controls.Add(this.logView);
            this.Controls.Add(this.btnMoveNext);
            this.Controls.Add(this.btnMoveBack);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Детектор номеров";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBoxDir.ResumeLayout(false);
            this.groupBoxDir.PerformLayout();
            this.groupBoxMarkup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algToolStripMenuItem;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBox;
        private System.Windows.Forms.ToolStripMenuItem loadImgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseAlgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evalAlgToolStripMenuItem;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Button btnMoveBack;
        private System.Windows.Forms.Button btnMoveNext;
        private System.Windows.Forms.TextBox tboxAlg;
        private System.Windows.Forms.Label lblAlg;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox tboxFolder;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.CheckBox chkBoxMarkup;
        private Utils.Logging.LogView logView;
        private System.Windows.Forms.GroupBox groupBoxDir;
        private System.Windows.Forms.GroupBox groupBoxMarkup;
    }
}

