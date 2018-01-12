using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.IO;

public class GrantReward : MonoBehaviour {

    public bool dungeonChest;
    public int keysRequired;
    public Text keyAmount;
    public Text pressedInfo;
    public GameObject button;
    public GameObject panel;
    public GameObject container;
    public GameObject itemPrefab;

    public Button normalRewardButton;
    public Button doubleRewardButton;

    public int rewardMultiplier;
    public int maxSmallReward, maxMedReward, maxLargeReward;

    public GameObject wavePanel;

    List<BaseItem> reward;
    int[] amount = new int[100];
    int index = 0;

    private Player player;


    public int rewardSize {
        get; set; }

    public static GrantReward Instance { get; set; }

    private void Awake() {
        Instance = this;
        if (dungeonChest) {

            gameObject.SetActive(false);
        }
    }

    private void Start() {
        player = Player.Instance;
        sound = Sound.Instance;
        getAllItems();
        for(int i = 0;i < amount.Length; i++) {
            amount[i] = 0;
        }
        reward = new List<BaseItem>();
        int dungeonSize = ZPlayerPrefs.GetInt("sp_DungeonSize");
        if (dungeonSize == 1) {
            rewardSize = maxSmallReward;
        }
        else if (dungeonSize == 2) {
            rewardSize = maxMedReward;
        }
        else if (dungeonSize == 3) {
            rewardSize = maxLargeReward;
        }
        else {
            rewardSize = 30;
        }
    }

    bool checkedKeys = false;

    private void OnGUI() {
        checkedKeys = false;
        if (!checkedKeys && keysRequired > 0) {
            keyAmount.text = player.Keys.ToString();
            if (player.Keys < keysRequired) {
                button.SetActive(false);
                checkedKeys = true;
            }
            else {
                button.SetActive(true);
                checkedKeys = true;
            }
        }
    }
    
    public void setRewardModifier(int modifier) {
        rewardMultiplier *= modifier;
    }

    public void clearReward() {
        reward.Clear();
        for (int i = 0; i < amount.Length; i++) {
            amount[i] = 0;
        }
        clearItems();
        pressedInfo.text = "press an item for its name";
        panel.SetActive(false);
    }



    public void openChest() {
        if(normalRewardButton != null && doubleRewardButton != null) {
            normalRewardButton.interactable = false;
            doubleRewardButton.interactable = false;
        }
        if (player.Keys >= keysRequired) {
            rollReward();
            if(keysRequired != -1) {
                player.Keys -= keysRequired;
                keyAmount.text = player.Keys.ToString();
            }
            spawnReward();
        }
        else {
            Debug.Log("not enough keys");
        }
    }

    public void waveChest() {
        Waves.Instance.giveReward();
        Debug.Log("reward size of: " + rewardSize);
        rollReward();
        spawnReward();
    }

    public void clearWaveReward() {
        reward.Clear();
        for (int i = 0; i < amount.Length; i++) {
            amount[i] = 0;
        }
        clearItems();
        pressedInfo.text = "press an item for its name";
        panel.SetActive(false);
    }

    public void openWavePanel() {
        wavePanel.SetActive(true);
    }

    Sound sound;

    public void spawnReward() {
        for(int i = 0;i < reward.Count; i++) {
            Debug.Log(reward[i].title);
            addButton(reward[i], amount[i]);
            player.addToInventory(reward[i].id, amount[i]);
        }
        sound.play(sound.openChest);
    }

    public void rollReward() {
        int numberOfRolls = (int)(Random.Range(rewardSize, rewardSize+1) * rewardMultiplier);
        BaseItem itemToBeAdded;
        for(int i = 0; i < ((int)(numberOfRolls*player.Loot)); i++) {
            int roll = Random.Range(0, 512);
            if((roll >= 246) && (roll < 503)) 
                {  //Common item
                itemToBeAdded = getItem(commonItems);
                if (itemToBeAdded != null) {
                        reward.Add(itemToBeAdded);
                }
            }
            else if((roll >= 503) && (roll < 508)) 
                { //Rare item
                itemToBeAdded = getItem(rareItems);
                if (itemToBeAdded != null) {
                    reward.Add(itemToBeAdded);
                }
            }
            else if((roll >= 508) && (roll < 511)) 
                { //Superrare item
                itemToBeAdded = getItem(superRareItems);
                if (itemToBeAdded != null) {
                    reward.Add(itemToBeAdded);
                }
            }
            else if((roll >= 511) && (roll < 512)) {
                itemToBeAdded = getItem(megaItems);
                if (itemToBeAdded != null) {
                    reward.Add(itemToBeAdded);
                }
            }
        }
    }


    BaseItem getItem(List<int> list) {
        BaseItem item = null;
        int random = Random.Range(0, list.Count);
        item = player.db.items[list[random]];
        for(int i = 0; i < reward.Count;i++) {
            if (reward[i].name == item.name) {
                amount[i] += 1;
                return null;
            }
        }
        amount[index++] += 1;
        return item;
    }

    public void clearItems() {
        foreach (Transform t in container.transform) {
            Destroy(t.gameObject);
        }
        index = 0;
    }


    public Scrollbar scrollbar;
    public GameObject scrollRegion;

    void updateScrollbar() {
        RectTransform region = scrollRegion.GetComponent<RectTransform>();

        int count = 0;
        foreach (Transform t in container.transform) {
            count += 1;
        }
        int extrarows = ((count / 6) - 1);

        Debug.Log(count);
        if (count > 12) {
            region.sizeDelta = new Vector2(1, (float)(120 * extrarows));
            scrollbar.value = 1;
        }
    }

    void addButton(BaseItem item, int amount) {
        InventoryItem inst = new InventoryItem();
        GameObject temp = Instantiate(itemPrefab) as GameObject;
        temp.transform.SetParent(container.gameObject.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        inst = temp.GetComponent<InventoryItem>();
        inst.buttonInit(item, amount.ToString(), false);
    }


    #region LoadingLootTables
    string filename = "loottable";
    private List<int> commonItems = new List<int>();
    private List<int> rareItems = new List<int>();
    private List<int> superRareItems = new List<int>();
    private List<int> megaItems = new List<int>();
    TextAsset textAsset = new TextAsset();
    XmlDocument xmldoc = new XmlDocument();


    public void getAllItems() {
        textAsset = new TextAsset();
        xmldoc = new XmlDocument();
        textAsset = (TextAsset)Resources.Load(filename);
        xmldoc.LoadXml(textAsset.text);
        getItems(commonItems, "Commons");
        getItems(rareItems, "Rares");
        getItems(superRareItems, "Supers");
        getItems(megaItems, "Megas");
    }

    public void getItems(List<int> list, string type) {
        bool inSection = false;
        int id = 0;

        XmlReader reader = XmlReader.Create((new StringReader(xmldoc.InnerXml)));

        while (reader.Read() && inSection == false) {
            if (reader.NodeType.Equals(XmlNodeType.Element) && reader.Name.Equals(type)) {
                inSection = true;
                break;
            }
        }
        while (reader.Read()) {
            if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals(type)) {
                break;
            }
            else if (reader.NodeType.Equals(XmlNodeType.EndElement) && reader.Name.Equals("item")) {
                list.Add(id);
            }
            else if (reader.NodeType.Equals(XmlNodeType.Element)) {
                if (reader.Name.Equals("id")) {
                    reader.Read();
                    id = int.Parse(reader.Value);
                }
            }
        }
    }
    #endregion
}
