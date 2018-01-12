using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    public static LoadingScreen Instance { get; set; }

	// Use this for initialization
    void Awake() {
        Instance = this;
    }

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void turnOff() {
        gameObject.SetActive(false);
    }

    public void turnOn() {
        gameObject.SetActive(true);
    }
}
