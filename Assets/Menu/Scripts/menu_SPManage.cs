using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class menu_SPManage : MonoBehaviour {

    public Text statusText;
    public GameObject startButton;
    Sound sound;

    Player player;
	void Awake () {
        if (!ZPlayerPrefs.HasKey("sp_DungeonSize")) {
            startButton.SetActive(false);
        }
        else {
            enableStartButton();
        }
	}

    public void enableStartButton() {
        sound = Sound.Instance;
        sound.play(sound.click2);
        int temp = ZPlayerPrefs.GetInt("sp_DungeonSize");
        statusText.text = "";
        if(temp == 1) {
            statusText.text = "Start Small Dungeon";
        }
        else if(temp == 2) {
            statusText.text = "Start Medium Dungeon";
        }
        else if(temp == 3) {
            statusText.text = "Start Large Dungeon";
        }
        startButton.SetActive(true);
    }

    public void StartGame() {
        Player.Instance.save();
        StartCoroutine(load("Dungeon"));
    }

    IEnumerator load(string scene) {
        Fader.Instance.FadeOut();
        while (Fader.Instance.black == false) {
            yield return null;
        }
        SceneManager.LoadScene(scene);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
