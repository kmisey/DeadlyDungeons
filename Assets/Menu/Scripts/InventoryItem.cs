using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItem : MonoBehaviour {

    public string desc;
    public string parentstring;

    Sound sound;

    public Image sprite;
    public Text amountOwned;
    public Text equippedStatus;

    public BaseItem item;

    private bool equipStatus;
    private string spritepath;
    private string description;
    private string ownedNumber;

	// Use this for initialization
	void Start () {
        sound = Sound.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pressed() {
        sound.play(sound.click2);
        GameObject t;
        t = GameObject.Find(desc);
        t.GetComponent<ItemDescription>().updateDescription(item, description);
        t = GameObject.Find(parentstring);
        if (parentstring == "") {

        }
        else if(parentstring == "inventory") {
            t.GetComponent<InventoryManager>().lastPressedItem = item;
        }
        else if(parentstring == "sell"){
            t.GetComponent<SellingInterface>().lastPressedItem = item;
        }
        else if (parentstring == "buy") {
            t.GetComponent<BuyingInterface>().lastPressedItem = item;
        }
    }

    public void buyingPressed() {
        sound.play(sound.click2);
        GameObject t;
        t = GameObject.Find(desc);
        t.GetComponent<ItemDescription>().updateDescription(item, item.buyDesc);
        t = GameObject.Find(parentstring);
        t.GetComponent<BuyingInterface>().lastPressedItem = item;
}

    public void rewardPressed() {
        sound.play(sound.click2);
        GameObject t;
        t = GameObject.Find(desc);
        t.GetComponent<Text>().text = item.title;
    }

    public void buttonInit(BaseItem item, string num, bool status) {
        this.item = item;
        description = item.sellDesc;
        ownedNumber = num;
        equipStatus = status;
        spritepath = item.path;
        updateButton();
    }

    public void buyInit(BaseItem item) {
        this.item = item;
        description = item.buyDesc;
        spritepath = item.path;
        Debug.Log(spritepath);
        updateButton();
    }

    public void updateButton() {
        if (equipStatus) {
            equippedStatus.text = "E";
        }
        else {
            equippedStatus.text = "";
        }
        sprite.overrideSprite = Resources.Load(spritepath, typeof(Sprite)) as Sprite;
        amountOwned.text = ownedNumber;
    }
}
