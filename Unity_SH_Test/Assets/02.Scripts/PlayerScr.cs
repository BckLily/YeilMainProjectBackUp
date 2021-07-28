using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Player Prefab�� �ٷ� ����� Script
public class PlayerScr : MonoBehaviour
{
    Transform tr; // Player�� Transform

    [Header("�÷��̾� �⺻ ����")]
    [SerializeField]
    private float maxHp; // �÷��̾� �ִ� ü��
    [SerializeField]
    private float currHp; // �÷��̾� ���� ü��
    [SerializeField]
    private float currDef; // �÷��̾� ���� ����
    [SerializeField]
    private float currAtk; // �÷��̾� ���� ���ݷ�

    [Header("�÷��̾� �߰� ����")]
    [SerializeField]
    private float addHp; // �÷��̾� �߰� ü��
    [SerializeField]
    private float addDef; // �÷��̾� �߰� ����
    [SerializeField]
    private float addAtk; // �÷��̾� �߰� ���ݷ�

    [Header("�÷��̾� �̵� ���� ����")]
    [SerializeField]
    private float useSpeed; // Player�� �����̴� �ӵ��� �����ؼ� ����� ����
    [SerializeField]
    private float walkSpeed; // Player�� �ȴ� �ӵ�.
    [SerializeField]
    private float runSpeed; // Player�� �޸��� �ӵ�
    [SerializeField]
    private float crouchSpeed; // Player�� �ɾ��� �� �ӵ�
    private Rigidbody myRb; // Player�� Rigidbody
    [SerializeField]
    private float currUpperBodyRotation; // Player�� ��ü ȸ�� �ӵ�
    [SerializeField]
    private float lookSensitivity; // ȭ�� ȸ���� �ΰ��� (�ӵ�)
    [SerializeField]
    private float upperBodyRotationLimit; // ��ü ȸ���� �Ѱ� ��.


    [Header("��Ÿ �÷��̾� ����")]
    [SerializeField]
    private bool isMove; // Player�� �ȴ°�
    //private bool isRun; // Player�� �޸��°�
    [SerializeField]
    private bool isCrouch; // Player�� �ɾ� �ִ°�
    //private bool isJump;
    //private bool isGround;

    // �÷��̾��� Ŭ����
    public enum PlayerClass
    {
        SOLDIER = 0, MEDIC = 1, ENGINEER = 2, HEAVY = 3,
    }

    public string playerName; // �÷��̾� �̸�
    public PlayerClass playerClass; // Player�� Class (����)



    public Animator playerAnim; // Player Animation





    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        //SetMaxHp(); // ���߿��� �Լ��� ���ؼ� �÷��̾��� maxHp�� ������ ��
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

    // �ɴ� ������ �õ��ϴ� �Լ�
    private void TryCrouch()
    {
        // Left Control Ű�� ������
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // �ɾ��ִ� ������ �ݴ�� �Ѵ�.
            // �� ������ �ɰ� �ɾ� ������ ����
            isCrouch = !isCrouch;

            // �ɾ� ������
            if (isCrouch)
            {
                // �̵� �ӵ��� �ɾ��� ���� �ӵ��� ����
                useSpeed = crouchSpeed;
            }
            // �� ������
            else
            {
                // �̵� �ӵ��� �� ���� ���� �ӵ��� ����.
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
            // �÷��̾ �ɾ� ������ �Ͼ�� �Ѵ�.
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
