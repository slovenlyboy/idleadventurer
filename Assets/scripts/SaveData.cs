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

    //キャラデータ（文字列で保持）
    public string charaData;

  

    //最高到達エリア(初期値は1)
    public int lastStage = 1;

    public void SetMoney(int money)
    {
        this.money = money;
    }

    //キャラデータ保存
    public void SaveCharaData(String charaData) {

        //jsonだと配列を一気に保存できないので分解して保存
        this.charaData = charaData;
        
    }
    
    //キャラデータ取得
    public String GetCharaData()
    {
        if (charaData != null) {
            return charaData;
        }

        return null;
    }

    //キャラのステータスを文字リストで取得
    public List<string> GetCharaStatus() {

        string str = charaData;

        string[] arr = str.Split(' ');

        var list = new List<string>();
        list.AddRange(arr);
        Console.WriteLine("{0}", string.Join(" ", list));


        return list;
    }

    //通常データの取得
    public string GetNormalData()
    {
        return "money: " + money;
    }


    //所持金の取得
    public int GetMoney()
    {
        return  money;
    }

    public string GetJsonData()
    {
        return JsonUtility.ToJson(this);
    }

}
