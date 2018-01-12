using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupLog : MonoBehaviour {

    public static PopupLog Instance { get; set; }

    private Animator anim;

    private void Awake() {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    public void showLog(string str) {
        StartCoroutine(activate(str));
    }

    //Display popup, if it is playing the scroll up animation wait until
    //it is finished otherwise play it again for the new popup message
    IEnumerator activate(string str) {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("idle")) {
            yield return new WaitForSeconds(.1f);
            StartCoroutine(activate(str));
        }
        else {
            GetComponent<Text>().text = str;
            anim.SetTrigger("play");
        }
    }
}
