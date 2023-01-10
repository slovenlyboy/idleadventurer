using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSpeedButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    float nomal = 1.0f;
    float fast = 1.5f;
    float faster = 2.0f;

    enum GAME_SPEED {
        nomal,
        fast,
        faster,

    }

    [SerializeField]
    TextMeshProUGUI butonText;

    GAME_SPEED currentSpeed = GAME_SPEED.nomal;

    void Start()
    {
        butonText.text = "1X";
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        switch (currentSpeed) {
            case GAME_SPEED.nomal:
                currentSpeed = GAME_SPEED.fast;
                Time.timeScale = fast;
                butonText.text = "1.5X";
                break;

            case GAME_SPEED.fast:
                currentSpeed = GAME_SPEED.faster;
                Time.timeScale = faster;
                butonText.text = "2X";
                break;

            case GAME_SPEED.faster:
                currentSpeed = GAME_SPEED.nomal;
                Time.timeScale = nomal;
                butonText.text = "1X";
                break;
        }

       
    }


    // タップダウン  
    public void OnPointerDown(PointerEventData eventData) { }
    // タップアップ  
    public void OnPointerUp(PointerEventData eventData) { }
}
