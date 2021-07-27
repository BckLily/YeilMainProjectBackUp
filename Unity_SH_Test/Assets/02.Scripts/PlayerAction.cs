using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Transform tr; // Player�� Transform
    public Transform weaponPos; // weaponPos

    private bool isBuild; // �÷��̾ ���ڸ� �Ǽ��ϰ� �ִ°�
    private bool isBuy; // �÷��̾ ������ �̿��ϰ� �ִ°�
    private bool isHeal; // �÷��̾ ȸ���� �ϰ� �ִ°�
    private bool isReload; // �÷��̾ �������� �ϰ� �ִ°�

    private Animator playerAnim; // �÷��̾��� �ִϸ��̼�

    public bool IsReload
    {
        get
        {
            return isReload;
        }
        set
        {
            isReload = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>(); // PlayerAction�� ��ġ



        isBuild = false;
        isBuy = false;
        isHeal = false;
        isReload = false;




    }

    // Update is called once per frame
    void Update()
    {
        TryReload();
        TryFire();
        CheckRaycast();


    }


    private void TryReload()
    {
        if (isReload == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
    }

    private void TryFire()
    {

    }

    private void CheckRaycast()
    {

    }
}
