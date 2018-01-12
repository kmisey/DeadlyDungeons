using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HeroCreation : MonoBehaviour {

    public static HeroCreation Instance { get; set; }
    public Animator anim;
    public InputField nameField;
    string playername = "";
    int gender = -1;
    string color = "";

    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    public void startCreation() {
        anim.SetTrigger("open");

    }
    

    public void setGender(int i) {
        gender = i;
    }

    public void setColor(string s) {
        color = s;
    }

    public void startGame() {
        StartCoroutine(start());
    }

    IEnumerator start() {
        playername = nameField.text;
        if(playername == "") {
            playername = "Hero";
        }
        if(gender == -1 || color == "") {
            Notify.Instance.showMessage("You hero needs a gender and color");
        }
        else if (gender == -1) {
            Notify.Instance.showMessage("You hero needs a gender");
        }
        else if (color == "") {
            Notify.Instance.showMessage("You hero needs a color");
        }
        else {
            string savestring = playername + "|" + gender + "|" + color;
            PlayerPrefs.SetString("hero_data", savestring);
            Fader.Instance.FadeOut();
            while (Fader.Instance.black == false) {
                yield return null;
            }
            SceneManager.LoadScene("Tutorial");
        }
    }
}
