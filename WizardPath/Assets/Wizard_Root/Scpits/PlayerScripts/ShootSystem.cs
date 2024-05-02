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
    [SerializeField] GameObject camHolder;
    


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

    float lookVector;

    [Header("Weapon Stats")]
    public int damage;
    public float range;
    public float spread;
    public float lowCD;
    public float strongCD;
    public float midCD;
    public float fastSpeed;
    public float slowSpeed;
    
    [Header("State Bools")]
    [SerializeField] bool shooting;
    [SerializeField] bool canShoot;


    

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

        lookVector = camHolder.gameObject.transform.rotation.x;

        
        
    }
    void ShootSyst()
    {

        switch(GameManager.Instance.actualElement)
        {
                
            case GameManager.ElementStatus.fire:
                if (canShoot)
                {
                    canShoot = false;
                    fire.gameObject.SetActive(true);
                    Invoke(nameof(ResetShoot), strongCD);
                }
                break;
            case GameManager.ElementStatus.water:
                if (canShoot)
                {
                    canShoot = false;
                    Rigidbody rb = Instantiate(water, shootPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * slowSpeed, ForceMode.Impulse);
                    Invoke(nameof(ResetShoot), strongCD);
                }
                break;
            case GameManager.ElementStatus.air:
                if (canShoot)
                {
                    canShoot = false;
                    Rigidbody rb = Instantiate(air, shootPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * fastSpeed, ForceMode.Impulse);
                    Invoke(nameof(ResetShoot), lowCD);
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
                            Invoke(nameof(ResetShoot), strongCD);
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

                    if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, enemyLayer))
                    {
                        Debug.DrawRay(fpsCam.transform.position, direction, Color.red);
                        Debug.Log(hit.collider.name);


                        if (hit.collider.CompareTag("Enemy"))
                        {

                            canShoot = false;
                            Instantiate(fw, hit.transform.position, Quaternion.identity);
                            Debug.Log("This hitable object is an enemy");
                            Invoke(nameof(ResetShoot), strongCD);
                        }

                    }
                }
                break;
            case GameManager.ElementStatus.fa:
                if (canShoot)
                {
                    canShoot = false;
                    Rigidbody rb = Instantiate(fa, shootPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * slowSpeed, ForceMode.Impulse);
                    Invoke(nameof(ResetShoot), strongCD);
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

                            Instantiate(earth, hit.transform.position, Quaternion.identity);
                            Debug.Log("This hitable object is an enemy");
                            Invoke(nameof(ResetShoot), midCD);
                        }

                    }
                }
                break;
            case GameManager.ElementStatus.wa:
                if (canShoot)
                {
                    canShoot = false;
                    PlayerController playerController = gameObject.GetComponent<PlayerController>();
                    playerController.SpeedBoost();
                    Invoke(nameof(ResetShoot), midCD);
                }
                break;
            case GameManager.ElementStatus.we:
                if (canShoot)
                {
                    canShoot = false;
                    we.gameObject.SetActive(true);
                    Invoke(nameof(ResetShoot), strongCD);
                }
                break;
            case GameManager.ElementStatus.ae:
                if (canShoot)
                {
                    canShoot = false;
                    Instantiate(ae, shootPoint.transform.position, Quaternion.identity);
                    Invoke(nameof(ResetShoot), midCD);
                }
                break;


        }

    }



    void ResetShoot()
    {
        canShoot = true;
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
