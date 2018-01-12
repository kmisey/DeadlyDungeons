using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BlockButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    public Animator anim;

    private Image img;
    private Image joystickImg;

    public static BlockButton Instance { get; set; }

    public bool buttondown { get; set; }

    PlayerController control;
    Button button;
    Button attackb;

    // Use this for initialization
    private void Awake() {
        button = GetComponent<Button>();
        Instance = this;
        img = GetComponent<Image>();
    }

    private void Start() {
        control = PlayerController.Instance;
        attackb = AttackButton.Instance.GetComponent<Button>() ;
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        if (!AttackButton.Instance.buttondown) {
            control.updateAnims("block");
            buttondown = true;
            attackb.interactable = false;
            button.interactable = false;
        }
        else {
            return;
        }
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        if (!AttackButton.Instance.buttondown && buttondown == true) {
            control.updateAnims("unblock");
            buttondown = false;
            attackb.interactable = true;
            button.interactable = true;
        }
    }
}
