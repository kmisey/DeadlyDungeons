using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notify : MonoBehaviour {

    public Text textObject;

    public string message;

    public Animator anim;

    public static Notify Instance { get; set; }

    private void Start() {
        Instance = this;
    }

    IEnumerator OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
           textObject.text = message;
            anim.SetTrigger("open");
            GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(4f);
            anim.SetTrigger("close");
            Destroy(gameObject);
        }
    }

    public void showMessage(string str) {
        StartCoroutine(openMessage(str));
    }

    IEnumerator openMessage(string str) {
        textObject.text = str;
        anim.SetTrigger("open");
        yield return new WaitForSeconds(4f);
        anim.SetTrigger("close");
    }
}
