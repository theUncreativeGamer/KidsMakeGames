using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBulletShooter : NormalAttack
{
    public GameObject BulletPrefab;
    public float bulletSpeed = 3f;
    [Space(5)]
    public float range = 3f;
    public float cooldown = 1f;
    public float currentCooldown = 0f;

    [SerializeField] private Animator anim;
    private bool useAnim;

    private void Awake()
    {
        useAnim = anim != null;
    }



    // Return true if the attack is performed sucessfully. Otherwise, return false.
    public override bool TryToAttack(IHurtable target)
    {
        if (currentCooldown < cooldown) return false;
        if (Vector3.Distance(transform.position, target.Transform.position) > range) return false;

        currentCooldown = 0;
        GameObject newBullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
        newBullet.transform.LookAt(target.Center);
        newBullet.transform.rotation =
            Quaternion.Euler(0, newBullet.transform.rotation.eulerAngles.y, 0);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletSpeed;
        newBullet.GetComponent<ICastable>().Caster = transform.parent.gameObject;
        BroadcastMessage("OnAttack", target.Transform, SendMessageOptions.DontRequireReceiver);
        if(useAnim) anim.SetTrigger("Attack");
        return true;

    }

    private void Update()
    {
        currentCooldown += Time.deltaTime;
    }
}
