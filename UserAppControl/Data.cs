using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UserAppControl
{
    public class Data
    {
        public Data()
        {
            history_list = new ObservableCollection<History>();
            mapa_list = new ObservableCollection<Mapa>();
        }

        public ObservableCollection<History> history_list { get; set; }
        public ObservableCollection<Mapa> mapa_list { get; set; }

        
    }

    public class Mapa
    {
        public string Title { get; set; }
        public ObservableCollection<Mapa> Maps { get; set; }
        public ObservableCollection<Bookmark> Bookmarks { get; set; }

        public Mapa()
        {
            Title = "New folder";
            Maps = new ObservableCollection<Mapa>();
            Bookmarks = new ObservableCollection<Bookmark>();
        }

        public Mapa(string title)
        {
            Title = title;
            Maps = new ObservableCollection<Mapa>();
            Bookmarks = new ObservableCollection<Bookmark>();
        }
    }

    public class History
    {
        [XmlIgnore]
        public Uri AddressURI { get; set; }
        [XmlIgnore]
        public bool SIDomain { get; set; }

        private string address, icon;

        public string Title { get; set; }
        public string Address { get { return address; } set { address = value; SIDomain = SIDOMAIN(); } }
        public DateTime Time { get; set; }
        public string Icon { get { return icon; } set { icon = value; URICONVERT(); } }

        public History()
        {
            Title = "Generic";
            Address = "";
            Time = DateTime.Now;
            Icon = "";
        }

        public History(string title, string address, DateTime time, string icon)
        {
            Title = title;
            Address = address;
            Time = time;
            Icon = icon;
        }

        public void URICONVERT()
        {
            string abs = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, new Uri(Icon, UriKind.Relative).ToString());
            AddressURI = new Uri(abs);
        }

        public bool SIDOMAIN()
        {
            if (Address != null)
            {
                try
                {
                    Uri URI = new Uri(Address);
                    if (URI.Host.EndsWith(".si"))
                    {
                        return true;
                    }
                }
                catch { }
                return false;
            }
            return false;
        }
    }

    public class Bookmark
    {
        [XmlIgnore]
        public Uri AddressURI { get; set; }
        [XmlIgnore]
        public bool SIDomain { get; set; }

        private string address, icon;

        public string Title { get; set; }
        public string Address { get { return address; } set { address = value; SIDomain = SIDOMAIN(); } }
        public DateTime Time { get; set; }
        public string Icon { get { return icon; } set { icon = value; URICONVERT(); } }

        public Bookmark()
        {
            Title = "New bookmark";
            Address = "";
            Time = DateTime.Now;
            Icon = "";
        }

        public Bookmark(string title, string address, DateTime time, string icon)
        {
            Title = title;
            Address = address;
            Time = time;
            Icon = icon;
        }

        public void URICONVERT()
        {
            string abs = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, new Uri(Icon, UriKind.Relative).ToString());
            AddressURI = new Uri(abs);
        }

        public bool SIDOMAIN()
        {
            if (Address != null)
            {
                try
                {
                    Uri URI = new Uri(Address);
                    if (URI.Host.EndsWith(".si"))
                    {
                        return true;
                    }
                }
                catch { }
                return false;
            }
            return false;
        }
    }
}
