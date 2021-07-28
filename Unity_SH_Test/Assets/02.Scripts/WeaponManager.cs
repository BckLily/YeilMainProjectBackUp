using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public Transform weaponPos; // weaponPos

    private GameObject currWeapon; // ���� ��.
    private GunsScr currGunScr; // ���� ������ �ִ� gunScr

    private bool isReload; // �÷��̾ �������� �ϰ� �ִ°�



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
        // ���⸦ �������� ������ ������ �ٷ� �ٲٱ�� �����ϱ�
        // �������� ������ ���⸦ Ȯ���ϰ�(������ ���� ������ �Ű� ������ ������ �Ǵ°�??
        // �������� ���⸦ �����ϸ�
        // ���� ������ �ִ� ���⸦ �����ϰ�
        // weaponPos�� ���ο� ���⸦ ������ ������ ���� ������

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
        // ���� �Ѿ� ���� ������ �Ѿ� ������ ���� ���
        if(currGunScr.currBullet < currGunScr.itemGun.reloadBullet)
        {
            // ������ �ִ� �Ѿ��� 0�ߺ��� ���� ���
            if(currGunScr.carryBullet > 0)
            {
                // isReload�� true�� ����
                isReload = true;
                // ������ ������ ���� ������ ��ٸ���.
                yield return new WaitForSeconds(currGunScr.itemGun.reloadTime);

                // ������ �Ѿ˿� ���� �Ѿ˸�ŭ ���� ���� ������ �Ѿ˸�ŭ ���ش�.
                currGunScr.carryBullet += (currGunScr.currBullet - currGunScr.itemGun.reloadBullet);
                // ���� �Ѿ��� ������ �Ѿ˷� �Ѵ�.
                currGunScr.currBullet = currGunScr.itemGun.reloadBullet;

                // �� ź ���� UI ����

            }
        }

        // �������� �ϰ� �ִ� ���°� �ƴϴ�.
        isReload = false;


    }

    // �߻� �õ�
    private void TryFire()
    {
        // ���� ����� ���� �ð��� ��� �����Ѵ�.
        currGunScr.fireTime += Time.deltaTime;

        // ������ ���� �ƴ� ��
        if (isReload == false)
        {
            // ���콺 ��Ŭ���� ���� ���
            if (Input.GetMouseButtonDown(0))
            {
                // ���� �� �� �ִ� ��Ȳ�̸� >> ù �߻�
                if (CheckFire())
                {
                    // fireTime�� 0���� �ʱ�ȭ�ؼ� fireTime�� �ٽ� ����ϰ� �Ѵ�.
                    currGunScr.fireTime = 0f;
                }   
            }
            // ���콺 ��Ŭ���� ������ ��� >> �������� �߻�
            else if (Input.GetMouseButton(0))
            {
                // ���� �� �� �ִ� ��Ȳ�̸�
                if (CheckFire())
                {
                    // fireTime���� delay��ŭ ���ҽ��� ���� �ӵ����� ���� �߻縦 �� �� ���� �Ѵ�.
                    currGunScr.fireTime -= currGunScr.fireDelay;

                }
            }
        }

    }

    // ���� �� �� �ִ��� Ȯ���ϰ� ����� ��ȯ���ִ� �Լ�.
    private bool CheckFire()
    {
        bool canFire = false;

        // ���� ������ �Ѿ��� 1�� �̻��� ���
        if (currGunScr.currBullet > 0)
        {
            // �� �߻� �����̺��� ���� ��� ���� �ð��� �� ũ�ų� ���� ���
            if (currGunScr.fireDelay <= currGunScr.fireTime)
            {
                canFire = true;
                // �� �߻縦 ����
                currGunScr.currBullet -= 1;
                currGunScr.BulletRaycast();

                // �߻� �ִϸ��̼� �� Effect Sound ���� ����� ���
            }
        }
        else
        {
            // ������ �Ѿ��� 0���� ��� �������� �����Ѵ�.
            // ������ ���� �ƴϱ� ������ ������ ���̶�� ���� �˷��ְ�,
            // ������ �ڷ�ƾ�� �����ϸ� �ȴ�.
            isReload = true;
            StartCoroutine(Reload());
        }

        return canFire;
    }

}
