using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StatusResetButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        gameManager.GetComponent<GameManager>().ResetMoney();
        gameManager.GetComponent<GameManager>().CreateChara();

    }


    // タップダウン  
    public void OnPointerDown(PointerEventData eventData) { }
    // タップアップ  
    public void OnPointerUp(PointerEventData eventData) { }
}
