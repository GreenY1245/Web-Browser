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

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;
using UserAppControl;
using UserAppControlBookmarks;

namespace SpletniBrskalnik
{
    /// <summary>
    /// Interaction logic for settings.xaml
    /// </summary>
    /// 


    public partial class settings : Window
    {
        public settings()
        {
            InitializeComponent();
            if (UserAppControlController.Controller != null)
            {
                listview_history.ItemsSource = UserAppControlController.Controller.history_list;
            }
        }

        int onLoadChecked;
        bool reset_home = SpletniBrskalnik.Properties.Settings.Default.showHome;
        bool reset_bookmark = SpletniBrskalnik.Properties.Settings.Default.showBookmarks;

        private void radio_reloadlast_Checked(object sender, RoutedEventArgs e)
        {
            SpletniBrskalnik.Properties.Settings.Default.reloadLast = true;
            SpletniBrskalnik.Properties.Settings.Default.reloadCustom = false;
            SpletniBrskalnik.Properties.Settings.Default.reloadDefault = false;
            SpletniBrskalnik.Properties.Settings.Default.radioChecked = 2;
            customSource.IsEnabled = false;
        }

        private void radio_custompage_Checked(object sender, RoutedEventArgs e)
        {
            SpletniBrskalnik.Properties.Settings.Default.reloadLast = false;
            SpletniBrskalnik.Properties.Settings.Default.reloadCustom = true;
            SpletniBrskalnik.Properties.Settings.Default.reloadDefault = false;
            SpletniBrskalnik.Properties.Settings.Default.radioChecked = 3;
            customSource.IsEnabled = true;
        }

        private void radio_default_Checked(object sender, RoutedEventArgs e)
        {
            SpletniBrskalnik.Properties.Settings.Default.reloadLast = false;
            SpletniBrskalnik.Properties.Settings.Default.reloadCustom = false;
            SpletniBrskalnik.Properties.Settings.Default.reloadDefault = true;
            SpletniBrskalnik.Properties.Settings.Default.radioChecked = 1;
            customSource.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            int radioChecked = userSettings.radioChecked;
            if (radioChecked == 1)
            {
                onLoadChecked = 1;
                radio_default.IsChecked = true;
                customSource.IsEnabled = false;
            }
            else if (radioChecked == 2)
            {
                onLoadChecked = 2;
                radio_reloadlast.IsChecked = true;
                customSource.IsEnabled = false;
            }
            else if (radioChecked == 3)
            {
                onLoadChecked = 3;
                radio_custompage.IsChecked = true;
                customSource.IsEnabled = true;
            }

            customSource.Text = userSettings.CustomHomePage;
            showBookmarks.IsChecked = userSettings.showBookmarks;
            showHomeButton.IsChecked = userSettings.showHome;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //CANCEL BUTTON

            returnToDefault(onLoadChecked);
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            int radioChecked = SpletniBrskalnik.Properties.Settings.Default.radioChecked;
            var browser = SpletniBrskalnik.Properties.Settings.Default;
            MainWindow mw = new MainWindow();

            if (radioChecked == 1)
            {
                browser.HomePage = "https://www.google.com";
            }
            else if (radioChecked == 2)
            {
                browser.HomePage = Convert.ToString(mw.browserSource1.Source);
            }
            else if (radioChecked == 3)
            {
                browser.HomePage = customSource.Text;
            }


            SpletniBrskalnik.Properties.Settings.Default.Save();
            this.Close();
        }

        public static void returnToDefault(int x)
        {
            var defaults = SpletniBrskalnik.Properties.Settings.Default;

            if (x == 1)
            {
                defaults.reloadDefault = true;
                defaults.reloadLast = false;
                defaults.reloadCustom = false;
            }
            else if (x == 3)
            {
                defaults.reloadLast = true;
                defaults.reloadDefault = false;
                defaults.reloadCustom = false;
            }
            else if (x == 2)
            {
                defaults.reloadCustom = true;
                defaults.reloadDefault = false;
                defaults.reloadLast = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            userSettings.showBookmarks = true;
        }

        private void showHomeButton_Checked(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            userSettings.showHome = true;
        }

        private void showBookmarks_Unchecked(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            userSettings.showBookmarks = false;
        }

        private void showHomeButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            userSettings.showHome = false;
        }

        private void Apply_style_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            userSettings.Save();
            this.Close();
        }

        private void Cancel_style_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = SpletniBrskalnik.Properties.Settings.Default;
            userSettings.showBookmarks = reset_bookmark;
            userSettings.showHome = reset_home;
            this.Close();
        }

        private void history_deleteOne_Click(object sender, RoutedEventArgs e)
        {
            UserAppControlController.Controller.history_list.Remove((History)listview_history.SelectedItem);
        }

        private void history_deleteAll_Click(object sender, RoutedEventArgs e)
        {
            UserAppControlController.Controller.history_list.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // ne pali
        }
    }
}
