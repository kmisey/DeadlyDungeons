using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUp : MonoBehaviour {

    public static LevelUp Instance { get; set; }

    public Text levelText;
    public Text keyNumText;
    public Animator anim;

    void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void playerLevelUp(int keyamount) {
        gameObject.SetActive(true);
        anim.SetTrigger("open");
        levelText.text = ("You are now level " + Player.Instance.Level.ToString() + "!");
        keyNumText.text = keyamount.ToString();
    }

    public void playerCraftLevelUp(int keyamount) {
        gameObject.SetActive(true);
        anim.SetTrigger("open");
        levelText.text = ("You are now crafting level " + Player.Instance.Craftlevel.ToString() + "!");
        keyNumText.text = keyamount.ToString();
    }

    public void closeLevelUp() {
        anim.SetTrigger("close");
    }

}
