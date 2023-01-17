using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //セーブデータを配置するパス
    string filePath;
    //データそのもの
    SaveData save;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //セーブパス設定
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        save = new SaveData();
        //データロード
        Load();
        //Debug.Log(save.GetJsonData());

    }

    //所持金取得
    public int GetMoney() {
        return save.GetMoney();
    }
    //所持金設定
    public void SetMoney(int money)
    {
         save.SetMoney(money);
    }

    //キャラデータ作成（新規）
    public void CreateChara() {

        Chara data =  gameObject.AddComponent<Chara>();

        UpdateChara(data.GetStatusData());

        Save();

        Destroy(gameObject.GetComponent<Chara>());

    }

    //キャラデータ更新
    public void UpdateChara(string charaData)
    {
        save.SaveCharaData(charaData);

        Save();

    }


    public string GetChara() {

        if (save.GetCharaData() != null) {
            return save.GetCharaData();
        }

        return null;
    }

    public List<string> GetCharaStatus()
    {

        if (save.GetCharaData() != null)
        {
            return save.GetCharaStatus();
        }

        return null;
    }

    //現在のデータをjson形式で保存する
    public void Save()
    {
        string json = JsonUtility.ToJson(save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();

    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<SaveData>(data);
        }
        else {

            gameManager.CreateChara();

            Save();
        }



    }

}
