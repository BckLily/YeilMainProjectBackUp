using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <date>2021-07-29.11:06</date>
/// <summary>
/// �÷��̾��� �⺻���� �������ͽ��� �̵� �� ī�޶� ȸ���� ��Ʈ�� �ϴ� �Լ�.
/// </summary>
/// 
/// <date>2021-07-29.11:12</date>
/// <modify>Player Move �Լ��� ������ �κ� ����</modify>
/// <detail>�÷��̾ �޸��� ���� ��� �ɴ� ������ ��Ȱ�ϰ� ������� �ʾ�
/// ������ ���� ���������� �����ϵ��� �Ͽ���.</detail>


// Player Prefab�� �ٷ� ����� Script
public class PlayerScr : MonoBehaviour
{
    Transform tr; // Player�� Transform
    public Transform playerCameraTr; // �÷��̾� ī�޶�

    Vector3 cameraPosition; // �÷��̾� ī�޶� ������ ������ ��.
    float cameraMoveSpeed = 4f;

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
    private float changedSpeed; // Player�� ��ȭ�� �ӵ�
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

    private float motionChangeSpeed; // �÷��̾��� �̵��ӵ� ���� �ӵ�


    [Header("��Ÿ �÷��̾� ����")]
    [SerializeField]
    private bool isMove; // Player�� �ȴ°�
    //private bool isRun; // Player�� �޸��°�
    [SerializeField]
    private bool isCrouch; // Player�� �ɾ� �ִ°�
    //private bool isJump;
    //private bool isGround;
    private bool doCrouch; // �÷��̾ �ɾ����� �� �޸��� �� ���� true�� ���� TryCrouch�� ����� �� �ְ� �Ѵ�.


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


    // �ɴ� ������ �õ��ϴ� �Լ�
    private void TryCrouch()
    {
        // Left Control Ű�� ������
        if (Input.GetKeyDown(KeyCode.LeftControl) || doCrouch)
        {
            doCrouch = false;
            // �ɾ��ִ� ������ �ݴ�� �Ѵ�.
            // �� ������ �ɰ� �ɾ� ������ ����
            isCrouch = !isCrouch;

            // �ɾ� ������
            if (isCrouch)
            {
                // �̵� �ӵ��� �ɾ��� ���� �ӵ��� ����
                //useSpeed = crouchSpeed;
                changedSpeed = crouchSpeed;
            }
            // �� ������
            else
            {
                // �̵� �ӵ��� �� ���� ���� �ӵ��� ����.
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
            // �÷��̾ �ɾ� ������ �Ͼ�� �Ѵ�.
            if (isCrouch)
            {
                doCrouch = true;
                TryCrouch();
            }
            // �Ͼ�� �̵� �ӵ��� walkSpeed�� �Ǳ� ������
            // ���Ŀ� runSpeed�� �������ش�.
            //useSpeed = runSpeed;
            changedSpeed = runSpeed;
        }
        else if(isCrouch == false)
        {
            // �ȴ� �ӵ��� �����Ѵ�.
            //useSpeed = walkSpeed;
            changedSpeed = walkSpeed;

            //useSpeed = Mathf.Lerp(useSpeed, walkSpeed, Time.deltaTime * motionChangeSpeed);
        }

        // �̵��ϴ� ���� ���� ����.
        Vector3 dir = new Vector3(h, 0f, v);
        // dir�� ũ�Ⱑ 1�� ���ͷ� �����.
        dir.Normalize();

        playerAnim.SetFloat("Horizontal", dir.x);
        playerAnim.SetFloat("Vertical", dir.z);


        // �÷��̾ �����̴� �ӵ��� ��ȭ���� �� �ٷ� ���ϴ°� �ƴ϶�
        // ���� ������ ����ؼ� õõ�� ��ȭ�Ѵ�.
        useSpeed = Mathf.Lerp(useSpeed, changedSpeed, Time.deltaTime * motionChangeSpeed);
        // ����ϴ� �ӵ��� ��ǥ�� �ӵ��� ���� �����ϸ� useSpeed�� changedSpeed�� �����ش�.
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
        // Slerp ���� ���� Lerp ���� ����
        playerCameraTr.localPosition = Vector3.Lerp(playerCameraTr.localPosition, cameraPosition, Time.deltaTime * cameraMoveSpeed);
        //if (Mathf.Round(playerCameraTr.localPosition.y) == Mathf.Round(cameraPosition.y))
        //    playerCameraTr.localPosition = cameraPosition;

    }



}
