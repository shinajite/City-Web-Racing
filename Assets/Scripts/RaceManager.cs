using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField]
    private UILogManager logManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Goal(string name, string time)
    {
        logManager.AddLog(0, new string[] { name + ":" + time});
    }
}
