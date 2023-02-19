using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BaseStats))]
public class GenericInfantryProperty : MonoBehaviour
{
    public bool isMoving;
    public Transform exit;
    public float visionRange;
    public float attackRange;
    public bool isStationary;
    public Animator animator;

    private NavMeshAgent agent;
    private Team team;
    private NormalAttack[] attacks;

    private void OnValidate()
    {
        if (!isStationary && GetComponent<NavMeshAgent>() == null)
            Debug.LogError("A non-stationary " + typeof(GenericInfantryProperty).Name + " requires a " + typeof(NavMeshAgent).Name + " to work properly!");
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!isStationary && agent == null)
            Debug.LogError("A non-stationary " + typeof(GenericInfantryProperty).Name + " requires a " + typeof(NavMeshAgent).Name + " to work properly!");
        team = GetComponent<BaseStats>().team;
        attacks = GetComponentsInChildren<NormalAttack>();
        /*
        if (attacks.Length > 0) 
            Debug.Log(gameObject.name + ": This is one of my attacks - " + attacks[0].name);
        else
            Debug.Log(gameObject.name + ": I have 0 attacks.");
        */
    }

    private void Start()
    {
        if (!isStationary) exit = GameObject.Find("Exit").transform;
    }
    // Update is called once per frame
    void Update()
    {
        

        // Is Stationary
        if (isStationary)
        {
            IHurtable target;
            target = GetTargetInRange();
            if (target == null) return;
            foreach (NormalAttack attack in attacks)
            {
                attack.TryToAttack(target);
            }
            return;
        }

        // Is Not Stationary
        GetTarget(out TargetSearchResult result);
        if (result.target == null)
        {
            //Debug.Log("No enemy in sight. Walk to the exit.");
            agent.isStopped = false;
            animator.SetBool("isMoving", true);
            agent.SetDestination(exit.position);
            agent.stoppingDistance = 1;
        } 
        else if (result.distanceToMe > attackRange)
        {
            //Debug.Log("Enemy in sight. Try to walk into attack range.");
            agent.isStopped = false;
            animator.SetBool("isMoving", true);
            agent.stoppingDistance = attackRange - result.distanceToSample;
            agent.SetDestination(result.samplePosition);
        }
        else
        {
            //Debug.Log("Enemy in atack range. Try to attack.");
            agent.SetDestination(transform.position);
            animator.SetBool("isMoving", false);
            foreach (NormalAttack attack in attacks)
            {
                attack.TryToAttack(result.target);
            }
        }
            

    }

    public struct TargetSearchResult
    {
        public IHurtable target;
        public Vector3 samplePosition;
        public float distanceToSample;
        public float distanceToMe;

    }
    protected virtual void GetTarget(out TargetSearchResult result)
    {
        result = new TargetSearchResult
        {
            target = null,
            distanceToMe = Mathf.Infinity
        };
        var targets = FindObjectsOfType<MonoBehaviour>().OfType<IHurtable>();
        foreach (var target in targets)
        {
            if (team == target.GetTeam()) continue;
            float dist = Vector3.Distance(transform.position, target.Transform.position);
            if (dist > visionRange) continue;
            if (dist < result.distanceToMe)
            {
                if (dist > attackRange)
                {
                    if (!NavMesh.SamplePosition(
                        target.Transform.position, 
                        out NavMeshHit closestEdge, 
                        attackRange, NavMesh.AllAreas)
                        ) continue;
                    
                    result.samplePosition = closestEdge.position;
                    result.distanceToSample = closestEdge.distance;
                }
                else
                {
                    result.samplePosition = transform.position;
                    result.distanceToSample = dist;
                }
                result.target = target;
                result.distanceToMe = dist;
            }
        }
        

    }
    protected virtual IHurtable GetTargetInRange()
    {
        BaseStats[] targets = Object.FindObjectsOfType<BaseStats>(false);
        BaseStats closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (BaseStats target in targets)
        {
            if (team == target.team) continue;
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist > attackRange) continue;
            closestTarget = target;
            closestDistance = dist;
        }

        return closestTarget;
    }
}
