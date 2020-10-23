using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //referenca na Rigidbody2D komponentu platforme
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            PlatformManager.Instance.StartCoroutine("SpawnPlatform", new Vector2(transform.position.x, transform.position.y));
            Invoke("DropPlatform", 0.5f);
            Destroy(gameObject, 1f);
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }

}
