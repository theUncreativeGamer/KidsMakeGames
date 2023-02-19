using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDeathEffect : MonoBehaviour
{
    public GameObject EffectPrefab;
    private bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!isQuitting)
        {
            GameObject effect = Instantiate(EffectPrefab, transform.position, Quaternion.identity);
        }
            
    }
}
