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
    //�G
    [SerializeField]
    Chara[] Enemys;
    //�G�̐�����
    int enemyAliveCount = 0;
    //����
    [SerializeField]
    Chara[] Charas;
    //�����̐�����
    int heroAliveCount = 0;
    //�S���j�b�g���i�[���郊�X�g
    List<Chara> TmpList = new List<Chara>();

    List<Chara> AliveCharaList = new List<Chara>();

    List<Chara> AliveEnemyList = new List<Chara>();

   
    int MaxWave = 5;

    int CurrentWave = 1;

    int enemyCount = 1;

    bool isActive = true;

    [SerializeField]
    TextMeshProUGUI battleText1;
    [SerializeField]
    TextMeshProUGUI battleText2;

    [SerializeField]
    TextMeshProUGUI moneyText;

    GameManager gameManager;

    [SerializeField]
    AudioClip[] se;

    AudioSource audioSource;
    EnemyInformation enemyInfo;

    float money;

    Coroutine _coroutine;
    Coroutine _coroutine2;


    [SerializeField]
    Image stageBG;
    // Start is called before the first frame update
    void Start()
    {

        enemyInfo = new EnemyInformation();
        enemyInfo.Init();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        audioSource = GetComponent<AudioSource>();

        encounter();

        //StartBattle();

        //�w�i�����ւ�
       stageBG.sprite = Resources.Load<Sprite>("image/bg/Dungeon/"+gameManager.GetStage());

        //�Q�[�����x��ʏ�ɖ߂�
        Time.timeScale = 1;

        //�S�[���h�\�L���X�V
        money = gameManager.GetMoney();

        moneyText.text = gameManager.GetMoney() + "G";

    }

    // Update is called once per frame
    void Update()
    {

        /*if (isActive) {
            //�G���^�[�L�[�����͂��ꂽ�ꍇ�utrue�v
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //�s�����c���Ă��郆�j�b�g������΍s��������
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

    //�퓬�J�n����
    public void StartBattle()
    {
        //�ǂ��炩���S�ł�����I��
        AliveCheck();

        //�S�Ẵ��j�b�g��Speed���Q�Ƃ��čs����������

        int cnt = 1;

        //���X�g�ɓG�����S�ē��ꍞ��
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

      

        //�S�Ẵ��j�b�g��Speed���Q�Ƃ��čs����������
        TmpList.Sort((a, b) => b.GetSpeed() - a.GetSpeed());


    }

    public void Battle()
    {

        //�������X�g��������
        AliveEnemyList = new List<Chara>();

        AliveCharaList = new List<Chara>();

        int cnt = 1;

        //���X�g�ɓG�����S�ē��ꍞ��
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


        //���[�����Ɛݒ�(�Ƃ肠�����G�ΐ��͂��烉���_��)
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

       
        //�s�������L���������X�g���珜�O����
        TmpList.RemoveAt(0);

        //���Ԍ���
       //foreach (Chara unit in TmpList)
        //{
          //  Debug.Log(unit.UnitName);
        //}

    }


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

            //�S�E�F�[�u�N���A�Ŗ߂�
            if (CurrentWave == MaxWave)
            {
                battleText1.text = "�킢�ɏ�������";
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

            battleText1.text = "�S�ł���";
            battleText2.text = "";
            StartCoroutine(BackToMain());
            isActive = false;
        }
       
        
    }




    void DamageCheck(Chara attackUnit, Chara targetUnit)
    {
        if (attackUnit.GetStatus() == Chara.STATUS.Alive)
        {
            int damage = attackUnit.GetAtk();


            targetUnit.TakeDamage(damage);
            //�U�����̃��[�V�����i�g�k�j
            MoveAction(attackUnit.transform.gameObject, targetUnit.transform.gameObject);

           
            if(targetUnit.status == Chara.STATUS.Dead&& targetUnit.GetComponent<Enemy>())
            {

                int getMoney = targetUnit.GetComponent<Enemy>().GetDropMoney();

                gameManager.addMoney(getMoney);

                StartCoroutine(MoneyAnimation(getMoney, 0.5f));
            }

            StartCoroutine(showBattleText(attackUnit.UnitName + "�̍U��",targetUnit.UnitName + "��" + damage + "�̃_���[�W�I"));


        }
        else {
            StartCoroutine(showBattleText("",""));
        }


    }

    private IEnumerator showBattleText(string text1, string text2) {

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

                //�s�����c���Ă��郆�j�b�g������΍s��������
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


                    hitUnit.transform.GetChild(0).GetChild(1).GetComponent<HpBer>().takeDamage();
                    //����ł�����
                    if (hitUnit.GetComponent<Chara>().GetStatus() == Chara.STATUS.Dead) {

                        hitUnit.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);

                    }


                });

            });
        });


       


    }



    void setUnits() { 
    
    
    
    }

    
    //�������o
    void encounter() {


        //�q�[���[�̃X�e�[�^�X��ݒ�
         gameManager.GetUnit();
        string[] arr = gameManager.GetUnit()[0].Split(':');
        Charas[0].SetName(arr[1]);
        arr = gameManager.GetUnit()[1].Split(':');
        Charas[0].SetHP(int.Parse(arr[1]));
        arr = gameManager.GetUnit()[2].Split(':');
        Charas[0].SetAtk(int.Parse(arr[1]));
        arr = gameManager.GetUnit()[3].Split(':');
        Charas[0].SetSpeed(int.Parse(arr[1]));

        Charas[0].status = Chara.STATUS.Alive;
        Charas[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

        Charas[0].transform.GetChild(0).GetChild(1).GetComponent<HpBer>().init();



        //wave���œG�̐���������
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

           

            enemy.SetName(enemyInfo.enemyName[enemyId]);
            enemy.SetHP(enemyInfo.hp[enemyId]);
            enemy.SetAtk(enemyInfo.atk[enemyId]);
            enemy.SetName(enemyInfo.enemyName[enemyId]);
            enemy.SetDropMoney(enemyInfo.dropMoney[enemyId]);
            enemy.SetIsBoss(enemyInfo.isBoss[enemyId]);
            enemy.status = Chara.STATUS.Alive;

            if (enemy.GetIsBoss() == 1)
            {

                enemy.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(3, 2, 1);


            }
            else {

                enemy.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            }
           

            enemy.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            enemy.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(enemyInfo.imageAddress[enemyId]);
            enemy.transform.GetChild(0).GetChild(1).GetComponent<HpBer>().init();
            Debug.Log(enemyInfo.imageAddress[enemyId]);
          
            battleText1.text += enemy.UnitName;
            battleText1.text += "�@";

            cnt++;
        }

        battleText2.text = "�����ꂽ";

        //�퓬�J�n
        _coroutine2 = StartCoroutine(DelayCoroutine());

    }


    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(2);

        _coroutine = StartCoroutine((AutoBattle()));

    }

    private IEnumerator BackToMain()
    {
        yield return new WaitForSeconds(3);


        StartCoroutine(gameManager.changeScene("MainScene", 0.5f));
    }


    IEnumerator MoneyAnimation(float addMoney, float time)
    {
        //�O��̃X�R�A
        float befor = money;
        //����̃X�R�A
        float after = money + addMoney;
       
        //0f���o�ߎ��Ԃɂ���
        float elapsedTime = 0.0f;

        //time���O�ɂȂ�܂Ń��[�v������
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // �e�L�X�g�̍X�V
            moneyText.text = (befor + (after - befor) * rate).ToString("f0") + "G";

            elapsedTime += Time.deltaTime;
            // 0.01�b�҂�
            yield return new WaitForSeconds(0.01f);
        }
        // �ŏI�I�Ȓ��n�̃X�R�A
        moneyText.text = after.ToString()+"G";

        money = gameManager.GetMoney();
    }


}
