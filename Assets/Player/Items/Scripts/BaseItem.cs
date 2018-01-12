using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class BaseItem {

    public string modelpath;
    public int id;
    public string name;
    public string title;
    public string path;
    public int amount;
    public int worth;
    public string desc;
    public string buyDesc;
    public string sellDesc;


    public BaseItem() {
        
    }
}
