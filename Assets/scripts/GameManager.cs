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
    //所持金
    [SerializeField]
    int money = 0;
    //所持金表示用テキスト
    [SerializeField]
    TextMeshProUGUI moneyText;
    //ゲームマネージャーのインスタンス
    public static GameManager instance = null;
    //　データを生成し保持しているスクリプト
    private SaveManager saveManager;

    //保持しておくステータスデータ
    public string tmpName;
    public int tmpHP;
    public int tmpAtk;
    public int tmpSpeed;

    //成長に使う金額（暫定）
    public int needGoldHP = 10;
    public int needGoldAtk = 100;
    public int needGoldSpeed = 30;

    //現行のステージ
    public int stage = 1;

    //保持している仕事
    [SerializeField]
    public Work[] Works;

    //フェードインで使用する画像
    [SerializeField]
    Image fadeImage;
    //フェード管理で使用するコルーチン（切り替え時にリセットするため）
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
        //データを読み込む
        saveManager = GetComponent<SaveManager>();
        saveManager.Load();

        //所持金の取得
        money = saveManager.GetMoney();

        //ヒーローのステータスを保持
        string[] arr = GetChara()[0].Split(':');
        tmpName = arr[1];
        arr = GetChara()[1].Split(':');
        tmpHP = int.Parse(arr[1]);
        arr = GetChara()[2].Split(':');
        tmpAtk = int.Parse(arr[1]);
        arr = GetChara()[3].Split(':');
        tmpSpeed = int.Parse(arr[1]);

        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        //最初のBGM再生
        gameObject.GetComponent<SoundManager>().PlayBGM(SceneManager.GetActiveScene().name);

    }

    // シーン読み込み時のイベントハンドラー
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        //Debug.Log(nextScene.name);
        //Debug.Log(mode);
        //シーン切り替え時にBGMを切り替える
        gameObject.GetComponent<SoundManager>().PlayBGM(SceneManager.GetActiveScene().name);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //キャラ作成（初期化も含む）
    public void CreateChara() {

        saveManager.CreateChara();

        saveManager.Save();

        UpdateMenu();

    }

    //セーブデータから主人公のステータスを文字リストで取得
    public List<string> GetChara() {
        return saveManager.GetCharaStatus();
    }

    //お金を増やす（セーブも行う）
    public void AddMoney(int getMoney)
    {

        money += getMoney;

        saveManager.SetMoney(money);

        saveManager.Save();
    }

    //所持金を取得する
    public int GetMoney()
    {

        return money;

    }

    //一時的に保持したステータスを文字リストで返す
    public List<string> GetTmpStatusData() {


        //現状のデータからステータスを取得
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

    //所持金のリセット
    public void ResetMoney(){
        money = 0;
        saveManager.SetMoney(money);
        saveManager.Save();
       //表示を更新する
        UpdateGold();
    }

    //名前を変更する
    public void  ChangeName(string newName)
    {

        tmpName = newName;
        saveManager.UpdateChara(UpdateCharaData());
        saveManager.Save();

        //表示更新
        UpdateMenu();

    }

    //ステータス取得に必要な金額を取得
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

    //各ステータスの成長
    //HP
    public void LevelupHp() {



        //要求するお金があれば消費して強化する
        if (money >= needGoldHP)
        {
            tmpHP++;
            money -= needGoldHP;


            saveManager.SetMoney(money);
            saveManager.UpdateChara(UpdateCharaData());
            saveManager.Save();

        }
        else {

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("お 金 が た り ま せ ん よ？. 早 く 稼 い で き て く だ さ い ね");
        }

        //表示更新
        UpdateMenu();

    }

    //ATK
    public void LevelupAtk()
    {

     

        //要求するお金があれば消費して強化する
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

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("お 金 が た り ま せ ん よ？. 早 く 稼 い で き て く だ さ い ね");
        }

        UpdateMenu();
    }

    //ATK
    public void LevelupSpeed()
    {


        //要求するお金があれば消費して強化する
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

            GameObject.Find("textWindow").GetComponent<MoveText>().ReadText("お 金 が た り ま せ ん よ？. 早 く 稼 い で き て く だ さ い ね");
        }

        UpdateMenu();
    }

    //一時保存したキャラデータを返す
    public string UpdateCharaData() {

        return "name:" + tmpName + " hp:" + tmpHP + " atk:" + tmpAtk + " speed:" + tmpSpeed;
    }


    //表示を更新する
    public void UpdateMenu() {

        GameObject.Find("StatusPanel").GetComponent<statusText>().UpdateStatus();
    }

    //表示金額の更新
    public void UpdateGold()
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
    public IEnumerator ChangeScene(string sceneName ,float time) {

        _fadeCorutine = StartCoroutine(FadeAnime(time,"fadeIn"));

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(sceneName);

        _fadeCorutine = StartCoroutine(FadeAnime(time, "fadeOut"));

    }

    //フェードのアニメーション
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
