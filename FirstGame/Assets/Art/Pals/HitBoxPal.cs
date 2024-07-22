using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPal : MonoBehaviour
{
    GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		Vector2 hitDir = collision.transform.position - parent.transform.position;
		hitDir = hitDir.normalized;
		collision.gameObject.SendMessage("BeHit", new float[] { 1, hitDir.x, hitDir.y } );
    }
}
