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
using System.Windows.Shapes;

using UserAppControl;
using UserAppControlBookmarks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;
using System.Net;

namespace UserAppControlBookmarks
{
    /// <summary>
    /// Interaction logic for BookmarkEdit.xaml
    /// </summary>
    public partial class BookmarkEdit : Window
    {
        public BookmarkEdit()
        {
            InitializeComponent();

            if (UserAppControlController.Controller != null)
            {
                treeview_bookmarks.ItemsSource = UserAppControlController.Controller.mapa_list;
            }
        }

        private void bkmark_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            listview_bookmarks.ItemsSource = ((Mapa)treeview_bookmarks.SelectedItem).Bookmarks;
        }

        private void listview_bookmarks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textbox_title.Text = ((Bookmark)listview_bookmarks.SelectedItem).Title;
            textbox_address.Text = ((Bookmark)listview_bookmarks.SelectedItem).Address;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newTitle = textbox_title.Text;
            string newAddress = textbox_address.Text;

            try
            {
                Uri newUri = new Uri(newAddress);

                Bookmark current = null;
                Mapa w = null;

                if ((Bookmark)listview_bookmarks.SelectedItem != null && (Mapa)treeview_bookmarks.SelectedItem != null)
                {
                    current = (Bookmark)listview_bookmarks.SelectedItem;
                    w = (Mapa)treeview_bookmarks.SelectedItem;
                }

                Bookmark newBookmark = new Bookmark();

                newBookmark.Title = newTitle;
                newBookmark.Time = DateTime.Now;

                if (current.Address != newAddress)
                {
                    newBookmark.Address = newAddress;
                    newBookmark.Icon = getimage(newUri.Host);
                } else
                {
                    newBookmark.Address = current.Address;
                    newBookmark.Icon = current.Icon;
                }

                int index = index = w.Bookmarks.IndexOf(current);
                
                if (index >= 0)
                {
                    w.Bookmarks[index] = newBookmark;
                } else
                {
                    w.Bookmarks.Add(newBookmark);
                }

                
            }
            catch
            {
                this.Close();
                //MessageBox.Show("Error Updating Bookmark: " + x.Message);
            }
        }

        private string getimage(string currentSource)
        {
            string imagepath;
            var client = new WebClient();
            Directory.CreateDirectory("host/");
            imagepath = "host/" + currentSource + ".ico";
            client.DownloadFile(@"https://www.google.com/s2/favicons?domain_url=" + currentSource, imagepath);
            return imagepath;
        }
    }
}
