using UnityEngine;
using System.Collections;

public class randomSpawn : MonoBehaviour {
    
	void Start () {
        switch (Random.Range(0, 6)) {
            case 0:
                gameObject.SetActive(true);
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
	}
}
