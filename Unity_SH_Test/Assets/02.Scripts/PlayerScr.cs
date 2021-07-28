using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Player Prefab에 바로 적용될 Script
public class PlayerScr : MonoBehaviour
{
    Transform tr; // Player의 Transform

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


    [Header("기타 플레이어 변수")]
    [SerializeField]
    private bool isMove; // Player가 걷는가
    //private bool isRun; // Player가 달리는가
    [SerializeField]
    private bool isCrouch; // Player가 앉아 있는가
    //private bool isJump;
    //private bool isGround;

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
        crouchSpeed = walkSpeed * 0.7f;

        myRb = GetComponent<Rigidbody>();
        currUpperBodyRotation = 0f;
        lookSensitivity = 8f;
        upperBodyRotationLimit = 35f;

        playerName = string.Format("Player1");
        playerClass = PlayerClass.SOLDIER;

        isMove = false;
        isCrouch = false;

        playerAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        TryCrouch();
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        //TryCrouch();
        //PlayerMove();
        //PlayerRotation();
        //UpperBodyRotation();



    }

    private void LateUpdate()
    {
        PlayerRotation();
        UpperBodyRotation();
    }

    // 앉는 동작을 시도하는 함수
    private void TryCrouch()
    {
        // Left Control 키가 눌리면
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // 앉아있는 동작을 반대로 한다.
            // 서 있으면 앉고 앉아 있으면 서고
            isCrouch = !isCrouch;

            // 앉아 있으면
            if (isCrouch)
            {
                // 이동 속도를 앉았을 때의 속도로 설정
                useSpeed = crouchSpeed;
            }
            // 서 있으면
            else
            {
                // 이동 속도를 서 있을 때의 속도로 설정.
                useSpeed = walkSpeed;
            }

        }

    }

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            useSpeed = runSpeed;
            // 플레이어가 앉아 있으면 일어서게 한다.
            if (isCrouch)
            {
                TryCrouch();
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            useSpeed = walkSpeed;
        }


        Vector3 dir = new Vector3(h, 0f, v);
        dir.Normalize();
        dir *= useSpeed * Time.deltaTime;

        if (dir.magnitude >= 0.01f) { isMove = true; }
        else { isMove = false; }


        playerAnim.SetFloat("Horizontal", h);
        playerAnim.SetFloat("Vertical", v);
        playerAnim.SetBool("IsMove", isMove);
        playerAnim.SetFloat("Speed", useSpeed);

        tr.Translate(dir);
        Debug.Log(dir);

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
}
