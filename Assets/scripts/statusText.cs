using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class statusText : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI NameText;
    [SerializeField]
    TextMeshProUGUI HPText;
    [SerializeField]
    TextMeshProUGUI AtkText;
    [SerializeField]
    TextMeshProUGUI SpeedText;
    [SerializeField]
    TextMeshProUGUI NextHPText;
    [SerializeField]
    TextMeshProUGUI NextAtkText;
    [SerializeField]
    TextMeshProUGUI NextSpeedText;


    GameObject gameManager;

    void Awake()
    {

        gameManager = GameObject.Find("GameManager");


       /* foreach (string name in gameManager.GetComponent<GameManager>().GetUnit())
        {
            Debug.Log(name);
        }*/

       
        if (gameManager.GetComponent<GameManager>().GetTmpStatusData() != null)
        {
            NameText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[0];
            HPText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[1];
            AtkText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[2];
            SpeedText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[3];


            NextHPText.text ="1up/"+ gameManager.GetComponent<GameManager>().GetNeedMoneyHP() + "G";
            NextAtkText.text = "1up/" + + gameManager.GetComponent<GameManager>().GetNeedMoneyAtk() + "G";
            NextSpeedText.text = "1up/" + + gameManager.GetComponent<GameManager>().GetNeedMoneySpeed() + "G";
        }
        else
        {
            NameText.text = "–¢‰Á“ü";
        }
    }

        // Start is called before the first frame update
        void Start()
    {

       

        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatus();
    }

    void OnEnable() {
       
    }

    public void UpdateStatus()
    {
      
        if (gameManager.GetComponent<GameManager>().GetTmpStatusData() != null)
        {
            NameText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[0];
            HPText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[1];
            AtkText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[2];
            SpeedText.text = gameManager.GetComponent<GameManager>().GetTmpStatusData()[3];


            NextHPText.text = "1up/" + gameManager.GetComponent<GameManager>().GetNeedMoneyHP() + "G";
            NextAtkText.text = "1up/" + +gameManager.GetComponent<GameManager>().GetNeedMoneyAtk() + "G";
            NextSpeedText.text = "1up/" + +gameManager.GetComponent<GameManager>().GetNeedMoneySpeed() + "G";

        }
        else
        {
            NameText.text = "–¢‰Á“ü";
        }
    }


}
