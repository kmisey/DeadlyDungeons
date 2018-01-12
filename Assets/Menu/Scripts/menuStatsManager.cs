using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuStatsManager : MonoBehaviour {

    public Text statsText;

    private string str;
    private Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateStats() {
        player = Player.Instance;
        if(gameObject.activeSelf) {
            StartCoroutine(player.updateStats());
        }
        str = "";
        str += "Hero: " + player.playerName +
            "\nGold: " + player.Money +
            "\nLevel: " + player.Level +
            "\nCrafting Level: " + player.Craftlevel +
            "\nDamage: " + player.Damage +
            "\nArmor: " + player.Armor +
            "\nCrit: " + ((int)(100.0f * player.Crit)) + " % " +
            "\nLifesteal : " + ((int)(100.0f * player.Lifesteal)) + " % " +
            "\nLoot: " + ((int)(100.0f * player.Loot)) + " % " +
            "\nMovespeed: " + ((int)(100.0f * player.Mspeed)) + " % ";

        statsText.text = str;
    }
}
