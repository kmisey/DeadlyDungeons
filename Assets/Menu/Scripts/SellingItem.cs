using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SellingItem : MonoBehaviour {


    public Image sprite;
    public Text amountOwned;
    public Text equippedStatus;

    public BaseItem item;
    public GameObject descObject;

    private bool equipStatus;
    private string spritepath;
    private string description;
    private string ownedNumber;

    Sound sound;
    // Use this for initialization
    void Start() {
        sound = Sound.Instance;
    }

    // Update is called once per frame
    void Update() {

    }

    public void pressed() {
        
        sound.play(sound.click2);
        GameObject.Find("SellDesc").GetComponent<ItemDescription>().updateDescription(item, description);
        GameObject.Find("sell").GetComponent<SellingInterface>().lastPressedItem = item;
    }

    public void buttonInit(BaseItem item, string num, bool status) {
        this.item = item;
        description = item.desc;
        ownedNumber = num;
        equipStatus = status;
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
