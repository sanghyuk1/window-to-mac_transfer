using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Move : MonoBehaviour
{
    Transform tr;
    [SerializeField]
    int speed = 5;
    [SerializeField]
    int spo = 6;
    public Text t;
    int score = 0;
    public Text scoreText;

    float h;
    float v;
    bool isJump, canJump;
    Rigidbody rigid;
    // 점프를 할 수 있는 상황인지 판단해주는 변수
    // 플레이어가 점프를 하면 변수를 false
    // 땅에 닿았을 때 다시 변수를 true
    // 충돌처리로 plane에 닿았을 때만
    // Start is called before the first frame update

    void Start()//게임 시작 후 한번만!
    {

        //transform.position = new Vector3(5, 0, 0); // 포지션을 5,0,0으로 움직인다.
        //transform.Translate(new Vector3(5, 0, 0)); // 5만큼 position을 움직인다.

    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }




    // Update is called once per frame
    void Update() // 매 프레임마다 실행
    {
        GetInput();
        Movement();
        Jump();

        //transform.position = new Vector3(5, 0, 0); // 포지션을 5,0,0으로 움직인다.
        //transform.Translate(new Vector3(1, 0, 0)); // 5만큼 position을 움직인다.
        /*if(Input.GetKey(KeyCode.A)) // T로 바꾸고 싶다. -> 
        {
            transform.Translate(new Vector3(1, 0, 0));
        }
        // 누르고 있을 때의 이벤트
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A가 눌리기 시작했습니다.");
        }
        // 누르기 시작할 때의 이벤트
        if(Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("A가 떼졌습니다.");
        }
        */
        // 뗄 떼의 이벤트
        /*if(Input.GetButton("Horizontal"))
        {
            Debug.Log("방향키 눌림");
        }
        */
        // Horizontal : a,d,방향키가 눌렸음을 확인
        // Debug.Log(Input.GetAxis("Horizontal"));
        // GetAxis : 부드러운 움직임을 구현할 때 사용된다.
        // Debug.Log(Input.GetAxisRaw("Horizontal"));
        // GetAxisRaw : 부드럽진 않지만, -1, 1까지 반환을 하는 함수

        // a d 를 누르면 양 옆으로 움직이게 해보세요.

        // deltaTime을 곱하면 이동거리를 공평하게 할 수 있다.(단, 스피드 설정 필수)
        //Vector3 vector = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        //transform.Translate(vector * speed);


    }

    void GetInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        isJump = Input.GetKeyDown(KeyCode.Space);
    }
    void Movement()
    {
        Vector3 vector = new Vector3(0.1f * h, 0, 0.1f * v);
        vector += vector * speed * Time.deltaTime;
        transform.Translate(vector);
    }
    void Jump()
    {
        if (isJump && canJump)
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            canJump = false;
            t.text = "점프가 눌렸습니다.";
        }
    }
    public void Jump2()
    {
        rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
    void LateUpdate() // 모든 업데이트가 끝나고 실행 - UI적용을 많이한다.
    {
        scoreText.text = score.ToString();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.name == "plane")
        {
            canJump = true;
        }
        if (collisionInfo.gameObject.tag == "Finish")
        {
            score++;
            Destroy(collisionInfo.gameObject);
        }
        // 정수형 변수를 설정하고 먹을대마다 1씩 늘린다.
    }
}

