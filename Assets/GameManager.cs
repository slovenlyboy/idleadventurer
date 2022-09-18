using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{



    [SerializeField]
    int money = 0;
    [SerializeField]
    TextMeshProUGUI  moneyText;

    [SerializeField]
    float waitTime = 2;


    public void addMoney(int getMoney){
    
    money += getMoney;

    }

    // Start is called before the first frame update
    void Start()
    {
         moneyText.text = "aaa";
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
