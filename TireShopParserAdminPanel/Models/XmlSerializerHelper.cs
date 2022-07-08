using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace TireShopParserAdminPanel.Models
{
    public static class XmlSerializerHelper
    {
        public static void Serialize<T>(string filename, T obectToSerialize)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));

            using (var fs = new FileStream(filename, FileMode.CreateNew))
            {
                using (var writer = new XmlTextWriter(fs, Encoding.UTF8))
                {
                    serializer.Serialize(writer, obectToSerialize);
                }
            }
        }

        public static T Deserialize<T>(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
            T deserializedObject;
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                using (var reader = new XmlTextReader(fs))
                {
                    deserializedObject = (T)serializer.Deserialize(reader);
                }
            }

            return deserializedObject;
        }



    }
}
