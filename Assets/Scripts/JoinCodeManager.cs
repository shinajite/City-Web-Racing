using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class JoinCodeManager : MonoBehaviour
{
    [SerializeField] TMP_Text joinCodeText;
    // Start is called before the first frame update
    void Start()
    {
        MessageBroker.Default.Receive<SetJoinCodeMsg>()
            .Subscribe(x =>
            { joinCodeText.text = x.code; })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}


// 送信するメッセージの型
public class SetJoinCodeMsg
{
    public string code { get; set; }
}
