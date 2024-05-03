using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Libreria para ref clases de NavMesh

public class EnemyAiBase : MonoBehaviour
{

    [Header("AI Conf")]
    [SerializeField] NavMeshAgent agent; // Ref al componente Agente, que permite que el objeto tenga IA
    [SerializeField] Transform target; // Ref al transform del objeto que la IA va a perseguir
    [SerializeField] LayerMask targetLayer; // Determina cual es la capa de deteccion del target
    [SerializeField] LayerMask groundLayer; // Determina cual es la capa de detección del suelo


    [Header("Patroling Stats")]
    [SerializeField] Vector3 walkPoint; // Direccion a la que la IA se va a mover si no detecta al target
    [SerializeField] float walkPointRange; // Rango maximo de direccion de movimiento si la IA no detecta target
    bool walkPointSet; // Bool que detetermina si la IA ha llegado al objetivo y entonces cambia de objeto

    [Header("Attack Configuration")]
    public float timeBetweenAttacks; // Tiempo de espera entre ataque y ataque
    bool alreadyAttacked; // Bool para determinar si se ha atacado

    // DISPARO FISICO
    [SerializeField] GameObject attack; // Ref al prefab del proyectil
    


    [Header("States & Detection")]
    [SerializeField] float sightRange; // Rango de detección de persecución de la IA
    [SerializeField] float attackRange; // Rango a partir del cual la IA ataca
    [SerializeField] bool targetInSightRange; // Bool que determina si el target está a distancia de detección
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
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, targetLayer);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);

        // Cambios dinámicos de estade de la IA
        // Si no detecta el target ni está en rango de ataque: Patrulla
        if (!targetInSightRange && !targetInAttackRange) Patroling();
        // Si detecta el target pero no está em rango de ataque: PERSIGUE
        if (targetInSightRange && !targetInAttackRange) ChaseTarget();
        // Si detecta al target y está en rango de ataque: ATACA
        if (targetInSightRange && targetInAttackRange) AttackTarget();
    }

    void Patroling()
    {
        if (!walkPointSet)
        {
            // Si no existe punto al que dirigirse, inicia el método de crearlo
            SearchWalkPoint();
        }
        else
        {
            // Si existe punto, el  personaje mueve la IA hacia ese punto
            agent.SetDestination(walkPoint);
        }

        // Sistema para que la IA busque un nuevo destino de patrullaje una vez ha llegado al destino actual
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1) { walkPointSet = false; }
    }

    void SearchWalkPoint()
    {
        // Crear el sistema de puntos "random" a patrullar

        // Sistema de creación de puntos Random
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);

        // Direncción a la que se mueve la IA
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Detección del suelo por debajo del personaje, para evitar caida
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    void ChaseTarget()
    {
        agent.SetDestination(target.position);
    }

    void AttackTarget() 
    {
        // Cuando comienza a atacar, no se mueve (se persigue a si mismo)
        agent.SetDestination(transform.position);
        // La IA mira directamente al target
        transform.LookAt(target);
        
        if (!alreadyAttacked) 
        {

            // Si no hemos atacado ya, atacamos
            attack.gameObject.SetActive(true);
            Debug.Log("Enemigo atacando");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Resetea el ataque con un intervalo
        
        }
    }
    
    void ResetAttack()
    {
        alreadyAttacked = false;
    }
 
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
