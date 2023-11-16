using UnityEngine;
using Unity.Netcode;
using System;
using UniRx;

public class VGameManager : NetworkBehaviour
{

    // �V���O���g���̐݌v�ɂ���
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

    // �X�^�[�g�^�C��
    private NetworkVariable<double> startTimeDouble = new NetworkVariable<double>();
    // �����
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
            var currentTime = DateTime.Now; // ���݂̎������擾
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
        // �����ŃN���C�A���g���Ŏ������g�p�i��: UI�ɕ\���j
    }
}


// ���M���郁�b�Z�[�W�̌^
public class GoalNotificationMsg
{
    public string playerName { get; set; }
    public DateTime goalTime { get; set; }
    public DateTime startTime { get; set; }
}


// ���M���郁�b�Z�[�W�̌^
public class StartNotificationMsg
{
    public DateTime startTime { get; set; }
}
