using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UILogManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> logInstances = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {
        MessageBroker.Default.Receive<AddBasicLogMsg>()
            .Subscribe(x => AddLog(0, new string[] { x.message }, x.lifeTime))
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void AddLog(int prefabIndex, string[] messages, float lifeTime)
    {
        BasicLog childInstance = Instantiate(logInstances[prefabIndex]).GetComponent<BasicLog>(); ;

        if (childInstance != null)
        {
            childInstance.transform.SetParent(this.transform, false);
            childInstance.init(messages, lifeTime);
        }
    }


}

public class AddBasicLogMsg
{
    public string message { get; set; }
    public float lifeTime { get; set; }

    public AddBasicLogMsg()
    {
        // デフォルト値を設定
        lifeTime = 5f;
    }
}