using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RaceManager : MonoBehaviour
{
    [SerializeField]
    private UILogManager logManager;
    // Start is called before the first frame update
    void Start()
    {
        MessageBroker.Default.Receive<GoalMsg>()
            .Subscribe(x => Goal(x.playerName, x.goalTime))
            .AddTo(this);
    }

    public void Goal(string name, float time)
    {
        logManager.AddLog(0, new string[] { "Goaled:" + name + " time:" + time.ToString()});
    }
}

// 送信するメッセージの型
public class GoalMsg
{
    public string playerName { get; set; }
    public float goalTime { get; set; }
}