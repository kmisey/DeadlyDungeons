using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class TraderDatabase {

    string filename = "tradersdatabase";
    TextAsset textAsset;
    XmlDocument xmldoc;
    public List<int> items = new List<int>();

    public TraderDatabase() {
        textAsset = new TextAsset();
        xmldoc = new XmlDocument();
        textAsset = (TextAsset)Resources.Load(filename);
        xmldoc.LoadXml(textAsset.text);
        getAllItems();
    }

    public void getAllItems() {
        getShop();
    }

    void getShop() {
        bool inSection = false;
        int id = 0;

        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));
        while (reader.Read() && inSection == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Items")) {
                inSection = true;
                break;
            }
        }
        while (reader.Read() && inSection) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Items")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("item")) {
                items.Add(id);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }
}
