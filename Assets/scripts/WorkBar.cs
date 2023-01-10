using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBar : MonoBehaviour
{

    Slider bar;

    GameManager gameManager;

    Work work;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Slider>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        work = GameObject.Find("GameManager").GetComponent<Work>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.value =   work.GetCurrentTime() / work.GetNeedTime();
    }
}
