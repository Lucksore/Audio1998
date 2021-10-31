
namespace Audio
{
    partial class AudioWindow
    {
        
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bPrev = new System.Windows.Forms.Button();
            this.bPlayPause = new System.Windows.Forms.Button();
            this.bNext = new System.Windows.Forms.Button();
            this.SelectMenu = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBackPanel = new System.Windows.Forms.Panel();
            this.progressForePanel = new System.Windows.Forms.Panel();
            this.musicNameLabel = new System.Windows.Forms.Label();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.TimePositionLabel = new System.Windows.Forms.Label();
            this.flowLayoutPlaylist = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SelectMenu.SuspendLayout();
            this.progressBackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.bPrev, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bPlayPause, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bNext, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // bPrev
            // 
            this.bPrev.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.bPrev, "bPrev");
            this.bPrev.Name = "bPrev";
            this.bPrev.UseVisualStyleBackColor = false;
            // 
            // bPlayPause
            // 
            this.bPlayPause.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.bPlayPause, "bPlayPause");
            this.bPlayPause.Name = "bPlayPause";
            this.bPlayPause.UseVisualStyleBackColor = false;
            // 
            // bNext
            // 
            this.bNext.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.bNext, "bNext");
            this.bNext.Name = "bNext";
            this.bNext.UseVisualStyleBackColor = false;
            // 
            // SelectMenu
            // 
            this.SelectMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            resources.ApplyResources(this.SelectMenu, "SelectMenu");
            this.SelectMenu.Name = "SelectMenu";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenuItem,
            this.openFolderMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            resources.ApplyResources(this.openFileMenuItem, "openFileMenuItem");
            // 
            // openFolderMenuItem
            // 
            this.openFolderMenuItem.Name = "openFolderMenuItem";
            resources.ApplyResources(this.openFolderMenuItem, "openFolderMenuItem");
            // 
            // progressBackPanel
            // 
            this.progressBackPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.progressBackPanel.Controls.Add(this.progressForePanel);
            resources.ApplyResources(this.progressBackPanel, "progressBackPanel");
            this.progressBackPanel.Name = "progressBackPanel";
            // 
            // progressForePanel
            // 
            this.progressForePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.progressForePanel.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.progressForePanel, "progressForePanel");
            this.progressForePanel.Name = "progressForePanel";
            // 
            // musicNameLabel
            // 
            resources.ApplyResources(this.musicNameLabel, "musicNameLabel");
            this.musicNameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.musicNameLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.musicNameLabel.Name = "musicNameLabel";
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.volumeTrackBar, "volumeTrackBar");
            this.volumeTrackBar.Maximum = 100;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.TickFrequency = 5;
            // 
            // TimePositionLabel
            // 
            resources.ApplyResources(this.TimePositionLabel, "TimePositionLabel");
            this.TimePositionLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TimePositionLabel.Name = "TimePositionLabel";
            // 
            // flowLayoutPlaylist
            // 
            resources.ApplyResources(this.flowLayoutPlaylist, "flowLayoutPlaylist");
            this.flowLayoutPlaylist.BackColor = System.Drawing.Color.LightGray;
            this.flowLayoutPlaylist.Name = "flowLayoutPlaylist";
            // 
            // AudioWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.flowLayoutPlaylist);
            this.Controls.Add(this.TimePositionLabel);
            this.Controls.Add(this.volumeTrackBar);
            this.Controls.Add(this.musicNameLabel);
            this.Controls.Add(this.progressBackPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.SelectMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.SelectMenu;
            this.MaximizeBox = false;
            this.Name = "AudioWindow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.SelectMenu.ResumeLayout(false);
            this.SelectMenu.PerformLayout();
            this.progressBackPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MusicList_DrawSubItem(object sender, System.Windows.Forms.DrawListViewSubItemEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void MusicList_DrawItem1(object sender, System.Windows.Forms.DrawListViewItemEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bPrev;
        private System.Windows.Forms.Button bPlayPause;
        private System.Windows.Forms.Button bNext;
        private System.Windows.Forms.MenuStrip SelectMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderMenuItem;
        private System.Windows.Forms.Panel progressBackPanel;
        private System.Windows.Forms.Label musicNameLabel;
        private System.Windows.Forms.Panel progressForePanel;
        private System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.Label TimePositionLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPlaylist;
    }
}

