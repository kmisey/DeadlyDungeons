using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class TradeItem : BaseItem {
    


    public TradeItem(int id, string name, string title, int amount, int worth) {
        this.id = id;
        this.name = name;
        this.title = title;
        this.amount = amount;
        this.worth = worth;
        path = "Items/Sprites/" + name + "sprite";

        desc = "A valuable item that could be sold to the traders.";
        sellDesc = desc + "\nSells for: " + ((int)(worth * 0.75f)) + "g";
        buyDesc = desc + "\nBuy for: " + ((int)(worth)) + "g";
    }
	
}
