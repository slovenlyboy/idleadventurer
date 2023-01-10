using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveText : MonoBehaviour
{

    private string talks = "�� �� �� �� �� �I �� �� �� �� �� �� �� �� �� �p �� �� �� �H";
    private string[] words;
    public TextMeshProUGUI textLabel;

    Coroutine _coroutine;

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = StartCoroutine(Dialogue());
    }

    // �R���[�`�����g���āA�P�������ƕ\������B
    IEnumerator Dialogue()
    {
        // ���p�X�y�[�X�ŕ����𕪊�����B
        words = talks.Split(' ');

        foreach (var word in words)
        {

            string text = word;

            if (word == ".") {
                text = "\n";
            }

           
            // 0.1�b���݂łP�������\������B
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
