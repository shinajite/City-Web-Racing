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
        MessageBroker.Default.Receive<SetPlayerNameMsg>()
            .Subscribe(x =>
            { userNameText.text = x.name; })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}


// ���M���郁�b�Z�[�W�̌^
public class SetPlayerNameMsg
{
    public string name { get; set; }
}