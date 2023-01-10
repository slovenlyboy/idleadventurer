using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Chara
{

    int dropMoney;

    int isBoss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetDropMoney(int money) {

        dropMoney = money;
    }

    public int GetDropMoney()
    {
        return dropMoney;
    }

    public void SetIsBoss(int isboss)
    {

        isBoss = isboss;
    }

    public int GetIsBoss()
    {
        return isBoss;
    }

}
