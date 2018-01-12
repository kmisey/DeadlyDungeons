using UnityEngine;
using System.Collections;

public class MultiRandomizer : MonoBehaviour {

    public GameObject[] spawnLocs;
    public GameObject[] spawnableAreas;

    public GameObject entranceDoor;


	// Use this for initialization
	void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void init() {
        GameObject temp = Instantiate(spawnableAreas[Random.Range(0, spawnableAreas.Length)], spawnLocs[Random.Range(0, spawnLocs.Length)].transform.position, Quaternion.Euler(Vector3.zero)) as GameObject;
        temp.transform.SetParent(gameObject.transform);
        temp.GetComponent<RoomRandomizer>().entranceDoor = entranceDoor;
        //temp.GetComponent<RoomRandomizer>().doors = GetComponent<GetSpawnPoints>();
    }
}
