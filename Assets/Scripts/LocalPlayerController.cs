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
        
        // 子メッシュを無視するレイヤーマスクを作成（例えば子メッシュがレイヤー8にあるとする
        layerMask = (1 << 8) | (1 << 9);
        layerMask = ~layerMask; // レイヤー8を除外する

        
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

                    // 2つのDateTimeの差を計算します
                    TimeSpan difference = x.goalTime - x.startTime;

                    // 結果を出力します
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
        DateTime countdownStart = targetTime.AddSeconds(-5); // 5秒前からカウントダウン開始

        Debug.Log("timerStartAt:" + countdownStart);

        while (DateTime.UtcNow < countdownStart)
        {
            yield return null; // 次のフレームまで待つ
        }


        int countdownSeconds = 5; // カウントダウン時間（秒）
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
        // 移動処理
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("moveSpeed", moveVertical);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement) * speed * Time.deltaTime;
        rb.AddForce(movement, ForceMode.Acceleration);


        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animator.SetTrigger("Jamp");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Acceleration);
        }
        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.H))
        {
            GoToStart();
        }

            // ワイヤー発射（マウス左クリック）
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

        // ワイヤー解除（マウス右クリック）
        if (Input.GetMouseButtonDown(0))
        {
            isWired = false;

            Debug.Log("dewired");
        }


        // 線の描画更新
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
            // プレイヤーを目標地点に引く
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