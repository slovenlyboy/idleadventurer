using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBer : MonoBehaviour
{

    [SerializeField]
    Chara Unit = null;

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


    public void init() {

        maxHp = Unit.GetHP();

        bar.value = 1;

        currentHp = maxHp;
    }


    public void takeDamage() {
        currentHp = Unit.GetHP();

        bar.value = (float)currentHp / (float)maxHp; ;
    }

}
