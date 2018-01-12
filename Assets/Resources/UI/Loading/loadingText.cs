using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loadingText : MonoBehaviour {


    public string[] loadstatements;
    Text t;
	// Use this for initialization
	void Start () {
        t = GetComponent<Text>();
        StartCoroutine(changeText());
	}
	
	IEnumerator changeText() {
        for(int i = 0; i < loadstatements.Length; i++) {
            t.text = loadstatements[i];
            yield return new WaitForSeconds(Random.Range(0.0f,0.4f));
            yield return null;
        }
    }
}
