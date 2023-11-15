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
    /*
    public void TriggerEventOnAllClients()
    {
        // サーバーでのみ実行されることを確認
        if (IsServer)
        {
            var message = new MyEventMessage { exampleData = 123 };

            // 全クライアントにメッセージを送信
            SendEventToAllClients(message);
        }
    }

    private void SendEventToAllClients(MyEventMessage message)
    {
        // RPCを使用して全クライアントにメッセージを送信
        SendEventClientRpc(message);
    }

    [ClientRpc]
    private void SendEventClientRpc(MyEventMessage message, ClientRpcParams rpcParams = default)
    {
        // ここでクライアント側のイベント処理を実行します
        Debug.Log($"Event received with data: {message.exampleData}");
    }
    /*
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
    public void SendCurrentTimeToClient(ulong clientId)
    {
        if (IsServer) // サーバー側のチェックを追加
        {
            Debug.Log("serverMethod!");
            DateTime serverTime = DateTime.Now;
            SendTimeToClientServerRpc(serverTime, clientId);
            //SendTimeToAllClientsServerRpc(serverTime);
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void SendTimeToClientServerRpc(DateTime time, ulong clientId)
    {
        Debug.Log("SendClientMethod!");
        ClientRpcParams rpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };

        TargetReceiveTimeClientRpc(time, rpcParams);
        Debug.Log(time);
        Debug.Log(clientId);
    }
    
    [ClientRpc]
    public void TargetReceiveTimeClientRpc(DateTime time, ClientRpcParams rpcParams = default)
    {
        Debug.Log(time);
        Debug.Log("Received server time: " + time.ToString());
        MessageBroker.Default.Publish(new AddBasicLogMsg { message = "Received server time: " + time.ToString() });
    }
    public void help()
    {

        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            ulong clientId = client.Key;
            SendCurrentTimeToClient(clientId);
            Debug.Log("Connected client ID: " + clientId);
        }
    }
        /*
    void Update()
    {

        Debug.Log("he!");
        Debug.Log(NetworkManager.Singleton.LocalClientId);

        //SendDateTimeToClients();
        Debug.Log("startMethod!");
        Debug.Log(getHostDateTimeNow());
        if (IsHost)
        {
            Debug.Log("I'm No.1 Hest!");
            //SendCurrentTimeToClient(NetworkManager.Singleton.LocalClientId);
        }*/
    //}

    /*
    private DateTime getHostDateTimeNow()
    {
        if (IsHost)
        {
            return DateTime.Now;
        }
    }
    */
    /*
    // クライアントに日時を送信するRPC
    [ClientRpc]
    void SendDateTimeToClientsClientRpc(string dateTime, ClientRpcParams rpcParams = default)
    {
        Debug.Log("Received DateTime from Host: " + dateTime);
    }

    // ホストが現在の日時を取得し、クライアントに送信するメソッド
    public void SendDateTimeToClients()
    {
        if (IsServer) // ホストかどうか確認
        {
            string currentDateTime = System.DateTime.Now.ToString();
            SendDateTimeToClientsClientRpc(currentDateTime);
        }
    }*/

    /*
    [ServerRpc(RequireOwnership = false)]
    private void SendTimeToAllClientsServerRpc(DateTime time)
    {
        Debug.Log("sendCliend!");
        SendTimeToClientClientRpc(time);
    }
    [ClientRpc]
    private void SendTimeToClientClientRpc(DateTime time)
    {
        Debug.Log("Received server time: " + time.ToString());
    }*/
    /*
void Update()
{
    // SendCurrentTimeToClient(NetworkManager.Singleton.LocalClientId);
    if (NetworkManager.Singleton.IsClient || NetworkManager.Singleton.IsServer)
    {
//            NetworkTime serverTime = NetworkManager.Singleton.ServerTime;
}
    }*/

    /*
   
    /*
    // レース開始時間を保有する変数
    public NetworkVariable<DateTime> startTime = new NetworkVariable<DateTime>(DateTime.Now);
    //private NetworkVariable<DateTime> startTime = new NetworkVariable<DateTime>();
    // Start is called before the first frame update
    void Start()
    {
        // ゴールイベントの登録
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);

        // startTimeが変わったとき
        startTime.OnValueChanged += (DateTime oldParam, DateTime newParam) =>
        {
//            TimeSpan startTimeSpan = TimeSpan.FromSeconds(newParam);
            StartCoroutine(Countdown(newParam));
            MessageBroker.Default.Publish(new AddBasicLogMsg { message = "Race will start. Please wait" });
        };

        
    }*/
    /*
    // スタートまでのカウントダウン関数
    private IEnumerator Countdown(DateTime targetTime)
    {
//        Debug.Log(startTimeSpan);

  //      DateTime targetTime = new DateTime(1970, 1, 1).Add(startTimeSpan);
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5秒前からカウントダウン開始

        Debug.Log(countdownStart);

        while (DateTime.Now < countdownStart)
        {
            yield return null; // 次のフレームまで待つ
        }

        int countdownSeconds = 5; // カウントダウン時間（秒）
        while (countdownSeconds > 0)
        {
            MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = countdownSeconds.ToString(), lifeTime = 100 }); ;
            yield return new WaitForSeconds(1.0f);
            countdownSeconds--;
        }
        MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = "Start!" });
    }

    // ゴールしたときの関数
    public void GoalRace(string name, float time)
    {/*
        Debug.Log(startTime.Value);
        Debug.Log(time);
        DateTime startDateTime = DateTimeOffset.FromUnixTimeSeconds((long)startTime.Value).DateTime;
        DateTime goalDateTime = DateTimeOffset.FromUnixTimeSeconds((long)time).DateTime;

        double secondsDifference = (goalDateTime - startDateTime).TotalSeconds;
        MessageBroker.Default.Publish(new AddBasicLogMsg { message = name + " goaled time:" + secondsDifference.ToString() });*/
    //}
    /*
        // スタートしたときの関数
        public void StartRace()
        {
            // 現在の時刻に10秒を加える
            DateTime nowPlusWaitTime = DateTime.Now.AddSeconds(10);
            /*
            // Unix エポックからの経過時間を取得
            TimeSpan elapsedTime = nowPlus10Seconds - new DateTime(1970, 1, 1);
            Debug.Log(elapsedTime);

            // 経過時間を秒数に変換
            float seconds = (float)elapsedTime.TotalSeconds;*/
    //startTime.Value = nowPlusWaitTime;
    //}

    /*
    [SerializeField]
    private NetworkVariable<float> startTime = new NetworkVariable<float>(0);
    // Start is called before the first frame update
    void Start()
    {
        // ゴールイベントの登録
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);
        
        // startTimeが変わったとき
        startTime.OnValueChanged += (float oldParam, float newParam) =>
        {
            Debug.Log(newParam);
            TimeSpan startTimeSpan = TimeSpan.FromSeconds(newParam);
            StartCoroutine(Countdown(startTimeSpan)); 
            MessageBroker.Default.Publish(new AddBasicLogMsg { message = "Race will start. Please wait" });
        };

        StartRace();
    }

    private IEnumerator Countdown(TimeSpan startTimeSpan)
    {
        DateTime targetTime = new DateTime(1970, 1, 1).Add(startTimeSpan);
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5秒前からカウントダウン開始

        while (DateTime.Now < countdownStart)
        {
            yield return null; // 次のフレームまで待つ
        }

        int countdownSeconds = 5; // カウントダウン時間（秒）
        while (countdownSeconds > 0)
        {
            MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = countdownSeconds.ToString(), lifeTime=100 }); ;
            yield return new WaitForSeconds(1.0f);
            countdownSeconds--;
        }
        MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = "Start!" });
    }

    public void GoalRace(string name, float time)
    {
        Debug.Log(startTime.Value);
        Debug.Log(time);
        DateTime startDateTime = DateTimeOffset.FromUnixTimeSeconds((long)startTime.Value).DateTime;
        DateTime goalDateTime = DateTimeOffset.FromUnixTimeSeconds((long)time).DateTime;

        double secondsDifference = (goalDateTime - startDateTime).TotalSeconds;
        MessageBroker.Default.Publish(new AddBasicLogMsg { message = name + " goaled time:" + secondsDifference.ToString() }) ;
    }
    
    public void StartRace()
    {
        // 現在の時刻に10秒を加える
        DateTime nowPlus10Seconds = DateTime.Now.AddSeconds(10);
        Debug.Log(nowPlus10Seconds.ToString());

        // Unix エポックからの経過時間を取得
        TimeSpan elapsedTime = nowPlus10Seconds - new DateTime(1970, 1, 1);

        // 経過時間を秒数に変換
        float seconds = (float)elapsedTime.TotalSeconds;
        startTime.Value = seconds;
    }
    */

    // レース開始時間を保有する変数
    public NetworkVariable<DateTime> startTime = new NetworkVariable<DateTime>(DateTime.Now);


    void Start()
    {
        // ゴールイベントの登録
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);

        // startTimeが変わったとき
        startTime.OnValueChanged += (DateTime oldParam, DateTime newParam) =>
        {
            // TimeSpan startTimeSpan = TimeSpan.FromSeconds(newParam);
            StartCoroutine(Countdown(newParam));
            MessageBroker.Default.Publish(new AddBasicLogMsg { message = "Race will start. Please wait" });
        };
    }

    private IEnumerator Countdown(DateTime targetTime)
    {
        //DateTime targetTime = new DateTime(1970, 1, 1).Add(startTimeSpan);
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5秒前からカウントダウン開始

        while (DateTime.Now < countdownStart)
        {
            yield return null; // 次のフレームまで待つ
        }

        int countdownSeconds = 5; // カウントダウン時間（秒）
        while (countdownSeconds > 0)
        {
            MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = countdownSeconds.ToString(), lifeTime = 100 }); ;
            yield return new WaitForSeconds(1.0f);
            countdownSeconds--;
        }
        MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = "Start!" });
    }


    public void StartRace()
    {
        // 現在の時刻に10秒を加える
        DateTime nowPlus10Seconds = DateTime.Now.AddSeconds(10);
        /*
        Debug.Log(nowPlus10Seconds.ToString());

        // Unix エポックからの経過時間を取得
        TimeSpan elapsedTime = nowPlus10Seconds - new DateTime(1970, 1, 1);

        // 経過時間を秒数に変換
        float seconds = (float)elapsedTime.TotalSeconds;*/
        startTime.Value = nowPlus10Seconds;
    }

    public void GoalRace(string name, DateTime time)
    {
        Debug.Log(startTime.Value);
        /*
        Debug.Log(time);
        DateTime startDateTime = DateTimeOffset.FromUnixTimeSeconds((long)startTime.Value).DateTime;
        DateTime goalDateTime = DateTimeOffset.FromUnixTimeSeconds((long)time).DateTime;

        double secondsDifference = (goalDateTime - startDateTime).TotalSeconds;
        MessageBroker.Default.Publish(new AddBasicLogMsg { message = name + " goaled time:" + secondsDifference.ToString() });*/
    }

}

// 送信するメッセージの型
public class GoalMsg
{
    public string playerName { get; set; }
    public DateTime goalTime { get; set; }
}
