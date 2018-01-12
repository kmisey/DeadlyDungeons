using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class ItemDatabase {

    public bool doneloading = false;

    string filename = "itemdatabase";
    public string path;
    public List<BaseItem> items = new List<BaseItem>();
    TextAsset textAsset = new TextAsset();
    XmlDocument xmldoc = new XmlDocument();

    public ItemDatabase() {
        for (int i = 0; i < 57; i++) {
            items.Add(new BaseItem());
        }
        textAsset = new TextAsset();
        xmldoc = new XmlDocument();
        textAsset = (TextAsset)Resources.Load(filename);
        xmldoc.LoadXml(textAsset.text);
        getAllItems();
        doneloading = true;
    }

    public int findID(string itemname) {
        for(int i = 0; i < items.Count; i++) {
            if (items[i].name.Equals(itemname)) {
                return i;
            }
        }
        return 0;
    }

    public void getAllItems() {
        getWeapons();
        getArmors();
        getBlessings1();
        getBlessings2();
        getValuables();
        getCraftables();
    }

    public void getWeapons() {
        bool inWeaponSection = false;
        string name = "";
        string title = "";
        int damage = 0;
        float lifesteal = 0.0f;
        float bonusdmg = 0.0f;
        float crit = 0.0f;
        int worth = 0;
        int id = 0;
        int amount = 0;

        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));

        while (reader.Read() && inWeaponSection == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Weapon")) {
                inWeaponSection = true;
                break;
            }
        }
        while (reader.Read()) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Weapon")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("BaseItem")) {
                items[id] = new WeaponItem(id, name, damage, crit, lifesteal, bonusdmg, title, amount, worth);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.HasAttributes) {
                    name = reader.GetAttribute("name");
                }
                else if (reader.Name.Equals("title")) {
                    reader.Read();
                    title = reader.Value;
                }
                else if (reader.Name.Equals("worth")) {
                    reader.Read();
                    worth = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("amount")) {
                    reader.Read();
                    amount = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("damage")) {
                    reader.Read();
                    damage = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("lifesteal")) {
                    reader.Read();
                    lifesteal = float.Parse(reader.Value);
                }
                else if (reader.Name.Equals("bonusdmg")) {
                    reader.Read();
                    bonusdmg = float.Parse(reader.Value);
                }
                else if (reader.Name.Equals("crit")) {
                    reader.Read();
                    crit = float.Parse(reader.Value);
                }
                else if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }
    public void getArmors() {
        bool inArmorSection = false;
        string name = "";
        string title = "";
        int id = 0;
        int armor = 0;
        float reduction = 0.0f;
        int worth = 0;
        int amount = 0;

        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));

        while (reader.Read() && inArmorSection == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Armor")) {
                inArmorSection = true;
                break;
            }
        }
        while (reader.Read()) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Armor")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("BaseItem")) {

                items[id] = new ArmorItem(id, name, armor, reduction, title, amount, worth);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.HasAttributes) {
                    name = reader.GetAttribute("name");
                }
                else if (reader.Name.Equals("title")) {
                    reader.Read();
                    title = reader.Value;
                }
                else if (reader.Name.Equals("worth")) {
                    reader.Read();
                    worth = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("amount")) {
                    reader.Read();
                    amount = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("armor")) {
                    reader.Read();
                    armor = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("reduction")) {
                    reader.Read();
                    reduction = float.Parse(reader.Value);
                }
                else if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }
    public void getBlessings1() {
        bool inBlessing1Section = false;
        string name = "";
        string title = "";
        string type = "";
        float percentage = 0.0f;
        int id = 0;
        int worth = 0;
        int amount = 0;
        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));
        while (reader.Read() && inBlessing1Section == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Blessing1")) {
                inBlessing1Section = true;
                break;
            }
        }
        while (reader.Read()) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Blessing1")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("BaseItem")) {
                items[id] = new BlessingItem(id, name, type, percentage, title, amount, worth);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.HasAttributes) {
                    name = reader.GetAttribute("name");
                }
                else if (reader.Name.Equals("title")) {
                    reader.Read();
                    title = reader.Value;
                }
                else if (reader.Name.Equals("worth")) {
                    reader.Read();
                    worth = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("amount")) {
                    reader.Read();

                    amount = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("type")) {
                    reader.Read();
                    type = reader.Value;
                }
                else if (reader.Name.Equals("modifier")) {
                    reader.Read();
                    percentage = float.Parse(reader.Value);
                }
                else if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }
    public void getBlessings2() {
        bool inBlessing2Section = false;
        string name = "";
        string title = "";
        string type = "";
        float percentage = 0.0f;
        int id = 0;
        int worth = 0;
        int amount = 0;
        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));
        while (reader.Read() && inBlessing2Section == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Blessing2")) {
                inBlessing2Section = true;
                break;
            }
        }
        while (reader.Read()) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Blessing2")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("BaseItem")) {
                items[id] = new Blessing2Item(id, name, type, percentage, title, amount, worth);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.HasAttributes) {
                    name = reader.GetAttribute("name");
                }
                else if (reader.Name.Equals("title")) {
                    reader.Read();
                    title = reader.Value;
                }
                else if (reader.Name.Equals("worth")) {
                    reader.Read();
                    worth = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("amount")) {
                    reader.Read();
                    amount = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("type")) {
                    reader.Read();
                    type = reader.Value;
                }
                else if (reader.Name.Equals("modifier")) {
                    reader.Read();
                    percentage = float.Parse(reader.Value);
                }
                else if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }

    public void getValuables() {
        bool inValuableSection = false;
        string name = "";
        string title = "";
        int worth = 0;
        int id = 0;
        int amount = 0;
        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));
        while (reader.Read() && inValuableSection == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Valuable")) {
                inValuableSection = true;
                break;
            }
        }
        while (reader.Read()) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Valuable")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("BaseItem")) {
                items[id] = new TradeItem(id, name, title, amount, worth);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.HasAttributes) {
                    name = reader.GetAttribute("name");
                }
                else if (reader.Name.Equals("title")) {
                    reader.Read();
                    title = reader.Value;
                }
                else if (reader.Name.Equals("worth")) {
                    reader.Read();
                    worth = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("amount")) {
                    reader.Read();
                    amount = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }

    public void getCraftables() {
        bool inSection = false;
        string name = "";
        string title = "";
        string desc = "";
        int id = 0;
        int worth = 0;
        int amount = 0;
        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));
        while (reader.Read() && inSection == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Craft")) {
                inSection = true;
                break;
            }
        }
        while (reader.Read()) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Craft")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("BaseItem")) {
                items[id] = new CraftItem(id, name, title, desc, amount, worth);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.HasAttributes) {
                    name = reader.GetAttribute("name");
                }
                else if (reader.Name.Equals("title")) {
                    reader.Read();
                    title = reader.Value;
                }
                else if (reader.Name.Equals("worth")) {
                    reader.Read();
                    worth = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("amount")) {
                    reader.Read();
                    amount = int.Parse(reader.Value);
                }
                else if (reader.Name.Equals("desc")) {
                    reader.Read();
                    desc = reader.Value;
                }
                else if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }

    public static void write(string path, Object obj) {
        XmlSerializer serializer = new XmlSerializer(typeof(Object));
        FileStream sw = new FileStream(path, FileMode.Create);
        serializer.Serialize(sw, obj);
        sw.Close();
    }
}
