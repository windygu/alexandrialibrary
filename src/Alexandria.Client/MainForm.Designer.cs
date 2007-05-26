namespace Alexandria.Client
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
				// Dispose other objects here
							
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.PlayPauseButton = new System.Windows.Forms.Button();
			this.StopButton = new System.Windows.Forms.Button();
			this.PlaybackTrackBar = new System.Windows.Forms.TrackBar();
			this.QueueGroupBox = new System.Windows.Forms.GroupBox();
			this.QueueListView = new System.Windows.Forms.ListView();
			this.VolumeTrackBar = new System.Windows.Forms.TrackBar();
			this.MuteButton = new System.Windows.Forms.Button();
			this.VolumeLabel = new System.Windows.Forms.Label();
			this.FileMenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.PlaybackStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.PositionStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.StreamingStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.NextButton = new System.Windows.Forms.Button();
			this.PreviousButton = new System.Windows.Forms.Button();
			this.SaveButton = new System.Windows.Forms.Button();
			this.PlaybackTimer = new System.Windows.Forms.Timer(this.components);
			this.LoadStatus = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.PlaybackTrackBar)).BeginInit();
			this.QueueGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).BeginInit();
			this.FileMenuStrip.SuspendLayout();
			this.StatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// PlayPauseButton
			// 
			resources.ApplyResources(this.PlayPauseButton, "PlayPauseButton");
			this.PlayPauseButton.Name = "PlayPauseButton";
			this.PlayPauseButton.UseVisualStyleBackColor = true;
			// 
			// StopButton
			// 
			resources.ApplyResources(this.StopButton, "StopButton");
			this.StopButton.Name = "StopButton";
			this.StopButton.UseVisualStyleBackColor = true;
			// 
			// PlaybackTrackBar
			// 
			resources.ApplyResources(this.PlaybackTrackBar, "PlaybackTrackBar");
			this.PlaybackTrackBar.Name = "PlaybackTrackBar";
			this.PlaybackTrackBar.TickFrequency = 10000;
			this.PlaybackTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
			// 
			// QueueGroupBox
			// 
			this.QueueGroupBox.Controls.Add(this.QueueListView);
			resources.ApplyResources(this.QueueGroupBox, "QueueGroupBox");
			this.QueueGroupBox.Name = "QueueGroupBox";
			this.QueueGroupBox.TabStop = false;
			// 
			// QueueListView
			// 
			this.QueueListView.AllowColumnReorder = true;
			this.QueueListView.FullRowSelect = true;
			this.QueueListView.HideSelection = false;
			resources.ApplyResources(this.QueueListView, "QueueListView");
			this.QueueListView.Name = "QueueListView";
			this.QueueListView.UseCompatibleStateImageBehavior = false;
			this.QueueListView.View = System.Windows.Forms.View.Details;
			// 
			// VolumeTrackBar
			// 
			resources.ApplyResources(this.VolumeTrackBar, "VolumeTrackBar");
			this.VolumeTrackBar.Name = "VolumeTrackBar";
			this.VolumeTrackBar.Value = 10;
			// 
			// MuteButton
			// 
			resources.ApplyResources(this.MuteButton, "MuteButton");
			this.MuteButton.Name = "MuteButton";
			this.MuteButton.UseVisualStyleBackColor = true;
			// 
			// VolumeLabel
			// 
			resources.ApplyResources(this.VolumeLabel, "VolumeLabel");
			this.VolumeLabel.Name = "VolumeLabel";
			// 
			// FileMenuStrip
			// 
			this.FileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
			resources.ApplyResources(this.FileMenuStrip, "FileMenuStrip");
			this.FileMenuStrip.Name = "FileMenuStrip";
			// 
			// FileToolStripMenuItem
			// 
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.ExitToolStripMenuItem});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			resources.ApplyResources(this.FileToolStripMenuItem, "FileToolStripMenuItem");
			// 
			// OpenToolStripMenuItem
			// 
			this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
			resources.ApplyResources(this.OpenToolStripMenuItem, "OpenToolStripMenuItem");
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			resources.ApplyResources(this.ExitToolStripMenuItem, "ExitToolStripMenuItem");
			// 
			// StatusStrip
			// 
			this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadStatus,
            this.PlaybackStatusLabel,
            this.PositionStatus,
            this.StreamingStatus});
			resources.ApplyResources(this.StatusStrip, "StatusStrip");
			this.StatusStrip.Name = "StatusStrip";
			// 
			// PlaybackStatusLabel
			// 
			this.PlaybackStatusLabel.Name = "PlaybackStatusLabel";
			resources.ApplyResources(this.PlaybackStatusLabel, "PlaybackStatusLabel");
			// 
			// PositionStatus
			// 
			this.PositionStatus.Name = "PositionStatus";
			resources.ApplyResources(this.PositionStatus, "PositionStatus");
			// 
			// StreamingStatus
			// 
			this.StreamingStatus.Name = "StreamingStatus";
			resources.ApplyResources(this.StreamingStatus, "StreamingStatus");
			// 
			// FileOpenDialog
			// 
			this.FileOpenDialog.FileName = "openFileDialog1";
			// 
			// NextButton
			// 
			resources.ApplyResources(this.NextButton, "NextButton");
			this.NextButton.Name = "NextButton";
			this.NextButton.UseVisualStyleBackColor = true;
			// 
			// PreviousButton
			// 
			resources.ApplyResources(this.PreviousButton, "PreviousButton");
			this.PreviousButton.Name = "PreviousButton";
			this.PreviousButton.UseVisualStyleBackColor = true;
			// 
			// SaveButton
			// 
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// PlaybackTimer
			// 
			this.PlaybackTimer.Enabled = true;
			this.PlaybackTimer.Interval = 1000;
			// 
			// LoadStatus
			// 
			this.LoadStatus.Name = "LoadStatus";
			resources.ApplyResources(this.LoadStatus, "LoadStatus");
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.PreviousButton);
			this.Controls.Add(this.NextButton);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.VolumeLabel);
			this.Controls.Add(this.MuteButton);
			this.Controls.Add(this.VolumeTrackBar);
			this.Controls.Add(this.QueueGroupBox);
			this.Controls.Add(this.PlaybackTrackBar);
			this.Controls.Add(this.StopButton);
			this.Controls.Add(this.PlayPauseButton);
			this.Controls.Add(this.FileMenuStrip);
			this.MainMenuStrip = this.FileMenuStrip;
			this.Name = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.PlaybackTrackBar)).EndInit();
			this.QueueGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).EndInit();
			this.FileMenuStrip.ResumeLayout(false);
			this.FileMenuStrip.PerformLayout();
			this.StatusStrip.ResumeLayout(false);
			this.StatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PlayPauseButton;
		private System.Windows.Forms.Button StopButton;
		private System.Windows.Forms.TrackBar PlaybackTrackBar;
		private System.Windows.Forms.GroupBox QueueGroupBox;
		private System.Windows.Forms.ListView QueueListView;
		private System.Windows.Forms.TrackBar VolumeTrackBar;
		private System.Windows.Forms.Button MuteButton;
		private System.Windows.Forms.Label VolumeLabel;
		private System.Windows.Forms.MenuStrip FileMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel PlaybackStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog FileOpenDialog;
		private System.Windows.Forms.ToolStripStatusLabel PositionStatus;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button PreviousButton;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Timer PlaybackTimer;
		private System.Windows.Forms.ToolStripStatusLabel StreamingStatus;
		private System.Windows.Forms.ToolStripStatusLabel LoadStatus;
	}
}

