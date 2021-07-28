using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsScr : MonoBehaviour
{
    public ItemGun itemGun; // 총 프리팹을 만들기 위한 정보를 가지고 있는 스크립트.

    public Transform firePos; // 총구 위치
    private Transform playerTr; // 플레이어 오브젝트의 위치

    // 프로퍼티 사용 안함.
    public int currBullet; // 현재 총알
    public int carryBullet; // 가지고 있는 총알
    //public int currBullet
    //{
    //    get
    //    {
    //        return _currBullet;
    //    }
    //}
    //public int carryBullet
    //{
    //    get
    //    {
    //        return _carryBullet;
    //    }
    //    set
    //    {
    //        _carryBullet = value;
    //    }
    //}

    public float fireDelay; // 총 발사 딜레이
    public float fireTime; // 총 쏘고 지난 시간
    //private float reloadTime; // 재장전 시간

    private float bulletDamage; // 총 데미지
    //private PlayerAction playerAct; // PlayerAction Component를 저장. // 지금은 사용하지 않는다.

    // 스크립트가 처음 시작될 때 시행
    private void Awake()
    {
        currBullet = itemGun.reloadBullet; // 시작할 때 장전된 총알 개수는 재장전 총알 개수와 동일
        carryBullet = itemGun.maxBullet - itemGun.reloadBullet; // 시작할 때 가지고 있는 총알의 개수는 최대 총알의 개수 - 재장전 총알의 개수

    }

    private void OnEnable()
    {
        playerTr = transform.parent.parent.GetComponent<Transform>(); // Player의 Transform 저장
        //playerAct = playerTr.Find("Character1_Neck").Find("PlayerCamera").GetComponent<PlayerAction>(); // Player GameObject가 가지고 있는 PlayerAction 컴포넌트를 저장.
        // 이였는데 PlayerAction이 Player의 Camera가 가지고 있을 것이라 playerAct를 얼마나 사용하느냐에 따라 수정해야겠네.
        // 지금은 사용하지 않는다.

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





    // 발사 조건의 확인은 WeaponManager에서 할 것이다.
    // 발사 동작을 하면 WeaponManager에서 BulletRaycast()를 실행시킨다.
    
    public void BulletRaycast()
    {
        // ray를 통해서 적을 찾는다.
        // 적을 찾으면 공격을 시행한다.
        RaycastHit hit;
        // if(Physics.Raycast())

        Debug.Log("Fire!");

    }



}
