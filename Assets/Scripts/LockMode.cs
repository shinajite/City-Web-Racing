using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMode : MonoBehaviour
{
    [SerializeField]private GameObject uiObject;
    private bool isLockMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (isLockMode)
            {
                isLockMode = false;
                // マウスカーソルの固定
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                isLockMode = true;
                // 固定解除
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            // uiオブジェクトの表示非表示
            uiObject.SetActive(!uiObject.activeSelf);
        }
    }
}
