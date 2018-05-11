namespace PlateDetector.UI
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Recall");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Precision");
			this.btnOpenFolder = new System.Windows.Forms.Button();
			this.tboxFolder = new System.Windows.Forms.TextBox();
			this.lblFolder = new System.Windows.Forms.Label();
			this.listViewMetrics = new System.Windows.Forms.ListView();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.btn_OK = new System.Windows.Forms.Button();
			this.lblMetrics = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOpenFolder
			// 
			this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFolder.BackgroundImage = global::PlateDetector.Properties.Resources.folder;
			this.btnOpenFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnOpenFolder.Location = new System.Drawing.Point(318, 24);
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new System.Drawing.Size(22, 22);
			this.btnOpenFolder.TabIndex = 13;
			this.btnOpenFolder.UseVisualStyleBackColor = true;
			// 
			// tboxFolder
			// 
			this.tboxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFolder.Location = new System.Drawing.Point(13, 25);
			this.tboxFolder.Name = "tboxFolder";
			this.tboxFolder.ReadOnly = true;
			this.tboxFolder.Size = new System.Drawing.Size(305, 20);
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
			this.listViewMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewMetrics.CheckBoxes = true;
			listViewItem1.Checked = true;
			listViewItem1.StateImageIndex = 1;
			listViewItem2.Checked = true;
			listViewItem2.StateImageIndex = 1;
			this.listViewMetrics.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listViewMetrics.Location = new System.Drawing.Point(13, 64);
			this.listViewMetrics.Name = "listViewMetrics";
			this.listViewMetrics.Size = new System.Drawing.Size(327, 156);
			this.listViewMetrics.TabIndex = 14;
			this.listViewMetrics.UseCompatibleStateImageBehavior = false;
			this.listViewMetrics.View = System.Windows.Forms.View.List;
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(265, 226);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 16;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			// 
			// btn_OK
			// 
			this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.Location = new System.Drawing.Point(184, 226);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 15;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			// 
			// lblMetrics
			// 
			this.lblMetrics.AutoSize = true;
			this.lblMetrics.Location = new System.Drawing.Point(12, 48);
			this.lblMetrics.Name = "lblMetrics";
			this.lblMetrics.Size = new System.Drawing.Size(54, 13);
			this.lblMetrics.TabIndex = 17;
			this.lblMetrics.Text = "Метрики:";
			// 
			// EvalForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 261);
			this.Controls.Add(this.lblMetrics);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_OK);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOpenFolder;
		private System.Windows.Forms.TextBox tboxFolder;
		private System.Windows.Forms.Label lblFolder;
		private System.Windows.Forms.ListView listViewMetrics;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Label lblMetrics;
	}
}