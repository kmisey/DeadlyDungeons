using UnityEngine;
using System.Collections;

public class TutorialSpawn : MonoBehaviour {

    public GetSpawnPoints doors;

    public GameObject entranceDoor;
    public GameObject exitDoor;

    public int enemyCount;
    public GameObject[] mobs;

    public int decalCount;
    GameObject decalParent;
    public GameObject[] decals;

    bool spawned = false;

    private int aliveEnemies = 0;

    Collider area;
    float size_x, size_z;

    public bool doneloading {
        get; set;
    }

    void Start() {
        Sound.Instance.startPlayMusic();
        doneloading = false;
        Debug.Log(doneloading);
        area = GetComponent<Collider>();
        size_x = area.bounds.size.x * Mathf.Cos(Mathf.Deg2Rad * 45);
        size_z = area.bounds.size.z * Mathf.Sin(Mathf.Deg2Rad * 45);
        Debug.Log("x : " + size_x + "  z : " + size_z);
        decalParent = gameObject;
        StartCoroutine(spawnDecals());

    }

    IEnumerator spawnDecals() {
        for (int i = 0; i < Random.Range(decalCount - 2, decalCount + 2); i++) {
            GameObject temp = Instantiate(decals[Random.Range(0, decals.Length)],
            new Vector3(Random.Range(-size_x / 2, size_x / 2) + area.transform.position.x, 0, Random.Range(-size_z / 2, size_z / 2) + area.transform.position.z),
            Quaternion.Euler(0, Random.Range(0, 360), 0)) as GameObject;
            temp.transform.SetParent(decalParent.transform);
        }
        yield return null;
    }

    IEnumerator spawnMob() {
        GameObject temp;
            temp = Instantiate(mobs[Random.Range(0, mobs.Length)],
            new Vector3(Random.Range(-size_x / 2, size_x / 2) + area.transform.position.x, 0, Random.Range(-size_z / 2, size_z / 2) + area.transform.position.z),
            Quaternion.Euler(0, 0, 0)) as GameObject;
            temp.transform.SetParent(area.transform);
            temp.GetComponent<TutorialEnemy>().parentArea = GetComponent<TutorialSpawn>();
            aliveEnemies += 1;
            yield return null;
    }

    IEnumerator OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && spawned == false) {
            spawned = true;
            entranceDoor.SetActive(true);
            switch (Random.Range(0, 1)) {
                case 0: //normal mob room
                    StartCoroutine(spawnMob());
                    yield break;
            }
        }
    }

    IEnumerator setEnemiesVisible() {
        foreach (Transform t in area.gameObject.transform) {
            t.gameObject.SetActive(true);
        }
        yield return null;
    }

    public void enemyDied() {
        Debug.Log("enemy died : " + aliveEnemies);
        aliveEnemies -= 1;
        if (aliveEnemies <= 0) {
            exitDoor.SetActive(false);
        }
    }

}
