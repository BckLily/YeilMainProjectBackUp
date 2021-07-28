using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public Transform weaponPos; // weaponPos

    private GameObject currWeapon; // 현재 총.
    private GunsScr currGunScr; // 총이 가지고 있는 gunScr

    private bool isReload; // 플레이어가 재장전을 하고 있는가



    // Start is called before the first frame update
    void Start()
    {
        weaponPos = GetComponent<Transform>();
        isReload = false;
        WeaponChange();
    }

    // Update is called once per frame
    void Update()
    {

        TryReload();
        TryFire();

    }

    private void WeaponChange()
    {
        // 무기를 상점에서 구매한 것으로 바로 바꾸기로 했으니까
        // 상점에서 구매한 무기를 확인하고(구매한 무기 정보를 매개 변수로 받으면 되는가??
        // 상점에서 무기를 구매하면
        // 현재 가지고 있는 무기를 제거하고
        // weaponPos에 새로운 무기를 가져다 박으면 되지 않을까

        currWeapon = weaponPos.GetChild(0).gameObject;
        Debug.Log(currWeapon.name);
        currGunScr = currWeapon.GetComponent<GunsScr>();


    }

    private void TryReload()
    {
        if (isReload == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        // 현재 총알 수가 재장전 총알 수보다 작은 경우
        if(currGunScr.currBullet < currGunScr.itemGun.reloadBullet)
        {
            // 가지고 있는 총알이 0발보다 많은 경우
            if(currGunScr.carryBullet > 0)
            {
                // isReload를 true로 변경
                isReload = true;
                // 재장전 동작이 끝날 때까지 기다린다.
                yield return new WaitForSeconds(currGunScr.itemGun.reloadTime);

                // 보유한 총알에 현재 총알만큼 더한 다음 재장전 총알만큼 빼준다.
                currGunScr.carryBullet += (currGunScr.currBullet - currGunScr.itemGun.reloadBullet);
                // 현재 총알을 재장전 총알로 한다.
                currGunScr.currBullet = currGunScr.itemGun.reloadBullet;

                // 총 탄 관련 UI 갱신

            }
        }

        // 재장전을 하고 있는 상태가 아니다.
        isReload = false;


    }

    // 발사 시도
    private void TryFire()
    {
        // 총을 쏘고나서 지난 시간은 계속 증가한다.
        currGunScr.fireTime += Time.deltaTime;

        // 재장전 중이 아닐 때
        if (isReload == false)
        {
            // 마우스 좌클릭을 누른 경우
            if (Input.GetMouseButtonDown(0))
            {
                // 총을 쏠 수 있는 상황이면 >> 첫 발사
                if (CheckFire())
                {
                    // fireTime을 0으로 초기화해서 fireTime을 다시 계산하게 한다.
                    currGunScr.fireTime = 0f;
                }   
            }
            // 마우스 좌클릭이 유지된 경우 >> 연속적인 발사
            else if (Input.GetMouseButton(0))
            {
                // 총을 쏠 수 있는 상황이면
                if (CheckFire())
                {
                    // fireTime에서 delay만큼 감소시켜 공격 속도보다 빠른 발사를 할 수 없게 한다.
                    currGunScr.fireTime -= currGunScr.fireDelay;

                }
            }
        }

    }

    // 총을 쏠 수 있는지 확인하고 결과를 반환해주는 함수.
    private bool CheckFire()
    {
        bool canFire = false;

        // 현재 장전된 총알이 1발 이상인 경우
        if (currGunScr.currBullet > 0)
        {
            // 총 발사 딜레이보다 총을 쏘고 지난 시간이 더 크거나 같을 경우
            if (currGunScr.fireDelay <= currGunScr.fireTime)
            {
                canFire = true;
                // 총 발사를 진행
                currGunScr.currBullet -= 1;
                currGunScr.BulletRaycast();

                // 발사 애니메이션 및 Effect Sound 등이 실행될 장소
            }
        }
        else
        {
            // 장전된 총알이 0발인 경우 재장전을 수행한다.
            // 재장전 중이 아니기 때문에 재장전 중이라는 것을 알려주고,
            // 재장전 코루틴을 시작하면 된다.
            isReload = true;
            StartCoroutine(Reload());
        }

        return canFire;
    }

}
