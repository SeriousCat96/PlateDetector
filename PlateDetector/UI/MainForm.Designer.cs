using OpenCvSharp.UserInterface;

namespace PlateDetector.UI
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
			if(disposing && (components != null))
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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadImgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.algToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.chooseAlgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.evalAlgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pictureBox = new OpenCvSharp.UserInterface.PictureBoxIpl();
			this.lboxLog = new System.Windows.Forms.ListBox();
			this.btnDetect = new System.Windows.Forms.Button();
			this.menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.algToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(887, 24);
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
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pictureBox.Location = new System.Drawing.Point(12, 27);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(736, 441);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			// 
			// lboxLog
			// 
			this.lboxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lboxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lboxLog.FormattingEnabled = true;
			this.lboxLog.ItemHeight = 16;
			this.lboxLog.Location = new System.Drawing.Point(12, 474);
			this.lboxLog.Name = "lboxLog";
			this.lboxLog.Size = new System.Drawing.Size(737, 116);
			this.lboxLog.TabIndex = 2;
			// 
			// btnDetect
			// 
			this.btnDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDetect.Location = new System.Drawing.Point(755, 474);
			this.btnDetect.Name = "btnDetect";
			this.btnDetect.Size = new System.Drawing.Size(120, 116);
			this.btnDetect.TabIndex = 3;
			this.btnDetect.Text = "Локализовать";
			this.btnDetect.UseVisualStyleBackColor = true;
			this.btnDetect.Click += new System.EventHandler(this.OnButtonDetectClick);
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnDetect;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(887, 599);
			this.Controls.Add(this.btnDetect);
			this.Controls.Add(this.lboxLog);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Детектор номеров";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
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
		private System.Windows.Forms.ListBox lboxLog;
		private System.Windows.Forms.Button btnDetect;
	}
}

