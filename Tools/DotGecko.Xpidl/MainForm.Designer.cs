namespace DotGecko.Xpidl
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label lblInputFolder;
			System.Windows.Forms.Label lblOutputFolder;
			System.Windows.Forms.Label lblInputFiles;
			System.Windows.Forms.Label lblSelect;
			System.Windows.Forms.StatusStrip statusStrip;
			this.prbProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.lblCurrentFile = new System.Windows.Forms.ToolStripStatusLabel();
			this.txtInputFolder = new System.Windows.Forms.TextBox();
			this.btnInputFolder = new System.Windows.Forms.Button();
			this.txtOutputFolder = new System.Windows.Forms.TextBox();
			this.btnOutputFolder = new System.Windows.Forms.Button();
			this.clbInputFiles = new System.Windows.Forms.CheckedListBox();
			this.lnkSelectAll = new System.Windows.Forms.LinkLabel();
			this.lnkSelectNone = new System.Windows.Forms.LinkLabel();
			this.lnkSelectInvert = new System.Windows.Forms.LinkLabel();
			this.btnConvert = new System.Windows.Forms.Button();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			lblInputFolder = new System.Windows.Forms.Label();
			lblOutputFolder = new System.Windows.Forms.Label();
			lblInputFiles = new System.Windows.Forms.Label();
			lblSelect = new System.Windows.Forms.Label();
			statusStrip = new System.Windows.Forms.StatusStrip();
			statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblInputFolder
			// 
			lblInputFolder.AutoSize = true;
			lblInputFolder.Location = new System.Drawing.Point(12, 9);
			lblInputFolder.Name = "lblInputFolder";
			lblInputFolder.Size = new System.Drawing.Size(94, 13);
			lblInputFolder.TabIndex = 0;
			lblInputFolder.Text = "Gecko IDL Folder:";
			// 
			// lblOutputFolder
			// 
			lblOutputFolder.AutoSize = true;
			lblOutputFolder.Location = new System.Drawing.Point(12, 48);
			lblOutputFolder.Name = "lblOutputFolder";
			lblOutputFolder.Size = new System.Drawing.Size(74, 13);
			lblOutputFolder.TabIndex = 3;
			lblOutputFolder.Text = "Output Folder:";
			// 
			// lblInputFiles
			// 
			lblInputFiles.AutoSize = true;
			lblInputFiles.Location = new System.Drawing.Point(12, 87);
			lblInputFiles.Name = "lblInputFiles";
			lblInputFiles.Size = new System.Drawing.Size(58, 13);
			lblInputFiles.TabIndex = 6;
			lblInputFiles.Text = "Input Files:";
			// 
			// lblSelect
			// 
			lblSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			lblSelect.AutoSize = true;
			lblSelect.Location = new System.Drawing.Point(12, 330);
			lblSelect.Name = "lblSelect";
			lblSelect.Size = new System.Drawing.Size(40, 13);
			lblSelect.TabIndex = 8;
			lblSelect.Text = "Select:";
			// 
			// statusStrip
			// 
			statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prbProgress,
            this.lblCurrentFile});
			statusStrip.Location = new System.Drawing.Point(0, 359);
			statusStrip.Name = "statusStrip";
			statusStrip.Size = new System.Drawing.Size(562, 22);
			statusStrip.TabIndex = 13;
			// 
			// prbProgress
			// 
			this.prbProgress.Name = "prbProgress";
			this.prbProgress.Size = new System.Drawing.Size(100, 16);
			// 
			// lblCurrentFile
			// 
			this.lblCurrentFile.Name = "lblCurrentFile";
			this.lblCurrentFile.Size = new System.Drawing.Size(445, 17);
			this.lblCurrentFile.Spring = true;
			this.lblCurrentFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtInputFolder
			// 
			this.txtInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInputFolder.Location = new System.Drawing.Point(12, 25);
			this.txtInputFolder.Name = "txtInputFolder";
			this.txtInputFolder.ReadOnly = true;
			this.txtInputFolder.Size = new System.Drawing.Size(497, 20);
			this.txtInputFolder.TabIndex = 1;
			// 
			// btnInputFolder
			// 
			this.btnInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInputFolder.Location = new System.Drawing.Point(515, 23);
			this.btnInputFolder.Name = "btnInputFolder";
			this.btnInputFolder.Size = new System.Drawing.Size(35, 23);
			this.btnInputFolder.TabIndex = 2;
			this.btnInputFolder.Text = "...";
			this.btnInputFolder.UseVisualStyleBackColor = true;
			this.btnInputFolder.Click += new System.EventHandler(this.btnInputFolder_Click);
			// 
			// txtOutputFolder
			// 
			this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputFolder.Location = new System.Drawing.Point(12, 64);
			this.txtOutputFolder.Name = "txtOutputFolder";
			this.txtOutputFolder.ReadOnly = true;
			this.txtOutputFolder.Size = new System.Drawing.Size(497, 20);
			this.txtOutputFolder.TabIndex = 4;
			// 
			// btnOutputFolder
			// 
			this.btnOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOutputFolder.Location = new System.Drawing.Point(515, 62);
			this.btnOutputFolder.Name = "btnOutputFolder";
			this.btnOutputFolder.Size = new System.Drawing.Size(35, 23);
			this.btnOutputFolder.TabIndex = 5;
			this.btnOutputFolder.Text = "...";
			this.btnOutputFolder.UseVisualStyleBackColor = true;
			this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
			// 
			// clbInputFiles
			// 
			this.clbInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.clbInputFiles.CheckOnClick = true;
			this.clbInputFiles.FormattingEnabled = true;
			this.clbInputFiles.HorizontalScrollbar = true;
			this.clbInputFiles.IntegralHeight = false;
			this.clbInputFiles.Location = new System.Drawing.Point(12, 103);
			this.clbInputFiles.Name = "clbInputFiles";
			this.clbInputFiles.Size = new System.Drawing.Size(538, 221);
			this.clbInputFiles.TabIndex = 7;
			// 
			// lnkSelectAll
			// 
			this.lnkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkSelectAll.AutoSize = true;
			this.lnkSelectAll.Location = new System.Drawing.Point(58, 330);
			this.lnkSelectAll.Name = "lnkSelectAll";
			this.lnkSelectAll.Size = new System.Drawing.Size(17, 13);
			this.lnkSelectAll.TabIndex = 9;
			this.lnkSelectAll.TabStop = true;
			this.lnkSelectAll.Text = "all";
			this.lnkSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectAll_LinkClicked);
			// 
			// lnkSelectNone
			// 
			this.lnkSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkSelectNone.AutoSize = true;
			this.lnkSelectNone.Location = new System.Drawing.Point(81, 330);
			this.lnkSelectNone.Name = "lnkSelectNone";
			this.lnkSelectNone.Size = new System.Drawing.Size(31, 13);
			this.lnkSelectNone.TabIndex = 10;
			this.lnkSelectNone.TabStop = true;
			this.lnkSelectNone.Text = "none";
			this.lnkSelectNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectNone_LinkClicked);
			// 
			// lnkSelectInvert
			// 
			this.lnkSelectInvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkSelectInvert.AutoSize = true;
			this.lnkSelectInvert.Location = new System.Drawing.Point(118, 330);
			this.lnkSelectInvert.Name = "lnkSelectInvert";
			this.lnkSelectInvert.Size = new System.Drawing.Size(33, 13);
			this.lnkSelectInvert.TabIndex = 11;
			this.lnkSelectInvert.TabStop = true;
			this.lnkSelectInvert.Text = "invert";
			this.lnkSelectInvert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectInvert_LinkClicked);
			// 
			// btnConvert
			// 
			this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConvert.Location = new System.Drawing.Point(475, 330);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(75, 23);
			this.btnConvert.TabIndex = 12;
			this.btnConvert.Text = "Convert";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			// 
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnConvert;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 381);
			this.Controls.Add(this.btnConvert);
			this.Controls.Add(statusStrip);
			this.Controls.Add(this.lnkSelectInvert);
			this.Controls.Add(this.lnkSelectNone);
			this.Controls.Add(this.lnkSelectAll);
			this.Controls.Add(lblSelect);
			this.Controls.Add(this.clbInputFiles);
			this.Controls.Add(lblInputFiles);
			this.Controls.Add(this.btnOutputFolder);
			this.Controls.Add(this.txtOutputFolder);
			this.Controls.Add(lblOutputFolder);
			this.Controls.Add(this.btnInputFolder);
			this.Controls.Add(this.txtInputFolder);
			this.Controls.Add(lblInputFolder);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "XPIDL Converter";
			statusStrip.ResumeLayout(false);
			statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtInputFolder;
		private System.Windows.Forms.TextBox txtOutputFolder;
		private System.Windows.Forms.CheckedListBox clbInputFiles;
		private System.Windows.Forms.ToolStripStatusLabel lblCurrentFile;
		private System.Windows.Forms.ToolStripProgressBar prbProgress;
		private System.Windows.Forms.Button btnConvert;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.Button btnInputFolder;
		private System.Windows.Forms.Button btnOutputFolder;
		private System.Windows.Forms.LinkLabel lnkSelectAll;
		private System.Windows.Forms.LinkLabel lnkSelectNone;
		private System.Windows.Forms.LinkLabel lnkSelectInvert;
		private System.Windows.Forms.ToolTip toolTip;
	}
}

