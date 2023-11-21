using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


public class LocalPlayerController : MonoBehaviour
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

        MessageBroker.Default.Publish(new SetPlayerNameMsg { name = "player" });
        
        // �q���b�V���𖳎����郌�C���[�}�X�N���쐬�i�Ⴆ�Ύq���b�V�������C���[8�ɂ���Ƃ���
        layerMask = (1 << 8) | (1 << 9);
        layerMask = ~layerMask; // ���C���[8�����O����

        
        MessageBroker.Default.Receive<StartNotificationMsg>()
            .Subscribe(x =>
            {
                    StartCoroutine(Countdown(x.startTime));
                    MessageBroker.Default.Publish(new AddBasicLogMsg { message = "Race will start. Please wait" });
                    GoToStart();
            })
            .AddTo(this);

        MessageBroker.Default.Receive<GoalNotificationMsg>()
            .Subscribe(x =>
            {

                    // 2��DateTime�̍����v�Z���܂�
                    TimeSpan difference = x.goalTime - x.startTime;

                    // ���ʂ��o�͂��܂�
                    int hours = difference.Hours;
                    int minutes = difference.Minutes;
                    int seconds = difference.Seconds;
                    int milliseconds = difference.Milliseconds;

                    MessageBroker.Default.Publish(new AddBasicLogMsg { message = x.playerName + " goaled!" }); ;
                    MessageBroker.Default.Publish(new AddBasicLogMsg { 
                        message = "time:" + (hours * 60 + minutes) + "m" + seconds + "." + milliseconds 
                    });

            })
            .AddTo(this);
    }


    private IEnumerator Countdown(DateTime targetTime)
    {
        //DateTime targetTime = new DateTime(1970, 1, 1).Add(startTimeSpan);
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5�b�O����J�E���g�_�E���J�n

        Debug.Log("timerStartAt:" + countdownStart);

        while (DateTime.UtcNow < countdownStart)
        {
            yield return null; // ���̃t���[���܂ő҂�
        }


        int countdownSeconds = 5; // �J�E���g�_�E�����ԁi�b�j
        while (countdownSeconds > 0)
        {
            MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = countdownSeconds.ToString(), lifeTime = 100 }); ;
            yield return new WaitForSeconds(1.0f);
            countdownSeconds--;
        }
        MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = "Start!" });
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�����
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("moveSpeed", moveVertical);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement) * speed * Time.deltaTime;
        rb.AddForce(movement, ForceMode.Acceleration);


        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animator.SetTrigger("Jamp");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Acceleration);
        }
        // �W�����v����
        if (Input.GetKeyDown(KeyCode.H))
        {
            GoToStart();
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
            rb.AddForce(forceDirection * wireSpeed * Time.deltaTime, ForceMode.Acceleration);
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
            MessageBroker.Default.Publish(new GoalMsg { playerName = "player" });
            MessageBroker.Default.Publish(new SetBigBasicLogMsg { message = "Finish!", lifeTime = 1.5f });
        }
    }

    void GoToStart()
    {
        this.transform.position = Vector3.zero;
    }
}