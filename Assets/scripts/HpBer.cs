using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBer : MonoBehaviour
{

    [SerializeField]
    Chara unit = null;

    int maxHp;
    int currentHp;

    [SerializeField]
    Slider bar = null;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //HPバーを初期化する
    public void Init() {

        maxHp = unit.GetHP();

        bar.value = 1;

        currentHp = maxHp;
    }

    //被ダメージの処理
    public void TakeDamage() {
        currentHp = unit.GetHP();

        bar.value = (float)currentHp / (float)maxHp; ;
    }

}
