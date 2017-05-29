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
using System.Timers;
using System.Windows.Media.Animation;

namespace SpletniBrskalnik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int num_of_tabs;

        DispatcherTimer timerController = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            timerController.Tick += new EventHandler(timerController_Tick);
            timerController.Interval = new TimeSpan(0, 5, 0);
            timerController.Start();

            string defautlPath = "Data.xml";

            if (File.Exists("Data.xml"))
            {
                Load_XML_Startup(defautlPath);
            } else
            {
                GenerateBasicStructure();
            }
        }

        public void timerController_Tick(object sender, EventArgs e)
        {
            UserAppControlController.Serialize("Data.xml");
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
                TabItem ti = new TabItem();
                ti.Header = "New Tab " + num_of_tabs;
                ti.Content = new Tab();
                ti.MouseWheel += TabItem_MouseWheel;
                tC_.Items.Add(ti);
                ti.IsSelected = true;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private void listView_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            settings x = new settings();

            DoubleAnimation fadeinout = new DoubleAnimation();
            fadeinout.From = 0;
            fadeinout.To = 1;
            fadeinout.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            x.BeginAnimation(Window.OpacityProperty, fadeinout);
            x.ShowDialog();
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

        private void TabItem_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //var target = (FrameworkElement)sender;
            //while (target is ContextMenu == false)
            //{
            //    target = (FrameworkElement)target.Parent;
            //}
            //var tabItem = (target as ContextMenu).PlacementTarget;
            //tC_.Items.Remove(tabItem);
        }

        private void Chrome__3_0_MouseUp(object sender, MouseButtonEventArgs e)
        {

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
        }
    }
}
