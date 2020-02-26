using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touched collision handler");
        if(collision.gameObject.tag == "tomato" || collision.gameObject.tag == "banana")
        {
            Debug.Log("Collision is with either a tomato or banana");
            Debug.Log(collision.collider);
            Debug.Log(gameObject.GetComponentInChildren<MeshCollider>());
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponentInChildren<MeshCollider>());
        }
    }
}
