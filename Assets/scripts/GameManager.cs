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

    [SerializeField]
    int money = 0;
    [SerializeField]
    TextMeshProUGUI moneyText;

    public static GameManager Instance = null;

    //　データを生成し保持しているスクリプト
    private SaveManager saveManager;

    //保持しておくステータスデータ
    public string tmpName;
    public int tmpHP;
    public int tmpAtk;
    public int tmpSpeed;


    public int needGoldHP = 10;
    public int needGoldAtk = 100;
    public int needGoldSpeed = 30;

    public int stage = 1;

    [SerializeField]
    public Work[] Works;

    [SerializeField]
    Image fadeImage;

    Coroutine _fadeCorutine;


    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        saveManager = GetComponent<SaveManager>();

        saveManager.Load();

        money = saveManager.GetMoney();

        //ヒーローのステータスを保持
        string[] arr = GetUnit()[0].Split(':');
        tmpName = arr[1];
        arr = GetUnit()[1].Split(':');
        tmpHP = int.Parse(arr[1]);
        arr = GetUnit()[2].Split(':');
        tmpAtk = int.Parse(arr[1]);
        arr = GetUnit()[3].Split(':');
        tmpSpeed = int.Parse(arr[1]);

        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;

        gameObject.GetComponent<SoundManager>().PlayBGM(SceneManager.GetActiveScene().name);

    }

    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log(nextScene.name);
        Debug.Log(mode);

        gameObject.GetComponent<SoundManager>().PlayBGM(SceneManager.GetActiveScene().name);

    }





    // Update is called once per frame
    void Update()
    {

    }

    public void CreateChara() {

        saveManager.CreateChara();

        saveManager.Save();

        updateMenu();

    }

    public List<string> GetUnit() {
        return saveManager.GetUnitStatus();
    }

    public void addMoney(int getMoney)
    {

        money += getMoney;

        saveManager.SetMoney(money);

        saveManager.Save();
    }

    public int GetMoney()
    {

        return money;

    }

    public List<string> GetTmpStatusData() {


        //現状のデータからステータスを取得

        string[] arr = GetUnit()[0].Split(':');
        tmpName = arr[1];
        arr = GetUnit()[1].Split(':');
        tmpHP = int.Parse(arr[1]);
        arr = GetUnit()[2].Split(':');
        tmpAtk = int.Parse(arr[1]);
        arr = GetUnit()[3].Split(':');
        tmpSpeed = int.Parse(arr[1]);


        string str = "name:" + tmpName + " hp:" + tmpHP + " atk:" + tmpAtk + " speed:" + tmpSpeed;

        arr = str.Split(' ');

        var list = new List<string>();
        list.AddRange(arr);
        Console.WriteLine("{0}", string.Join(" ", list));

        //Debug.Log(list);

        return list;

    }

    public void ResetMoney(){
        money = 0;
        saveManager.Save();
        updateGold();
    }

    public void  ChangeName(string newName)
    {

        tmpName = newName;
        saveManager.UpdateChara(UpdateUnitData());
        saveManager.Save();


        updateMenu();

    }


    public int GetNeedMoneyHP() {
        return needGoldHP;
    }
    public int GetNeedMoneyAtk()
    {
        return needGoldAtk;
    }
    public int GetNeedMoneySpeed()
    {
        return needGoldSpeed;
    }



    public void LevelupHp() {



        //要求するお金があれば消費して強化する
        if (money >= needGoldHP)
        {
            tmpHP++;
            money -= needGoldHP;


            saveManager.SetMoney(money);
            saveManager.UpdateChara(UpdateUnitData());
            saveManager.Save();

        }
        else {

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("お 金 が た り ま せ ん よ？. 早 く 稼 い で き て く だ さ い ね");
        }

        updateMenu();

    }

    public void LevelupAtk()
    {

     

        //要求するお金があれば消費して強化する
        if (money >= needGoldAtk)
        {
            tmpAtk++;
            money -= needGoldAtk;


            saveManager.SetMoney(money);
            saveManager.UpdateChara(UpdateUnitData());
            saveManager.Save();

        }
        else
        {

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("お 金 が た り ま せ ん よ？. 早 く 稼 い で き て く だ さ い ね");
        }

        updateMenu();
    }

    public void LevelupSpeed()
    {


        //要求するお金があれば消費して強化する
        if (money >= needGoldSpeed)
        {
            tmpSpeed++;
            money -= needGoldSpeed;


            saveManager.SetMoney(money);
            saveManager.UpdateChara(UpdateUnitData());
            saveManager.Save();

        }
        else
        {

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("お 金 が た り ま せ ん よ？. 早 く 稼 い で き て く だ さ い ね");
        }

        updateMenu();
    }

    public string UpdateUnitData() {

        return "name:" + tmpName + " hp:" + tmpHP + " atk:" + tmpAtk + " speed:" + tmpSpeed;
    }


    //表示を更新する
    public void updateMenu() {

        GameObject.Find("StatusPanel").GetComponent<statusText>().UpdateStatus();
    }


    public void updateGold()
    {

        if (!moneyText) {
            moneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        }
       
       moneyText.text = GetMoney()+"G";
    }


    //ステージ情報の取得
    public int GetStage() {

        return stage;
    }

    //ステージ情報の取得
    public void SetStage(int stageNum)
    {
        stage = stageNum;
    }


    //シーン切り替え　（フェード時間）
    public IEnumerator changeScene(string sceneName ,float time) {

        _fadeCorutine = StartCoroutine(fadeAnime(time,"fadeIn"));

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(sceneName);

        _fadeCorutine = StartCoroutine(fadeAnime(time, "fadeOut"));

    }


    IEnumerator fadeAnime(float time, string stg)
    {

        fadeImage.enabled = true;

        if (stg == "fadeIn")
        {
            fadeImage.fillAmount = 0;
        }
        else {
            fadeImage.fillAmount = 1;
        }

        

        //0fを経過時間にする
        float elapsedTime = 0.0f;

        //timeが０になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // テキストの更新
            if (stg == "fadeIn")
            {
                fadeImage.fillAmount = rate;

            }
            else
            {
                fadeImage.fillAmount = 1 - rate;

            }

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
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
