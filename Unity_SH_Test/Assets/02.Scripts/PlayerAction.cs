using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Transform tr; // Player�� Transform


    private bool isBuild; // �÷��̾ ���ڸ� �Ǽ��ϰ� �ִ°�
    private bool isBuy; // �÷��̾ ������ �̿��ϰ� �ִ°�
    private bool isHeal; // �÷��̾ ȸ���� �ϰ� �ִ°�


    private Animator playerAnim; // �÷��̾��� �ִϸ��̼�


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>(); // PlayerAction�� ��ġ



        isBuild = false;
        isBuy = false;
        isHeal = false;

    }

    // Update is called once per frame
    void Update()
    {
        CheckRaycast();


    }

    // �ٸ� ������Ʈ ��ȣ�ۿ� �Լ�
    private void CheckRaycast()
    {

    }



}

