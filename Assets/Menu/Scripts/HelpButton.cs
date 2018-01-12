using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpButton : MonoBehaviour {

    public GameObject parent;

    public string title;
    public Text titleInPanel;

    public string message;
    public Text textInPanel;

   

    private void Start() {
        textInPanel.text = message;
        titleInPanel.text = title;
    }

	public void showHelpBox() {
        parent.SetActive(true);
    }

    public void closeHelpBox() {
        parent.SetActive(false);
    }

}
