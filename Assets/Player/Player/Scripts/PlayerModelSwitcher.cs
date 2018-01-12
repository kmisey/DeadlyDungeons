using UnityEngine;
using System.Collections;

public class PlayerModelSwitcher : MonoBehaviour {

    public static PlayerModelSwitcher Instance { get; set; }

    public string righthandname;
    public string lefthandname;
    public GameObject HELMET_SLOT;
    public GameObject PLATE_SLOT;
    public GameObject LEG_SLOT;
    public Animator[] anims = new Animator[3];

    private Player player;
    private GameObject weapon, plate, legs, helmet, cape, shield;
    
    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
    }

	void Update () {
	
	}

    public void clearModel() {
        Destroy(weapon);
        Destroy(plate);
        Destroy(legs);
        Destroy(helmet);
        
    }

    public void spawnModel() {
        player = Player.Instance;
        spawnHelmet();
        spawnPlate();
        spawnLegs();
        spawnWeapon();
        spawnShield();
        anims[0] = helmet.GetComponent<Animator>();
        anims[1] = plate.GetComponent<Animator>();
        anims[2] = legs.GetComponent<Animator>();
    }

    void spawnHelmet() {
        GameObject model;
        model = loadModel(player.equippedItems[1]) as GameObject;
        helmet = Instantiate(model, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        helmet.transform.SetParent(HELMET_SLOT.transform);
    }

    void spawnPlate() {
        GameObject model;
        model = loadModel(player.equippedItems[2]) as GameObject;
        plate = Instantiate(model, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        plate.transform.SetParent(PLATE_SLOT.transform);
    }

    void spawnLegs() {
        GameObject model;
        model = loadModel(player.equippedItems[3]) as GameObject;
        legs = Instantiate(model, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        legs.transform.SetParent(LEG_SLOT.transform);
    }

    void spawnWeapon() {
        GameObject model;
        model = loadModel(player.equippedItems[0]) as GameObject;
        foreach(Transform t in plate.transform) {
            if (t.name.Equals("Character1_RightHand")) {
                foreach(Transform w in t) {
                        weapon = Instantiate(model) as GameObject;
                        weapon.transform.SetParent(w);
                        weapon.transform.localPosition = Vector3.zero;
                        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
                }
            }
        }
    }
    void spawnShield() {
        GameObject model;
        model = loadModel(player.equippedItems[6]) as GameObject;
        foreach (Transform t in plate.transform) {
            if (t.name.Equals("Character1_LeftHand")) {
                foreach (Transform w in t) {
                    shield = Instantiate(model) as GameObject;
                    shield.transform.SetParent(w);
                    shield.transform.localPosition = Vector3.zero;
                    shield.transform.localRotation = Quaternion.Euler(new Vector3(45, 0 , 0));
                }
            }
        }
    }

    Object loadModel(BaseItem item) {
        return Resources.Load(item.modelpath);
    }
}
