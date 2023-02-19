using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttackTurning : MonoBehaviour
{
    public Transform[] horizontalRotators;
    public Transform[] verticalRotators;

    public void OnAttack(Transform target)
    {
        foreach (Transform stuff in horizontalRotators)
        {
            stuff.LookAt(target);
            stuff.rotation = Quaternion.Euler(0, stuff.rotation.eulerAngles.y, 0);
        }
        foreach (Transform stuff in verticalRotators)
        {
            stuff.LookAt(target);
        }

    }
}
