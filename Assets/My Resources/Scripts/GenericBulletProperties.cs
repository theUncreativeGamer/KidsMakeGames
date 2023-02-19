using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBulletProperties : MonoBehaviour, ICastable
{
    [Header("to this game object if you want it to hit walls.")]
    [Header("Please add the ProjectileColliderProperty component")]
    
    public ProjectileColliderProperty colliderProperty;
    public ProjectileHurtboxProperty hurtboxProperty;
    public int damage = 10;
    public int pierce = 2;
    public float range = 5;
    private Vector3 originalPosition;
    private bool doLog = false;
    public GameObject Caster { get; set; }

    private void OnValidate()
    {
        gameObject.layer = 8;
        colliderProperty = GetComponent<ProjectileColliderProperty>();
        Transform hurtbox = transform.Find("Hurtbox");
        if (hurtbox == null)
        {
            hurtbox = new GameObject().transform;
            hurtbox.name = "Hurtbox";
            hurtbox.SetParent(transform, false);
        }
        hurtbox.gameObject.layer = 9;
        hurtboxProperty = hurtbox.GetComponent<ProjectileHurtboxProperty>();
        if (hurtboxProperty == null) hurtboxProperty = hurtbox.gameObject.AddComponent<ProjectileHurtboxProperty>();
    }

    public void OnHit(Collider other)
    {
        if (other.gameObject.layer != 7 || gameObject.CompareTag(other.gameObject.tag)) return;
        IHurtable targetHitbox = other.GetComponent<IHurtable>();
        if (targetHitbox != null)
        {
            if(doLog) Debug.Log(gameObject.name + " hit " + other.name + ".");
            targetHitbox.Hurt(damage, Caster);
            pierce--;
            if (pierce <= 0) Destroy(gameObject);
        }
    }

    public void OnHitWall(Collider other)
    {
        if(doLog) Debug.Log(gameObject.name + " hit " + other.name + ".");
        Destroy(gameObject);
    }

    private void Awake()
    {
        if (GetComponent<ShowDebugInfo>() != null) doLog = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Caster != null) gameObject.name = Caster.name + "'s Bullet";
        if(doLog) Debug.Log(gameObject.name + " pew");
        originalPosition = transform.position;
        if (Caster != null) gameObject.tag = Caster.tag;
    }

    // Update is called once per frame
    void Update()
    {
        //if (originalPosition == null) originalPosition = transform.position;
        if (Vector3.Distance(originalPosition, transform.position) > range)
        {
            if(doLog) Debug.Log(gameObject.name + " hit nothing.");
            Destroy(gameObject);
        }
            
    }
}
