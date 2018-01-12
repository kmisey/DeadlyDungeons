using UnityEngine;
using System.Collections;

public class GenerateDungeon : MonoBehaviour {

    public GameObject startingPrefab;
    public GameObject endingPrefab;
    public GameObject[] spawnPrefabs;
    public GameObject testCollider;
    public GameObject dungeonParent;

    public int smallSize;
    public int mediumSize;
    public int largeSize;


    private string lastroomname = "";

    private int DungeonSize;
    private bool collision;
    private int dir;
    bool host;

    public static GenerateDungeon Instance { get; set; }

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        Sound sound = Sound.Instance;
        sound.play(sound.StartGame);
        sound.startPlayMusic();
        DungeonSize = ZPlayerPrefs.GetInt("sp_DungeonSize");
        if(DungeonSize == 1) {
            DungeonSize = smallSize;
        }
        else if (DungeonSize == 2) {
            DungeonSize = mediumSize;
        }
        else if (DungeonSize == 3) {
            DungeonSize = largeSize;
        }
        amountOfPossibleRooms = spawnPrefabs.Length;
        //init spawn
        spawnRoom = Instantiate(startingPrefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 45, 0))) as GameObject;
        spawnRoom.transform.parent = dungeonParent.transform;
        StartCoroutine(spawnNextRoom());
    }

    private int amountOfPossibleRooms;

    private int roomCount = 0;
    public GameObject previousRoom { get; set; }
    public GameObject spawnRoom { get; set; }

    public IEnumerator spawnNextRoom() {
        int random;
        int roomToSpawn;
        roomToSpawn = Random.Range(0, amountOfPossibleRooms);

        if (!lastroomname.Equals(spawnPrefabs[roomToSpawn].name)) {

            if (roomCount < DungeonSize) {
                previousRoom = spawnRoom;
                //which door to spawn the room at
                random = Random.Range(0, previousRoom.GetComponent<GetSpawnPoints>().spawnpoints.Length);
                //random number for different styled rooms
                dir = previousRoom.GetComponent<GetSpawnPoints>().spawnpointsdir[random];


                //Test if can spawn
                //if its possible at this location save the door it will be spawned at
                //so we can delete the door when the room is completed.
                spawnRoom.GetComponent<GetSpawnPoints>().nextDoor = random;

                if (roomCount == 0) {
                    disableCurrentDoor();
                }
                //the previous room is now the one we're spawning.
                //spawn new room at location where it randomly decided

                spawnRoom = Instantiate(spawnPrefabs[roomToSpawn],
                        previousRoom.GetComponent<GetSpawnPoints>().spawnpoints[random].transform.position,
                        spawnRoom.transform.rotation) as GameObject;
                lastroomname = spawnPrefabs[roomToSpawn].name;

                //Rotate the room correctly (i figured it out by testing this worked
                spawnRoom.transform.Rotate(new Vector3(0, (dir * 90) - 90, 0));

                    //setparent;
                spawnRoom.transform.parent = dungeonParent.transform;

                    //index;
                    roomCount += 1;
            }
            else {
                random = Random.Range(0, spawnRoom.GetComponent<GetSpawnPoints>().spawnpoints.Length);
                Debug.Log(random);
                dir = spawnRoom.GetComponent<GetSpawnPoints>().spawnpointsdir[random];
                //spawn finishing room
                    spawnRoom.GetComponent<GetSpawnPoints>().nextDoor = random;
                    previousRoom = spawnRoom;
                    spawnRoom = Instantiate(endingPrefab, previousRoom.GetComponent<GetSpawnPoints>().spawnpoints[random].transform.position, spawnRoom.transform.rotation) as GameObject;
                    spawnRoom.transform.Rotate(new Vector3(0, (dir * 90) - 90, 0));
                    spawnRoom.transform.parent = dungeonParent.transform;
            }

        }
        else {
            StartCoroutine(spawnNextRoom());
        }
        yield return null;
    }

    public void disableCurrentDoor() {
        previousRoom.GetComponent<GetSpawnPoints>().disable();
    }


    public void deletePreviousRoom() {
        StartCoroutine(deleteprevroom());
    }

    IEnumerator deleteprevroom() {
        Destroy(previousRoom);
        yield return null;
    }

    /*
        bool canBuild(GameObject from, int dir) {
        Vector3 pos = from.GetComponent<GetSpawnPoints>().spawnpoints[dir].transform.position;
        GameObject collider = Instantiate(testCollider, pos, from.transform.rotation) as GameObject;
        collider.gameObject.transform.Rotate(new Vector3(0, (dir * 90) - 90, 0));
        Collider c = collider.GetComponent<Collider>();
        Debug.Log("Testing collision");

        if (Physics.OverlapBox(c.bounds.center, c.bounds.extents, c.transform.rotation, LayerMask.GetMask("Collision")).Length > 1) {
            Destroy(collider);
            Debug.Log("collided");
            return false;
        } 
        else {
            Destroy(collider);
            return true;
            }
        }
    */
}
