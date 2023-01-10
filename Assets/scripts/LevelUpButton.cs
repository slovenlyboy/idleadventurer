using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelUpButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{


    public enum STATUS_TYPE
    {
        HP,    // �O�[
        ATK,  // �`���L
        SPEED,    // �p�[
    }

    [SerializeField]
    STATUS_TYPE LevelupType;

    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        switch (LevelupType) {


            case STATUS_TYPE.HP:
                gameManager.GetComponent<GameManager>().LevelupHp();
                break;

            case STATUS_TYPE.ATK:
                gameManager.GetComponent<GameManager>().LevelupAtk();
                break;

            case STATUS_TYPE.SPEED:
                gameManager.GetComponent<GameManager>().LevelupSpeed();
                break;


        }



    }

    // �^�b�v�_�E��  
    public void OnPointerDown(PointerEventData eventData) { }
    // �^�b�v�A�b�v  
    public void OnPointerUp(PointerEventData eventData) { }

}
