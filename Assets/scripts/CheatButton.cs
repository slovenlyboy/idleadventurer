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

        gameManager.GetComponent<GameManager>().addMoney(1000);

        gameManager.GetComponent<GameManager>().updateGold();

    }


    // タップダウン  
    public void OnPointerDown(PointerEventData eventData) { }
    // タップアップ  
    public void OnPointerUp(PointerEventData eventData) { }
}
