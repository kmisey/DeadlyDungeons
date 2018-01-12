using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupLoot : MonoBehaviour {

    public int amount;
    public string itemtoadd;

	IEnumerator OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StartCoroutine(addItemToPlayer());
        }
        yield return null;
    }

    IEnumerator addItemToPlayer() {
        string itemname = "";
        switch (itemtoadd) {
            case "key":
                Player.Instance.Keys += amount;
                itemname = "Divine Key";
                break;
            case "erra":
                itemname = "Erra Shrine";
                Player.Instance.ErraShrines += amount;
                break;
            case "vozium":
                itemname = "Vozium Shrine";
                Player.Instance.VoziumShrines += amount;
                break;
            case "gold":
                itemname = "Gold Shrine";
                Player.Instance.GoldShrines += amount;
                break;
            case "stardust":
                itemname = "Stardust Shrine";
                Player.Instance.StardustShrines += amount;
                break;
            case "coin":
                itemname = "Coins";
                Player.Instance.Money += amount;
                break;
            case "Default":
                itemname = "New Item";
                break;
        }
        PopupLog.Instance.showLog("+" + amount + " " + itemname);
        Destroy(gameObject);
        yield return null;
    }
}
