using UnityEngine;
using System.Collections;

public class DungeonData : MonoBehaviour {

    public static DungeonData Instance { get; set; }

    public GameObject[] mobs;
    public GameObject[] boss;
    public GameObject[] traps;
    public GameObject[] decals;
    public GameObject[] loots;
    public GameObject[] shrines;

    void Awake() {
        Instance = this;
    }
}
