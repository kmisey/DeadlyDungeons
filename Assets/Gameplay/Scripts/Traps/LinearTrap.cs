using UnityEngine;
using System.Collections;

public class LinearTrap : MonoBehaviour {

    public int trapDamage;

	void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            other.GetComponent<PlayerController>().applyTrueDamage(trapDamage);
        }
    }

    public void setDamageZero() {
        trapDamage = 0;
    }
}
