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
using System.Collections;
using UserAppControl;

namespace UserAppControlBookmarks
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        int treeview_bookmarkss = 3;

        public UserControl1()
        {
            InitializeComponent();
            if (UserAppControlController.Controller != null)
            {
                treeview_bookmarks.ItemsSource = UserAppControlController.Controller.mapa_list;
            }
        }

        private void AddBookmark_Click(object sender, RoutedEventArgs e)
        {

            Mapa m = (Mapa)treeview_bookmarks.SelectedItem;
            Bookmark bk = new Bookmark();

            string bkm;

            if (m != null)
            {
                try
                {
                    string temphost = newBookmarkURL.Text;

                    if (!(temphost.StartsWith("http") || temphost.StartsWith("https")))
                    {
                        temphost = "http://" + temphost;
                    }

                    Uri temp = new Uri(temphost);

                    bk.Address = Convert.ToString(temp);
                    bk.Title = Convert.ToString(temp.Host);
                    bk.Icon = getimage(temp.Host);
                    bk.Time = DateTime.Now;

                    UserAppControlController.Controller.mapa_list[0].Bookmarks.Add(bk);
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error adding bookmark: " + x.Message);
                }
            }
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

        private void addFolder_Click(object sender, RoutedEventArgs e)
        {
            string folder_name = newFolder.Text;
            if (string.IsNullOrWhiteSpace(newFolder.Text))
            {
                folder_name = "New Folder";
            }

            if (treeview_bookmarks.SelectedItem != null)
            {
                Mapa m = (Mapa)treeview_bookmarks.SelectedItem;
                m.Maps.Add(new Mapa(folder_name));
            }
        }

        private void RemoveFolder_Click(object sender, RoutedEventArgs e)
        {
            Mapa x = (Mapa)treeview_bookmarks.SelectedItem;

            bool del = false;

            if (UserAppControlController.Controller.mapa_list.IndexOf(x) != 0)
            {
                del = RemoveFolder_(x, UserAppControlController.Controller.mapa_list);
            }
        }

        private bool RemoveFolder_(Mapa m, ObservableCollection<Mapa> list)
        {
            if (list != null)
            {
                if (list.Contains(m))
                {
                    list.Remove(m);
                    return true;
                }
                else
                {
                    foreach (var subFolder in list.Select(x => x.Maps))
                    {
                        if (RemoveFolder_(m, subFolder))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // BOOKMARKS REMOVE SELECTED BUTTON
        {
            Mapa m = (Mapa)treeview_bookmarks.SelectedItem;

            m.Bookmarks.Remove((Bookmark)listview_bookmarks.SelectedItem);
        }

        private void bkmark_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            listview_bookmarks.ItemsSource = ((Mapa)treeview_bookmarks.SelectedItem).Bookmarks;
        }

        private void DeleteContext_Click(object sender, RoutedEventArgs e)
        {
            Mapa x = (Mapa)treeview_bookmarks.SelectedItem;
            bool del = false;
            if (UserAppControlController.Controller.mapa_list.IndexOf(x) != 0)
            {
                del = RemoveFolder_(x, UserAppControlController.Controller.mapa_list);

                if (del == false)
                {
                    MessageBox.Show("Folder could not be deleted!");
                }
            }
        }

        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }

        private void treeview_bookmarks_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem tvi = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (tvi != null)
            {
                tvi.Focus();
                e.Handled = true;
            }
        }
    }
}
