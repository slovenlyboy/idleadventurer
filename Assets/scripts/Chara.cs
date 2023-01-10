using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{

    [SerializeField]
    public string UnitName = "ヒーロー";
    [SerializeField]
    public int Atk = 2;
    [SerializeField]
    public int HP = 10;
    [SerializeField]
    public int Speed = 3;
    [SerializeField]
    Chara Target = null;

    public enum STATUS
    {
        Alive, Dead
    }

    //状態（基本は生存）
    public STATUS status = STATUS.Alive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetName()
    {
        return UnitName;
    }


    public int GetAtk() {
        return Atk;
    }

    public int GetHP()
    {
        return HP;
    }

    public int GetSpeed()
    {
        return Speed;
    }

    public void SetName(string name)
    {
        UnitName = name;
    }

    public void SetAtk(int atk)
    {
         Atk = atk;
    }

    public void SetHP(int hp)
    {
         HP = hp;
    }
    
    public void SetSpeed(int speed)
    {
        Speed = speed;
    }

    public void SetTarget(Chara unit) {
        Target = unit;
    }

    public Chara GetTarget()
    {
        return Target;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0) {
            status = STATUS.Dead;
        }
    }

    public STATUS GetStatus()
    {
        return status;
    }



    //基本情報
    public string GetStatusData()
    {
        return "name:" + UnitName + " hp:" + HP + " atk:" + Atk+ " speed:" + Speed;
    }




}
