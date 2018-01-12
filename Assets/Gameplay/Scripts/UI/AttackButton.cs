using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AttackButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private Image img;

    public float seconds;

    public static AttackButton Instance { get; set; }


    public bool buttondown { get; set; }

    Button button;
    Button blockButton;
    PlayerController player;

    // Use this for initialization
    private void Awake() {
        Instance = this;
        img = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start() {
        blockButton = BlockButton.Instance.GetComponent<Button>();
        player = PlayerController.Instance;
    }

    public virtual void OnPointerDown(PointerEventData ped) {
    }

    public virtual void OnPointerUp(PointerEventData ped) {
    }

    public void pressed() {
        StartCoroutine(startAttackTimer());
    }

    IEnumerator startAttackTimer() {
        button.interactable = false;
        blockButton.interactable = false;
        player.canMove = false;
        buttondown = true;
        yield return new WaitForSeconds(seconds);
        buttondown = false;
        player.canMove = true;
        blockButton.interactable = true;
        button.interactable = true;
    }
}
