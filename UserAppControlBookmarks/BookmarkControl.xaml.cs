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

            string bkm;

            if (m != null)
            {
                if (newBookmark.Text == null)
                {
                    treeview_bookmarkss++;
                    bkm = "new_bookmark" + treeview_bookmarkss;
                }
                else
                {
                    bkm = newBookmark.Text;
                }

                m.Bookmarks.Add(new Bookmark(bkm, newBookmark.Text, DateTime.Now, ""));
            }

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
    }
}
