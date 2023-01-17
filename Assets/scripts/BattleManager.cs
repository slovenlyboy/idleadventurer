using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Threading;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;
using Image = UnityEngine.UI.Image;
using Unity.Collections.LowLevel.Unsafe;

public class BattleManager : MonoBehaviour
{
    //敵
    [SerializeField]
    Chara[] Enemys;
    //敵の生存数
    int enemyAliveCount = 0;
    //味方
    [SerializeField]
    Chara[] Charas;
    //味方の生存数
    int heroAliveCount = 0;
    //全ユニットを格納するリスト
    List<Chara> TmpList = new List<Chara>();

    List<Chara> AliveCharaList = new List<Chara>();

    List<Chara> AliveEnemyList = new List<Chara>();

    //最大ウェーブ数
    int MaxWave = 5;
    //現在のウェーブ数
    int CurrentWave = 1;
    //敵のカウント
    int enemyCount = 1;
    //戦闘アクティブ状態か
    bool isActive = true;

    //戦闘状況を表示するテキスト
    [SerializeField]
    TextMeshProUGUI battleText1;
    [SerializeField]
    TextMeshProUGUI battleText2;
    //所持金を表示するテキスト
    [SerializeField]
    TextMeshProUGUI moneyText;
    //ゲームマネージャー
    GameManager gameManager;
    //戦闘se
    [SerializeField]
    AudioClip[] se;
    AudioSource audioSource;
    //敵情報データ
    EnemyInformation enemyInfo;

    //所持金
    float money;

    //戦闘制御で使用するコルーチン
    Coroutine _coroutine;
    Coroutine _coroutine2;

    //ステージ背景（切り替え用に持っておく）
    [SerializeField]
    Image stageBG;

    // Start is called before the first frame update
    void Start()
    {
        //敵情報の取得
        enemyInfo = new EnemyInformation();
        enemyInfo.Init();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        audioSource = GetComponent<AudioSource>();

        //エンカウント
        encounter();

        //StartBattle();

        //背景差し替え
       stageBG.sprite = Resources.Load<Sprite>("image/bg/Dungeon/"+gameManager.GetStage());

        //ゲーム速度を通常に戻す
        Time.timeScale = 1;

        //ゴールド表記を更新
        money = gameManager.GetMoney();

        moneyText.text = gameManager.GetMoney() + "G";

    }

    // Update is called once per frame
    void Update()
    {

        /*if (isActive) {
            //エンターキーが入力された場合「true」
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //行動を残しているユニットがいれば行動させる
                if (TmpList.Count > 0)
                {
                    Battle();
                }
                else
                {
                    StartBattle();
                }
            }
        }*/

    }

    //戦闘開始処理
    public void StartBattle()
    {
        //どちらかが全滅したら終了
        AliveCheck();

        //全てのユニットのSpeedを参照して行動順を決定

        int cnt = 1;

        //リストに敵味方全て入れ込む
        foreach (Chara enemy in Enemys)
        {
            if (enemyCount < cnt) {

                continue;
            }

            if (enemy.GetStatus() == Chara.STATUS.Alive) {
                TmpList.Add(enemy);
            }

            cnt++;
        }
           
        foreach (Chara chara in Charas)
        {
            if (chara.GetStatus() == Chara.STATUS.Alive)
            {
                TmpList.Add(chara);
            }
        }

      

        //全てのユニットのSpeedを参照して行動順を決定
        TmpList.Sort((a, b) => b.GetSpeed() - a.GetSpeed());


    }

    //戦闘の処理
    public void Battle()
    {

        //生存リストを初期化
        AliveEnemyList = new List<Chara>();

        AliveCharaList = new List<Chara>();

        int cnt = 1;

        //リストに敵味方全て入れ込む
        foreach (Chara enemy in Enemys)
        {
            if (enemyCount < cnt)
            {
                continue;
            }

            if (enemy.GetStatus() == Chara.STATUS.Alive)
            {
                AliveEnemyList.Add(enemy);
            }

            cnt++;
        }

        foreach (Chara chara in Charas)
        {
            if (chara.GetStatus() == Chara.STATUS.Alive)
            {
                AliveCharaList.Add(chara);
            }
        }


        //たーげっと設定(とりあえず敵対勢力からランダム)
        foreach (Chara unit in AliveCharaList)
        {
            if (AliveEnemyList.Count > 0) {
                unit.SetTarget(AliveEnemyList[Random.Range(0, AliveEnemyList.Count)]);
            }

            
        }

        foreach (Chara unit in AliveEnemyList)
        {
            if (AliveCharaList.Count > 0)
            {
                unit.SetTarget(AliveCharaList[Random.Range(0, AliveCharaList.Count)]);
            }
        }

        //TmpList[0].GetTerget().TakeDamage(TmpList[0].GetAtk());
        if (TmpList[0].GetTarget().GetStatus() == Chara.STATUS.Alive) {
            DamageCheck(TmpList[0], TmpList[0].GetTarget());
        }

       
        //行動したキャラをリストから除外する
        TmpList.RemoveAt(0);

        //順番決定
       //foreach (Chara unit in TmpList)
        //{
          //  Debug.Log(unit.UnitName);
        //}

    }

    //生存確認
    void AliveCheck()
    {
        enemyAliveCount = 0;
        heroAliveCount = 0;

        int cnt = 1;

        foreach (Chara unit in Enemys)
        {

            if (enemyCount < cnt) {
                continue;
            }

            if (unit.GetStatus() == Chara.STATUS.Alive) {
                enemyAliveCount++;
            }

            cnt++;


        }

        foreach (Chara unit in Charas)
        {
            if (unit.GetStatus() == Chara.STATUS.Alive)
            {
                heroAliveCount++;
            }
        }

        if (enemyAliveCount <= 0) {
            
            StopCoroutine(_coroutine);
            StopCoroutine(_coroutine2);

            //全ウェーブクリアで戻る
            if (CurrentWave == MaxWave)
            {
                battleText1.text = "戦いに勝利した";
                battleText2.text = "";
                StartCoroutine(BackToMain());
            }
            else
            {
                battleText1.text = "";
                battleText2.text = "";
                CurrentWave++;

                encounter();
            }

        

        } else if(heroAliveCount<= 0) {

            StopCoroutine(_coroutine);
            StopCoroutine(_coroutine2);

            battleText1.text = "全滅した";
            battleText2.text = "";
            StartCoroutine(BackToMain());
            isActive = false;
        }
       
        
    }



    //戦闘のダメージチェック
    void DamageCheck(Chara attackUnit, Chara targetUnit)
    {
        if (attackUnit.GetStatus() == Chara.STATUS.Alive)
        {
            int damage = attackUnit.GetAtk();


            targetUnit.TakeDamage(damage);
            //攻撃時のモーション（拡縮）
            MoveAction(attackUnit.transform.gameObject, targetUnit.transform.gameObject);

           
            if(targetUnit.status == Chara.STATUS.Dead&& targetUnit.GetComponent<Enemy>())
            {

                int getMoney = targetUnit.GetComponent<Enemy>().GetDropMoney();

                gameManager.AddMoney(getMoney);

                StartCoroutine(MoneyAnimation(getMoney, 0.5f));
            }

            StartCoroutine(ShowBattleText(attackUnit.UnitName + "の攻撃",targetUnit.UnitName + "に" + damage + "のダメージ！"));


        }
        else {
            StartCoroutine(ShowBattleText("",""));
        }


    }

    //戦闘テキスト表示
    private IEnumerator ShowBattleText(string text1, string text2) {

        battleText1.text = "";
        battleText2.text = "";


        battleText1.text = text1;

        yield return new WaitForSeconds(1);


        if (text2 != "") {
            battleText2.text = text2;
        }
       

    }


    IEnumerator AutoBattle()
    {
        while (true)
        {
            // Do anything
            if (isActive) {

                //行動を残しているユニットがいれば行動させる
                if (TmpList.Count > 0)
                {
                    Battle();
                }
                else
                {
                    StartBattle();
                }

            }



            yield return new WaitForSeconds(2);
        }
    }



    //キャラの動き（攻撃、被ダメ）
    void MoveAction(GameObject unit , GameObject hitUnit) {

        Vector3 scale = unit.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale ;
        Vector3 scalemax =  new Vector3(scale.x*1.5f,scale.y * 1.5f, scale.z);


        audioSource.PlayOneShot(se[0]);
         unit.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOScale(new Vector3(scalemax.x, scalemax.y, scalemax.z), 0.4f).OnComplete(() =>
        {

            unit.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOScale(new Vector3(scale.x, scale.y, scale.z), 0.4f).OnComplete(() =>
            {
                audioSource.PlayOneShot(se[1]);
                hitUnit.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOPunchPosition(new Vector3(50, 0, 0), 0.4f).OnComplete(() => {


                    hitUnit.transform.GetChild(0).GetChild(1).GetComponent<HpBer>().TakeDamage();
                    //死んでいたら
                    if (hitUnit.GetComponent<Chara>().GetStatus() == Chara.STATUS.Dead) {

                        hitUnit.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);

                    }


                });

            });
        });


       


    }


    
    //遭遇演出
    void encounter() {

        //ヒーローのステータスを設定
         gameManager.GetChara();
        string[] arr = gameManager.GetChara()[0].Split(':');
        Charas[0].SetName(arr[1]);
        arr = gameManager.GetChara()[1].Split(':');
        Charas[0].SetHP(int.Parse(arr[1]));
        arr = gameManager.GetChara()[2].Split(':');
        Charas[0].SetAtk(int.Parse(arr[1]));
        arr = gameManager.GetChara()[3].Split(':');
        Charas[0].SetSpeed(int.Parse(arr[1]));

        Charas[0].status = Chara.STATUS.Alive;
        Charas[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

        Charas[0].transform.GetChild(0).GetChild(1).GetComponent<HpBer>().Init();



        //wave数で敵の数が増える
        if (CurrentWave < 2)
        {
            enemyCount = 1;

            Enemys[1].gameObject.SetActive(false);
            Enemys[2].gameObject.SetActive(false);

        }
        else if (CurrentWave < 3)
        {
            enemyCount = 2;
            Enemys[1].gameObject.SetActive(true);
            Enemys[2].gameObject.SetActive(false);
        }
        else if (CurrentWave == MaxWave)
        {
            enemyCount = 1;
            Enemys[1].gameObject.SetActive(false);
            Enemys[2].gameObject.SetActive(false);
        }
        else {

            enemyCount = 3;
            Enemys[1].gameObject.SetActive(true);
            Enemys[2].gameObject.SetActive(true);

        }

       int cnt = 1;

        foreach (Enemy enemy in Enemys)
        {

            if (enemyCount < cnt) {

                continue;

            }

            int enemyId;

            if (CurrentWave == MaxWave)
            {
                enemyId = 5 * gameManager.GetStage();
            }
            else {

                switch (gameManager.GetStage()) {
                    default:
                    case 1:
                        enemyId = Random.Range(1, 5);
                        break;
                    case 2:
                        enemyId = Random.Range(6, 10);
                        break;


                }

             
            }

           
            //敵の各種ステータス設定
            enemy.SetName(enemyInfo.enemyName[enemyId]);
            enemy.SetHP(enemyInfo.hp[enemyId]);
            enemy.SetAtk(enemyInfo.atk[enemyId]);
            enemy.SetName(enemyInfo.enemyName[enemyId]);
            enemy.SetDropMoney(enemyInfo.dropMoney[enemyId]);
            enemy.SetIsBoss(enemyInfo.isBoss[enemyId]);
            enemy.status = Chara.STATUS.Alive;

            //ボスだった場合は大きくする
            if (enemy.GetIsBoss() == 1)
            {

                enemy.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(3, 2, 1);


            }
            else {

                enemy.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            }
           

            enemy.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            enemy.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(enemyInfo.imageAddress[enemyId]);
            enemy.transform.GetChild(0).GetChild(1).GetComponent<HpBer>().Init();
           
          
            battleText1.text += enemy.UnitName;
            battleText1.text += "　";

            cnt++;
        }

        battleText2.text = "が現れた";

        //戦闘開始
        _coroutine2 = StartCoroutine(DelayCoroutine());

    }

    //数秒待ってから戦闘処理を開始する
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(2);

        _coroutine = StartCoroutine((AutoBattle()));

    }

    //メインシーンに戻る（戦闘終了時）
    private IEnumerator BackToMain()
    {
        yield return new WaitForSeconds(3);


        StartCoroutine(gameManager.ChangeScene("MainScene", 0.5f));
    }

    //お金の増減アニメーション
    IEnumerator MoneyAnimation(float addMoney, float time)
    {
        //前回のスコア
        float befor = money;
        //今回のスコア
        float after = money + addMoney;
       
        //0fを経過時間にする
        float elapsedTime = 0.0f;

        //timeが０になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // テキストの更新
            moneyText.text = (befor + (after - befor) * rate).ToString("f0") + "G";

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的な着地のスコア
        moneyText.text = after.ToString()+"G";

        money = gameManager.GetMoney();
    }


}
