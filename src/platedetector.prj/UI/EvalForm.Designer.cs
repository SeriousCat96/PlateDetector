namespace Platedetector.UI
{
    partial class EvalForm
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
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tboxFolder = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.listViewMetrics = new System.Windows.Forms.ListView();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblMetrics = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.tboxFileName = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblAlg = new System.Windows.Forms.Label();
            this.tboxAlg = new System.Windows.Forms.TextBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.BackgroundImage = global::Platedetector.Properties.Resources.doc;
            this.btnOpenFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOpenFolder.Location = new System.Drawing.Point(363, 24);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(22, 22);
            this.btnOpenFolder.TabIndex = 13;
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.OnButtonOpenFolderClick);
            // 
            // tboxFolder
            // 
            this.tboxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxFolder.Location = new System.Drawing.Point(13, 25);
            this.tboxFolder.Name = "tboxFolder";
            this.tboxFolder.ReadOnly = true;
            this.tboxFolder.Size = new System.Drawing.Size(350, 20);
            this.tboxFolder.TabIndex = 12;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(10, 9);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(98, 13);
            this.lblFolder.TabIndex = 11;
            this.lblFolder.Text = "Текущий каталог:";
            // 
            // listViewMetrics
            // 
            this.listViewMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMetrics.Location = new System.Drawing.Point(13, 64);
            this.listViewMetrics.Name = "listViewMetrics";
            this.listViewMetrics.Size = new System.Drawing.Size(372, 83);
            this.listViewMetrics.TabIndex = 14;
            this.listViewMetrics.UseCompatibleStateImageBehavior = false;
            this.listViewMetrics.View = System.Windows.Forms.View.List;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(312, 295);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 16;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.OnStopButtonClick);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(231, 295);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.OnButtonStartClickAsync);
            // 
            // lblMetrics
            // 
            this.lblMetrics.AutoSize = true;
            this.lblMetrics.Location = new System.Drawing.Point(10, 48);
            this.lblMetrics.Name = "lblMetrics";
            this.lblMetrics.Size = new System.Drawing.Size(54, 13);
            this.lblMetrics.TabIndex = 17;
            this.lblMetrics.Text = "Метрики:";
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.lblFileName);
            this.groupBox.Controls.Add(this.tboxFileName);
            this.groupBox.Controls.Add(this.progressBar);
            this.groupBox.Location = new System.Drawing.Point(15, 192);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(372, 97);
            this.groupBox.TabIndex = 18;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Прогресс";
            this.groupBox.Visible = false;
            // 
            // lblFileName
            // 
            this.lblFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(3, 50);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(130, 13);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "Обрабатываемый файл:";
            // 
            // tboxFileName
            // 
            this.tboxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxFileName.Location = new System.Drawing.Point(6, 66);
            this.tboxFileName.Name = "tboxFileName";
            this.tboxFileName.ReadOnly = true;
            this.tboxFileName.Size = new System.Drawing.Size(360, 20);
            this.tboxFileName.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 24);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 23);
            this.progressBar.TabIndex = 0;
            // 
            // lblAlg
            // 
            this.lblAlg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlg.AutoSize = true;
            this.lblAlg.Location = new System.Drawing.Point(12, 150);
            this.lblAlg.Name = "lblAlg";
            this.lblAlg.Size = new System.Drawing.Size(59, 13);
            this.lblAlg.TabIndex = 20;
            this.lblAlg.Text = "Алгоритм:";
            // 
            // tboxAlg
            // 
            this.tboxAlg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxAlg.Location = new System.Drawing.Point(15, 166);
            this.tboxAlg.Name = "tboxAlg";
            this.tboxAlg.ReadOnly = true;
            this.tboxAlg.Size = new System.Drawing.Size(372, 20);
            this.tboxAlg.TabIndex = 19;
            // 
            // EvalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 327);
            this.Controls.Add(this.lblAlg);
            this.Controls.Add(this.tboxAlg);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.lblMetrics);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.listViewMetrics);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.tboxFolder);
            this.Controls.Add(this.lblFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EvalForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Оценить алгоритм";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TextBox tboxFolder;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.ListView listViewMetrics;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMetrics;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox tboxFileName;
        private System.Windows.Forms.Label lblAlg;
        private System.Windows.Forms.TextBox tboxAlg;
    }
}