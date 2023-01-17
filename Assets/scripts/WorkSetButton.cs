using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkSetButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    string workName;

    [SerializeField]
    int money;

    [SerializeField]
    float needTime;


    GameObject gameManager;

    GameObject panel;

    [SerializeField]
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        panel = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("�̂���");

        panel.SetActive(true);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
       // Debug.Log("�ł���");

        panel.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        gameManager.GetComponent<Work>().SetWork(workName, needTime, money);


        text.text = workName;
    }

    // �^�b�v�_�E��  
    public void OnPointerDown(PointerEventData eventData) { }
    // �^�b�v�A�b�v  
    public void OnPointerUp(PointerEventData eventData) { }


}

