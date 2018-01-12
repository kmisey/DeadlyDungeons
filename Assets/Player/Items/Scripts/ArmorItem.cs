using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public enum armorType {
    helmet, chest, legs, shield, cape
}

public class ArmorItem : BaseItem {

    

    public armorType type;
    public int armor;
    public float reduction;

    public ArmorItem() {

    }

    public ArmorItem(int id, string name, int armor, float reduction, string title, int amount, int worth) {
        this.id = id;
        this.name = name;
        this.armor = armor;
        this.reduction = reduction;
        this.title = title;
        this.amount = amount;
        this.worth = worth;
        modelpath = "Items/Models/" + name;
        path = "Items/Sprites/" + name + "sprite";

        if (name.Contains("helmet")) {
            type = armorType.helmet;
        }
        else if (name.Contains("plate")) {
            type = armorType.chest;
        }
        else if (name.Contains("legs")) {
            type = armorType.legs;
        }
        else if (name.Contains("shield")) {
            type = armorType.shield;
        }
        else if (name.Contains("cape")) {
            type = armorType.cape;
        }

        desc = "";
        desc += "Armor: " + armor;
        if (reduction > 0.0f) {
            desc += "\nDmg. Reduction: " + (int)(reduction * 100) + "%";
        }
        sellDesc = desc + "\nSells for: " + ((int)(worth * 0.75f)) + "g";
        buyDesc = desc + "\nBuy for: " + ((int)(worth)) + "g";
    }

}
