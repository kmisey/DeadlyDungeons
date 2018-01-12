using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetKeys : MonoBehaviour {

    Player player;

    void Start() {
        player = Player.Instance;
    }

	void OnGUI() {
        GetComponent<Text>().text = player.Keys.ToString();
    }
}
