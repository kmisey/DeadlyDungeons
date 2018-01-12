using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InteractButton : MonoBehaviour {

    public Collider checkArea;

    public void activated() {
        StartCoroutine(pressed());
    }

    IEnumerator pressed() {
        Sound sound = Sound.Instance;
        sound.play(sound.click);
        Collider[] col = Physics.OverlapBox(checkArea.bounds.center, checkArea.bounds.extents, transform.rotation, LayerMask.GetMask("MenuArea"));
            foreach (Collider c in col) {
                if (c.tag.Equals("MenuArea")) {
                    MenuManager.Instance.changeMenu(c.gameObject.name);
                    break;
                }
            }
            yield return null;
    }
}
