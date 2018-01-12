using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class WeaponItem : BaseItem {

    public int damage;
    public float crit;
    public float lifesteal;
    public float bonusdmg;
    

    public WeaponItem() {

    }

    public WeaponItem(int id, string name, int damage, float crit, float lifesteal, float bonusdmg, string title, int amount, int worth) {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.crit = crit;
        this.lifesteal = lifesteal;
        this.bonusdmg = bonusdmg;
        this.title = title;
        this.amount = amount;
        this.worth = worth;

        modelpath = "Items/Models/" + name;
        path = "Items/Sprites/" + name + "sprite";

        desc = "";
        desc += "Damage: " + damage;
        if(crit > 0.00f) {
            desc += "\nCrit Chance: " + (int)(crit * 100) + "%";
        }
        if(lifesteal > 0.00f) {
            desc += "\nLifesteal: " + (int)(lifesteal * 100) + "%";
        }
        if (bonusdmg > 0.00f) {
            desc += "\nBonus Damage: " + (int)(bonusdmg * 100) + "%";
        }
        sellDesc = desc + "\nSells for: " + ((int)(worth * 0.75f)) + "g";
        buyDesc = desc + "\nBuy for: " + ((int)(worth)) + "g";
    }
}
