using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    public UnityEvent buttonClick;

    public Sprite buttonNormal;
    public Sprite buttonHover;

    void Awake()
    {

        if (buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }
    }


    void OnMouseDown()
    {
        //print("Click");
        buttonClick.Invoke();
    }

    void OnMouseEnter()
    {
        GetComponent<Image>().sprite = buttonHover;
    }

    void OnMouseExit()
    {
        GetComponent<Image>().sprite = buttonNormal;
    }
}
