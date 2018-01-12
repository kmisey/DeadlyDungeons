using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class RecipeDatabase {

    string filename = "recipedatabase";
    public string path;
    public List<Recipe> recipes = new List<Recipe>();

    TextAsset textAsset;
    XmlDocument xmldoc;

    public RecipeDatabase() {
        textAsset = new TextAsset();
        xmldoc = new XmlDocument();
        textAsset = (TextAsset)Resources.Load(filename);
        xmldoc.LoadXml(textAsset.text);
        getAllItems();
    }

    public void getAllItems() {
        getRecipes();
    }

    public Recipe getIngredients(string itemname) {
        foreach(Recipe r in recipes) {
            if (r.resultname.Equals(itemname)) {
                return r;
            }
        }
        return new Recipe();
    }

    void getRecipes() {
        bool inSection = false;
        List<string> r = new List<string>();
        string resultname = "";
        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));
        while (reader.Read() && inSection == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals("Recipes")) {
                inSection = true;
                break;
            }
        }
        while (reader.Read() && inSection) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("Recipes")) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("recipe")) {
                recipes.Add(new Recipe(resultname, r.ToArray()));
                r.Clear();
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.HasAttributes) {
                    resultname = reader.GetAttribute("name");
                }
                else if (reader.Name.Equals("item")) {
                    reader.Read();
                    r.Add(reader.Value);
                }
            }
        }
    }
}

