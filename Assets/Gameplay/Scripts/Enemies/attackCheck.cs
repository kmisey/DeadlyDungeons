using UnityEngine;
using System.Collections;

public class attackCheck : MonoBehaviour {

    Sound sound;

    public int strength;

    public Collider attackHitbox;

    private void Start() {
        sound = Sound.Instance;
    }

    public void checkIfHit() {
        Collider[] col = Physics.OverlapBox(attackHitbox.bounds.center, attackHitbox.bounds.extents, transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in col) {
            if (c.tag == "Player") {
                //play sound
                sound.play(sound.swordhit);
                c.GetComponent<PlayerController>().applyDamage(strength);
            }
        }
    }

    public void Death() {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
