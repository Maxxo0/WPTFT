using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [Header("Damage Configuration")]
    [SerializeField] float health;
    [SerializeField] float maxHealth;

    [Header("Feedback System")]
    [SerializeField] float feedbackTime;
    GameObject model; // Ref al objeto que contiene el mesh del personaje (solo en caso de que el mesh vaya aparte del código)
    MeshRenderer modelRend; // Ref al meshRenderer del objeto con modelado (permite acceder a su material)

    // Start is called before the first frame update
    void Start()
    {
        model = GameObject.Find("EnemyBody");
        modelRend = model.GetComponent<MeshRenderer>();
        
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthManagement();
    }

    void HealthManagement()
    {
        if (health <= 0) { Destroy(gameObject); }
    }

    public void TakeDamage(int damageToTake)
    {
        // Aquí cabe codear cualquier efecto de recibir daño que se desee

        health -= damageToTake;
    }

    
}
