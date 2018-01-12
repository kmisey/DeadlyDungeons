using UnityEngine;
using System.Collections;

public class DeletePreviousRoom : MonoBehaviour {

    public GameObject entranceDoor;

	void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            entranceDoor.SetActive(true);
            GenerateDungeon.Instance.deletePreviousRoom();
        }
    }
}
