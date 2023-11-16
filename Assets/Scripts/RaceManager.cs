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

    // INetworkSerializable の実装
    public void NetworkSerialize(NetworkSerializer serializer)
    {
        serializer.Serialize(ref exampleData);
    }
}
*/

public class RaceManager : NetworkBehaviour
{
    
    // ネットワーク上に一つしか存在しないように
    public static RaceManager Instance { get; private set; } // staticで宣言することでclassにおいて同一の値となる

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // シーン遷移時に破棄されないようにする
        }
    }

    // レース開始時間を保有する変数
    public int previewInt = 0;

    private NetworkVariable<int> networkData = new NetworkVariable<int>(
        0,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Owner        // 書き込み権限
        );
    /*
    private NetworkVariable<double> startTime = new NetworkVariable<double>(default,
            NetworkVariableReadPermission.Everyone,  // サーバー（またはホスト）のみが書き込み可能
            NetworkVariableWritePermission.Owner    // すべてのクライアントが読み取り可能
                                                    // 0.1秒ごとに変更を送信,
    );


    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    */
    void Start()
    {
        /*
        // ゴールイベントの登録
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);

        // startTimeが変わったとき
        startTime.OnValueChanged += (double oldParam, double newParam) =>
        {
            Debug.Log(oldParam);
            Debug.Log(newParam);
            // UnixタイムスタンプをDateTimeに変換
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
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5秒前からカウントダウン開始

        Debug.Log("timerStartAt:"+ countdownStart);

        while (DateTime.UtcNow < countdownStart)
        {
            yield return null; // 次のフレームまで待つ
        }

        Debug.Log(countdownStart);
        Debug.Log(DateTime.UtcNow);

        int countdownSeconds = 5; // カウントダウン時間（秒）
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
        // startDateTimeをUTCで取得
        DateTime startDateTime = DateTime.UtcNow.AddSeconds(7);
        TimeSpan timeSpan = startDateTime - epoch;
        double unixTimestamp = (double)timeSpan.TotalSeconds;

        // unixTimestampを保存する
        double savedUnixTimestamp = unixTimestamp;

        // UnixタイムスタンプからDateTimeに戻す場合
        DateTime dateTime = epoch.AddSeconds(savedUnixTimestamp);
        Debug.Log(startDateTime);
        Debug.Log(dateTime);
        startTime.Value = savedUnixTimestamp;

//        AmmoCount.Value = AmmoCount.Value + 1;


        /*
        float unixTimestamp = (float)(nowPlus10Seconds.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;


        // UnixタイムスタンプをDateTimeに変換
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
