using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun", menuName ="New Gun/Gun", order =int.MaxValue)]
public class ItemGun : ScriptableObject
{
    public enum GunType
    {
        RIFLE = 0, SMG = 1, SG = 2, LMG = 3,
    }

    public enum GunRarity
    {
        COMMON = 0, RARE = 1, EPIC = 2,
    }
    public GunType gunType;
    public GunRarity gunRarity;

    public int reloadBullet;
    public int maxBullet;

    public float damage;
    public float fireDelay;
    public float reloadTime;

    private string gunName;

    public GameObject gunPrefab;
    public Sprite itemImage;



}
