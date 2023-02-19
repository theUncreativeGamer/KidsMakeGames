using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitboxComponent : MonoBehaviour, IHurtable
{
    public BaseStats mainObject;

    public Transform Transform => transform;

    public Vector3 Center => GetComponent<Collider>().bounds.center;

    

    private void OnValidate()
    {
        mainObject = transform.parent.GetComponent<BaseStats>();
    }

    public void Hurt(float damage, GameObject caster = null)
    {
        mainObject.Hurt(damage, caster);
    }

    public Team GetTeam()
    {
        return mainObject.team;
    }
}
