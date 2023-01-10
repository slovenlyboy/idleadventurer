using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SaveData : object
{

    // ������
    public int money;

    //�������j�b�g�f�[�^�i������ŕێ��j
    public string unitData;

  

    //�ō����B�G���A(�����l��1)
    public int lastStage = 1;

    public void SetMoney(int money)
    {
        this.money = money;
    }


    public void saveUnitData(String charaData) {

        //json���Ɣz�����C�ɕۑ��ł��Ȃ��̂ŕ������ĕۑ�
        this.unitData = charaData;
        
    }
    

    public String GetUnitData()
    {
        if (unitData != null) {
            return unitData;
        }

        return null;
    }


    public List<string> getUnitStatus() {

        string str = unitData;

        string[] arr = str.Split(' ');

        var list = new List<string>();
        list.AddRange(arr);
        Console.WriteLine("{0}", string.Join(" ", list));


        return list;
    }


    public string GetNormalData()
    {
        return "money: " + money;
    }



    public int GetMoney()
    {
        return  money;
    }

    public string GetJsonData()
    {
        return JsonUtility.ToJson(this);
    }

}
