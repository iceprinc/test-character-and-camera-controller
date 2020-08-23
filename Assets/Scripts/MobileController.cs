using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    [SerializeField]//отображение в инспекторе
    public Image joystickBG, joystick;
    private Vector2 inputVector;//полученные координаты джостика
    private void Start()
    {
        //joystickBG = GetComponent<Image>();
        //joystick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData eventData)//отжатие
    {
        OnDrag(eventData);
    }
    public virtual void OnPointerUp(PointerEventData eventData)//нажатие
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData eventData)//удержание
    {
        Vector2 pos;//позиция касания на джостик
        //сравнения угла отклонения между центром объекта касания и местом касания
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform,eventData.position,eventData.pressEventCamera,out pos))
        {
            //координаты касания
            pos.x = (pos.x/joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);
            //установка точных координат из касания
            inputVector = new Vector2(pos.x*2,pos.y*2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x*(joystickBG.rectTransform.sizeDelta.x/2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }
}
