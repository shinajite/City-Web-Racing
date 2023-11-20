using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;

public class UIBigLogManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    IEnumerator currentTimer = null;


    // Start is called before the first frame update
    void Start()
    {
        MessageBroker.Default.Receive<SetBigBasicLogMsg>()
            .Subscribe(x =>
            {
                text.text = x.message;
                if (currentTimer != null)
                {
                    StopCoroutine(currentTimer);
                }
                currentTimer = ClearTextAfterDelay(x.lifeTime);
                StartCoroutine(currentTimer);
            })
            .AddTo(this);

    }

    IEnumerator ClearTextAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        text.text = "";
    }
}

public class SetBigBasicLogMsg
{
    public string message { get; set; }
    public float lifeTime { get; set; }

    public SetBigBasicLogMsg()
    {
        // デフォルト値を設定
        lifeTime = 1f;
    }
}