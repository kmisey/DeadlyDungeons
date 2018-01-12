using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    Sound sound;

    public enum menuname {
        main,
        play,
        traders,
        loadout,
        options,
        singleplay,
        multiplay,
        buy,
        sell,
        openchest,
        playerstats,
        inventory,
        shrinetrader,
        vb,
        achievements
    }

    private string[] enumstrings =
        { "vb", "main", "play", "traders",
        "loadout", "options", "singleplay",
        "survival", "buy", "sell",
        "openchest", "playerstats", "inventory", "craft", "market", "shrinetrader", "achievements" };

    //Camera Fields
    public float CameraHeight;
    public float CameraDist;
    public float cameraSpeed;

    private Transform camTransform;
    private Vector3 offset;

    private float transition = 0;

    private bool inTransition = false;
    private bool cameraIdle = true;
    private bool goBackToDefault = false;
    private Transform nextLoc;

    public GameObject HubRoot;

    public GameObject UIroot;

    private Animator anim;
    private List<GameObject> UI = new List<GameObject>();

    public static MenuManager Instance { get; set; }

    private void Start() {
        Instance = this;

        anim = GetComponent<Animator>();

        offset = new Vector3(0, CameraHeight, CameraDist);
        //camTransform = Camera.main.transform;

        foreach(Transform t in UIroot.transform) {
            UI.Add(t.gameObject);
        }


        sound = Sound.Instance;
        sound.startMenuMusic();
    }

    public void changeMenu(string name) {
        StartCoroutine(menuSwap(name));
    }

    IEnumerator menuSwap(string name) {
        sound.play(sound.click);
        foreach (GameObject panel in UI) {
            if (panel.activeSelf) {
                panel.SetActive(false);
                break;
            }
        }
        for (int i = 0; i < enumstrings.Length; i++) {
            if (name.Equals(enumstrings[i])) {
                UI[i].SetActive(true);
                break;
            }
        }
        yield return null;
    }

    /*

    private void FixedUpdate() {
        if (cameraIdle) {
            idleMoveCamera();
            transitionDefault();
        }
        else if (inTransition && !cameraIdle) {
            transitionMoveCamera();
        }
        else if (!cameraIdle && !inTransition) {

        }

    }
    */

    private void idleMoveCamera() {
        transition += Time.deltaTime * cameraSpeed;
        Vector3 desiredPos = offset;
        Quaternion orientation = Quaternion.Euler(0, transition, 0);
        defaultCameraWaypoint.position = orientation * desiredPos;
        defaultCameraWaypoint.LookAt(Vector3.up * camTransform.position.y);
    }

    public Transform defaultCameraWaypoint;
    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private void transitionMoveCamera() {
        camTransform.position = Vector3.Lerp(camTransform.position, desiredPosition, Time.deltaTime);
        camTransform.rotation = Quaternion.Lerp(camTransform.rotation, desiredRotation, Time.deltaTime);
        if(isClose(camTransform.position, desiredPosition) && isClose(camTransform.rotation.eulerAngles, desiredRotation.eulerAngles)){
            cameraIdle = false;
            inTransition = false;
        }
    }

    private void transitionDefault() {
        camTransform.position = Vector3.Lerp(camTransform.position, defaultCameraWaypoint.position, Time.deltaTime);
        camTransform.rotation = Quaternion.Lerp(camTransform.rotation, defaultCameraWaypoint.rotation, Time.deltaTime);
    }

    public void SetDesiredWaypoint(string loc) {
        if (loc.Equals(defaultCameraWaypoint.name)) {
            cameraIdle = true;
        }
        else {
            goBackToDefault = false;
            foreach (Transform waypoint in HubRoot.transform) {
                if (waypoint.name.Equals(loc)) {
                    nextLoc = waypoint;
                }
            }
            if (nextLoc == null) {
                return;
            }
            desiredPosition = nextLoc.position;
            desiredRotation = nextLoc.rotation;
            goBackToDefault = false;
            inTransition = true;
            cameraIdle = false;
        }
        
    }

    bool isClose(Vector3 A, Vector3 B) {

        float distX = A.x - B.x;
        float distY = A.y - B.y;
        float distZ = A.z - B.z;
        float r = 0.5f;

        if ((distX > -r && distX < r)
            && (distY > -r && distY < r)
                && (distZ > -r && distZ < r)) {
                    return true;
        }
        else {
            return false;
        }
        }

}
