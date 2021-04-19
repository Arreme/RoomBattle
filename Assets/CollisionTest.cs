using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private CustomPhysics _phy;

    private void Awake()
    {
        _phy = gameObject.GetComponent<CustomPhysics>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 surfacetangent = collision.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
        surfacetangent = new Vector2(surfacetangent.y, -surfacetangent.x);
        _phy.movementRestriction = surfacetangent.normalized;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _phy.movementRestriction = Vector2.zero;
    }
}
