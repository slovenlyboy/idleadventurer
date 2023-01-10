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



    // タップダウン  
    public void OnPointerDown(PointerEventData eventData) { }
    // タップアップ  
    public void OnPointerUp(PointerEventData eventData) { }

}
