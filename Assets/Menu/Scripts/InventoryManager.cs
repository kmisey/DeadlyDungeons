using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryManager : MonoBehaviour {

    public BaseItem lastPressedItem {
        get; set; }

    public PlayerModelSwitcher model;
    public GameObject inv_Group;
    public GameObject buttonPrefab;
    private Player player;
    private bool noItemsOnScreen = true;

    Sound sound;

    private void Awake() {
        sound = Sound.Instance;
    }

    private void OnGUI() {
        if (noItemsOnScreen) {
            player = Player.Instance;
            SpawnInventoryItems();
            updateScrollbar();
        }
    }



    private void Update() {

    }

    public void pressedEquipButton() {
        if(lastPressedItem != null) {
            sound.play(sound.storeSound);
            if (lastPressedItem.title.Contains("Stardust")) {
                if (player.Level < 5) {
                    Notify.Instance.showMessage("Level 5 is required to equip");
                    return;
                }
            }
            else if (lastPressedItem.title.Contains("Erra") || lastPressedItem.title.Contains("Vozium")) {
                if (player.Level < 10) {
                    Notify.Instance.showMessage("Level 10 is required to equip");
                    return;
                }
            }
            else if (lastPressedItem.title.Contains("7th")) {
                if (player.Level < 15) {
                    Notify.Instance.showMessage("Level 15 is required to equip");
                    return;
                }
            }
            player.equip(lastPressedItem);
            refresh();
            PlayerModelSwitcher.Instance.clearModel();
            PlayerModelSwitcher.Instance.spawnModel();
        }
    }

    public void refresh() {
        clearInventoryItems();
        SpawnInventoryItems();
    }

    public void clearInventoryItems() {
        foreach (Transform t in inv_Group.transform) {
            Destroy(t.gameObject);
        }
        ItemDescription.Instance.resetText();
        noItemsOnScreen = true;
    }

    public Scrollbar scrollbar;
    public GameObject scrollRegion;

    void updateScrollbar() {
        RectTransform region = scrollRegion.GetComponent<RectTransform>();

        int count = 0;
        foreach(Transform t in inv_Group.transform) {
            count += 1;
        }
        int extrarows = ((count / 4) - 2);

        if (count > 12) {
            region.sizeDelta = new Vector2(1, (float)(120 * extrarows));
            scrollbar.value = 1;
        }
    }
    
    public void SpawnInventoryItems() {
        noItemsOnScreen = false;
        for(int i = 0;i < player.inventory.Length; i++) {
            if(player.inventory[i] > 0) {
                addButton(player.db.items[i], player.inventory[i]);
            }
        }
        
    }

    void addButton(BaseItem item, int amount) {
        InventoryItem inst = new InventoryItem();
        GameObject temp = Instantiate(buttonPrefab) as GameObject;
        temp.transform.SetParent(inv_Group.gameObject.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        inst = temp.GetComponent<InventoryItem>();
        for (int i = 0; i < player.equipped.Length; i++) {
            if (player.equipped[i] == item.id) {
                inst.buttonInit(item, amount.ToString(), true);
                return;
            }
        }
        inst.buttonInit(item, amount.ToString(), false);
    }

}
