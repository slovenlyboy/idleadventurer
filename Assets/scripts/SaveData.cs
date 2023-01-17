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

    //�L�����f�[�^�i������ŕێ��j
    public string charaData;

  

    //�ō����B�G���A(�����l��1)
    public int lastStage = 1;

    public void SetMoney(int money)
    {
        this.money = money;
    }

    //�L�����f�[�^�ۑ�
    public void SaveCharaData(String charaData) {

        //json���Ɣz�����C�ɕۑ��ł��Ȃ��̂ŕ������ĕۑ�
        this.charaData = charaData;
        
    }
    
    //�L�����f�[�^�擾
    public String GetCharaData()
    {
        if (charaData != null) {
            return charaData;
        }

        return null;
    }

    //�L�����̃X�e�[�^�X�𕶎����X�g�Ŏ擾
    public List<string> GetCharaStatus() {

        string str = charaData;

        string[] arr = str.Split(' ');

        var list = new List<string>();
        list.AddRange(arr);
        Console.WriteLine("{0}", string.Join(" ", list));


        return list;
    }

    //�ʏ�f�[�^�̎擾
    public string GetNormalData()
    {
        return "money: " + money;
    }


    //�������̎擾
    public int GetMoney()
    {
        return  money;
    }

    public string GetJsonData()
    {
        return JsonUtility.ToJson(this);
    }

}
