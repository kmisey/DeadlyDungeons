using UnityEngine;
using System.Collections;

public class SpawnNextRoom : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            StartCoroutine(GenerateDungeon.Instance.spawnNextRoom());
            gameObject.SetActive(false);
        }
    }
}
