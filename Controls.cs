using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using WMPLib;

namespace AudioControl
{
    public class FileControl
    {
        readonly WindowsMediaPlayer player;

        public FileControl(WindowsMediaPlayer player)
        {
            this.player = player;
        }

        public void OpenFileWithString(string file)
        {
            CreatePlayList(
                Directory.GetFiles(file.Substring(0, file.LastIndexOf('\\')))
                .Where(fileName => CheckFormat(fileName))
                .ToArray<string>(), file
                );

        }
        public void OpenFile(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
                if (fileDialog.ShowDialog() == DialogResult.OK) {
                    CreatePlayList(
                        Directory.GetFiles(fileDialog.FileName.Substring(0, fileDialog.FileName.LastIndexOf('\\') + 1))
                        .Where(fileName => CheckFormat(fileName))
                        .ToArray<string>(), fileDialog.FileName
                        );
                }
        }
        public void OpenFolder(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog()) {
                folderDialog.ShowNewFolderButton = false;
                if (folderDialog.ShowDialog() == DialogResult.OK) {
                    CreatePlayList(
                        Directory.GetFiles(folderDialog.SelectedPath)
                        .Where(fileName => CheckFormat(fileName))
                        .ToArray<string>(), string.Empty
                        );
                }
            }
        }
        private void CreatePlayList(string[] fileNames, string currentMediaName)
        {
            player.currentPlaylist.clear();
            IWMPPlaylist playlist = player.currentPlaylist;
            int currentIndex = 0;
            for (int i = 0; i < fileNames.Length; i++) {
                player.URL = fileNames[i];
                playlist.appendItem(player.currentMedia);
                if (fileNames[i] == currentMediaName) currentIndex = i;
            }
            player.currentPlaylist = playlist;
            player.controls.playItem(player.currentPlaylist.Item[currentIndex]);
            if (currentMediaName == string.Empty) player.controls.pause();
        }
        private bool CheckFormat(string fileName)
        {
            string[] formats = { ".mp3", ".mp4", ".m4a", "wav" };
            foreach (string s in formats) {
                if (fileName.EndsWith(s)) return true;
            }
            return false;
        }
    }

    public class PlayControl
    {
        readonly WindowsMediaPlayer Player;
        readonly Panel ProgressForePanel;
        readonly int MaxProgressWidth;
        readonly TrackBar VolumeTrackBar;

        public PlayControl(WindowsMediaPlayer player, Panel fore, int maxProgressWidth, TrackBar volumeTrackBar)
        {
            Player = player;
            MaxProgressWidth = maxProgressWidth; 
            ProgressForePanel = fore;
            VolumeTrackBar = volumeTrackBar;
        }

        public void PlayNext(object sender, EventArgs e) => Player.controls.next();
        public void PlayPrev(object sender, EventArgs e) => Player.controls.previous();
        public void PlayOrPause(object sender, EventArgs e)
        {
            if (Player.playState == WMPPlayState.wmppsPlaying) {
                Player.controls.pause();
            }
            else if (
                Player.playState == WMPPlayState.wmppsPaused ||
                Player.playState == WMPPlayState.wmppsReady ||
                Player.playState == WMPPlayState.wmppsStopped) Player.controls.play();
        }
        public void ChangeVolume(object sender, EventArgs e)
        {
            Player.settings.volume = VolumeTrackBar.Value;
        }
        public void ChangePositionByClick(object sender, MouseEventArgs e)
        {
            if (Player.playState != WMPPlayState.wmppsUndefined) {
                Player.controls.currentPosition = Player.currentMedia.duration * e.X / MaxProgressWidth;
                ProgressForePanel.Width = e.X;
            }
        }
    }

    public class StateControl
    {
        readonly WindowsMediaPlayer Player;
        readonly Label MusicNameLabel;
        readonly Label TimePositionLabel;
        readonly Panel ProgressBackPanel;
        readonly Panel ProgressForePanel;
        readonly Button PlayOrPauseBtn;
        readonly Timer timer;
        public int sec = 0;

        public StateControl
            (WindowsMediaPlayer player, Label musicNameLabel, Label timePositionLabel, Panel back, Panel fore, Button playOrPausBtm)
        {
            Player = player;
            MusicNameLabel = musicNameLabel;
            TimePositionLabel = timePositionLabel;
            ProgressBackPanel = back;
            ProgressForePanel = fore;
            PlayOrPauseBtn = playOrPausBtm;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += EachSecondTick;
        }
        public void MediaChange(object item)
        {
            if (Player.playState == WMPPlayState.wmppsUndefined) 
                MusicNameLabel.Text = "";
            else 
                MusicNameLabel.Text = GetFitName(38);
            ProgressForePanel.Width = 0;
        }
        public void PlayStateChange(int newState)
        {
            if (newState == 3) {
                PlayOrPauseBtn.Text = "Pause";
                timer.Start();
            }
            if (newState == 2 || newState == 1) {
                PlayOrPauseBtn.Text = "Play";
                timer.Stop();
            }
        }
        private void EachSecondTick(object sender, EventArgs e)
        {
            ProgressForePanel.Width = (int)(Player.controls.currentPosition / Player.currentMedia.duration * ProgressBackPanel.Width);
            TimePositionLabel.Text = GetStrTimePostion();
            sec++;
        }
        private string GetFitName(int len)
        {
            if (Player.currentMedia.name.Length < len) return Player.currentMedia.name;
            else return Player.currentMedia.name.Substring(0, len - 3) + "...";
        }
        private string GetStrTimePostion()
        {
            int minTotal = (int)(Player.currentMedia.duration / 60);
            int secTotal = (int)(Player.currentMedia.duration - minTotal * 60);

            int minCurrent = (int)(Player.controls.currentPosition / 60);
            int secCurrent = (int)(Player.controls.currentPosition - minCurrent * 60);

            return
                    $"{TwoCharTime(minCurrent)}:{TwoCharTime(secCurrent)} " +
                    $"/{TwoCharTime(minTotal)}:{TwoCharTime(secTotal)}";
        }
        private string TwoCharTime(int value)
        {
            string str = value.ToString();
            if (str.Length == 1) return '0' + str;
            return str;
        }
    }

    public class VisualPlaylistControl
    {
        readonly WindowsMediaPlayer Player;
        readonly FlowLayoutPanel PlaylistPanel;

        public static Color HoverColor = Color.FromArgb(192, 192, 192);
        public static Color BackColor = Color.LightGray;
        public static Color CurrentColor = Color.Gray;
        private int currentIndex = 0;

        public VisualPlaylistControl(WindowsMediaPlayer player,FlowLayoutPanel playlistPanel)
        {
            Player = player;
            PlaylistPanel = playlistPanel;
        }

        public void MediaChange(object item)
        {
            Label[] labels = PlaylistPanel.Controls.OfType<Label>().ToArray();
            labels[currentIndex].BackColor = BackColor;
            for (int i = 0; i < labels.Length; i++) 
                if (Player.controls.currentItem.name == labels[i].Text) {
                    labels[i].BackColor = CurrentColor;
                    currentIndex = i;
                }
        }
        public async void CurrentPlaylistChange(WMPPlaylistChangeEventType change)
        {
            currentIndex = 0;
            PlaylistPanel.Controls.Clear();
            foreach (var item in await LoadLabelsAsync())
                PlaylistPanel.Controls.Add(item);
        }
        
        private async Task<Label[]> LoadLabelsAsync()
        {
            return await Task.Run(() => {
                Label[] labels = new Label[Player.currentPlaylist.count];
                for (int i = 0; i < labels.Length; i++) {
                    labels[i] = new Label() {
                        Width = PlaylistPanel.ClientSize.Width,
                        Text = Player.currentPlaylist.Item[i].name,
                        BackColor = BackColor,
                        Margin = new Padding(0),
                        Padding = new Padding(5, 5, 3 , 5)
                    };
                    labels[i].Click += MusicLabelClick;
                    labels[i].MouseHover += LabelHover;
                    labels[i].MouseLeave += LabelLeave;
                }
                return labels;
            });
        }
        private void LabelLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text != Player.controls.currentItem.name) label.BackColor = BackColor;
        }
        private void LabelHover(object sender, EventArgs e) {
            Label label = (Label)sender;
            if (label.Text != Player.controls.currentItem.name) label.BackColor = HoverColor; 
        }
        private  void MusicLabelClick(object sender, EventArgs e) =>
            Player.controls.playItem(
                Player.currentPlaylist.Item[PlaylistPanel.Controls.GetChildIndex((Label)sender)]
                );
    }

    public class SaveControl
    {
        readonly private string Path;
        readonly private WindowsMediaPlayer Player;
        readonly private XmlDocument Document = new XmlDocument();
        readonly private XmlNodeList SettingsNodes;
        readonly private StateControl stateControl;

        public SaveControl(WindowsMediaPlayer player, StateControl stateControl)
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Audio1998\";
            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            Path += @"settings.xml";

            Player = player;
            if (File.Exists(Path)) {
                try {
                    Document.Load(Path);
                }
                catch (XmlException e) {
                    MessageBox.Show(e.Message);
                    CreateNewXml();
                }
            }
            else CreateNewXml();
            SettingsNodes = Document.ChildNodes[0].ChildNodes;
            this.stateControl = stateControl;
        }

        public void SaveChanges(object sender, FormClosedEventArgs e)
        {
            Volume = Player.settings.volume;
            if (Player.currentMedia != null)
                LastOpenedFolder = Player.currentMedia.sourceURL
                    .Substring(0, Player.currentMedia.sourceURL
                    .LastIndexOf('\\'));
            TotalSec = TotalSec + stateControl.sec;
            Document.Save(Path);
        }
        private void CreateNewXml()
        {
            Document.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = Document.CreateElement("Config");
            root.AppendChild(Document.CreateElement("Volume"));
            root.AppendChild(Document.CreateElement("TotalSeconds"));
            root.AppendChild(Document.CreateElement("LastOpenedFolder"));
            root.ChildNodes[0].InnerText = "50";
            root.ChildNodes[1].InnerText = "0";
            root.ChildNodes[2].InnerText = "";
            Document.AppendChild(root);
            Document.Save(Path);
        }

        public int Volume {
            get { return int.Parse(SettingsNodes[0].InnerText); }
            set { SettingsNodes[0].InnerText = value.ToString(); }
        }
        public int TotalSec {
            get { return int.Parse(SettingsNodes[1].InnerText); }
            set { SettingsNodes[1].InnerText = value.ToString(); }
        }
        public string LastOpenedFolder {
            get { return SettingsNodes[2].InnerText; }
            set { SettingsNodes[2].InnerText = value; }
        }
    }
}
