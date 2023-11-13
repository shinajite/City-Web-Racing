using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public float speed = 5.0f;
    public float maxDistance = 100f;
    public float wireSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float rotationSpeed = 200.0f;
    public Animator animator;
    public Camera playerCamera;

    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isWired = false;
    private bool isGround = false;
    private LineRenderer lineRenderer;
    private int layerMask;

    [SerializeField]
    private RaceManager raceManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.material = new Material(Shader.Find("Standard"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // �v���C���[�����[�J���ł��邩�ǂ������m�F
        if (IsLocalPlayer)
        {
            // ���̃v���C���[�̃J�������A�N�e�B�u�ɂ���
            playerCamera.enabled = true;
        }
        else
        {
            // ���̃v���C���[�̃J�����͔�A�N�e�B�u�ɂ���
            playerCamera.enabled = false;
        }

        // �q���b�V���𖳎����郌�C���[�}�X�N���쐬�i�Ⴆ�Ύq���b�V�������C���[8�ɂ���Ƃ���
        layerMask = (1 << 8) | (1 << 9);
        layerMask = ~layerMask; // ���C���[8�����O����
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�����
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("moveSpeed", moveVertical);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement) * speed;
        rb.AddForce(movement, ForceMode.Acceleration);


        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animator.SetTrigger("Jamp");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Acceleration);
        }

        // ���C���[���ˁi�}�E�X���N���b�N�j
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("fire");
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
            {
                targetPosition = hit.point;
                isWired = true;
            }
        }

        // ���C���[�����i�}�E�X�E�N���b�N�j
        if (Input.GetMouseButtonDown(0))
        {
            isWired = false;

            Debug.Log("dewired");
        }


        // ���̕`��X�V
        if (isWired)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, targetPosition);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }


    void FixedUpdate()
    {
        if (isWired)
        {
            // �v���C���[��ڕW�n�_�Ɉ���
            Vector3 forceDirection = (targetPosition - transform.position).normalized;
            rb.AddForce(forceDirection * wireSpeed, ForceMode.Acceleration);
        }
    }

    private bool isGrounded;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "player")
        {
            animator.SetBool("InAir", false);
            isGround = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag != "player")
        {
            animator.SetBool("InAir", true);
            isGround = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if (layerName == "Goal")
        {
            raceManager.Goal("player", "00");
        }
    }
}
