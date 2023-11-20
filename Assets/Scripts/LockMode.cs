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
                // �}�E�X�J�[�\���̌Œ�
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                isLockMode = true;
                // �Œ����
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            // ui�I�u�W�F�N�g�̕\����\��
            uiObject.SetActive(!uiObject.activeSelf);
        }
    }
}
