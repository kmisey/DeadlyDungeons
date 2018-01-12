using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyingInterface : MonoBehaviour {



    public BaseItem lastPressedItem {
        get; set;
    }

    public TraderDatabase shop;

    public Text coinAmount;
    public GameObject descObject;
    public GameObject buy_Group;
    public GameObject buttonPrefab;
    private Player player;
    private bool noItemsOnScreen = true;

    Sound sound;

    private void Awake() {
        sound = Sound.Instance;
        shop = new TraderDatabase();
    }

    public void updateCoins() {
        coinAmount.text = player.Money.ToString();
    }

    private void OnGUI() {
        if (noItemsOnScreen) {
            player = Player.Instance;
            SpawnItems();
        }
    }



    private void Update() {

    }

    public void pressedBuyButton() {
        if (lastPressedItem != null) {
            sound.play(sound.storeSound);
            player.buyItem(lastPressedItem.id);
            coinAmount.text = player.Money.ToString();
        }
    }

    public void refresh() {
        clearItems();
        SpawnItems();
    }

    public void clearItems() {
        foreach (Transform t in buy_Group.transform) {
            Destroy(t.gameObject);
        }
        descObject.GetComponent<ItemDescription>().resetText();
        noItemsOnScreen = true;
    }

    public Scrollbar scrollbar;
    public GameObject scrollRegion;

    void updateScrollbar() {
        RectTransform region = scrollRegion.GetComponent<RectTransform>();

        int count = 0;
        foreach (Transform t in buy_Group.transform) {
            count += 1;
        }
        int extrarows = ((count / 4) - 2);
        if(count > 12) {
            region.sizeDelta = new Vector2(1, (float)(120 * extrarows));
            scrollbar.value = 1;
        }
        
    }

    public void SpawnItems() {
        lastPressedItem = null;
        coinAmount.text = player.Money.ToString();
        noItemsOnScreen = false;
        Debug.Log("Spawn Buy Screen Items!");
        for (int i = 0; i < shop.items.Count; i++) {

            //shop.items[i] - contains the item id, so we can use that to add it form the item database
            addButton(player.db.items[shop.items[i]]);
        }
        updateScrollbar();
    }

    void addButton(BaseItem item) {
        InventoryItem inst = new InventoryItem();
        GameObject temp = Instantiate(buttonPrefab) as GameObject;
        temp.transform.SetParent(buy_Group.gameObject.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        inst = temp.GetComponent<InventoryItem>();
        inst.buyInit(item);
    }
}
