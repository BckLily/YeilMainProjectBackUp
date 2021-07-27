using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsScr : MonoBehaviour
{
    public ItemGun itemGun; // 총 프리팹을 만들기 위한 정보를 가지고 있는 스크립트.

    public Transform firePos; // 총구 위치
    private Transform playerTr; // 플레이어 오브젝트의 위치

    private int currBullet; // 현재 총알
    private int carryBullet; // 가지고 있는 총알

    private float fireDelay; // 총 발사 딜레이
    private float fireTime; // 총 쏘고 지난 시간
    //private float reloadTime; // 재장전 시간

    private float bulletDamage; // 총 데미지
    private PlayerAction playerAct; // PlayerAction Component를 저장.

    // 스크립트가 처음 시작될 때 시행
    private void Awake()
    {
        currBullet = itemGun.reloadBullet; // 시작할 때 장전된 총알 개수는 재장전 총알 개수와 동일
        carryBullet = itemGun.maxBullet - itemGun.reloadBullet; // 시작할 때 가지고 있는 총알의 개수는 최대 총알의 개수 - 재장전 총알의 개수


    }

    private void OnEnable()
    {
        playerTr = transform.parent.parent.GetComponent<Transform>(); // Player의 Transform 저장
        playerAct = playerTr.Find("PlayerCamera").GetComponent<PlayerAction>(); // Player GameObject가 가지고 있는 PlayerAction 컴포넌트를 저장.
        // 이였는데 PlayerAction이 Player의 Camera가 가지고 있을 것이라 playerAct를 얼마나 사용하느냐에 따라 수정해야겠네.

    }
        

    // Start is called before the first frame update
    void Start()
    {
        fireDelay = itemGun.fireDelay; // 총 발사 딜레이는 각 총마다 다를 수 있으므로 총이 가지고 있는 값을 가져온다.
        fireTime = fireDelay; // 총을 바로 쏠 수 있도록 한다.
        bulletDamage = itemGun.damage; // 총마다 공격력이 다르므로 총에서 공격력을 가져온다.

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator TryReload()
    {
        while (true)
        {
            // 현재 총알의 수가 재장전 총알 수보다 적은 경우
            if (currBullet < itemGun.reloadBullet)
            {
                // 가지고 있는 총알이 0발보다 많은 경우
                if (carryBullet > 0)
                {
                    // PlayerAct에 IsReload를 true로 변경
                    playerAct.IsReload = true;
                    // 재장전 동작을 할 동안 기다린다.
                    yield return new WaitForSeconds(itemGun.reloadTime);

                    // 보유 총알에 현재 총알만큼 더한 다음 재장전 총알만큼을 빼준다.
                    carryBullet += (currBullet - itemGun.reloadBullet);
                    // 현재 총알을 재장전 총알로 한다.
                    currBullet = itemGun.reloadBullet;
                }

            }
            // PlayerAct에 isReload를 false로 변경
            playerAct.IsReload = false;
            // 코루틴을 종료한다.
            yield break;
        }
    }

    // 마우스 좌클릭이 입력 되어야 수행한다.
    public void TryFire()
    {
        if(fireDelay <= fireTime)
        {
            if(currBullet > 0)
            {
                // 공격 동작 수행.
                BulletRaycast();
            }
            else
            {
                TryReload();
            }
        }
    }

    
    private void BulletRaycast()
    {
        // ray를 통해서 적을 찾는다.
        // 적을 찾으면 공격을 시행한다.
    }



}
