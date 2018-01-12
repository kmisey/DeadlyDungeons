using UnityEngine;
using System.Collections;

public class RoomRandomizer : MonoBehaviour {

    public GameObject entranceDoor;

    public int enemyCount;
    private GameObject[] mobs;
    private GameObject[] boss;

    public int decalCount;
    GameObject decalParent;
    private GameObject[] decals;

    public int lootAmount;
    private GameObject[] loots;
    private GameObject[] shrines;

    bool spawned = false;

    private int aliveEnemies = 0;

    Collider area;
    float size_x, size_z;

    public bool doneloading {
        get; set; }

	IEnumerator Start () {
        DungeonData dd = DungeonData.Instance;
        doneloading = false;
        Debug.Log(doneloading);
        area = GetComponent<Collider>();
        size_x = area.bounds.size.x * Mathf.Cos(Mathf.Deg2Rad * 45);
        size_z = area.bounds.size.z * Mathf.Sin(Mathf.Deg2Rad * 45);
        Debug.Log("x : " + size_x  + "  z : " + size_z );
        decalParent = gameObject;

        //Grab data from universal dungeon spawnable holder
        mobs = dd.mobs;
        boss = dd.boss;
        decals = dd.decals;
        loots = dd.loots;
        shrines = dd.shrines;

        StartCoroutine(spawnDecals());
        StartCoroutine(spawnLoot());
        yield return null;
    }

    IEnumerator spawnDecals() {
        for (int i = 0; i < Random.Range(decalCount - 2, decalCount + 1); i++) {
            GameObject temp = Instantiate(decals[Random.Range(0, decals.Length)],
            new Vector3(Random.Range(-size_x / 2, size_x / 2) + area.transform.position.x, 0, Random.Range(-size_z / 2, size_z / 2) + area.transform.position.z),
            Quaternion.Euler(0, Random.Range(0, 360), 0)) as GameObject;
            temp.transform.SetParent(decalParent.transform);
        }
        yield return null;
    }

    IEnumerator spawnLoot() {
        GameObject temp;
        for (int i = 0; i < Random.Range(lootAmount - 2, lootAmount + 2); i++) {
            int rng = Random.Range(0, 128);
            if (rng < 30){ //SpawnKey
                temp = Instantiate(loots[0],
                    new Vector3(Random.Range(-size_x / 2, size_x / 2) + area.transform.position.x, 0, Random.Range(-size_z / 2, size_z / 2) + area.transform.position.z),
                    Quaternion.Euler(0, Random.Range(0, 360), 0)) as GameObject;
                    temp.transform.SetParent(decalParent.transform);
            }
            else if(rng > 30 && rng < 40){
                temp = Instantiate(shrines[Random.Range(1, shrines.Length)],
                    new Vector3(Random.Range(-size_x / 2, size_x / 2) + area.transform.position.x, 0, Random.Range(-size_z / 2, size_z / 2) + area.transform.position.z),
                    Quaternion.Euler(0, Random.Range(0, 360), 0)) as GameObject;
                    temp.transform.SetParent(decalParent.transform);
            }
        }
        yield return null;
    }

    IEnumerator spawnMobs() {
        GameObject temp;
        int count = 0;
        int times = Random.Range(enemyCount - 2, enemyCount);
        bool spawning = true;
        while(spawning) {
            ++count;
            temp = Instantiate(mobs[Random.Range(0, mobs.Length)],
            new Vector3(Random.Range(-size_x / 2, size_x / 2) + area.transform.position.x, 0, Random.Range(-size_z / 2, size_z / 2) + area.transform.position.z),
            Quaternion.Euler(0, 0, 0)) as GameObject;
            temp.transform.SetParent(area.transform);
            temp.GetComponent<Enemy>().parentArea = GetComponent<RoomRandomizer>();
            aliveEnemies += 1;
            if(count == times) {
                break;
            }
            yield return null;
        }
    }

    IEnumerator spawnBoss() {
        GameObject temp = Instantiate(boss[Random.Range(0, boss.Length)],
            new Vector3(area.transform.position.x, 0, area.transform.position.z),
            Quaternion.Euler(0, 0, 0)) as GameObject;
        temp.transform.SetParent(area.transform);
        temp.GetComponent<Enemy>().parentArea = GetComponent<RoomRandomizer>();
        aliveEnemies += 1;
        yield return null;
    }

    IEnumerator OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && spawned == false) {
            spawned = true;
            entranceDoor.SetActive(true);
            GenerateDungeon.Instance.deletePreviousRoom();
            switch(Random.Range(0, 2)) {
                case 0: //normal mob room
                    StartCoroutine(spawnMobs());
                    yield break;
                case 1: //boss room
                    StartCoroutine(spawnBoss());
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
        if(aliveEnemies <= 0) {
            StartCoroutine(GenerateDungeon.Instance.spawnNextRoom());
        }
    }

}
