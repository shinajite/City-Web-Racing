using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Netcode;
using System;
/*
public class MyEventMessage : INetworkSerializable
{
    public int exampleData;

    // INetworkSerializable �̎���
    public void NetworkSerialize(NetworkSerializer serializer)
    {
        serializer.Serialize(ref exampleData);
    }
}
*/

public class RaceManager : NetworkBehaviour
{
    
    // �l�b�g���[�N��Ɉ�������݂��Ȃ��悤��
    public static RaceManager Instance { get; private set; } // static�Ő錾���邱�Ƃ�class�ɂ����ē���̒l�ƂȂ�

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // �V�[���J�ڎ��ɔj������Ȃ��悤�ɂ���
        }
    }

    // ���[�X�J�n���Ԃ�ۗL����ϐ�
    public int previewInt = 0;

    private NetworkVariable<int> networkData = new NetworkVariable<int>(
        0,                                          // �����l
        NetworkVariableReadPermission.Everyone,     // �ǂݎ�茠��
        NetworkVariableWritePermission.Owner        // �������݌���
        );
    /*
    private NetworkVariable<double> startTime = new NetworkVariable<double>(default,
            NetworkVariableReadPermission.Everyone,  // �T�[�o�[�i�܂��̓z�X�g�j�݂̂��������݉\
            NetworkVariableWritePermission.Owner    // ���ׂẴN���C�A���g���ǂݎ��\
                                                    // 0.1�b���ƂɕύX�𑗐M,
    );


    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    */
    void Start()
    {
        /*
        // �S�[���C�x���g�̓o�^
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);

        // startTime���ς�����Ƃ�
        startTime.OnValueChanged += (double oldParam, double newParam) =>
        {
            Debug.Log(oldParam);
            Debug.Log(newParam);
            // Unix�^�C���X�^���v��DateTime�ɕϊ�
            DateTime dateTime = epoch.AddSeconds(newParam);


            Debug.Log("changeStart:" + dateTime);
            StartCoroutine(Countdown(dateTime));
            MessageBroker.Default.Publish(new AddBasicLogMsg { message = "Race will start. Please wait" });
        };
        */
        /*
        MessageBroker.Default.Receive<StartMsg>()
            .Subscribe(x => StartRace())
            .AddTo(this);
        
        networkData.OnValueChanged += (int oldParam, int newParam) =>
        {
            previewInt = newParam;
        };*/
    }
    /*
    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

        private IEnumerator Countdown(DateTime targetTime)
    {
        //DateTime targetTime = new DateTime(1970, 1, 1).Add(startTimeSpan);
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5�b�O����J�E���g�_�E���J�n

        Debug.Log("timerStartAt:"+ countdownStart);

        while (DateTime.UtcNow < countdownStart)
        {
            yield return null; // ���̃t���[���܂ő҂�
        }

        Debug.Log(countdownStart);
        Debug.Log(DateTime.UtcNow);

        int countdownSeconds = 5; // �J�E���g�_�E�����ԁi�b�j
        while (countdownSeconds > 0)
        {
            MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = countdownSeconds.ToString(), lifeTime = 100 }); ;
            yield return new WaitForSeconds(1.0f);
            countdownSeconds--;
        }
        MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = "Start!" });
    }
    */

    public void StartRace()
    {
        previewInt += 1;
        /*
        // startDateTime��UTC�Ŏ擾
        DateTime startDateTime = DateTime.UtcNow.AddSeconds(7);
        TimeSpan timeSpan = startDateTime - epoch;
        double unixTimestamp = (double)timeSpan.TotalSeconds;

        // unixTimestamp��ۑ�����
        double savedUnixTimestamp = unixTimestamp;

        // Unix�^�C���X�^���v����DateTime�ɖ߂��ꍇ
        DateTime dateTime = epoch.AddSeconds(savedUnixTimestamp);
        Debug.Log(startDateTime);
        Debug.Log(dateTime);
        startTime.Value = savedUnixTimestamp;

//        AmmoCount.Value = AmmoCount.Value + 1;


        /*
        float unixTimestamp = (float)(nowPlus10Seconds.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;


        // Unix�^�C���X�^���v��DateTime�ɕϊ�
        DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(unixTimestamp);

        Debug.Log(dateTime);


        startTime.Value = unixTimestamp;
        */
    }
    /*
    public void GoalRace(string name, DateTime time)
    {
        //DateTime goalDateTime = epoch.AddSeconds(time);
        DateTime startDateTime = epoch.AddSeconds(startTime.Value);
        Debug.Log(startDateTime);
        //Debug.Log(goalDateTime);

        double secondsDifference = (time - startDateTime).TotalSeconds;
        MessageBroker.Default.Publish(new AddBasicLogMsg { message = name + " goaled time:" + secondsDifference.ToString(), lifeTime=10f });
    }*/

}
