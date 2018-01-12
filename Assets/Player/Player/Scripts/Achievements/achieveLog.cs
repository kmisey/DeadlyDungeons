using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class achieveLog : MonoBehaviour {

    public static achieveLog Instance { get; set; }
    public GameObject textPrefab;

    public Scrollbar scrollbar;
    public GameObject scrollRegion;
    public GameObject group;

    private Achievements data;

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        updateScrollbar();
        data = Achievements.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool noItemsOnScreen = true;

    private void OnGUI() {
        if (noItemsOnScreen) {
            populateList();
            noItemsOnScreen = false;
        }
    }

    public void populateList() {
        StartCoroutine(updateList());
    }

    IEnumerator updateList() {
        yield return StartCoroutine(checkKillCount());
        yield return StartCoroutine(checkDungeons());
        yield return StartCoroutine(checkGodArmor());
        yield return StartCoroutine(checkGoldArmor());
        yield return StartCoroutine(checkStardustArmor());
        yield return StartCoroutine(check7thSword());
        yield return StartCoroutine(checkWave());
        yield return null;
    }

    IEnumerator checkKillCount() {
        int kc = data.KC;
        int[] req = { 100, 250, 500, 750, 1000 };
        for (int i = 0; i < req.Length; i++) {
            if (req[i] <= kc) {
                spawnItem("Kill " + req[i] + " enemies.");
            }
        }
        yield return null;
    }

    IEnumerator checkDungeons() {
        int t = data.DC;
        int[] req = { 10, 50, 100, 200, 300, 400, 500 };
        for (int i = 0; i < req.Length; i++) {
            if (req[i] <= t) {
                spawnItem("Clear " + req[i] + " dungeons.");
            }
        }
        yield return null;
    }

    IEnumerator checkGodArmor() {
        int t = data.GodArmor;
        int[] req = { 10, 25, 50 };
        if (t >= 1) {
            spawnItem("Craft a god tier item.");
        }
        for (int i = 0; i < req.Length; i++) {
            if (req[i] <= t) {
                spawnItem("Craft " + req[i] + " god tier items.");
            }
        }
        yield return null;
    }

    IEnumerator checkGoldArmor() {
        int t = data.GoldArmor;
        int[] req = { 10, 100, 200 };
        if (t >= 1) {
            spawnItem("Craft a gold item.");
        }
        for (int i = 0; i < req.Length; i++) {
            if (req[i] <= t) {
                spawnItem("Craft " + req[i] + " gold items.");
            }
        }
        yield return null;
    }

    IEnumerator checkStardustArmor() {
        int t = data.StardustArmor;
        int[] req = {10, 100};
        if(t >= 1) {
            spawnItem("Craft a stardust item.");
        }
        for (int i = 0; i < req.Length; i++) {
            if (req[i] <= t) {
                spawnItem("Craft " + req[i] + " stardust items.");
            }
        }
        yield return null;
    }
    
    IEnumerator check7thSword() {
        int t = data.Sword7;
        if (t >= 1) {
            spawnItem("Craft the legendary 7th sword.");
        }
        yield return null;
    }

    IEnumerator checkWave() {
        int t = ZPlayerPrefs.GetInt("highest_wave");
        int[] req = {10, 25, 50, 75, 100 };
        for (int i = 0; i < req.Length; i++) {
            if (req[i] <= t) {
                spawnItem("Survive " + req[i] + " waves.");
            }
        }
        yield return null;
    }

    void spawnItem(string s) {
        GameObject temp = Instantiate(textPrefab);
        temp.transform.SetParent(group.transform);
        temp.GetComponent<Text>().text = s;
        temp.transform.localScale = new Vector3(1, 1, 1);
    }




    #region Scrollbar

    void updateScrollbar() {
        RectTransform region = scrollRegion.GetComponent<RectTransform>();

        int count = 0;
        foreach (Transform t in group.transform) {
            count += 1;
        }
        int extrarows = ((count - 6));
        if (extrarows > 0) {
            region.sizeDelta = new Vector2(1, (float)(75 * extrarows));
            scrollbar.value = 1;
        }
    }
    #endregion


}
