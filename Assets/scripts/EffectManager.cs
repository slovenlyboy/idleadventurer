using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public AnimationClip test_1;
    public AnimationClip test_2;

    // Use this for initialization
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {

      

    }

    //任意の場所にエフェクトを出す
    public void startEffect(string effectName,Vector3 pos) {

        anim.transform.gameObject.GetComponent<RectTransform>().localPosition = pos;

        anim.SetTrigger(effectName);

    }



}
