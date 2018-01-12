using UnityEngine;
using System.Collections;

public class AdReward : MonoBehaviour {

	public void action(string type) {
        Debug.Log("Pressed ad button");
        AdManager.Instance.ShowAd(type);
    }
}
