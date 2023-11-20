using UnityEngine;
using Unity.Netcode;
using System;
using UniRx;

public class VGameManager : NetworkBehaviour
{

    // シングルトンの設計にする
    public static VGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x =>
            {
                if (IsServer)
                {
                    GoalRaceByServerRpc(x.playerName);
                }

            })
            .AddTo(this);
    }

    // スタートタイム
    private NetworkVariable<double> startTimeDouble = new NetworkVariable<double>();
    // 基準時間
    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    
    [ServerRpc(RequireOwnership = false)]
    public void StartRaceByServerRpc()
    {
        if (IsServer)
        {
            DateTime startDateTime = DateTime.UtcNow.AddSeconds(10);
            TimeSpan timeSpan = startDateTime - epoch;
            double unixTimestamp = (double)timeSpan.TotalSeconds;

            startTimeDouble.Value = unixTimestamp;
            NotificationPlayeresRaceStart();
        }
    }

    private void NotificationPlayeresRaceStart()
    {
        DateTime startTime = epoch.AddSeconds(startTimeDouble.Value);
        StartRaceClientRpc(startTime);
    }

    [ClientRpc]
    private void StartRaceClientRpc(DateTime startTime)
    {
        MessageBroker.Default.Publish(new StartNotificationMsg { startTime = startTime });
    }


    [ServerRpc(RequireOwnership = false)]
    public void GoalRaceByServerRpc(string playerName)
    {
        if (IsServer)
        {
            DateTime goalTime = DateTime.UtcNow;
            
            NotificationPlayeresRaceGoal(playerName, goalTime);
        }
    }

    private void NotificationPlayeresRaceGoal(string playerName, DateTime goalTime)
    {
        DateTime startTime = epoch.AddSeconds(startTimeDouble.Value);
        GoalRaceClientRpc(playerName, startTime, goalTime);
    }

    [ClientRpc]
    private void GoalRaceClientRpc(string playerName, DateTime startTime, DateTime goalTime)
    {
        MessageBroker.Default.Publish(new GoalNotificationMsg { playerName = playerName, startTime = startTime, goalTime = goalTime });
    }


    [ServerRpc]
    public void RequestTimeServerRpc()
    {
        if (IsServer)
        {
            var currentTime = DateTime.Now; // 現在の時刻を取得
            SendTimeToClient(currentTime);
        }
    }

    private void SendTimeToClient(DateTime time)
    {
        SendTimeToClientClientRpc(time.ToString());
    }

    [ClientRpc]
    private void SendTimeToClientClientRpc(string time)
    {
        Debug.Log(time);
        // ここでクライアント側で時刻を使用（例: UIに表示）
    }
}


// 送信するメッセージの型
public class GoalNotificationMsg
{
    public string playerName { get; set; }
    public DateTime goalTime { get; set; }
    public DateTime startTime { get; set; }
}


// 送信するメッセージの型
public class StartNotificationMsg
{
    public DateTime startTime { get; set; }
}
