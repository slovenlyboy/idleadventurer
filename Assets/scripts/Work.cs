using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour
{
    //�d���̖��O
    [SerializeField]
    string workName = "";

    //�K�v�Ȏ���
    [SerializeField]
    float needTime;

    //�o�ߎ���
    [SerializeField]
    float currentTime;
    [SerializeField]
    int getMoney = 10;

    //�ғ�����
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
        //�A�N�e�B�u�Ȃ玞�Ԍo��
        if (isActive) {

            currentTime += Time.deltaTime;

            if (currentTime >= needTime) {

                currentTime = 0;

                gameManager.addMoney(getMoney);

                gameManager.updateGold();

            }
        }
    }


    //�d�����Z�b�g����
    public void SetWork(string name,float time,int money)
    {

        workName = name;
       
        getMoney = money;

        needTime = time;

        //���ԃ��Z�b�g
        currentTime = 0;
        //�ғ�
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
