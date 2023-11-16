using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class UserNameManager : MonoBehaviour
{

    [SerializeField] TMP_Text userNameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MessageBroker.Default.Receive<SetPlayerNameMsg>()
            .Subscribe(x =>
            { userNameText.text = x.name; })
            .AddTo(this);
    }
}


// 送信するメッセージの型
public class SetPlayerNameMsg
{
    public string name { get; set; }
}
