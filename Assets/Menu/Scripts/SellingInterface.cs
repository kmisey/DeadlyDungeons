using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SellingInterface : MonoBehaviour {


    public BaseItem lastPressedItem {
        get; set;
    }

    public Text coinAmount;
    public GameObject descObject;
    public GameObject sell_Group;
    public GameObject buttonPrefab;
    private Player player;
    private bool noItemsOnScreen = true;

    Sound sound;

    private void Awake() {
        sound = Sound.Instance;
    }

    private void OnGUI() {
        if (noItemsOnScreen) {
            StartCoroutine(openedUI());
            noItemsOnScreen = false;
        }
    }

    IEnumerator openedUI() {
        player = Player.Instance;
        StartCoroutine(updateItems());
        yield return null;
    }

    public void closedUI(string toMenu) {
        noItemsOnScreen = true;
        lastPressedItem = null;
        clearItems();
        MenuManager.Instance.changeMenu(toMenu);
    }



    private void Update() {

    }

    public void pressedSellButton() {
        if (lastPressedItem != null) {
            sound.play(sound.storeSound);
            player.sellItem(lastPressedItem.id);
            refresh();
        }
    }

    public void refresh() {
        StartCoroutine(updateItems());
    }

    IEnumerator updateItems() {
        bool notequipped = true;
        coinAmount.text = player.Money.ToString();
        if (lastPressedItem != null) {
            //Check for if equipped item or not so no negative values possible
            if (player.inventory[lastPressedItem.id] == 1) {
                for (int i = 0; i < player.equipped.Length; i++) {
                    if (player.equipped[i] == lastPressedItem.id) {
                        if (player.inventory[lastPressedItem.id] == 1) {
                            descObject.GetComponent<ItemDescription>().resetText();
                            foreach (Transform t in sell_Group.transform) {
                                if (t.GetComponent<InventoryItem>().item.name == lastPressedItem.name) {
                                    Destroy(t.gameObject);
                                    //Debug.Log("destroy this item!");
                                    lastPressedItem = null;
                                    notequipped = false;
                                    //noItemsOnScreen = true;
                                }
                            }
                        }
                    }
                }
                if (notequipped) {
                    descObject.GetComponent<ItemDescription>().updateDescription(lastPressedItem, lastPressedItem.sellDesc);
                    foreach (Transform t in sell_Group.transform) {
                        if (t.GetComponent<InventoryItem>().item.name == lastPressedItem.name) {
                            t.GetComponent<InventoryItem>().amountOwned.text = (int.Parse(t.GetComponent<InventoryItem>().amountOwned.text) - 1).ToString();
                        }
                    }
                }
            }
            //Standard decrement
            else if (player.inventory[lastPressedItem.id] > 1) {
                descObject.GetComponent<ItemDescription>().updateDescription(lastPressedItem, lastPressedItem.sellDesc);
                foreach (Transform t in sell_Group.transform) {
                    if(t.GetComponent<InventoryItem>().item.name == lastPressedItem.name) {
                        t.GetComponent<InventoryItem>().amountOwned.text = (int.Parse(t.GetComponent<InventoryItem>().amountOwned.text) - 1).ToString();
                    }
                }
            }
            //if item is 0
            else if (player.inventory[lastPressedItem.id] == 0) {
                //Debug.Log("item should be deleted.");
                descObject.GetComponent<ItemDescription>().resetText();
                foreach (Transform t in sell_Group.transform) {
                    if (t.GetComponent<InventoryItem>().item.name == lastPressedItem.name) {
                        Destroy(t.gameObject);
                        //Debug.Log("destroy this item!");
                        lastPressedItem = null;
                        //noItemsOnScreen = true;
                    }
                }
            }
        }
        else {
            descObject.GetComponent<ItemDescription>().resetText();
            lastPressedItem = null;
            StartCoroutine(SpawnItems());
            noItemsOnScreen = true;
        }
        yield return null;
    }


    public Scrollbar scrollbar;
    public GameObject scrollRegion;

    void updateScrollbar() {
        RectTransform region = scrollRegion.GetComponent<RectTransform>();

        int count = 0;
        foreach (Transform t in sell_Group.transform) {
            count += 1;
        }
        int extrarows = ((count / 4) - 1);

        Debug.Log(count);
        if (count > 8) {
            region.sizeDelta = new Vector2(1, (float)(120 * extrarows));
            scrollbar.value = 1;
        }
    }

    void clearItems() {
        foreach (Transform t in sell_Group.transform) {
            Destroy(t.gameObject);
        }
    }

    public IEnumerator SpawnItems() {
        bool skipthisitem = false;
        coinAmount.text = player.Money.ToString();
        noItemsOnScreen = false;
        Debug.Log("Spawn Sell Screen Items!");
        for (int i = 0; i < player.inventory.Length; i++) {
            if (player.inventory[i] > 0) {
                for(int j = 0; j < player.equipped.Length; j++) {
                    if (i == player.equipped[j] && player.inventory[i] == 1) {
                        skipthisitem = true;
                    }
                }
                if (!skipthisitem) {
                    addButton(player.db.items[i], player.inventory[i]);
                }
                skipthisitem = false;
            }
        }
        if (lastPressedItem == null) {
            descObject.GetComponent<ItemDescription>().resetText();
        }

        updateScrollbar();
        yield return null;
    }

    void addButton(BaseItem item, int amount) {
        int amountAbleToBeSold = amount;
        for (int i = 0; i < player.equipped.Length; i++) {
            if (player.equipped[i] == item.id) {
                if (player.inventory[item.id] <= 1) {

                }
                else {
                    amountAbleToBeSold -= 1;
                    break;
                }
            }
        }
        InventoryItem inst = new InventoryItem();
        GameObject temp = Instantiate(buttonPrefab) as GameObject;
        temp.transform.SetParent(sell_Group.gameObject.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        inst = temp.GetComponent<InventoryItem>();
        inst.buttonInit(item, amountAbleToBeSold.ToString(), false);
    }

}
