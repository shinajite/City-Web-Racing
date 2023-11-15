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
    /*
    public void TriggerEventOnAllClients()
    {
        // �T�[�o�[�ł̂ݎ��s����邱�Ƃ��m�F
        if (IsServer)
        {
            var message = new MyEventMessage { exampleData = 123 };

            // �S�N���C�A���g�Ƀ��b�Z�[�W�𑗐M
            SendEventToAllClients(message);
        }
    }

    private void SendEventToAllClients(MyEventMessage message)
    {
        // RPC���g�p���đS�N���C�A���g�Ƀ��b�Z�[�W�𑗐M
        SendEventClientRpc(message);
    }

    [ClientRpc]
    private void SendEventClientRpc(MyEventMessage message, ClientRpcParams rpcParams = default)
    {
        // �����ŃN���C�A���g���̃C�x���g���������s���܂�
        Debug.Log($"Event received with data: {message.exampleData}");
    }
    /*
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
    public void SendCurrentTimeToClient(ulong clientId)
    {
        if (IsServer) // �T�[�o�[���̃`�F�b�N��ǉ�
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
    // �N���C�A���g�ɓ����𑗐M����RPC
    [ClientRpc]
    void SendDateTimeToClientsClientRpc(string dateTime, ClientRpcParams rpcParams = default)
    {
        Debug.Log("Received DateTime from Host: " + dateTime);
    }

    // �z�X�g�����݂̓������擾���A�N���C�A���g�ɑ��M���郁�\�b�h
    public void SendDateTimeToClients()
    {
        if (IsServer) // �z�X�g���ǂ����m�F
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
    // ���[�X�J�n���Ԃ�ۗL����ϐ�
    public NetworkVariable<DateTime> startTime = new NetworkVariable<DateTime>(DateTime.Now);
    //private NetworkVariable<DateTime> startTime = new NetworkVariable<DateTime>();
    // Start is called before the first frame update
    void Start()
    {
        // �S�[���C�x���g�̓o�^
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);

        // startTime���ς�����Ƃ�
        startTime.OnValueChanged += (DateTime oldParam, DateTime newParam) =>
        {
//            TimeSpan startTimeSpan = TimeSpan.FromSeconds(newParam);
            StartCoroutine(Countdown(newParam));
            MessageBroker.Default.Publish(new AddBasicLogMsg { message = "Race will start. Please wait" });
        };

        
    }*/
    /*
    // �X�^�[�g�܂ł̃J�E���g�_�E���֐�
    private IEnumerator Countdown(DateTime targetTime)
    {
//        Debug.Log(startTimeSpan);

  //      DateTime targetTime = new DateTime(1970, 1, 1).Add(startTimeSpan);
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5�b�O����J�E���g�_�E���J�n

        Debug.Log(countdownStart);

        while (DateTime.Now < countdownStart)
        {
            yield return null; // ���̃t���[���܂ő҂�
        }

        int countdownSeconds = 5; // �J�E���g�_�E�����ԁi�b�j
        while (countdownSeconds > 0)
        {
            MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = countdownSeconds.ToString(), lifeTime = 100 }); ;
            yield return new WaitForSeconds(1.0f);
            countdownSeconds--;
        }
        MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = "Start!" });
    }

    // �S�[�������Ƃ��̊֐�
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
        // �X�^�[�g�����Ƃ��̊֐�
        public void StartRace()
        {
            // ���݂̎�����10�b��������
            DateTime nowPlusWaitTime = DateTime.Now.AddSeconds(10);
            /*
            // Unix �G�|�b�N����̌o�ߎ��Ԃ��擾
            TimeSpan elapsedTime = nowPlus10Seconds - new DateTime(1970, 1, 1);
            Debug.Log(elapsedTime);

            // �o�ߎ��Ԃ�b���ɕϊ�
            float seconds = (float)elapsedTime.TotalSeconds;*/
    //startTime.Value = nowPlusWaitTime;
    //}

    /*
    [SerializeField]
    private NetworkVariable<float> startTime = new NetworkVariable<float>(0);
    // Start is called before the first frame update
    void Start()
    {
        // �S�[���C�x���g�̓o�^
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);
        
        // startTime���ς�����Ƃ�
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
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5�b�O����J�E���g�_�E���J�n

        while (DateTime.Now < countdownStart)
        {
            yield return null; // ���̃t���[���܂ő҂�
        }

        int countdownSeconds = 5; // �J�E���g�_�E�����ԁi�b�j
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
        // ���݂̎�����10�b��������
        DateTime nowPlus10Seconds = DateTime.Now.AddSeconds(10);
        Debug.Log(nowPlus10Seconds.ToString());

        // Unix �G�|�b�N����̌o�ߎ��Ԃ��擾
        TimeSpan elapsedTime = nowPlus10Seconds - new DateTime(1970, 1, 1);

        // �o�ߎ��Ԃ�b���ɕϊ�
        float seconds = (float)elapsedTime.TotalSeconds;
        startTime.Value = seconds;
    }
    */

    // ���[�X�J�n���Ԃ�ۗL����ϐ�
    public NetworkVariable<DateTime> startTime = new NetworkVariable<DateTime>(DateTime.Now);


    void Start()
    {
        // �S�[���C�x���g�̓o�^
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => GoalRace(x.playerName, x.goalTime))
            .AddTo(this);

        // startTime���ς�����Ƃ�
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
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5�b�O����J�E���g�_�E���J�n

        while (DateTime.Now < countdownStart)
        {
            yield return null; // ���̃t���[���܂ő҂�
        }

        int countdownSeconds = 5; // �J�E���g�_�E�����ԁi�b�j
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
        // ���݂̎�����10�b��������
        DateTime nowPlus10Seconds = DateTime.Now.AddSeconds(10);
        /*
        Debug.Log(nowPlus10Seconds.ToString());

        // Unix �G�|�b�N����̌o�ߎ��Ԃ��擾
        TimeSpan elapsedTime = nowPlus10Seconds - new DateTime(1970, 1, 1);

        // �o�ߎ��Ԃ�b���ɕϊ�
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

// ���M���郁�b�Z�[�W�̌^
public class GoalMsg
{
    public string playerName { get; set; }
    public DateTime goalTime { get; set; }
}
