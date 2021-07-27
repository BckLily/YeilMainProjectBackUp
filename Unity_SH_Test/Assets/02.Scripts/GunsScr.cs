using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsScr : MonoBehaviour
{
    public ItemGun itemGun; // �� �������� ����� ���� ������ ������ �ִ� ��ũ��Ʈ.

    public Transform firePos; // �ѱ� ��ġ
    private Transform playerTr; // �÷��̾� ������Ʈ�� ��ġ

    private int currBullet; // ���� �Ѿ�
    private int carryBullet; // ������ �ִ� �Ѿ�

    private float fireDelay; // �� �߻� ������
    private float fireTime; // �� ��� ���� �ð�
    //private float reloadTime; // ������ �ð�

    private float bulletDamage; // �� ������
    private PlayerAction playerAct; // PlayerAction Component�� ����.

    // ��ũ��Ʈ�� ó�� ���۵� �� ����
    private void Awake()
    {
        currBullet = itemGun.reloadBullet; // ������ �� ������ �Ѿ� ������ ������ �Ѿ� ������ ����
        carryBullet = itemGun.maxBullet - itemGun.reloadBullet; // ������ �� ������ �ִ� �Ѿ��� ������ �ִ� �Ѿ��� ���� - ������ �Ѿ��� ����


    }

    private void OnEnable()
    {
        playerTr = transform.parent.parent.GetComponent<Transform>(); // Player�� Transform ����
        playerAct = playerTr.Find("PlayerCamera").GetComponent<PlayerAction>(); // Player GameObject�� ������ �ִ� PlayerAction ������Ʈ�� ����.
        // �̿��µ� PlayerAction�� Player�� Camera�� ������ ���� ���̶� playerAct�� �󸶳� ����ϴ��Ŀ� ���� �����ؾ߰ڳ�.

    }
        

    // Start is called before the first frame update
    void Start()
    {
        fireDelay = itemGun.fireDelay; // �� �߻� �����̴� �� �Ѹ��� �ٸ� �� �����Ƿ� ���� ������ �ִ� ���� �����´�.
        fireTime = fireDelay; // ���� �ٷ� �� �� �ֵ��� �Ѵ�.
        bulletDamage = itemGun.damage; // �Ѹ��� ���ݷ��� �ٸ��Ƿ� �ѿ��� ���ݷ��� �����´�.

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator TryReload()
    {
        while (true)
        {
            // ���� �Ѿ��� ���� ������ �Ѿ� ������ ���� ���
            if (currBullet < itemGun.reloadBullet)
            {
                // ������ �ִ� �Ѿ��� 0�ߺ��� ���� ���
                if (carryBullet > 0)
                {
                    // PlayerAct�� IsReload�� true�� ����
                    playerAct.IsReload = true;
                    // ������ ������ �� ���� ��ٸ���.
                    yield return new WaitForSeconds(itemGun.reloadTime);

                    // ���� �Ѿ˿� ���� �Ѿ˸�ŭ ���� ���� ������ �Ѿ˸�ŭ�� ���ش�.
                    carryBullet += (currBullet - itemGun.reloadBullet);
                    // ���� �Ѿ��� ������ �Ѿ˷� �Ѵ�.
                    currBullet = itemGun.reloadBullet;
                }

            }
            // PlayerAct�� isReload�� false�� ����
            playerAct.IsReload = false;
            // �ڷ�ƾ�� �����Ѵ�.
            yield break;
        }
    }

    // ���콺 ��Ŭ���� �Է� �Ǿ�� �����Ѵ�.
    public void TryFire()
    {
        if(fireDelay <= fireTime)
        {
            if(currBullet > 0)
            {
                // ���� ���� ����.
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
        // ray�� ���ؼ� ���� ã�´�.
        // ���� ã���� ������ �����Ѵ�.
    }



}
