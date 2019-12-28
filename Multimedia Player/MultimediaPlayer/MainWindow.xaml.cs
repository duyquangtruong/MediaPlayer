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
using System.Windows.Threading;

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
            backupList = new ObservableCollection<Song>();
            random = new Random();
        }

        ObservableCollection<Song> songList;
        ObservableCollection<Song> backupList;
        Song currentSong;
        TimeSpan TotalTime;
        DispatcherTimer mediaTimer;
        Random random;

        public int RNG(int previousNumber, int limit)
        {
            int RandomNumber = random.Next(0, limit);
            while (RandomNumber == previousNumber)
            {
                RandomNumber = random.Next(0, limit);
            } 
            return RandomNumber;
        }

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
                for (int i = 0; i < songList.Count;i++)
                {
                    songList.Move(i, RNG(i, songList.Count-1));
                }
                lvPlayList.ItemsSource = songList;
            }
            else
            {
                var img = new BitmapImage(
                new Uri("Images/shuffleoff.png",
                UriKind.Relative));
                imgShuffle.Source = img;
                btnShuffle.Tag = "0";
                lvPlayList.ItemsSource = backupList;
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
                if (mediaPlayer.Position.TotalMilliseconds == 0)
                {
                    PlayMedia();
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
                mediaPlayer.LoadedBehavior = MediaState.Pause;
            }
        }

        private void PlayMedia()
        {
            try
            {
                currentSong = lvPlayList.SelectedItem as Song;
                mediaPlayer.Source = new Uri(currentSong.SongDir);
                mediaPlayer.LoadedBehavior = MediaState.Play;
                
                while (mediaPlayer.NaturalDuration.HasTimeSpan == false)
                {
                    continue;
                }

                DisPlayDurationTime();                

                timeCurrent.Text = "0:00";
            }
            catch (Exception e)
            {
                MessageBox.Show("Choose a song in playlist to play");
                StopMedia();
            }
        }

        private void DisPlayDurationTime()
        {
            if (mediaPlayer.NaturalDuration.TimeSpan.Hours == 0)
            {
                if (mediaPlayer.NaturalDuration.TimeSpan.Minutes == 0)
                {
                    timeEnd.Text = "0:";
                }
                else
                {
                    timeEnd.Text = mediaPlayer.NaturalDuration.TimeSpan.Minutes.ToString()
                        + ":";
                }
            }
            else
            {
                timeEnd.Text = mediaPlayer.NaturalDuration.TimeSpan.Hours.ToString()
                    + ":" + mediaPlayer.NaturalDuration.TimeSpan.Minutes.ToString()
                    + ":";
            }

            if (mediaPlayer.NaturalDuration.TimeSpan.Seconds < 10)
            {
                timeEnd.Text += "0" + mediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString();
            }
            else
            {
                timeEnd.Text += mediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString();
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
                    newSong.SongDir = openFileDialog.FileNames[i];
                    newSong.SongName = System.IO.Path.GetFileNameWithoutExtension(newSong.SongDir);
                    songList.Add(newSong);
                    backupList.Add(newSong);
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
                    for (int j = 0; j < backupList.Count; j++)
                    {
                        if (songList[i].SongName == backupList[j].SongName)
                        {
                            backupList.RemoveAt(j);
                        }
                    }
                    songList.RemoveAt(i);
                    break;
                }
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopMedia();
        }

        private void StopMedia()
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
            mediaPlayer.Volume = e.NewValue/10;

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

        private void mediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaTimer.Stop();
            StopMedia();
            timeSlider.Value = 0;
            int index = 0;
      
            for (int i = 0; i < songList.Count; i++)
            {
                if (songList[i].SongName == currentSong.SongName)
                {
                    index = i;
                    break;
                }
            }

            //khong lap
            if (btnRepeat.Tag.ToString() == "0")
            {
                //Cuoi danh sach, dung phat
                if (index == songList.Count - 1)
                {
                    StopMedia();
                }
                else 
                {
                    PlayNext();
                }
            }

            //Lap vo tan
            if (btnRepeat.Tag.ToString() == "1")
            {
                PlayNext();
            }

            //Lap 1 bai
            if (btnRepeat.Tag.ToString() == "2")
            {
                mediaPlayer.LoadedBehavior = MediaState.Play;
            }
        }



        private void PlayIndex(int index)
        {
            try
            {
                currentSong = songList[index];
                mediaPlayer.Source = new Uri(currentSong.SongDir);
                mediaPlayer.LoadedBehavior = MediaState.Play;

                var img = new BitmapImage(
                new Uri("Images/pause.png",
                UriKind.Relative));
                imgPlay.Source = img;
                btnPlay.Tag = "1";

                while (mediaPlayer.NaturalDuration.HasTimeSpan == false)
                {
                    continue;
                }

                DisPlayDurationTime();                

                timeCurrent.Text = "0:00";
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot find the song");
                StopMedia();
            }
        }

        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = mediaPlayer.NaturalDuration.TimeSpan;

            if (mediaPlayer.HasVideo == false)
            {
                OpenBackGround();
            }
            else
            {
                GridLength ImgWidth = new GridLength(0, GridUnitType.Star);
                GridLength VideoWidth = new GridLength(10, GridUnitType.Star);

                videoFrame.Width = VideoWidth;
                musicFrame.Width = ImgWidth;
            }

            // Create a timer that will update the counters and the time slider
            mediaTimer = new DispatcherTimer();
            mediaTimer.Interval = TimeSpan.FromMilliseconds(1000);
            mediaTimer.Tick += new EventHandler(timer_Tick);
            mediaTimer.Start();
        }

        private void OpenBackGround()
        {
            string[] bgList = {"background.jpg", "background1.jpg", "background2.jpg", "background3.jpg" };
            int index = RNG(-1, 3);
            GridLength ImgWidth = new GridLength(10, GridUnitType.Star);
            GridLength VideoWidth = new GridLength(0, GridUnitType.Star);

            videoFrame.Width = VideoWidth;
            musicFrame.Width = ImgWidth;

            var img = new BitmapImage(
               new Uri("Images/"+bgList[index],
               UriKind.Relative));
            musicBackground.Source = img;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds > 0)
            {
                if (TotalTime.TotalMilliseconds > 0)
                {
                    timeSlider.Value = mediaPlayer.Position.TotalMilliseconds / TotalTime.TotalMilliseconds;

                    if (mediaPlayer.Position.Hours == 0)
                    {
                        if(mediaPlayer.Position.Minutes == 0)
                        {
                            timeCurrent.Text = "0:";
                        }
                        else 
                        {
                            timeCurrent.Text = mediaPlayer.Position.Minutes.ToString() + ":";
                        }                        
                    }
                    else 
                    {
                        timeCurrent.Text = mediaPlayer.Position.Hours.ToString() 
                            + ":" + mediaPlayer.Position.Minutes.ToString() + ":";
                    }

                    if (mediaPlayer.Position.Seconds < 10)
                    {
                        timeCurrent.Text += "0" + mediaPlayer.Position.Seconds.ToString();
                    }
                    else
                    {
                        timeCurrent.Text += mediaPlayer.Position.Seconds.ToString();
                    }
                }
            }
        }

       /* private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalTime.TotalMilliseconds > 0)
            {
                mediaPlayer.Position = TimeSpan.FromMilliseconds(timeSlider.Value * TotalTime.TotalMilliseconds);
            }
        }*/

        private void timeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TotalTime.TotalMilliseconds > 0)
            {
                mediaPlayer.Position = TimeSpan.FromMilliseconds(timeSlider.Value * TotalTime.TotalMilliseconds);
            }
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            PlayNext();
        }

        private void PlayNext()
        {
            int index = 0;

            for (int i = 0; i < songList.Count; i++)
            {
                if (songList[i].SongName == currentSong.SongName)
                {
                    index = i;
                    break;
                }
            }

            //neu cuoi playlist thi quay lai bai hat dau tien
            if (index == songList.Count - 1)
            {
                index = -1;
            }

            //tang index choi bai ke tiep
            PlayIndex(index + 1);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                using (StreamWriter streamWriter = new StreamWriter("Playlist.txt"))
                {
                    foreach (var song in songList)
                    {
                        streamWriter.WriteLine(song.SongDir);
                    }
                }
                MessageBox.Show("Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save playlist");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                songList.Clear();
                using (StreamReader sr = new StreamReader("Playlist.txt"))
                {
                    string dir;
                    while ((dir = sr.ReadLine()) != null)
                    {
                        Song newSong = new Song();
                        newSong.SongDir = dir;
                        newSong.SongName = System.IO.Path.GetFileNameWithoutExtension(dir);
                        songList.Add(newSong);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot load playlist");
                songList = new ObservableCollection<Song>(backupList);
            }
        }
    }
}
