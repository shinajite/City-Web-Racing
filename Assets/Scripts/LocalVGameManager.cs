using UnityEngine;
using Unity.Netcode;
using System;
using UniRx;

public class LocalVGameManager : MonoBehaviour
{

    private void Awake()
    {
        Debug.Log("heno!");

        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x =>
            {
                    GoalRaceByServerRpc(x.playerName);
            })
            .AddTo(this);
    }

    // スタートタイム
    private double startTimeDouble;
    // 基準時間
    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    
    public void StartRaceByServerRpc()
    {
        Debug.Log("heno!");
        DateTime startDateTime = DateTime.UtcNow.AddSeconds(10);
        TimeSpan timeSpan = startDateTime - epoch;
        double unixTimestamp = (double)timeSpan.TotalSeconds;

        startTimeDouble = unixTimestamp;
        NotificationPlayeresRaceStart();
    }

    private void NotificationPlayeresRaceStart()
    {
        DateTime startTime = epoch.AddSeconds(startTimeDouble);
        StartRaceClientRpc(startTime);
    }
    private void StartRaceClientRpc(DateTime startTime)
    {
        MessageBroker.Default.Publish(new StartNotificationMsg { startTime = startTime });
    }


    public void GoalRaceByServerRpc(string playerName)
    {
        DateTime goalTime = DateTime.UtcNow;
            
        NotificationPlayeresRaceGoal(playerName, goalTime);
    }

    private void NotificationPlayeresRaceGoal(string playerName, DateTime goalTime)
    {
        DateTime startTime = epoch.AddSeconds(startTimeDouble);
        GoalRaceClientRpc(playerName, startTime, goalTime);
    }

    private void GoalRaceClientRpc(string playerName, DateTime startTime, DateTime goalTime)
    {
        MessageBroker.Default.Publish(new GoalNotificationMsg { playerName = playerName, startTime = startTime, goalTime = goalTime });
    }

    public void RequestTimeServerRpc()
    {
        var currentTime = DateTime.Now; // 現在の時刻を取得
        SendTimeToClient(currentTime);
    }

    private void SendTimeToClient(DateTime time)
    {
        SendTimeToClientClientRpc(time.ToString());
    }

    private void SendTimeToClientClientRpc(string time)
    {
        Debug.Log(time);
        // ここでクライアント側で時刻を使用（例: UIに表示）
    }
}

