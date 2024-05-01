using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootSystem : MonoBehaviour
{

    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform shootPoint;
    [SerializeField] RaycastHit hit;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask groundLayer;
    GameObject player;
    

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
    Transform enemyT;
    



    [Header("State Bools")]
    [SerializeField] bool shooting;
    [SerializeField] bool canShoot;
    

    [Header("FeedBack & Graphics")]
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject hitGraphic;

    #endregion


    private void Awake()
    {
        
        canShoot = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shooting)
        {
            ShootSyst();
        }
    }
    void ShootSyst()
    {

        switch (GameManager.Instance.actualElement)
        {

            case GameManager.ElementStatus.fire:
                if (canShoot)
                {
                    canShoot = false;
                    fire.gameObject.SetActive(true);
                }
                break;
            case GameManager.ElementStatus.water:
                if (canShoot)
                {
                    canShoot = false;
                    Instantiate(water, shootPoint.transform.position, shootPoint.transform.rotation);
                }
                break;
            case GameManager.ElementStatus.air:
                if (canShoot)
                {
                    canShoot = false;
                    Instantiate(air, shootPoint.transform.position, shootPoint.transform.rotation);
                }
                break;
            case GameManager.ElementStatus.earth:
                if (canShoot)
                {

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
                            canShoot = false;
                            hit.transform.position = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
                            Instantiate(earth, hit.transform.position, Quaternion.identity);

                            Debug.Log("This hitable object is an enemy");
                        }




                    }

                }
                break;
            case GameManager.ElementStatus.fw:
                if (canShoot)
                {
                    float spreadX = Random.Range(-spread, spread);
                    float spreadY = Random.Range(-spread, spread);
                    float spreadZ = Random.Range(-spread, spread);
                    Vector3 direction = fpsCam.transform.forward + new Vector3(spreadX, spreadY, spreadZ);

                    if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, groundLayer))
                    {
                        Debug.DrawRay(fpsCam.transform.position, direction, Color.red);
                        Debug.Log(hit.collider.name);


                        if (hit.collider.CompareTag("Ground"))
                        {

                            canShoot = false;
                            Instantiate(fw, hit.transform.position, Quaternion.identity);
                            Debug.Log("This hitable object is an enemy");
                        }




                    }
                }
                break;
            case GameManager.ElementStatus.fa:
                if (canShoot)
                {
                    canShoot = false;
                    Instantiate(fe, shootPoint.transform.position, shootPoint.transform.rotation);
                }
                break;
            case GameManager.ElementStatus.fe:
                if (canShoot)
                {
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

                            Invoke(nameof(Explosion), 0);
                            Debug.Log("This hitable object is an enemy");
                        }

                    }
                }
                break;
            case GameManager.ElementStatus.wa:
                if (canShoot)
                {
                    PlayerController playerController = gameObject.GetComponent<PlayerController>();
                    playerController.SpeedBoost();
                }
                break;
            case GameManager.ElementStatus.we:
                if (canShoot)
                {
                    Ice();
                }
                break;
            case GameManager.ElementStatus.ae:
                if (canShoot)
                {
                    FlyingRock();
                }
                break;


        }

    }


    void Ice()
    {
        canShoot = false;
        we.gameObject.SetActive(true);
    }

    void FlyingRock()
    {
        Instantiate(ae, shootPoint.transform.position, shootPoint.transform.rotation);
    }

    void Explosion()
    {
        Instantiate(earth, hit.transform.position, Quaternion.identity);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            Debug.Log("Dispara");
            shooting = true;
        
        }
        if(context.canceled) 
        {
            shooting= false;
        }
    }

}
