using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalAttack : MonoBehaviour
{

    // Return true if the attack is performed sucessfully. Otherwise, return false.
    public abstract bool TryToAttack(IHurtable target); 
    
}
