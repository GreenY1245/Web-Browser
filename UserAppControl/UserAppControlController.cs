using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UserAppControl
{
    public class UserAppControlController
    {
        public static Data Controller { get; set; }


        public static void Serialize(string file_name)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            using (TextWriter writer = new StreamWriter(file_name))
            {
                serializer.Serialize(writer, Controller);
            }
        }
        public static void Deserialize(string file_name)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            using (StreamReader rd = new StreamReader(file_name))
            {
                Controller = serializer.Deserialize(rd) as Data;
            }
        }

        public static void CreateNewFolder(string folder_name, Mapa map)
        {
            if (Controller.mapa_list != null)
            {
                map.Title = folder_name;
                Controller.mapa_list.Add(map);
            }
        }
    }
}
