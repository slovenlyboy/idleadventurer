using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheatButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        gameManager.GetComponent<GameManager>().AddMoney(1000);

        gameManager.GetComponent<GameManager>().UpdateGold();

    }


    // �^�b�v�_�E��  
    public void OnPointerDown(PointerEventData eventData) { }
    // �^�b�v�A�b�v  
    public void OnPointerUp(PointerEventData eventData) { }
}
