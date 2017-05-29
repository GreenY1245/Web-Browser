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
using System.Windows.Threading;
using System.Web;

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int num_of_tabs, num_of_his_items, num_of_bkm_items;


        //public static ObservableCollection<Mapa> Bookmarks = new ObservableCollection<Mapa>();
        //public static ObservableCollection<History> History = new ObservableCollection<History>();


        public MainWindow()
        {
            InitializeComponent();

            string defautlPath = "Data.xml";

            if (File.Exists("Data.xml"))
            {
                Load_XML_Startup(defautlPath);
            } else
            {
                GenerateBasicStructure();
            }
        }

        public static void GenerateBasicStructure()
        {
            UserAppControlController.Controller = new Data();
            Mapa m = new Mapa();
            UserAppControlController.CreateNewFolder("Folder", m);
        }

        public static void Load_XML_Startup(string file_name)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Data));
            using (StreamReader rd = new StreamReader(file_name))
            {
                UserAppControlController.Controller = xs.Deserialize(rd) as Data;
            }
        }


        private void exitButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void nTab_button_Click(object sender, RoutedEventArgs e)
        {
            if (num_of_tabs >= 10)
            {
                MessageBox.Show("Max number of tabs reached!");
            } else
            {
                num_of_tabs++;
                //TabItem nTab = new TabItem();
                //TextBox tBox = new TextBox();
                //tBox.Name = "searchBox_" + num_of_tabs;
                //nTab.Header = "New Tab" + num_of_tabs;
                //nTab.Content = new UserControl();
                //tC_.Items.Add(nTab);

                TabItem item = null;
                DockPanel docp = null;
                TextBox tbx = null;
                Button btn = null;

                try
                {
                    tbx = new TextBox();
                    tbx.Name = "searchBox" + num_of_tabs;
                    DockPanel.SetDock(tbx, Dock.Left);
                    tbx.Loaded += searchBox_Loaded;
                    tbx.GotFocus += searchBox_GotFocus;
                    tbx.KeyUp += searchBox_KeyUp;

                    btn = new Button();
                    btn.Width = 18;
                    btn.Height = 18;
                    btn.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("/images/bookmark.png", UriKind.Relative))
                    };
                    DockPanel.SetDock(btn, Dock.Right);
                    btn.Name = "addBookmarkButton" + num_of_tabs;
                    btn.Click += addBookmarkButton_Click;
                    btn.BorderBrush = null;
                    btn.Foreground = null;
                    btn.Background = Brushes.White;

                    docp = new DockPanel();
                    docp.Children.Add(btn);
                    docp.Children.Add(tbx);
                    docp.LastChildFill = true;

                    item = new TabItem();
                    item.Header = "New Tab" + num_of_tabs;
                    item.Content = docp;

                    tC_.Items.Add(item);
                    tC_.SelectedItem = item;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating Tab: " + ex.Message);
                }
                finally
                {
                    tbx = null;
                    docp = null;
                    item = null;
                    btn = null;
                }
            }
        }

        private void searchBox_Loaded(object sender, RoutedEventArgs e)
        {
            //searchBox.Text = browserSource1.Uid.ToString();
            //searchBox.Text = browserSource1.Source.OriginalString;
            searchBox.Text = Convert.ToString(browserSource1.Source);
            //searchBox.Text = "https://www.google.com/";
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = null;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private void browserSource1_Loaded(object sender, RoutedEventArgs e)
        {
            string x = SpletniBrskalnik.Properties.Settings.Default.HomePage;

            if (x.StartsWith("https://") || x.StartsWith("http://"))
            {
                browserSource1.Source = new Uri(x);
            } else
            {
                browserSource1.Source = new Uri("http://"+x);
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
            string srcString = searchBox.Text;

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

        private void listView_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void b_google_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            MessageBox.Show(Convert.ToString(item.Content));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            settings x = new settings();
            x.ShowDialog();
        }

        private void browserSource1_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            searchBox.Text = Convert.ToString(browserSource1.Source);
            txB_sb_it1.Text = Convert.ToString(browserSource1.Source);
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

        private void browserSource1_Navigated(object sender, NavigationEventArgs e)
        {
            num_of_his_items++;
            ListViewItem newITM = new ListViewItem();
            newITM.Name = "historyITEM_" + num_of_his_items;
            newITM.Content = Convert.ToString(browserSource1.Source);
            newITM.MouseLeftButtonUp += b_google_MouseLeftButtonUp;
            searchBox.Text = Convert.ToString(browserSource1.Source);
            his_listView.Items.Add(newITM);


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
            ListViewItem item = new ListViewItem
            {
                Content = Convert.ToString(browserSource1.Source),
                Name = "BookmarkITEM_" + num_of_bkm_items
            };

            item.MouseDoubleClick += b_google_MouseDoubleClick;
            item.MouseLeftButtonUp += b_google_MouseLeftButtonUp;
            bkmark.Items.Add(item);

            Bookmark bkm = new Bookmark();
            bkm.Address = Convert.ToString(browserSource1.Source);
            bkm.Time = DateTime.Now;
            bkm.Title = ((dynamic)browserSource1.Document).Title;
            bkm.Icon = getimage(browserSource1.Source.Host);

            UserAppControlController.Controller.mapa_list[0].Bookmarks.Add(bkm);
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {
            browserSource1_Loaded(sender, e);
        }

        private void Chrome__3_0_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
            UserAppControlController.Serialize("Data.xml");
        }

        public static void XML_EXPORT()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Data";
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "XML (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() == true)
            {
                UserAppControlController.Serialize(saveFileDialog.FileName);
            }
        }

        public static void XML_IMPORT()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".xml";
            openFileDialog.Filter = "XML (*.xml)|*.xml";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true)
            {
                XmlSerializer xs = new XmlSerializer(typeof(Data));
                using (StreamReader rd = new StreamReader(openFileDialog.FileName))
                {
                    UserAppControlController.Controller = xs.Deserialize(rd) as Data;
                }
            }
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            XML_EXPORT();
        }

        private void Chrome__3_0_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserAppControlController.Serialize("Data.xml");
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            XML_IMPORT();
        }

        private void Chrome__3_0_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // null yet
        }

        public void Chrome__3_0_Loaded(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;

            if (userSettings.showBookmarks == true)
            {
                bookmarks.Visibility = Visibility.Visible;
            } else
            {
                bookmarks.Visibility = Visibility.Hidden;
            }

            if (userSettings.showHome == true)
            {
                backHome.Visibility = Visibility.Visible;
            } else
            {
                backHome.Visibility = Visibility.Hidden;
            }
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
