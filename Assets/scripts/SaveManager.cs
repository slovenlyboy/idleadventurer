using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    string filePath;
    SaveData save;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        save = new SaveData();

        Load();


        //Debug.Log(save.GetJsonData());

    }

    public int GetMoney() {
        return save.GetMoney();
    }

    public void SetMoney(int money)
    {
         save.SetMoney(money);
    }

    public void CreateChara() {

        Chara data =  gameObject.AddComponent<Chara>();

        UpdateChara(data.GetStatusData());

        Save();

        Destroy(gameObject.GetComponent<Chara>());

    }

    public void UpdateChara(string charaData)
    {
        save.saveUnitData(charaData);

        Save();

    }


    public string GetUnit() {

        if (save.GetUnitData() != null) {
            return save.GetUnitData();
        }

        return null;
    }

    public List<string> GetUnitStatus()
    {

        if (save.GetUnitData() != null)
        {
            return save.getUnitStatus();
        }

        return null;
    }


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
