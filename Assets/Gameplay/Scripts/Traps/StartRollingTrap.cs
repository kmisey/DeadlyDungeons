using UnityEngine;
using System.Collections;

public class StartRollingTrap : MonoBehaviour {

    public Animator move;
    public Animator rot;

    IEnumerator OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            move.SetTrigger("start");
            rot.SetTrigger("start");
            yield return null;  
        }
    }
}
