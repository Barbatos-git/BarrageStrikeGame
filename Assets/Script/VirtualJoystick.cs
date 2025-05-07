using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, 
    IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    public float joystickRadius = 100f;
    private Vector2 inputDirection = Vector2.zero;
    public Vector2 InputDirection => inputDirection;
    // Start is called before the first frame update
    void Start()
    {
        HideJoystick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out position
            );
        joystickBackground.anchoredPosition = position;
        ShowJoystick();
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
        HideJoystick();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 positoin;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground, 
            eventData.position, 
            eventData.pressEventCamera, 
            out positoin);
        positoin = Vector2.ClampMagnitude(positoin, joystickRadius);
        joystickHandle.anchoredPosition = positoin;
        inputDirection = positoin / joystickRadius;
    }

    private void ShowJoystick()
    {
        joystickBackground.gameObject.SetActive(true);
        joystickHandle.gameObject.SetActive(true);
    }

    private void HideJoystick()
    {
        joystickBackground.gameObject.SetActive(false);
        joystickHandle.gameObject.SetActive(false);
    }
}
