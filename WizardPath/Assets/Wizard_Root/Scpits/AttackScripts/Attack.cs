using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage;
    public bool isntGrounded;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDamage enemyDamage = collision.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.TakeDamage(damage);

            if (isntGrounded)
            {
                Die();
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isntGrounded)
            {
                Die();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.TakeDamage(damage);

            if (isntGrounded)
            {
                Die();
            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            if (isntGrounded)
            {
                Die();
            }
        }
    }


    public void PowerOff()
    {
        gameObject.SetActive(false);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
