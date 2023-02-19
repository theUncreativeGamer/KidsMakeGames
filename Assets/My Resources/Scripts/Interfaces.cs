using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICastable
{
    public GameObject Caster { get; set; }
}

public interface IHurtable
{
    public Team GetTeam();
    public Transform Transform { get; }
    public Vector3 Center { get; }
    public void Hurt(float damage, GameObject caster = null);
}

