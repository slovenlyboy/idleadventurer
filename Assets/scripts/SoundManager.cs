using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    //--シングルトン始まり--
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //--シングルトン終わり--

    public AudioSource audioSourceBGM; // BGMのスピーカー
    public AudioClip[] audioClipsBGM;  // BGMの音源

    public AudioSource audioSourceSE; // SEのスピーカー
    public AudioClip[] audioClipsSE;// SEの音源

    //シーンに応じたBGMの鳴らす方法
    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();
        switch (sceneName)
        {
            default:
            case "TitleScene":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "MainScene":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "DungeonScene":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
        }
        audioSourceBGM.loop = true;

        audioSourceBGM.Play();
    }

    // SEを一度だけならす方法
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]);
    }
}

