using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CraftingInterfaceItem : MonoBehaviour {

    public Image sprite;
    public Text amountOwned;

    public BaseItem item;

    private bool equipStatus;
    private string spritepath;
    private string description;
    private string ownedNumber;
    Sound sound;
    // Use this for initialization
    void Start() {

       sound  = Sound.Instance;
    }

    // Update is called once per frame
    void Update() {

    }

    public void buttonInit(BaseItem item, string num) {
        gameObject.name = item.name;
        this.item = item;
        description = item.desc;
        ownedNumber = num;
        spritepath = item.path;
        updateButton();
    }

    public void updateButton() {
        sprite.overrideSprite = Resources.Load(spritepath, typeof(Sprite)) as Sprite;
        amountOwned.text = ownedNumber;
    }

    public void toggled() {
        Sound sound = Sound.Instance;
        sound.play(sound.click2);
        CraftingInterface.Instance.toggledPressed();
    }

    public void resultPressed() {
        CraftingInterface.Instance.lastPressedItem = item;
    }
}
