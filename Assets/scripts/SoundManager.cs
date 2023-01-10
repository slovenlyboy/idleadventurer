using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    //--�V���O���g���n�܂�--
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
    //--�V���O���g���I���--

    public AudioSource audioSourceBGM; // BGM�̃X�s�[�J�[
    public AudioClip[] audioClipsBGM;  // BGM�̉���

    public AudioSource audioSourceSE; // SE�̃X�s�[�J�[
    public AudioClip[] audioClipsSE;// SE�̉���

    //�V�[���ɉ�����BGM�̖炷���@
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

    // SE����x�����Ȃ炷���@
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]);
    }
}

