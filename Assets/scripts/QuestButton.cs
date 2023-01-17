using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class QuestButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{



    [SerializeField]
    int stageNum = 1;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        // Debug.Log("押された!");  // ログを出力
        gameManager.SetStage(stageNum);


        StartCoroutine(gameManager.ChangeScene("DungeonScene",0.5f));

       
    }

    // タップダウン  
    public void OnPointerDown(PointerEventData eventData) { }
    // タップアップ  
    public void OnPointerUp(PointerEventData eventData) { }

}
