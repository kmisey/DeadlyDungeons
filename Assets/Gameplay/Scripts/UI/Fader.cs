using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour {

    Animator anim;

    public static Fader Instance { get; set; }

    public bool black { get; set; }

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void FadeIn() {
        anim.SetTrigger("fadein");
    }

    public void FadeOut() {
        gameObject.GetComponent<Image>().enabled = true;
        anim.SetTrigger("fadeout");
    }

    public void fadedOut() {
        black = true;
    }

    public void fadedIn() {
        black = false;
        gameObject.GetComponent<Image>().enabled = false;
    }
}
