using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelOpenButton : MonoBehaviour,
IPointerClickHandler,  IPointerDownHandler,  IPointerUpHandler  
{
    public GameObject testObject;

    bool isActive = false;

    [SerializeField]
    string text;

    [SerializeField]
    GameObject textWindow;

    [SerializeField]
    GameObject[] otherPanels;

   public void OnPointerClick(PointerEventData eventData)
    {

 

        foreach(var panel in otherPanels)
        {
            panel.GetComponent<PanelOpenButton>().SetActive(false);
        }

        isActive = !isActive;

        testObject.SetActive(isActive);


        if (isActive)
        {
            textWindow.GetComponent<MoveText>().ReadText(text);
        }
        else {
            textWindow.GetComponent<MoveText>().ReadText("こ ん に ち は ！ 今 日 は ど の よ う な 御 用 で す か ？");
        }

        //SceneManager.LoadScene("DungeonScene", LoadSceneMode.Single);
    }


    public bool GetActive() {

        return isActive;

    }

    public void SetActive(bool flag)
    {

         isActive = flag;

         testObject.SetActive(isActive);

    }



    // タップダウン  
    public void OnPointerDown(PointerEventData eventData){}  
    // タップアップ  
    public void OnPointerUp(PointerEventData eventData){} 

}
