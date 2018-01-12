using UnityEngine;
using System.Collections;

public class Achievements : MonoBehaviour {

    public static Achievements Instance { get; set; }

    public int KC { get { return kc; } set { kc = value; } }
    public int DC { get { return dc; } set { dc = value; } }
    public int GodArmor { get { return godArmor; } set { godArmor = value; } }
    public int GoldArmor { get { return goldArmor; } set { goldArmor = value; } }
    public int StardustArmor { get { return stardustArmor; } set { stardustArmor = value; } }
    public int Sword7 { get { return sword7; } set { sword7 = value; } }

    int kc = 0;
    int dc = 0;
    int goldArmor = 0;
    int stardustArmor = 0;
    int godArmor = 0;
    int sword7 = 0;
    int recordWave = 0;

    void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start() {
        recordWave = ZPlayerPrefs.GetInt("highest_wave");
        load();
    }

    // Update is called once per frame
    void Update() {

    }

    void save() {
        string savestring = "";

        savestring += kc + "|" +
                        dc + "|" +
                        goldArmor + "|" +
                        stardustArmor + "|" +
                        godArmor + "|" +
                        sword7 + "|" +
                        recordWave;

        ZPlayerPrefs.SetString("achievements_save", savestring);
    }

    void load() {
        if (!ZPlayerPrefs.HasKey("achievements_save")) {
            save();
        }
        else {
            string saved = ZPlayerPrefs.GetString("achievements_save");
            string[] misc = saved.Split('|');
            kc = int.Parse(misc[0]);
            dc = int.Parse(misc[1]);
            goldArmor = int.Parse(misc[2]);
            stardustArmor = int.Parse(misc[3]);
            godArmor = int.Parse(misc[4]);
            sword7 = int.Parse(misc[5]);
            recordWave = int.Parse(misc[6]);
        }
    }

    private void OnApplicationQuit() {
        save();
    }

    public bool checkKc() {
        kc += 1;
        bool temp = false;
        switch (kc) {
            case 1000:
                temp = true;
                break;
            case 750:
                temp = true;
                break;
            case 500:
                temp = true;
                break;
            case 250:
                temp = true;
                break;
            case 100:
                temp = true;
                break;
        }
        if(temp == true) {
            Notify.Instance.showMessage("Achievement Unlocked:\n" + kc + " Enemies Slain.");
            return true;
        }
        return false;
    }

    public bool checkDc() {
        dc += 1;
        bool temp = false;
        switch (dc) {
            case 500:
                temp = true;
                break;
            case 400:
                temp = true;
                break;
            case 300:
                temp = true;
                break;
            case 200:
                temp = true;
                break;
            case 100:
                temp = true;
                break;
            case 50:
                temp = true;
                break;
            case 10:
                temp = true;
                break;
        }
        if (temp == true) {
            Notify.Instance.showMessage("Achievement Unlocked:\n" + dc + " Dungeons Complete.");
            return true;
        }
        return false;
    }

    public bool checkGodArmor() {
        godArmor += 1;
        if(godArmor == 1) {
            Notify.Instance.showMessage("Achievement Unlocked:\nCraft a God Item.");
            return true;
        }
        else if (godArmor == 10) {
            Notify.Instance.showMessage("Achievement Unlocked:\nCraft " + godArmor + " God Items");
            return true;
        }
        return false;
    }

    public bool checkGoldArmor() {
        goldArmor += 1;
        if (goldArmor == 1) {
            Notify.Instance.showMessage("Achievement Unlocked:\nCraft a Gold Item.");
            return true;
        }
        else if (goldArmor == 10) {
            Notify.Instance.showMessage("Achievement Unlocked:\nCraft " + goldArmor + " Gold Items.");
            return true;
        }
        return false;

    }

    public bool checkStardustArmor() {
        stardustArmor += 1;
        if (stardustArmor == 1) {
            Notify.Instance.showMessage("Achievement Unlocked:\nCraft Stardust Item.");
            return true;
        }
        else if (stardustArmor == 10) {
            Notify.Instance.showMessage("Achievement Unlocked:\nCraft " + stardustArmor + " Stardust Items.");
            return true;
        }
        return false;
    }

    public bool check7thSword() {
        sword7 += 1;
        if (sword7 == 1) {
            Notify.Instance.showMessage("Achievement Unlocked:\nLegendary Artisan.");
            return true;
        }
        return false;
    }

    public bool checkWave() {
        bool temp = false;
        switch (recordWave) {
            case 100:
                temp = true;
                break;
            case 75:
                temp = true;
                break;
            case 50:
                temp = true;
                break;
            case 25:
                temp = true;
                break;
            case 10:
                temp = true;
                break;
        }
        if (temp == true) {
            Notify.Instance.showMessage("Achievement Unlocked:\n" + recordWave + " record wave!");
            return true;
        }
        return false;
    }
}
