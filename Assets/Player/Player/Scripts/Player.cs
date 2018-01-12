using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum itemType {
    sword, helmet, platebody, legs, bonus, utility, shield, cape
}

public class Player : MonoBehaviour {

    public static Player Instance { set; get; }

    public ItemDatabase db;
    public PlayerModelSwitcher model;
    public RecipeDatabase recipes;
    public int[] inventory;
    public int[] equipped;

    public BaseItem[] equippedItems;
    //0 sword
    //1 helmet
    //2 platebody
    //3 legs
    //4 bonus
    //5 utility
    //6 shield
    //7 cape

    //stats





    public int Money {
        get { return money; }
        set { money = value;
            PlayerUI.Instance.updateLevel();
        }
    }
    public int GoldShrines {
        get { return goldShrines; }
        set { goldShrines = value; }
    }
    public int StardustShrines {
        get { return stardustShrines; }
        set { stardustShrines = value; }
    }
    public int ErraShrines {
        get { return erraShrines; }
        set { erraShrines = value; }
    }
    public int VoziumShrines {
        get { return voziumShrines; }
        set { voziumShrines = value; }
    }

    public int Keys {   get { return keys; }
                        set { keys = value; } }

    public int Level { get { return level; } }
    public string Color { get { return color; } }
    public int Gender { get { return gender; } }
    public int Experience { get { return exp; } }
    public int Craftlevel { get { return craftlevel; } }
    public int DamageLevel { get { return damageLevel; } }
    public int ArmorLevel { get { return armorLevel; } }
    public int TotalLevel { get { return totalLevel; } }
    public int Damage { get { return damage; } }
    public int Armor { get { return armor; } }
    public float Lifesteal { get { return lifesteal; } }
    public float Crit { get { return crit; } }
    public float Loot { get { return loot; } }
    public float Mspeed { get { return mspeed; } }
    public bool HasAds { get { return hasAds; } set { hasAds = value; } }

    public bool SoundOn { get { return soundOn; } set { soundOn = value;
            if(value == false) {
                ZPlayerPrefs.SetInt("soundon", 0);
            }
            else {
                ZPlayerPrefs.SetInt("soundon", 1);
            }
            Sound.Instance.GetVolume();
        } }
    public bool MusicOn { get { return musicOn; } set {
            if (value == false) {
                ZPlayerPrefs.SetInt("musicon", 0);
            }
            else {
                ZPlayerPrefs.SetInt("musicon", 1);
            }
            musicOn = value;
            Sound.Instance.GetVolume();
        } }


    public string playerName { get; set; }
    int money = 0;
    int gender = 0;
    string color = "red";
    int keys = 0;
    int level = 1;
    int craftlevel = 1;
    int damageLevel = 1;
    int armorLevel = 1;
    int totalLevel = 2;
    int exp = 0;
    int craftexp = 0;
    int damage = 0;
    int armor = 0;
    float lifesteal = 0.00f;
    float crit = 0.00f;
    float loot = 1.00f;
    float mspeed = 1.00f;
    bool hasAds = true;
    bool soundOn = true;
    bool musicOn = true;

    int erraShrines = 0;
    int voziumShrines = 0;
    int goldShrines = 0;
    int stardustShrines = 0;

    float version_id = 1.00f;

    private void Awake() {
        //Insert some sort of app store check;
        ZPlayerPrefs.Initialize("password", SystemInfo.deviceUniqueIdentifier);
        Instance = this;

        equipped = new int[8];
        equippedItems = new BaseItem[8];
        recipes = new RecipeDatabase();
        db = new ItemDatabase();
        inventory = new int[db.items.Count];
        if (!ZPlayerPrefs.HasKey("SaveString")) {
            firstStart();
        }
        Application.targetFrameRate = ZPlayerPrefs.GetInt("framerate");
        initEquippedItemsArrayTypes();
        loadPlayerData();
        loadEquippedItemsArray();
        StartCoroutine(updateStats());
        model.spawnModel();
    }

    void Start() {
        PlayerUI.Instance.updateAll();
    }

    private void checkAppStore() {

    }


    private void playerStart() {
    }

    private void OnApplicationQuit() {
        save();
    }

    public void lastChosenDungeon(int size) {
        //1 - small, 2 - medium, 3 - large
        ZPlayerPrefs.SetInt("sp_DungeonSize", size);
    }

    #region player equipped items

    void initEquippedItemsArrayTypes() {
        equippedItems[0] = new WeaponItem();
        equippedItems[1] = new ArmorItem();
        equippedItems[2] = new ArmorItem();
        equippedItems[3] = new ArmorItem();
        equippedItems[4] = new BlessingItem();
        equippedItems[5] = new Blessing2Item();
        equippedItems[6] = new ArmorItem();
        equippedItems[7] = new ArmorItem();
    }

    void loadEquippedItemsArray() {
        for(int i = 0; i < equipped.Length; i++) {
            if(equipped[i] >= 0) {
                equippedItems[i] = db.items[equipped[i]];
            }
            else {
                equippedItems[i] = new BaseItem();
            }
        }
    }

    public IEnumerator updateStats() {
        StartCoroutine(calculateDamage());
        StartCoroutine(calculateArmor());
        StartCoroutine(calculateCrit());
        StartCoroutine(calculateLifesteal());
        StartCoroutine(calculateMovement());
        StartCoroutine(calculateLuck());
        StartCoroutine(calculateDamageLevel());
        StartCoroutine(calculateArmorLevel());
        StartCoroutine(calculateTotalLevel());
        yield return null;
    }

    public void giveAllItems() {
        for(int i = 0; i < inventory.Length; i++) {
            inventory[i] = Random.Range(0,12);
        }
        level = 21;
        keys = 39;
    }

    public void equip(BaseItem item) {
        if (item.GetType().Equals(typeof(WeaponItem))) {
            equippedItems[(int)itemType.sword] = (WeaponItem)item;
            equipped[(int)itemType.sword] = item.id;
            calculateDamage();
        }
        else if (item.GetType().Equals(typeof(ArmorItem))) {
            ArmorItem temp = (ArmorItem)item;
            int armortype = (int)temp.type;
            if (armortype == 0) {
                equippedItems[(int)itemType.helmet] = temp;
                equipped[(int)itemType.helmet] = item.id;
                PlayerUI.Instance.updatePicture();
            }
            else if(armortype == 1) {
                equippedItems[(int)itemType.platebody] = temp;
                equipped[(int)itemType.platebody] = item.id;
            }
            else if(armortype == 2) {
                equippedItems[(int)itemType.legs] = temp;
                equipped[(int)itemType.legs] = item.id;
            }
            else if (armortype == 3) {
                equippedItems[(int)itemType.shield] = temp;
                equipped[(int)itemType.shield] = item.id;
            }
            else if (armortype == 4) {
                equippedItems[(int)itemType.cape] = temp;
                equipped[(int)itemType.cape] = item.id;
            }
            calculateArmor();
        }
        else if (item.GetType().Equals(typeof(BlessingItem))) {
            equippedItems[(int)itemType.bonus] = (BlessingItem)item;
            equipped[(int)itemType.bonus] = item.id;
            calculateDamage();
            calculateArmor();
            calculateCrit();
            calculateLifesteal();
        }
        else if (item.GetType().Equals(typeof(Blessing2Item))) {
            equippedItems[(int)itemType.utility] = (Blessing2Item)item;
            equipped[(int)itemType.utility] = item.id;
            calculateMovement();
            calculateLuck();
        }
    }

    #endregion


    #region player data saving

    void firstStart() {
        ZPlayerPrefs.SetInt("soundon", 1);
        ZPlayerPrefs.SetInt("musicon", 1);
        ZPlayerPrefs.SetInt("framerate", 60);
        Application.targetFrameRate = ZPlayerPrefs.GetInt("framerate");
        money = 0;
        keys = 0;
        level = 1;
        craftlevel = 1;
        exp = 0;
        damage = 0;
        armor = 0;
        lifesteal = 0.00f;
        crit = 0.00f;
        loot = 1.00f;
        mspeed = 1.00f;
        ZPlayerPrefs.SetString("SaveString", "");
        ZPlayerPrefs.SetInt("noAds", 1);

        playerName = SystemInfo.deviceName;
        Debug.Log(SystemInfo.deviceName);
        for (int i = 0; i < inventory.Length; i++) {
            inventory[i] = 0;
        }

        giveDefaultItems();

        save();

        loadPlayerData();
        loadEquippedItemsArray();
        updateStats();
    }

    void giveDefaultItems() {

        inventory[0] += 1;
        inventory[5] += 1;
        inventory[6] += 1;
        inventory[7] += 1;
        inventory[39] += 1;

        equipped[0] = 0; //equip steelsword
        equipped[1] = 5; //steel helmet
        equipped[2] = 6; //plate
        equipped[3] = 7; //legs
        equipped[4] = -1; //bonus
        equipped[5] = -1; //utility
        equipped[6] = 39; //shield
        equipped[7] = -1; //utility
    }

    void loadHeroData() {
        string[] misc = PlayerPrefs.GetString("hero_data").Split('|');
        playerName = misc[0];
        gender = int.Parse(misc[1]);
        color = misc[2];
    }

    void loadPlayerData() {
        loadHeroData();
        loadShrines();
        if (ZPlayerPrefs.GetInt("musicon") == 0) {
            musicOn = false;
        }
        else {
            musicOn = true;
        }

        if (ZPlayerPrefs.GetInt("soundon") == 0) {
            soundOn = false;
        }
        else {
            soundOn = true;
        }

        string save = ZPlayerPrefs.GetString("SaveString");

        string[] data = save.Split('|'); //0 = stats, 1 = inventory, 2 = equipped

        //Getting Stats Section to memory
        string[] misc = data[0].Split('%');
        playerName = misc[0];
        level = int.Parse(misc[1]);
        money = int.Parse(misc[2]);
        exp = int.Parse(misc[3]);
        keys = int.Parse(misc[4]);
        craftlevel = int.Parse(misc[5]);
        craftexp = int.Parse(misc[6]);

        loot = ZPlayerPrefs.GetInt("noAds");

        //Getting inventory information to memory
        misc = data[1].Split('%');
        for(int i = 0; i < misc.Length-1; i++) {
            inventory[i] = int.Parse(misc[i]);
        }

        //We put items read from xml database into inventory.length before reading memory.
        //save this number because this will always be larger when we update.
        //We want to prevent null exceptions when we add new items to the game.
        int updatedNumberOfGameItems = inventory.Length;

        //Last saved items, so if there was an items update where ex: LastKnownNumberOfItems = 30, and the update added 10 (40 total),
        //we put 0's into the new spots of memory instead of errors.
        int lastKnownNumberOfItems = misc.Length-1;
        if (updatedNumberOfGameItems > lastKnownNumberOfItems) {
            Debug.Log("More items have been added to the game");
            for(int i = lastKnownNumberOfItems; i < updatedNumberOfGameItems; i++) {
                Debug.Log("new id: " + i + "  new item name item: " + db.items[i].name);
                inventory[i] = 0;
            }
        }

        //Getting equipped information to memory
        misc = data[2].Split('%');
        for (int i = 0; i < equipped.Length; i++) {
            equipped[i] = int.Parse(misc[i]);
        }

        //done
    }

    public void save() {
        ZPlayerPrefs.SetString("SaveString", getSaveString());
        saveShrines();

    }

    void loadShrines() {
        if (ZPlayerPrefs.HasKey("shrines")) {
            string[] shrineString = ZPlayerPrefs.GetString("shrines").Split('|');
            goldShrines = int.Parse(shrineString[0]);
            stardustShrines = int.Parse(shrineString[1]);
            erraShrines = int.Parse(shrineString[2]);
            voziumShrines = int.Parse(shrineString[3]);
        }
        else {
            goldShrines = 0;
            stardustShrines = 0;
            erraShrines = 0;
            voziumShrines = 0;
        }
    }

    void saveShrines() {
        string savestr = goldShrines + "|" +
                        stardustShrines + "|" +
                        erraShrines + "|" +
                        voziumShrines;
        ZPlayerPrefs.SetString("shrines", savestr);
    }

    string getSaveString() {
        string data = "";

        //Player Stats Section
        data += playerName + "%" +
                level + "%" +
                money + "%" +
                exp + "%" +
                keys + "%" +
                craftlevel + "%" +
                craftexp + "|";

        //Player Inventory Section
        for (int i = 0; i < inventory.Length; i++) {
            data += inventory[i] + "%";
            if ((i + 1) == inventory.Length) {
                data += inventory[i] + "|";
            }
        }

        //Player Equipped Section
        for(int i = 0; i < equipped.Length; i++) {
            data += equipped[i] + "%";
            if ((i + 1) == equipped.Length) {
                data += equipped[i] + "|";
            }
        }
        return data;
    }

    #endregion

    #region inventorymanagement

    public void addToInventory(int itemid) {
        inventory[itemid] += 1;
    }

    public void addToInventory(int itemid, int amount) {
        inventory[itemid] += amount;
    }

    public void removeFromInventory(int itemid) {
        inventory[itemid] -= 1;
    }

    public bool canSell(int itemid) {
        for (int i = 0; i < equipped.Length; i++) {
            if (equipped[i] == itemid && inventory[itemid] == 1) {
                return false;
            }
        }
        return true;
    }

    public void sellItem(int itemid) {
        if (canSell(itemid)) {
            Money += ((int)(db.items[itemid].worth*.75));
            inventory[itemid] -= 1;
        }
    }

    public bool canBuy(int itemid) {
        if(Money >= db.items[itemid].worth) return true;
        return false;
    }

    public void buyItem(int itemid) {
        if (canBuy(itemid)) {
            Money -= db.items[itemid].worth;
            inventory[itemid] += 1;
        }
    }

    #endregion

    #region experience
    public void addExperience(int experience) {
        int temp = exp += experience;
        Debug.Log(temp);
        int requirement = ((100 * (level+1) * (level+1)) - (100 * level * level));
        PopupLog.Instance.showLog("+" + experience + " experience");
        if (temp >= requirement) {
            temp -= requirement;
            levelUp();
        }
        else {
            exp += experience;
            temp = 0;
        }
        PlayerUI.Instance.updateExperience();
        if(temp > 0) {
            addExperience(temp);
        }
    }

    void levelUp() {
        int keyReward = 9;
        if(((level+1) % 5) == 0) {
            keyReward = 30;
        }
        keys += keyReward;
        ++level;
        Debug.Log("Level up!");
        PlayerUI.Instance.updateLevel();
        LevelUp.Instance.playerLevelUp(keyReward);
    }

    public void addCraftingExperience(string item) {
        int experience = getCraftingExperience(item);
        int temp = craftexp += experience;
        PopupLog.Instance.showLog("+" + experience + " crafting experience");
        int requirement = ((100 * (craftlevel + 1) * (craftlevel + 1)) - (100 * craftlevel * craftlevel));
        if (temp >= requirement) {
            temp -= requirement;
            craftexp = 0 + temp;
            craftLevelUp();
        }
        else {
            exp += experience;
        }
    }

    void craftLevelUp() {
        int keyReward = 9;
        if (((level + 1) % 5) == 0) {
            keyReward = 30;
        }
        keys += keyReward;
        ++craftlevel;
        LevelUp.Instance.playerCraftLevelUp(keyReward);
    }

    int getCraftingExperience(string item) {
        if (item.Contains("Gold")) {
            Achievements.Instance.checkGoldArmor();
            return 200;
        }
        else if (item.Contains("Stardust")) {
            Achievements.Instance.checkStardustArmor();
            return 500;
        }
        else if (item.Contains("Erra") || item.Contains("Vozium")) {
            Achievements.Instance.checkGodArmor();
            return 1000;
        }
        else if (item.Contains("7th")) {
            Achievements.Instance.check7thSword();
            return 3000;
        }
        return 0;
    }


    public IEnumerator calculateDamageLevel() {
        damageLevel = (int)((float)damage / 50.0f);
        yield return null;
    }

    public IEnumerator calculateArmorLevel() {
        armorLevel = (int)((float)armor / 70.0f);
        yield return null;
    }

    public IEnumerator calculateTotalLevel() {
        totalLevel = armorLevel + damageLevel;
        yield return null;
    }


    #endregion

    #region DamageArmorCalcs



    public IEnumerator calculateDamage() {
        float temp = 1.00f;
        damage = 0;
        damage += level * 10;
        damage += ((WeaponItem)equippedItems[0]).damage;
        temp += ((WeaponItem)equippedItems[0]).bonusdmg;
        if(equippedItems[4] != null && equippedItems[4].GetType().Equals(typeof(BlessingItem)))
            if(((BlessingItem)equippedItems[4]).type.Equals(bType.damage)) {
            temp += ((BlessingItem)equippedItems[4]).percentage;
        }
        damage = (int)(damage * temp);
        yield return null;
    }

    public IEnumerator calculateArmor() {
        float temp = 1.00f;
        armor = 0;
        armor += level * 10;
        if (equippedItems[1] == null) {
            giveDefaultItems();
        }
        armor += ((ArmorItem)equippedItems[1]).armor;
        armor += ((ArmorItem)equippedItems[2]).armor;
        armor += ((ArmorItem)equippedItems[3]).armor;
        temp += ((ArmorItem)equippedItems[1]).reduction;
        temp += ((ArmorItem)equippedItems[2]).reduction;
        temp += ((ArmorItem)equippedItems[3]).reduction;
        if (equippedItems[4] != null && equippedItems[4].GetType().Equals(typeof(BlessingItem)))
            if (((BlessingItem)equippedItems[4]).type.Equals(bType.reduction)) {
                temp += ((BlessingItem)equippedItems[4]).percentage;
            }
        armor = (int)(armor * temp);
        yield return null;
    }

    public IEnumerator calculateCrit() {
        crit = 0.00f;
        crit += ((WeaponItem)equippedItems[0]).crit;
        if (equippedItems[5] != null && equippedItems[4].GetType().Equals(typeof(BlessingItem)))
            if (((BlessingItem)equippedItems[4]).type.Equals(bType.crit)) {
                crit += ((BlessingItem)equippedItems[4]).percentage;
            }
        yield return null;
    }

    public IEnumerator calculateLifesteal() {
        lifesteal = 0.00f;
        lifesteal += ((WeaponItem)equippedItems[0]).lifesteal;
        if (equippedItems[4] != null && equippedItems[4].GetType().Equals(typeof(BlessingItem)))
            if (((BlessingItem)equippedItems[4]).type.Equals(bType.lifesteal)) {
                lifesteal += ((BlessingItem)equippedItems[4]).percentage;
            }
        yield return null;
    }

    public IEnumerator calculateMovement() {
        mspeed = 1.00f;
        if (equippedItems[5] != null && equippedItems[5].GetType().Equals(typeof(Blessing2Item)))
            if (((Blessing2Item)equippedItems[5]).type.Equals(bType.movement)) {
                mspeed += ((Blessing2Item)equippedItems[5]).percentage;
            }
        yield return null;
    }
    public IEnumerator calculateLuck() {
        loot = 1.00f;
        if (equippedItems[5] != null && equippedItems[5].GetType().Equals(typeof(Blessing2Item)))
            if (((Blessing2Item)equippedItems[5]).type.Equals(bType.luck)) {
                Debug.Log(loot);
                loot += ((Blessing2Item)equippedItems[5]).percentage;
                Debug.Log(loot);
            }
        yield return null;
    }
    #endregion DamageArmorCalcs

}
