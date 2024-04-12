using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{

    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform shootPoint;
    [SerializeField] RaycastHit hit;
    [SerializeField] LayerMask enemyLayer;

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
        Inputs();
        VisualEffects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Inputs()
    {
        if(canShoot && shooting && !reloading && bullletsLeft > 0)
        {
            bullletsShot = BulletsPerTap;
            Shoot();
        }
    }

    void VisualEffects()
    {

    }

    void Shoot()
    {
        canShoot = false;
        float spreadX = Random.Range(-spread, spread);
        float spreadY = Random.Range(-spread, spread);
        float spreadZ = Random.Range(-spread, spread);
        Vector3 direction = fpsCam.transform.forward + new Vector3(spreadX, spreadY, spreadZ);

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, enemyLayer))
        {
            Debug.DrawRay(fpsCam.transform.position, direction, Color.red);
            Debug.Log(hit.collider.name);


            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyDamage enemyScript = hit.collider.GetComponent<EnemyDamage>();
                enemyScript.TakeDamage(damage);
            }

            bullletsLeft--;
            bullletsShot--;

            if (!IsInvoking(nameof(ResetShoot)) && !canShoot)
            {
                Invoke(nameof(ResetShoot), shootingCooldown);
            }

            if(bullletsShot > 0 && bullletsLeft > 0) 
            {
                Invoke(nameof(Shoot), timeBetweenShoots);
            }

        }

    }

    void ResetShoot()
    {
        canShoot = true;
    }

    void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }
    void ReloadFinished()
    {
        bullletsLeft = ammoSize;
        reloading = false;
    }


    #region New Input Methods

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && !allowButtonHold) 
        {
            shooting = true;
            Debug.Log("Dispara");
        }
        if (context.canceled)
        {
            shooting = false;
            Debug.Log("Deja de disparar");
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            if (bullletsLeft < ammoSize && !reloading)
            {
                Reload();
            }
        }
    }

    #endregion
}
