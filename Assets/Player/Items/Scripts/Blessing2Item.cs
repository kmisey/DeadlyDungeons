using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Blessing2Item : BaseItem {


    public bType type;
    public float percentage;

    public Blessing2Item() {

    }

    public Blessing2Item(int id, string name, string type, float percentage, string title, int amount, int worth) {
        this.id = id;
        this.name = name;
        this.percentage = percentage;
        this.title = title;
        this.amount = amount;
        this.worth = worth;
        desc = "";

        if (type.Equals("movement")) {
            desc += "This utility blessing grants bonus movement speed.\nMovespeed: " + (int)(percentage * 100) + "%";
            this.type = bType.movement;
        }
        else if (type.Equals("luck")) {
            desc += "This utility blessing grants bonus rewards.\nLuck: " + (int)(percentage * 100) + "%";
            this.type = bType.luck;
        }
        sellDesc = desc + "\nSells for: " + ((int)(worth * 0.75f)) + "g";
        buyDesc = desc + "\nBuy for: " + ((int)(worth)) + "g";

        path = "Items/Sprites/" + name + "sprite";
    }
	
}
