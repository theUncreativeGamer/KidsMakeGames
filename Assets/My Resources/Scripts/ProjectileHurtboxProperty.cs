using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ProjectileHurtboxProperty : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.BroadcastMessage("OnHit", other);
    }
}
