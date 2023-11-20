using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicLog : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(string[] messages, float lifeTime)
    {
        for (int i = 0; i < messages.Length; i++)
        {
            texts[i].text = messages[i];
        }

        Destroy(gameObject, lifeTime);
    }
}
