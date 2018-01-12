using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CraftingInterface : MonoBehaviour {

    public static CraftingInterface Instance { get; set; }

    public BaseItem lastPressedItem {
        get; set;
    }

    public GameObject ing_Group;
    public GameObject result_Group;

    public ToggleGroup ingredientsGroup;
    public ToggleGroup resultsGroup;

    public GameObject resultPrefab;
    public GameObject buttonPrefab;

    public Animator craftinganim;

    private Player player;
    private bool noItemsOnScreen = true;

    private int goldLvl = 1;
    private int stardustLvl = 5;
    private int godLvl = 10;

    public void pressedCraftButton() {
        if (!resultsGroup.AnyTogglesOn()) {
            return;
        }
        if (resultsGroup.AnyTogglesOn()) {
            int temp = lastPressedItem.id;
            BaseItem resultItem = player.db.items[temp];

            if (resultItem.title.Contains("Stardust")) {
                if (player.Craftlevel < 5) {
                    Notify.Instance.showMessage("Level 5 crafting is required");
                    return;
                }
            }
            else if (resultItem.title.Contains("Erra") || resultItem.title.Contains("Vozium")) {
                if (player.Craftlevel < 10) {
                    Notify.Instance.showMessage("Level 10 crafting is required");
                    return;
                }
            }
            else if (resultItem.title.Contains("7th")) {
                if (player.Craftlevel < 15) {
                    Notify.Instance.showMessage("Level 15 crafting is required");
                    return;
                }
            }
            sound.play(sound.craftItem);
            player.addToInventory(temp);
            foreach (Transform t in ing_Group.transform) {
                if (t.GetComponent<Toggle>().isOn) {
                    for (int i = 0; i < requiredOre(resultItem); i++) {
                        BaseItem tem = t.GetComponent<CraftingInterfaceItem>().item;
                        player.removeFromInventory(tem.id);
                    }
                }
            }
            craftinganim.SetTrigger("craft");
            player.addCraftingExperience(resultItem.title);
            clearItems();
            clearResults();
            SpawnItems();

        }

            
    }

    public void clearItems() {
        foreach (Transform t in ing_Group.transform) {
            Destroy(t.gameObject);
        }
    }

    Sound sound;

    private void Awake() {
        Instance = this;
        sound = Sound.Instance;
    }

    private void OnGUI() {
        if (noItemsOnScreen) {
            player = Player.Instance;
            SpawnItems();
            updateScrollbar();
        }
    }

    public void closed() {
        noItemsOnScreen = true;
    }

    public Scrollbar scrollbar;
    public GameObject scrollRegion;

    void updateScrollbar() {
        RectTransform region = scrollRegion.GetComponent<RectTransform>();

        int count = 0;
        foreach (Transform t in ing_Group.transform) {
            count += 1;
        }
        int extrarows = ((count / 4) - 2);

        if (count > 12) {
            region.sizeDelta = new Vector2(1, (float)(120 * extrarows));
            scrollbar.value = 1;
        }
    }

    public void toggledPressed() {
        spawnResults();
    }

    public void SpawnItems() {
        noItemsOnScreen = false;

        for (int i = 0; i < player.inventory.Length; i++) {
            if (player.inventory[i] > 0) {
                BaseItem temp = (player.db.items[i]);
                if (temp.GetType().Equals(typeof(CraftItem))) {

                    addIng(temp, player.inventory[i]);
                }
            }
        }
        
    }

    void addIng(BaseItem item, int amount) {
        CraftingInterfaceItem inst = new CraftingInterfaceItem();
        GameObject temp = Instantiate(buttonPrefab) as GameObject;
        temp.transform.SetParent(ing_Group.gameObject.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        inst = temp.GetComponent<CraftingInterfaceItem>();
        temp.GetComponent<Toggle>().group = ingredientsGroup;
        inst.buttonInit(item, amount.ToString());
    }



    public void spawnResults() {
        clearResults();
        Recipe tRecipe;
        BaseItem tempItem;
        int resID;
        int oreID;
        string orename = "";

        HashSet<string> temp = new HashSet<string>();

        foreach(Transform t in ing_Group.transform) {
            if (t.GetComponent<Toggle>().isOn) {
                if (t.name.Contains("ore")) {
                    orename = t.name;
                }
                temp.Add(t.GetComponent<CraftingInterfaceItem>().name);
            }
        }
        for(int i = 0;i < player.recipes.recipes.Count; i++) {

            tRecipe = player.recipes.recipes[i];

            if (temp.SetEquals(tRecipe.set)) 
                {
                tempItem = player.db.items[player.db.findID(tRecipe.resultname)];
                oreID = player.db.items[player.db.findID(orename)].id;

                if(player.inventory[oreID] >= requiredOre(tempItem)) 
                    {
                    addRes(tempItem);
                }
            }
        }
    }

    public void clearInventory() {
        foreach (Transform t in ing_Group.transform) {
            Destroy(t.gameObject);
        }
    }

    public void clearResults() {
        foreach (Transform t in result_Group.transform) {
            Destroy(t.gameObject);
        }
    }

    void addRes(BaseItem item) {
        CraftingInterfaceItem inst = new CraftingInterfaceItem();
        GameObject temp = Instantiate(resultPrefab) as GameObject;
        temp.transform.SetParent(result_Group.gameObject.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        inst = temp.GetComponent<CraftingInterfaceItem>();
        temp.GetComponent<Toggle>().group = resultsGroup;
        inst.buttonInit(item, "");
    }

    int requiredOre(BaseItem item) {
        if (item.GetType().Equals(typeof(WeaponItem))) {
            return 3;
        }
        else if (item.GetType().Equals(typeof(ArmorItem))) {
            armorType t = ((ArmorItem)item).type;
            if (t.Equals(armorType.helmet)) {
                return 2;
            }
            if (t.Equals(armorType.chest)) {
                return 4;
            }
            if (t.Equals(armorType.legs)) {
                return 3;
            }
            if (t.Equals(armorType.shield)) {
                return 3;
            }
        }
        return -1;
    }

}
