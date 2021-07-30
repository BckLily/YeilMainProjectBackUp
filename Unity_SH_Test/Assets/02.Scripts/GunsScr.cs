using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsScr : MonoBehaviour
{
    public ItemGun itemGun; // �� �������� ����� ���� ������ ������ �ִ� ��ũ��Ʈ.

    public Transform firePos; // �ѱ� ��ġ
    private Transform playerTr; // �÷��̾� ������Ʈ�� ��ġ

    // ������Ƽ ��� ����.
    public int currBullet; // ���� �Ѿ�
    public int carryBullet; // ������ �ִ� �Ѿ�
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

    public float fireDelay; // �� �߻� ������
    public float fireTime; // �� ��� ���� �ð�
    //private float reloadTime; // ������ �ð�

    private float bulletDamage; // �� ������
    //private PlayerAction playerAct; // PlayerAction Component�� ����. // ������ ������� �ʴ´�.

    // ��ũ��Ʈ�� ó�� ���۵� �� ����
    private void Awake()
    {
        currBullet = itemGun.reloadBullet; // ������ �� ������ �Ѿ� ������ ������ �Ѿ� ������ ����
        carryBullet = itemGun.maxBullet - itemGun.reloadBullet; // ������ �� ������ �ִ� �Ѿ��� ������ �ִ� �Ѿ��� ���� - ������ �Ѿ��� ����

    }

    private void OnEnable()
    {
        playerTr = transform.parent.parent.GetComponent<Transform>(); // Player�� Transform ����
        //playerAct = playerTr.Find("Character1_Neck").Find("PlayerCamera").GetComponent<PlayerAction>(); // Player GameObject�� ������ �ִ� PlayerAction ������Ʈ�� ����.
        // �̿��µ� PlayerAction�� Player�� Camera�� ������ ���� ���̶� playerAct�� �󸶳� ����ϴ��Ŀ� ���� �����ؾ߰ڳ�.
        // ������ ������� �ʴ´�.

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





    // �߻� ������ Ȯ���� WeaponManager���� �� ���̴�.
    // �߻� ������ �ϸ� WeaponManager���� BulletRaycast()�� �����Ų��.
    
    public void BulletRaycast()
    {
        // ray�� ���ؼ� ���� ã�´�.
        // ���� ã���� ������ �����Ѵ�.
        RaycastHit hit;
        // if(Physics.Raycast())

        Debug.Log("Fire!");

    }



}
