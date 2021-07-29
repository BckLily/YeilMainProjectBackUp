using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Transform tr; // Player의 Transform


    private bool isBuild; // 플레이어가 물자를 건설하고 있는가
    private bool isBuy; // 플레이어가 상점을 이용하고 있는가
    private bool isHeal; // 플레이어가 회복을 하고 있는가


    private Animator playerAnim; // 플레이어의 애니메이션


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>(); // PlayerAction의 위치



        isBuild = false;
        isBuy = false;
        isHeal = false;

    }

    // Update is called once per frame
    void Update()
    {
        CheckRaycast();


    }

    // 다른 오브젝트 상호작용 함수
    private void CheckRaycast()
    {

    }



}

