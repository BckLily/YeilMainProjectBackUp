using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <date>2021-07-29.11:06</date>
/// <summary>
/// 플레이어의 기본적인 스테이터스와 이동 및 카메라 회전을 컨트롤 하는 함수.
/// </summary>
/// 
/// <date>2021-07-29.11:12</date>
/// <modify>Player Move 함수의 부족한 부분 수정</modify>
/// <detail>플레이어가 달리고 있을 경우 앉는 동작이 원활하게 진행되지 않아
/// 수정을 거쳐 정상적으로 동작하도록 하였다.</detail>


// Player Prefab에 바로 적용될 Script
public class PlayerScr : MonoBehaviour
{
    Transform tr; // Player의 Transform
    public Transform playerCameraTr; // 플레이어 카메라

    Vector3 cameraPosition; // 플레이어 카메라 변경할 포지션 값.
    float cameraMoveSpeed = 4f;

    [Header("플레이어 기본 스탯")]
    [SerializeField]
    private float maxHp; // 플레이어 최대 체력
    [SerializeField]
    private float currHp; // 플레이어 현재 체력
    [SerializeField]
    private float currDef; // 플레이어 현재 방어력
    [SerializeField]
    private float currAtk; // 플레이어 현재 공격력

    [Header("플레이어 추가 스탯")]
    [SerializeField]
    private float addHp; // 플레이어 추가 체력
    [SerializeField]
    private float addDef; // 플레이어 추가 방어력
    [SerializeField]
    private float addAtk; // 플레이어 추가 공격력

    [Header("플레이어 이동 관련 변수")]
    [SerializeField]
    private float useSpeed; // Player의 움직이는 속도를 대입해서 사용할 변수
    [SerializeField]
    private float changedSpeed; // Player의 변화할 속도
    [SerializeField]
    private float walkSpeed; // Player의 걷는 속도.
    [SerializeField]
    private float runSpeed; // Player의 달리는 속도
    [SerializeField]
    private float crouchSpeed; // Player의 앉았을 때 속도
    private Rigidbody myRb; // Player의 Rigidbody
    [SerializeField]
    private float currUpperBodyRotation; // Player의 상체 회전 속도
    [SerializeField]
    private float lookSensitivity; // 화면 회전의 민감도 (속도)
    [SerializeField]
    private float upperBodyRotationLimit; // 상체 회전의 한계 값.

    private float motionChangeSpeed; // 플레이어의 이동속도 보간 속도


    [Header("기타 플레이어 변수")]
    [SerializeField]
    private bool isMove; // Player가 걷는가
    //private bool isRun; // Player가 달리는가
    [SerializeField]
    private bool isCrouch; // Player가 앉아 있는가
    //private bool isJump;
    //private bool isGround;
    private bool doCrouch; // 플레이어가 앉아있을 때 달리면 이 값을 true로 만들어서 TryCrouch가 실행될 수 있게 한다.


    // 플레이어의 클래스
    public enum PlayerClass
    {
        SOLDIER = 0, MEDIC = 1, ENGINEER = 2, HEAVY = 3,
    }

    public string playerName; // 플레이어 이름
    public PlayerClass playerClass; // Player의 Class (직업)


    public Animator playerAnim; // Player Animation





    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        //SetMaxHp(); // 나중에는 함수를 통해서 플레이어의 maxHp를 설정할 것
        maxHp = 100;
        currHp = maxHp;
        currDef = 0f;
        currAtk = 0f;

        addHp = 0f;
        addDef = 0f;
        addAtk = 0f;

        walkSpeed = 8f;
        useSpeed = walkSpeed;
        runSpeed = walkSpeed * 1.5f;
        crouchSpeed = walkSpeed * 0.35f;
        changedSpeed = walkSpeed;

        motionChangeSpeed = 4f;

        myRb = GetComponent<Rigidbody>();
        currUpperBodyRotation = 0f;
        lookSensitivity = 8f;
        upperBodyRotationLimit = 35f;

        playerName = string.Format("Player1");
        playerClass = PlayerClass.SOLDIER;

        isMove = false;
        isCrouch = false;

        playerAnim = GetComponent<Animator>();
        cameraPosition = playerCameraTr.position;
    }

    private void FixedUpdate()
    {
        //TryCrouch();
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        TryCrouch();
        //PlayerMove();
        //PlayerRotation();
        //UpperBodyRotation();



    }

    private void LateUpdate()
    {
        //TryCrouch();
        PlayerRotation();
        UpperBodyRotation();
        PlayerCameraMove();
    }


    // 앉는 동작을 시도하는 함수
    private void TryCrouch()
    {
        // Left Control 키가 눌리면
        if (Input.GetKeyDown(KeyCode.LeftControl) || doCrouch)
        {
            doCrouch = false;
            // 앉아있는 동작을 반대로 한다.
            // 서 있으면 앉고 앉아 있으면 서고
            isCrouch = !isCrouch;

            // 앉아 있으면
            if (isCrouch)
            {
                // 이동 속도를 앉았을 때의 속도로 설정
                //useSpeed = crouchSpeed;
                changedSpeed = crouchSpeed;
            }
            // 서 있으면
            else
            {
                // 이동 속도를 서 있을 때의 속도로 설정.
                //useSpeed = walkSpeed;
                changedSpeed = walkSpeed;
            }

            if (isCrouch)
            {
                cameraPosition.y += -0.375f;
            }
            else
            {
                cameraPosition.y += 0.375f;
            }
        }




    }

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 플레이어가 앉아 있으면 일어서게 한다.
            if (isCrouch)
            {
                doCrouch = true;
                TryCrouch();
            }
            // 일어서면 이동 속도가 walkSpeed가 되기 때문에
            // 이후에 runSpeed로 변경해준다.
            //useSpeed = runSpeed;
            changedSpeed = runSpeed;
        }
        else if(isCrouch == false)
        {
            // 걷는 속도로 변경한다.
            //useSpeed = walkSpeed;
            changedSpeed = walkSpeed;

            //useSpeed = Mathf.Lerp(useSpeed, walkSpeed, Time.deltaTime * motionChangeSpeed);
        }

        // 이동하는 방향 벡터 설정.
        Vector3 dir = new Vector3(h, 0f, v);
        // dir을 크기가 1인 벡터로 만든다.
        dir.Normalize();

        playerAnim.SetFloat("Horizontal", dir.x);
        playerAnim.SetFloat("Vertical", dir.z);


        // 플레이어가 움직이는 속도가 변화했을 때 바로 변하는게 아니라
        // 선형 보간을 사용해서 천천히 변화한다.
        useSpeed = Mathf.Lerp(useSpeed, changedSpeed, Time.deltaTime * motionChangeSpeed);
        // 사용하는 속도와 목표의 속도가 거의 동일하면 useSpeed를 changedSpeed로 맞춰준다.
        if (useSpeed != changedSpeed && Mathf.Abs((useSpeed - changedSpeed) / changedSpeed) <= 0.1f)
        {
            useSpeed = changedSpeed;
        }
        //if(Mathf.Approximately(useSpeed, changedSpeed))
        //{
        //    useSpeed = changedSpeed;
        //}




        // dir = dir * useSpeed * Time.deltaTime;
        dir *= useSpeed * Time.deltaTime;

        if (dir.magnitude >= 0.01f) { isMove = true; }
        else { isMove = false; }



        playerAnim.SetBool("IsMove", isMove);
        playerAnim.SetBool("IsCrouch", isCrouch);
        playerAnim.SetFloat("Speed", useSpeed);

        tr.Translate(dir);
        //Debug.Log(dir);

    }

    private void PlayerRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;

        myRb.MoveRotation(myRb.rotation * Quaternion.Euler(characterRotationY));
        
    }

    private void UpperBodyRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse Y");
        float bodyRotation = rotation * lookSensitivity;

        currUpperBodyRotation -= bodyRotation;
        currUpperBodyRotation = Mathf.Clamp(currUpperBodyRotation, -upperBodyRotationLimit, upperBodyRotationLimit);

        //playerAnim.SetFloat("UpperBodyRotation", currUpperBodyRotation);


    }

    private void PlayerCameraMove()
    {
        // Slerp 구형 보간 Lerp 선형 보간
        playerCameraTr.localPosition = Vector3.Lerp(playerCameraTr.localPosition, cameraPosition, Time.deltaTime * cameraMoveSpeed);
        //if (Mathf.Round(playerCameraTr.localPosition.y) == Mathf.Round(cameraPosition.y))
        //    playerCameraTr.localPosition = cameraPosition;

    }



}
