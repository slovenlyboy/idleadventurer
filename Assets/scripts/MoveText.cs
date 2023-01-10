using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveText : MonoBehaviour
{

    private string talks = "こ ん に ち は ！ 今 日 は ど の よ う な 御 用 で す か ？";
    private string[] words;
    public TextMeshProUGUI textLabel;

    Coroutine _coroutine;

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = StartCoroutine(Dialogue());
    }

    // コルーチンを使って、１文字ごと表示する。
    IEnumerator Dialogue()
    {
        // 半角スペースで文字を分割する。
        words = talks.Split(' ');

        foreach (var word in words)
        {

            string text = word;

            if (word == ".") {
                text = "\n";
            }

           
            // 0.1秒刻みで１文字ずつ表示する。
            textLabel.text = textLabel.text + text;
            //AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
            yield return new WaitForSeconds(0.05f);
        }
    }


    public void ReadText(string text) {

        StopCoroutine(_coroutine);

        textLabel.text = "";

        talks = text;

        _coroutine = StartCoroutine(Dialogue());

    }


}
