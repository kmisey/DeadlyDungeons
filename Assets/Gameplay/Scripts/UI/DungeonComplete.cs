using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DungeonComplete : MonoBehaviour {



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void turnOn() {
        gameObject.SetActive(true);
    }

    public void turnOff() {
        gameObject.SetActive(false);
    }

    public void loadScene(string scene) {
        Player.Instance.save();
        StartCoroutine(load(scene));
    }

    IEnumerator load(string scene) {
        int randNum = Random.Range(0, 6);
        Fader.Instance.FadeOut();
        while (Fader.Instance.black == false) {
            yield return null;
        }
        if(randNum == 5) {
            AdManager.Instance.ShowAd();
        }
        SceneManager.LoadScene(scene);
    }

    IEnumerator loadNoAd(string scene) {
        Fader.Instance.FadeOut();
        while (Fader.Instance.black == false) {
            yield return null;
        }
        SceneManager.LoadScene(scene);
    }

    IEnumerator firstStart() {
        HeroCreation.Instance.startCreation();
        yield return null;
    }

    public void loadmenu() {
        Debug.Log("loadmenu");
        if (!PlayerPrefs.HasKey("hero_data")) {
            StartCoroutine(firstStart());
        }
        else {
            StartCoroutine(loadNoAd("Menu"));
        }
    }
}
