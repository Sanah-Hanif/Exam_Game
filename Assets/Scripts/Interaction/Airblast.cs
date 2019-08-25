using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Interactions;
using UnityEngine;

public class Airblast : InteractionController
{

    [SerializeField] private float velocity = 3f;
    [SerializeField] private float destroyTime = 2f;
    
    public override void Interact()
    {
        Collider2D col = GetComponent<Collider2D>();
        Collider2D[] cols = Physics2D.OverlapBoxAll((Vector2) transform.position, (Vector2) col.bounds.size, 
            transform.rotation.eulerAngles.z);
        foreach (var collider in cols)
        {
            var rb = collider.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.velocity = transform.right * velocity;
            }
        }
        Destroy(gameObject, destroyTime);
    }
}
