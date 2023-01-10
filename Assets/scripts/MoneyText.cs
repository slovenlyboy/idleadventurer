using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{

    TextMeshProUGUI moneyText;

    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();

        gameManager = GameObject.Find("GameManager");

        moneyText.text = gameManager.GetComponent<GameManager>().GetMoney()+"G";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
