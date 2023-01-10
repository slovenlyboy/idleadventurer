using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyInformation
{
    static TextAsset csvFile;//CSV�t�@�C����ϐ��Ƃ��Ĉ������߂ɐ錾
    static List<string[]> enemyData = new List<string[]>();//CSV�t�@�C���̒��g������z����`�B�S�Ẵf�[�^��������`���Ŋi�[�����
    //�ϐ���[i]���G�l�~�[ID��i�̏������ꂼ�ꎦ��
    public int[] enemyID = new int[100];//�G�l�~�[��ID
    public string[] enemyName = new string[100];//�G�l�~�[�̖��O
    public int[] hp = new int[100];//�G�l�~�[��HP
    public int[] atk = new int[100];//�G�l�~�[�̍U����
    public int[] speed = new int[100];//�G�l�~�[�̑��x
    public string[] imageAddress = new string[100];//�G�l�~�[�̉摜�C���[�W�̃A�h���X
    public int[] isBoss = new int[100];//�G�l�~�[���{�X���ۂ�
    public int[] dropMoney = new int[100];//�G�l�~�[�̗��Ƃ�����
   

    //�w�肵���A�h���X�ɕۊǂ���Ă���CSV�t�@�C���������ǂݎ��AenemyData�ɏ��𕶎���Ƃ��Ċi�[���郁�\�b�h�B
    //enemyData[i][j]��CSV�t�@�C����i�s�Aj��ڂ̃f�[�^��\���B�A���擪�s�i�^�C�g�������j��0�s�ڂƍl������̂Ƃ���B
    static void CsvReader()
    {
        csvFile = Resources.Load("CSV/enemyCSV") as TextAsset;//�w�肵���t�@�C����TextAsset�Ƃ��ēǂݍ���(�t�@�C������.csv�͕s�v�Ȃ��Ƃɒ���)�@�ŏ��̍s�i�^�C�g�������j���ǂݍ��܂��̂ł����͎g�p���Ȃ�
        StringReader reader = new StringReader(csvFile.text);//
        while (reader.Peek() != -1)//�Ō�܂œǂݍ��ނ�-1�ɂȂ�֐�
        {
            string line = reader.ReadLine();//��s���ǂݍ���
            enemyData.Add(line.Split(','));//,��؂�Ń��X�g�ɒǉ����Ă���
        }
    }
    //enemyData�Ɉ�xCSV�t�@�C���̃f�[�^��ǂݍ��񂾂瑼�̃v���O�������爵���₷���悤��`����enemyID���̕ϐ��Ƀf�[�^���i�[����
    public void Init()
    {
        //�G�f�[�^��������
        enemyData = new List<string[]>();
        CsvReader();//enemyData�֏����ꎞ�i�[
        //�e�ϐ��փf�[�^���i�[
        for (int i = 1; i < enemyData.Count; i++)//�G�l�~�[ID���L�q���ꂽ�Ō�܂œǂݍ��݁B��s�ڂ̓^�C�g���Ȃ̂�i=0�̓f�[�^�Ƃ��Ĉ���Ȃ�
        {
            enemyID[i] = int.Parse(enemyData[i][0]);//string�^����int�^�֕ϊ�
            enemyName[i] = enemyData[i][1];
            hp[i] = int.Parse(enemyData[i][2]);
            atk[i] = int.Parse(enemyData[i][3]);
            speed[i] = int.Parse(enemyData[i][4]);
            imageAddress[i] = enemyData[i][5];
            isBoss[i] = int.Parse(enemyData[i][6]);
            dropMoney[i]= int.Parse(enemyData[i][7]);

        }
    }
}
