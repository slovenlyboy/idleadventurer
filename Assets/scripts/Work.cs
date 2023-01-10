using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour
{
    //仕事の名前
    [SerializeField]
    string workName = "";

    //必要な時間
    [SerializeField]
    float needTime;

    //経過時間
    [SerializeField]
    float currentTime;
    [SerializeField]
    int getMoney = 10;

    //稼働中か
    [SerializeField]
    bool isActive = false;

    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //アクティブなら時間経過
        if (isActive) {

            currentTime += Time.deltaTime;

            if (currentTime >= needTime) {

                currentTime = 0;

                gameManager.addMoney(getMoney);

                gameManager.updateGold();

            }
        }
    }


    //仕事をセットして
    public void SetWork(string name,float time,int money)
    {

        workName = name;
       
        getMoney = money;

        needTime = time;

        //時間リセット
        currentTime = 0;
        //稼働
        isActive = true;
        
    }

    public float GetNeedTime() {

        return needTime;
    }

    public float GetCurrentTime()
    {

        return currentTime;
    }

}
