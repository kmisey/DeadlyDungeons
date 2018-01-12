using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class CraftItem : BaseItem {
    


    public CraftItem(int id, string name, string title, string desc, int amount, int worth) {
        this.id = id;
        this.name = name;
        this.title = title;
        this.desc = desc;
        this.amount = amount;
        this.worth = worth;
        path = "Items/Sprites/" + name + "sprite";

        sellDesc = desc + "\nSells for: " + ((int)(worth * 0.75f)) + "g";
        buyDesc = desc + "\nBuy for: " + ((int)(worth)) + "g";
    }
	
}
