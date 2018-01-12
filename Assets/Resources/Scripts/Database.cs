using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class Database {

    public string path;
    public List<object> list = new List<object>();

    public void write<T>(string filepath) {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        FileStream sw = new FileStream(filepath, FileMode.Create);
        serializer.Serialize(sw, list);
        sw.Close();
    }

    public void load<T>(string filepath) {
        XmlSerializer serializer = new XmlSerializer(typeof(Object));
        FileStream sw = new FileStream(filepath, FileMode.Open);
        List<T> list = (List<T>)serializer.Deserialize(sw);
        sw.Close();
        
    }

}
