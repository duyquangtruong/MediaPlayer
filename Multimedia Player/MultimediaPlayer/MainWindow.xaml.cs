using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MultimediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class Song
        {
            string songName;
            string songDir;

            public string SongName { get; set; }
            public string SongDir { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            songList = new ObservableCollection<Song>();
        }

        ObservableCollection<Song> songList;
        TimeSpan position;

        private void btnPlayList_Click(object sender, RoutedEventArgs e)
        {
            if (btnPlayList.Tag.ToString() == "0")
            {
                var img = new BitmapImage(
                new Uri("Images/playlist.png",
                UriKind.Relative));
                imgPlaylist.Source = img;
                btnPlayList.Tag = "1";
                GridLength g2 = new GridLength(4, GridUnitType.Star);
                PlayList.Width = g2;
            }
            else
            {
                var img = new BitmapImage(
                new Uri("Images/playlistoff.png",
                UriKind.Relative));
                imgPlaylist.Source = img;
                btnPlayList.Tag = "0";
                GridLength g2 = new GridLength(0, GridUnitType.Star);
                PlayList.Width = g2;
            }            
        }

        private void btnRepeat_Click(object sender, RoutedEventArgs e)
        {
            if (btnRepeat.Tag.ToString() == "0")
            {
                var img = new BitmapImage(
                new Uri("Images/repeat.png", UriKind.Relative));
                imgRepeat.Source = img;
                btnRepeat.Tag = "1";
            }
            else 
            {
                if (btnRepeat.Tag.ToString() == "1")
                {
                    var img = new BitmapImage(
                    new Uri("Images/repeatone.png", UriKind.Relative));
                    imgRepeat.Source = img;
                    btnRepeat.Tag = "2";
                }
                else 
                {
                    var img = new BitmapImage(
                    new Uri("Images/repeatoff.png", UriKind.Relative));
                    imgRepeat.Source = img;
                    btnRepeat.Tag = "0";
                }
            }
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            if (btnShuffle.Tag.ToString() == "0")
            {
                var img = new BitmapImage(
                new Uri("Images/shuffle.png",
                UriKind.Relative));
                imgShuffle.Source = img;
                btnShuffle.Tag = "1";
            }
            else
            {
                var img = new BitmapImage(
                new Uri("Images/shuffleoff.png",
                UriKind.Relative));
                imgShuffle.Source = img;
                btnShuffle.Tag = "0";
            }
        }

        private void btnVolume_Click(object sender, RoutedEventArgs e)
        {
            if (btnVolume.Tag.ToString() == "0")
            {
                var img = new BitmapImage(
                new Uri("Images/mute.png",
                UriKind.Relative));
                imgVolume.Source = img;
                btnVolume.Tag = "1";
                sliderVolume.Value = 0;
                mediaPlayer.Volume = 0;
            }
            else 
            {
                var img = new BitmapImage(
                new Uri("Images/speaker.png",
                UriKind.Relative));
                imgVolume.Source = img;
                btnVolume.Tag = "0";
                sliderVolume.Value = 7;
                mediaPlayer.Volume = 7;
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (btnPlay.Tag.ToString() == "0")
            {
                var img = new BitmapImage(
                new Uri("Images/pause.png",
                UriKind.Relative));
                imgPlay.Source = img;
                btnPlay.Tag = "1";
                if (position.TotalMilliseconds == 0)
                {
                    Play(position);
                }
                else {
                    mediaPlayer.LoadedBehavior = MediaState.Play;
                }
            }
            else 
            {
                var img = new BitmapImage(
                new Uri("Images/play.png",
                UriKind.Relative));
                imgPlay.Source = img;
                btnPlay.Tag = "0";
                position = mediaPlayer.Position;
                mediaPlayer.LoadedBehavior = MediaState.Pause;
            }
        }

        private void Play(TimeSpan position)
        {
            try
            {
                Song selectedSong = lvPlayList.SelectedItem as Song;
                mediaPlayer.Source = new Uri(selectedSong.SongDir);
                mediaPlayer.Position = position;
                mediaPlayer.LoadedBehavior = MediaState.Play;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All media files |*.mp3;*.wav;*.m4a;*.mp4;*.avi;*.mov;*.3gp| Audio files (*.mp3,*.wav,*.m4a)|*.mp3;*.wav;*.m4a| Video files (*.mp4,*.avi,*.mov,*.3gp)|*.mp4;*.avi;*.mov;*.3gp";
            if (openFileDialog.ShowDialog() == true)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    Song newSong = new Song();
                    newSong.SongName = openFileDialog.SafeFileNames[i];
                    newSong.SongDir = openFileDialog.FileNames[i];
                    songList.Add(newSong);
                }
                lvPlayList.ItemsSource = songList;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvPlayList.SelectedItem == null)
            {
                return;
            }

            var selectedSong = lvPlayList.SelectedItem as Song;
            string deletedSong = selectedSong.SongName;

            for(int i=0;i<songList.Count;i++)
            {
                if (songList[i].SongName == deletedSong)
                {
                    songList.RemoveAt(i);                    
                    break;
                }
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.LoadedBehavior = MediaState.Stop;
            var img = new BitmapImage(
                new Uri("Images/play.png",
                UriKind.Relative));
            imgPlay.Source = img;
            btnPlay.Tag = "0";            
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = e.NewValue;

            if (e.NewValue == 0)
            {
                var img = new BitmapImage(
                new Uri("Images/mute.png",
                UriKind.Relative));
                imgVolume.Source = img;
                btnVolume.Tag = "1";
            }
            else
            {
                if (btnVolume.Tag.ToString() == "1")
                {
                    var img = new BitmapImage(
                    new Uri("Images/speaker.png",
                    UriKind.Relative));
                    imgVolume.Source = img;
                    btnVolume.Tag = "0";
                }
            }
        }
    }
}
