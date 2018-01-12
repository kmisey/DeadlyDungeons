using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class disableNC : MonoBehaviour {

    public static disableNC Instance { get; set; }

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        Instance = this;
        checkIfBought();
	}

    public void checkIfBought() {
        if (ZPlayerPrefs.GetInt("noAds") == 2) {
            gameObject.SetActive(false);
            GetComponent<Button>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
