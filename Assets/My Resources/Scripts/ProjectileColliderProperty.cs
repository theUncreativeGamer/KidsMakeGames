using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ProjectileColliderProperty : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        transform.BroadcastMessage("OnHitWall", collision.collider);
    }
}
