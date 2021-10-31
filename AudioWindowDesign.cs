using System.Windows.Forms;
using WMPLib;

namespace Audio
{
    public partial class AudioWindow : Form
    {
        readonly WindowsMediaPlayer Player;
 
        AudioControl.FileControl fileControl;
        AudioControl.PlayControl playControl;
        AudioControl.StateControl stateControl;
        AudioControl.VisualPlaylistControl visualPlaylistControl;
        AudioControl.SaveControl saveControl;

        public AudioWindow(string[] args)
        {
            InitializeComponent();

            Player = new WindowsMediaPlayer();
            Player.settings.autoStart = false;
            Player.settings.setMode("loop", true);

            ConfigFileControl();
            ConfigPlayControl();
            ConfigStateControl();
            ConfigVisualPlaylistControl();
            ConfigSaveControl();

            volumeTrackBar.Value = Player.settings.volume;
            if (args.Length != 0) fileControl.OpenFileWithString(args[0]);
        }
        
        private void ConfigFileControl()
        {
            fileControl = new AudioControl.FileControl(Player);
            openFileMenuItem.Click += fileControl.OpenFile;
            openFolderMenuItem.Click += fileControl.OpenFolder;
        }
        private void ConfigPlayControl()
        {
            playControl = new AudioControl.PlayControl(Player, progressForePanel, progressBackPanel.Width, volumeTrackBar);
            bNext.Click += playControl.PlayNext;
            bPrev.Click += playControl.PlayPrev;
            bPlayPause.Click += playControl.PlayOrPause;
            progressForePanel.MouseClick += playControl.ChangePositionByClick;
            progressBackPanel.MouseClick += playControl.ChangePositionByClick;
            volumeTrackBar.Scroll += playControl.ChangeVolume;
        }
        private void ConfigStateControl()
        {
            stateControl = new AudioControl.StateControl
                (Player, musicNameLabel, TimePositionLabel, progressBackPanel, progressForePanel, bPlayPause);
            Player.MediaChange += stateControl.MediaChange;
            Player.PlayStateChange += stateControl.PlayStateChange;
        }
        private void ConfigVisualPlaylistControl()
        {
            visualPlaylistControl = new AudioControl.VisualPlaylistControl(Player, flowLayoutPlaylist);
            Player.MediaChange += visualPlaylistControl.MediaChange;
            Player.CurrentPlaylistChange += visualPlaylistControl.CurrentPlaylistChange;  
        }
        private void ConfigSaveControl()
        {
            saveControl = new AudioControl.SaveControl(Player, stateControl);
            Player.settings.volume = saveControl.Volume;
            this.FormClosed += saveControl.SaveChanges;
        }
    }

    


    
}
