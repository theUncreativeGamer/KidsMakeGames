using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public enum Team { Friend, Bully }
public class BaseStats : MonoBehaviour, IHurtable
{
    
    public Team team;
    public float maxHP = 100;
    public float currentHP = 100;
    public UnityEvent OnHurt;
    public UnityEvent OnDeath;

    public Transform Transform => transform;

    public Vector3 Center => (GetComponent<Collider>() != null) ? GetComponent<Collider>().bounds.center : transform.position;

    public void Hurt(float damage, GameObject caster = null)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            OnDeath.Invoke();
            Destroy(gameObject);
            this.enabled = false;
        }
        else OnHurt.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Team GetTeam()
    {
        return team;
    }
}
