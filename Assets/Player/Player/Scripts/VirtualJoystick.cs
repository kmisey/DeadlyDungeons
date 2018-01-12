using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image bgImg;
    private Image joystickImg;
    public bool joystickEnabled { get; set; }

    public static VirtualJoystick Instance { get; set; }
    public Quaternion rotation {
        get { return getRotation(); }
    }
    public bool buttondown { get; set; }

    public Vector3 InputDirection { set; get; }

	// Use this for initialization
	private void Awake () {
        joystickEnabled = true;
        Instance = this;
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped) {
        if (joystickEnabled) {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle
                (bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

                float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
                float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

                InputDirection = new Vector3(x, 0, y);

                if (InputDirection.magnitude > 0.05f) {
                    InputDirection = InputDirection.normalized;
                }
                joystickImg.rectTransform.anchoredPosition =
                    new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3), InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3), 0);
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData ped) {
            buttondown = true;
            OnDrag(ped);
    }
	
    public virtual void OnPointerUp(PointerEventData ped) {
        buttondown = false;
        joystickImg.rectTransform.anchoredPosition =
                new Vector3(0, 0, 0);
    }

    public Quaternion getRotation() {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, (Mathf.Atan2(-InputDirection.z, InputDirection.x)/3.14f)*180.0f, 0));
        return rotation;
    }
}
