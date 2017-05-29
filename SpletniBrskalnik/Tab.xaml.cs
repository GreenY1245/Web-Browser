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

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Collections.ObjectModel;
using UserAppControl;
using UserAppControlBookmarks;

namespace SpletniBrskalnik
{
    /// <summary>
    /// Interaction logic for Tab.xaml
    /// </summary>
    public partial class Tab : UserControl
    {
        int num_of_bkm_items;

        public Tab()
        {
            InitializeComponent();
        }

        private void searchBox_Loaded(object sender, RoutedEventArgs e)
        {
            searchBox.Text = Convert.ToString(browserSource1.Source);
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = null;
        }

        private void browserSource1_Loaded(object sender, RoutedEventArgs e)
        {
            string x = SpletniBrskalnik.Properties.Settings.Default.HomePage;

            if (x.StartsWith("https://") || x.StartsWith("http://"))
            {
                browserSource1.Source = new Uri(x);
            }
            else
            {
                browserSource1.Source = new Uri("http://" + x);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (browserSource1.CanGoBack)
            {
                browserSource1.GoBack();
            }
        }

        private void forwB_Click(object sender, RoutedEventArgs e)
        {
            if (browserSource1.CanGoForward)
            {
                browserSource1.GoForward();
            }
        }

        private void searchBox_KeyUp(object sender, KeyEventArgs e)
        {
            string srcString = Convert.ToString(sender);

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                txB_sb_it1.Text = "Loading...";

                if (srcString.StartsWith("https://") || srcString.StartsWith("http://"))
                {
                    browserSource1.Source = new Uri(srcString);
                }
                else
                {
                    browserSource1.Source = new Uri("http://" + srcString);
                }
            }
        }

        private void refrB_Click(object sender, RoutedEventArgs e)
        {
            browserSource1.Refresh();
        }

        private void browserSource1_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            searchBox.Text = Convert.ToString(browserSource1.Source);
            txB_sb_it1.Text = Convert.ToString(browserSource1.Source);
        }

        private void browserSource1_Navigated(object sender, NavigationEventArgs e)
        {
            History his = new History();
            his.Address = Convert.ToString(browserSource1.Source);
            his.Time = DateTime.Now;
            his.Title = ((dynamic)browserSource1.Document).Title;
            his.Icon = getimage(browserSource1.Source.Host);

            UserAppControlController.Controller.history_list.Add(his);

            txB_sb_it1.Text = "Loaded";
        }

        private void addBookmarkButton_Click(object sender, RoutedEventArgs e)
        {
            num_of_bkm_items++;
            Bookmark bkm = new Bookmark();
            bkm.Address = Convert.ToString(browserSource1.Source);
            bkm.Time = DateTime.Now;
            bkm.Title = ((dynamic)browserSource1.Document).Title;
            bkm.Icon = getimage(browserSource1.Source.Host);

            UserAppControlController.Controller.mapa_list[0].Bookmarks.Add(bkm);
        }

        private string getimage(string currentSource)
        {
            string imagepath;
            var client = new System.Net.WebClient();
            Directory.CreateDirectory("host/");
            imagepath = "host/" + currentSource + ".ico";
            client.DownloadFile(@"https://www.google.com/s2/favicons?domain_url=" + currentSource, imagepath);
            return imagepath;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            if (userSettings.showHome == true)
            {
                backHome.Visibility = Visibility.Visible;
            }
            else
            {
                backHome.Visibility = Visibility.Hidden;
            }
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {
            browserSource1_Loaded(sender, e);
        }

        private void searchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = Convert.ToString(browserSource1.Source);
        }

        private void b_google_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var srcString = sender.ToString().Remove(0, sender.ToString().IndexOf(' ') + 1);
            // DEBUG MessageBox.Show(srcString);

            txB_sb_it1.Text = "Loading...";

            if (srcString.StartsWith("https://") || srcString.StartsWith("http://"))
            {
                browserSource1.Source = new Uri(srcString);
            }
            else
            {
                browserSource1.Source = new Uri("http://" + srcString);
            }

        }
    }
}
