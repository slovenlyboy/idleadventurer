using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //�Z�[�u�f�[�^��z�u����p�X
    string filePath;
    //�f�[�^���̂���
    SaveData save;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //�Z�[�u�p�X�ݒ�
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        save = new SaveData();
        //�f�[�^���[�h
        Load();
        //Debug.Log(save.GetJsonData());

    }

    //�������擾
    public int GetMoney() {
        return save.GetMoney();
    }
    //�������ݒ�
    public void SetMoney(int money)
    {
         save.SetMoney(money);
    }

    //�L�����f�[�^�쐬�i�V�K�j
    public void CreateChara() {

        Chara data =  gameObject.AddComponent<Chara>();

        UpdateChara(data.GetStatusData());

        Save();

        Destroy(gameObject.GetComponent<Chara>());

    }

    //�L�����f�[�^�X�V
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

    //���݂̃f�[�^��json�`���ŕۑ�����
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
