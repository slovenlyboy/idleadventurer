using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoutButton : MonoBehaviour,
IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    GameObject gameManager;

    public void OnPointerClick(PointerEventData eventData)
    {

        gameManager = GameObject.Find("GameManager");

        gameManager.GetComponent<GameManager>().CreateChara();

    }



    // �^�b�v�_�E��  
    public void OnPointerDown(PointerEventData eventData) { }
    // �^�b�v�A�b�v  
    public void OnPointerUp(PointerEventData eventData) { }

}
