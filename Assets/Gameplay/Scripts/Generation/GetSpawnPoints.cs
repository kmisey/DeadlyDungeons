using UnityEngine;
using System.Collections;

public class GetSpawnPoints : MonoBehaviour {

    public GameObject[] spawnpoints;
    public int[] spawnpointsdir;

    public int nextDoor {
        get; set; } //saves next door in terms of possible spawn points 0 1 2.. etc.


	GameObject[] spawnPoints() {
        return spawnpoints;
    }

    public void disable() {
        spawnpoints[nextDoor].gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void disableSpecific(int disablenum) {
        spawnpoints[disablenum].gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void disablePreviousDoor() {
        GenerateDungeon.Instance.disableCurrentDoor();
    }
}
