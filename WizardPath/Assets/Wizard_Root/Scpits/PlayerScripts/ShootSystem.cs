using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{

    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform shootPoint;
    [SerializeField] RaycastHit hit;
    [SerializeField] LayerMask enemyLayer;

    [Header("Elements")]
    [SerializeField] GameObject fire;
    [SerializeField] GameObject water;
    [SerializeField] GameObject air;
    [SerializeField] GameObject earth;
    [SerializeField] GameObject fw;
    [SerializeField] GameObject fa;
    [SerializeField] GameObject fe;
    [SerializeField] GameObject wa;
    [SerializeField] GameObject we;
    [SerializeField] GameObject ae;



    [Header("Weapon Stats")]
    public int damage;
    public float range;
    public float spread;
    public float shootingCooldown;
    public float timeBetweenShoots;
    public float reloadTime;
    public bool allowButtonHold;

    [Header("Bullet Management")]
    public int ammoSize;
    public int BulletsPerTap;
    [SerializeField] int bullletsLeft;
    [SerializeField] int bullletsShot;

    [Header("State Bools")]
    [SerializeField] bool shooting;
    [SerializeField] bool canShoot;
    [SerializeField] bool reloading;

    [Header("FeedBack & Graphics")]
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject hitGraphic;

    #endregion


    private void Awake()
    {
        bullletsLeft = ammoSize;
        canShoot = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShootSyst();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShootSyst()
    {
        switch (GameManager.Instance.actualElement)
        {

            case GameManager.ElementStatus.fire:
                if (canShoot && shooting && !reloading && bullletsLeft > 0)
                {
                    bullletsShot = BulletsPerTap;
                    FireShoot();
                }
                break;
















        }
    }

    void FireShoot()
    {
        canShoot = false;
        Instantiate(fire, shootPoint.transform.position, shootPoint.transform.rotation);
    }

    void WaterShoot()
    {
        canShoot = false;
        Instantiate(water, shootPoint.transform.position, shootPoint.transform.rotation);
    }

}
