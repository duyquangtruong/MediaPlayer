using System;
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

namespace MultimediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
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
            }
            else
            {
                var img = new BitmapImage(
                new Uri("Images/playlistoff.png",
                UriKind.Relative));
                imgPlaylist.Source = img;
                btnPlayList.Tag = "0";
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
            }
            else 
            {
                var img = new BitmapImage(
                new Uri("Images/speaker.png",
                UriKind.Relative));
                imgVolume.Source = img;
                btnVolume.Tag = "0";
                sliderVolume.Value = 7;
            }
        }
    }
}
