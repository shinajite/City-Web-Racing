using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> logInstances = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLog(int prefabIndex, string[] messages)
    {
        BasicLog childInstance = Instantiate(logInstances[prefabIndex]).GetComponent<BasicLog>(); ;

        if (childInstance != null)
        {
            childInstance.transform.parent = this.transform;
            childInstance.init(messages, 5f);
        }
    }
}
