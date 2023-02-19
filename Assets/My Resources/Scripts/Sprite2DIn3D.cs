using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite2DIn3D : MonoBehaviour
{
    public Transform sprite;
    public Camera mainCamera;
    public enum Dir { Right, Left, DoNotFlip}
    public Dir defaultDirection;

    private void OnValidate()
    {
        sprite = transform.Find("Sprite");
        if (sprite == null)
        {
            sprite = new GameObject().transform;
            sprite.name = "Sprite";
            sprite.SetParent(transform, false);
        }

        object sr = sprite.GetComponent<SpriteRenderer>();
        if (sr == null) sprite.gameObject.AddComponent<SpriteRenderer>();

        if (mainCamera == null) mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        sprite.rotation = mainCamera.transform.rotation;
        if (defaultDirection == Dir.DoNotFlip) return;
        float num = sprite.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;

        if (num < -180f) num += 360f;
        else if (num > 180f) num -= 360f;

        if (num == 0) return;
        if (num > 0 ^ defaultDirection == Dir.Right) sprite.GetComponent<SpriteRenderer>().flipX = false;
        else sprite.GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
