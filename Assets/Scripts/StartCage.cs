using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class StartCage : MonoBehaviour
{
    [SerializeField] private GameObject cage;
    // Start is called before the first frame update
    void Start()
    {

        MessageBroker.Default.Receive<StartNotificationMsg>()
            .Subscribe(x =>
            {
                cage.SetActive(true);
                StartCoroutine(Countdown(x.startTime));
            })
            .AddTo(this);
    }



    private IEnumerator Countdown(DateTime targetTime)
    {
        
        while (DateTime.UtcNow < targetTime)
        {
            yield return null; // ŽŸ‚ÌƒtƒŒ[ƒ€‚Ü‚Å‘Ò‚Â
        }

        cage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
