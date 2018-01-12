using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public enum bType {
    lifesteal, damage, reduction, crit, movement, luck
}

public class BlessingItem : BaseItem {

    public bType type;
    public float percentage;

    public BlessingItem() {

    }

    public BlessingItem(int id, string name, string type, float percentage, string title, int amount, int worth) {
        this.id = id;
        this.name = name;
        this.percentage = percentage;
        this.title = title;
        this.amount = amount;
        this.worth = worth;
        desc = "";
        if (type.Equals("lifesteal")) {
            desc += "This offensive blessing grants bonus lifesteal. (Heals for damage dealt)\nLifesteal: " + (int)(percentage * 100) + "%";
            this.type = bType.lifesteal;
        }
        else if (type.Equals("damage")) {
            desc += "This offensive blessing grants bonus damage.\nBonus Dmg.: " + (int)(percentage * 100) + "%";
            this.type = bType.damage;
        }
        else if (type.Equals("reduction")) {
            desc += "This offensive blessing grants bonus damage reduction.\nDmg. Red.: " + (int)(percentage * 100) + "%";
            this.type = bType.reduction;
        }
        else if (type.Equals("crit")) {
            desc += "This offensive blessing grants bonus critical strike chance.\nCrit. Chance: " + (int)(percentage * 100) + "%";
            this.type = bType.crit;
        }
        sellDesc = desc + "\nSells for: " + ((int)(worth * 0.75f)) + "g";
        buyDesc = desc + "\nBuy for: " + ((int)(worth)) + "g";
        path = "Items/Sprites/" + name + "sprite";
        
    }
	
}
