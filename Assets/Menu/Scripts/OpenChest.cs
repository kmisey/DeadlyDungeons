using UnityEngine;
using System.Collections;

public class OpenChest : MonoBehaviour {

	public void getMyReward() {
        Achievements.Instance.checkDc();
        GrantReward.Instance.openChest();
    }

    public void open() {
        GetComponent<Animator>().SetTrigger("openChest");
    }

    public void close() {
        GetComponent<Animator>().SetTrigger("closeChest");
    }
}
