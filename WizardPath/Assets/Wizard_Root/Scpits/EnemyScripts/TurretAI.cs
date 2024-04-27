using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretAI : MonoBehaviour
{

    [Header("AI Conf")]
    [SerializeField] NavMeshAgent agent; // Ref al componente Agente, que permite que el objeto tenga IA
    [SerializeField] Transform target; // Ref al transform del objeto que la IA va a perseguir
    [SerializeField] LayerMask targetLayer; // Determina cual es la capa de deteccion del target

    [Header("Attack Configuration")]
    public float timeBetweenAttacks; // Tiempo de espera entre ataque y ataque
    bool alreadyAttacked; // Bool para determinar si se ha atacado

    // DISPARO FISICO
    [SerializeField] GameObject projectile; // Ref al prefab del proyectil
    [SerializeField] Transform shootPoint; // Ref a la posición desde donde se disparan los proyectiles
    [SerializeField] float shootSpeedZ; // Vel. de disparo hacia delante
    [SerializeField] float shootSpeedY; // Vel. de disparo hacia arriba (en caso de bolea)

    [Header("States & Detection")]
    [SerializeField] float attackRange; // Rango a partir del cual la IA ataca
    [SerializeField] bool targetInAttackRange; // Bool que determina si el target está a distancia de ataque

    private void Awake()
    {
        target = GameObject.Find("Player").transform; // Al inicio referencia el transform del Player, para poder perseguirlo cuando toca
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Chequear si el target está en los rangos de detección y de ataque
       
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);
        // Si detecta al target y está en rango de ataque: ATACA
        if (targetInAttackRange) AttackTarget();
    }


    void AttackTarget()
    {
        // La IA mira directamente al target
        transform.LookAt(target);

        if (!alreadyAttacked)
        {

            // Si no hemos atacado ya, atacamos
            GenetateProjectile();
            Debug.Log("Enemigo atacando");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Resetea el ataque con un intervalo

        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void GenetateProjectile()
    {
        Rigidbody rb = Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootSpeedZ, ForceMode.Impulse);
        // rb.AddForce(transform.up * shootSpeedY, ForceMode.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
}
