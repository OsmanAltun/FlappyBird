using FlappyBird.Shared.Screens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FlappyBird.Shared
{
    public static class XmlManager
    {
        public static T Load<T>(string fileName)
        {
            string basePath = "./";
#if ANDROID
            basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
#endif

            if (!File.Exists(System.IO.Path.Combine(basePath, fileName)))
            {
                Save(fileName, new SaveData { BestScore = 0, CurrentScore = 0});
            }

            using (var reader = new StreamReader(System.IO.Path.Combine(basePath, fileName)))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                return (T)xml.Deserialize(reader);
            }
        }

        public static void Save<T>(string fileName, T obj)
        {
            string basePath = "./";
#if ANDROID
            basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
#endif
            using (TextWriter writer = new StreamWriter(System.IO.Path.Combine(basePath, fileName)))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(writer, obj);
            }
        }
        }
}
