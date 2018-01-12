using UnityEngine;
using System.Collections;

public class SpawnObjectCollider : MonoBehaviour {

    bool collisionDetected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other) {
        Debug.Log("There has abeen a collision on gameObject");
        collisionDetected = true;
    }

    public bool collided() {
        Debug.Log("collision: " + collisionDetected);
        return collisionDetected;
    }

}
