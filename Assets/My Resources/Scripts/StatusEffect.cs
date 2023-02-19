using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : Object
{
    public string ID { get; protected set; }
    public string Name { get; protected set; }
    public float power;
    public float duration;
    public GameObject caster;
    public abstract void OnReceive(GameObject owner);
    public abstract void OnTick(GameObject owner);//Remember to remove itself once the duration reaches 0.
}

public class GenericFire : StatusEffect
{
    public float tickInterval = 0.5f;
    private float currTickInterval = 0;
    GenericFire()
    {
        ID = "generic_fire";
        Name = "Fire";
        power = 10;
        duration = 2;
    }

    public override void OnReceive(GameObject owner)
    {
        
    }

    public override void OnTick(GameObject owner)
    {
        BaseStats stats = owner.GetComponent<BaseStats>();
        if (stats == null) return;
        duration -= Time.deltaTime;
        currTickInterval += Time.deltaTime;
        while (currTickInterval >= tickInterval)
        {
            stats.Hurt(Mathf.FloorToInt(power), caster);
            currTickInterval -= tickInterval;
        }
        if (duration <= 0) Destroy(this);
    }
}
