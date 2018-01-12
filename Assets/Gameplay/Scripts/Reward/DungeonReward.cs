using UnityEngine;
using System.Collections;

public class DungeonReward : MonoBehaviour {

    public GameObject chestTop;
    Sound sound;

	// Use this for initialization
	void Start () {
        sound = Sound.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            //sound.play(sound.reward);
            PlayerController.Instance.canMove = false;
            GrantReward.Instance.gameObject.SetActive(true);
            CongratzBox.Instance.openCongratzBoxD();
        }
    }

    void openChest() {

    }
}
