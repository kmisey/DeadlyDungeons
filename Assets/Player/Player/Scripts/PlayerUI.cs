using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

    public static PlayerUI Instance { get; set; }
    public Text coins;
    public Text level;
    public Image sprite;

    public Image healthbar;
    public Image experiencebar;
    public SpriteRenderer playerRing;
    public Image playerColor;
    private bool cancel = false;

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        updateAll();
        setPlayerColors();
    }

    void setPlayerColors() {
        Color color = new Color(0f, 0f, 0f, 1f); ;
        switch (Player.Instance.Color) {
            case "blue":
                color = new Color(0f, 0f, 1f, 1f);
                break;
            case "red":
                color = new Color(1f, 0f, 0f, 1f);
                break;
            case "green":
                color = new Color(0f, 1f, 0f, 1f);
                break;
            case "orange":
                color = new Color(1f, 1f, 0f, 1f);
                break;
            case "purple":
                color = new Color(1f, 0f, 1f, 1f);
                break;
        }
        playerRing.color = color;
        playerColor.color = color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateAll() {
        updatePicture();
        updateCoins();
        updateLevel();
        updateExperience();
    }

    public void updatePicture() {
        sprite.overrideSprite = Resources.Load(Player.Instance.equippedItems[1].path, typeof(Sprite)) as Sprite;
    }

    public void updateCoins() {
        coins.text = Player.Instance.Money.ToString();
    }

    public void updateLevel() {
        level.text = "LVL: " + Player.Instance.Level.ToString();
    }

    public void updateExperience() {
        StopCoroutine("updateExperienceBar");
        StartCoroutine(updateExperienceBar());
    }

    public void updateHealthBar(float health) {
        StopCoroutine("updateHealth");
        StartCoroutine(updateHealth(health));
    }

    IEnumerator updateExperienceBar() {
        float experience = Player.Instance.Experience;
        int lvl = Player.Instance.Level;
        float requirement = ((lvl + 1) * (lvl + 1) * 100) - ((lvl) * (lvl) * 100);
        Vector3 start = experiencebar.transform.localScale;
        float dest = experience / requirement;
        while((experiencebar.transform.localScale.x-dest > .01f)) {
            experiencebar.transform.localScale = Vector3.LerpUnclamped(experiencebar.transform.localScale, new Vector3(dest, 1, 1), Time.deltaTime*8.0f) ;
            yield return null;
        }
        yield return null;
    }

    IEnumerator updateHealth(float health) {
        Vector3 start = healthbar.transform.localScale;
        float dest = (health / 100.0f);
        if (healthbar.transform.localScale.x - dest > 0) {
            while (healthbar.transform.localScale.x - dest > .01f) {
                healthbar.transform.localScale = Vector3.LerpUnclamped(healthbar.transform.localScale, new Vector3(dest, 1, 1), Time.deltaTime * 12.0f);
                yield return null;
            }
        }
        else {
            while (healthbar.transform.localScale.x - dest < -.01f) {
                healthbar.transform.localScale = Vector3.LerpUnclamped(healthbar.transform.localScale, new Vector3(dest, 1, 1), Time.deltaTime * 12.0f);
                yield return null;
            }
        }
        yield return null;
    }
}
