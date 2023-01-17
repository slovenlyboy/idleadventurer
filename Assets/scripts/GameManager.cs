using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //������
    [SerializeField]
    int money = 0;
    //�������\���p�e�L�X�g
    [SerializeField]
    TextMeshProUGUI moneyText;
    //�Q�[���}�l�[�W���[�̃C���X�^���X
    public static GameManager instance = null;
    //�@�f�[�^�𐶐����ێ����Ă���X�N���v�g
    private SaveManager saveManager;

    //�ێ����Ă����X�e�[�^�X�f�[�^
    public string tmpName;
    public int tmpHP;
    public int tmpAtk;
    public int tmpSpeed;

    //�����Ɏg�����z�i�b��j
    public int needGoldHP = 10;
    public int needGoldAtk = 100;
    public int needGoldSpeed = 30;

    //���s�̃X�e�[�W
    public int stage = 1;

    //�ێ����Ă���d��
    [SerializeField]
    public Work[] Works;

    //�t�F�[�h�C���Ŏg�p����摜
    [SerializeField]
    Image fadeImage;
    //�t�F�[�h�Ǘ��Ŏg�p����R���[�`���i�؂�ւ����Ƀ��Z�b�g���邽�߁j
    Coroutine _fadeCorutine;


    void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        //�f�[�^��ǂݍ���
        saveManager = GetComponent<SaveManager>();
        saveManager.Load();

        //�������̎擾
        money = saveManager.GetMoney();

        //�q�[���[�̃X�e�[�^�X��ێ�
        string[] arr = GetChara()[0].Split(':');
        tmpName = arr[1];
        arr = GetChara()[1].Split(':');
        tmpHP = int.Parse(arr[1]);
        arr = GetChara()[2].Split(':');
        tmpAtk = int.Parse(arr[1]);
        arr = GetChara()[3].Split(':');
        tmpSpeed = int.Parse(arr[1]);

        // �C�x���g�ɃC�x���g�n���h���[��ǉ�
        SceneManager.sceneLoaded += SceneLoaded;
        //�ŏ���BGM�Đ�
        gameObject.GetComponent<SoundManager>().PlayBGM(SceneManager.GetActiveScene().name);

    }

    // �V�[���ǂݍ��ݎ��̃C�x���g�n���h���[
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        //Debug.Log(nextScene.name);
        //Debug.Log(mode);
        //�V�[���؂�ւ�����BGM��؂�ւ���
        gameObject.GetComponent<SoundManager>().PlayBGM(SceneManager.GetActiveScene().name);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //�L�����쐬�i���������܂ށj
    public void CreateChara() {

        saveManager.CreateChara();

        saveManager.Save();

        UpdateMenu();

    }

    //�Z�[�u�f�[�^�����l���̃X�e�[�^�X�𕶎����X�g�Ŏ擾
    public List<string> GetChara() {
        return saveManager.GetCharaStatus();
    }

    //�����𑝂₷�i�Z�[�u���s���j
    public void AddMoney(int getMoney)
    {

        money += getMoney;

        saveManager.SetMoney(money);

        saveManager.Save();
    }

    //���������擾����
    public int GetMoney()
    {

        return money;

    }

    //�ꎞ�I�ɕێ������X�e�[�^�X�𕶎����X�g�ŕԂ�
    public List<string> GetTmpStatusData() {


        //����̃f�[�^����X�e�[�^�X���擾
        string[] arr = GetChara()[0].Split(':');
        tmpName = arr[1];
        arr = GetChara()[1].Split(':');
        tmpHP = int.Parse(arr[1]);
        arr = GetChara()[2].Split(':');
        tmpAtk = int.Parse(arr[1]);
        arr = GetChara()[3].Split(':');
        tmpSpeed = int.Parse(arr[1]);


        string str = "name:" + tmpName + " hp:" + tmpHP + " atk:" + tmpAtk + " speed:" + tmpSpeed;

        arr = str.Split(' ');

        var list = new List<string>();
        list.AddRange(arr);
        Console.WriteLine("{0}", string.Join(" ", list));

        //Debug.Log(list);

        return list;

    }

    //�������̃��Z�b�g
    public void ResetMoney(){
        money = 0;
        saveManager.SetMoney(money);
        saveManager.Save();
       //�\�����X�V����
        UpdateGold();
    }

    //���O��ύX����
    public void  ChangeName(string newName)
    {

        tmpName = newName;
        saveManager.UpdateChara(UpdateCharaData());
        saveManager.Save();

        //�\���X�V
        UpdateMenu();

    }

    //�X�e�[�^�X�擾�ɕK�v�ȋ��z���擾
    //HP
    public int GetNeedMoneyHP() {
        return needGoldHP;
    }
    //ATK
    public int GetNeedMoneyAtk()
    {
        return needGoldAtk;
    }
    //Speed
    public int GetNeedMoneySpeed()
    {
        return needGoldSpeed;
    }

    //�e�X�e�[�^�X�̐���
    //HP
    public void LevelupHp() {



        //�v�����邨��������Ώ���ċ�������
        if (money >= needGoldHP)
        {
            tmpHP++;
            money -= needGoldHP;


            saveManager.SetMoney(money);
            saveManager.UpdateChara(UpdateCharaData());
            saveManager.Save();

        }
        else {

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("�� �� �� �� �� �� �� �� ��H. �� �� �� �� �� �� �� �� �� �� �� ��");
        }

        //�\���X�V
        UpdateMenu();

    }

    //ATK
    public void LevelupAtk()
    {

     

        //�v�����邨��������Ώ���ċ�������
        if (money >= needGoldAtk)
        {
            tmpAtk++;
            money -= needGoldAtk;


            saveManager.SetMoney(money);
            saveManager.UpdateChara(UpdateCharaData());
            saveManager.Save();

        }
        else
        {

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("�� �� �� �� �� �� �� �� ��H. �� �� �� �� �� �� �� �� �� �� �� ��");
        }

        UpdateMenu();
    }

    //ATK
    public void LevelupSpeed()
    {


        //�v�����邨��������Ώ���ċ�������
        if (money >= needGoldSpeed)
        {
            tmpSpeed++;
            money -= needGoldSpeed;


            saveManager.SetMoney(money);
            saveManager.UpdateChara(UpdateCharaData());
            saveManager.Save();

        }
        else
        {

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("�� �� �� �� �� �� �� �� ��H. �� �� �� �� �� �� �� �� �� �� �� ��");
        }

        UpdateMenu();
    }

    //�ꎞ�ۑ������L�����f�[�^��Ԃ�
    public string UpdateCharaData() {

        return "name:" + tmpName + " hp:" + tmpHP + " atk:" + tmpAtk + " speed:" + tmpSpeed;
    }


    //�\�����X�V����
    public void UpdateMenu() {

        GameObject.Find("StatusPanel").GetComponent<statusText>().UpdateStatus();
    }

    //�\�����z�̍X�V
    public void UpdateGold()
    {

        if (!moneyText) {
            moneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        }
       
       moneyText.text = GetMoney()+"G";
    }


    //�X�e�[�W���̎擾
    public int GetStage() {

        return stage;
    }

    //�X�e�[�W���̎擾
    public void SetStage(int stageNum)
    {
        stage = stageNum;
    }


    //�V�[���؂�ւ��@�i�t�F�[�h���ԁj
    public IEnumerator ChangeScene(string sceneName ,float time) {

        _fadeCorutine = StartCoroutine(FadeAnime(time,"fadeIn"));

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(sceneName);

        _fadeCorutine = StartCoroutine(FadeAnime(time, "fadeOut"));

    }

    //�t�F�[�h�̃A�j���[�V����
    IEnumerator FadeAnime(float time, string stg)
    {

        fadeImage.enabled = true;

        if (stg == "fadeIn")
        {
            fadeImage.fillAmount = 0;
        }
        else {
            fadeImage.fillAmount = 1;
        }

        

        //0f���o�ߎ��Ԃɂ���
        float elapsedTime = 0.0f;

        //time���O�ɂȂ�܂Ń��[�v������
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // �e�L�X�g�̍X�V
            if (stg == "fadeIn")
            {
                fadeImage.fillAmount = rate;

            }
            else
            {
                fadeImage.fillAmount = 1 - rate;

            }

            elapsedTime += Time.deltaTime;
            // 0.01�b�҂�
            yield return new WaitForSeconds(0.01f);
        }
        // 

        yield return new WaitForSeconds(time);

        if (stg == "fadeIn")
        {
            fadeImage.fillAmount = 1;
        }
        else
        {
            fadeImage.fillAmount = 0;
        }

        fadeImage.enabled = false;

       

        StopCoroutine(_fadeCorutine);
    }

}
