using UnityEngine;
using System.Collections;

public class TrapGenerator : MonoBehaviour {
    public Transform RoomRotation;

    public bool RandomRotation;
    public GameObject[] spawnPoints;
    private GameObject[] trapsToInclude;


	void Start () {
        trapsToInclude = DungeonData.Instance.traps;
        if (RandomRotation) {
            StartCoroutine(spawnTraps(360));
        }
        else {
            StartCoroutine(spawnTraps(0));
        }
	}

    IEnumerator spawnTraps(int maxAngle) {
        GameObject temp;
        for (int i = 0; i < spawnPoints.Length; i++) {
            temp = Instantiate(trapsToInclude[Random.Range(0, trapsToInclude.Length)], spawnPoints[i].transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, maxAngle), 0))) as GameObject;
            temp.transform.parent = (spawnPoints[i].transform);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, maxAngle), 0));
            temp.transform.localScale = new Vector3(1, 1, 1);
        }
        yield return null;
    }
}
