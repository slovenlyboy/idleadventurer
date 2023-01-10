using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SaveData : object
{

    // 所持金
    public int money;

    //所持ユニットデータ（文字列で保持）
    public string unitData;

  

    //最高到達エリア(初期値は1)
    public int lastStage = 1;

    public void SetMoney(int money)
    {
        this.money = money;
    }


    public void saveUnitData(String charaData) {

        //jsonだと配列を一気に保存できないので分解して保存
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
