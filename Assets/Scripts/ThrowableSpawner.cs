using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tomato;
    public GameObject self;
    public void OnTriggerEnter(Collider other)
    {
        GameObject rulesUI = GameObject.FindGameObjectWithTag("ui");
        if(rulesUI)
        {
            Destroy(rulesUI);
        }
        if ((other.tag == "tomato") || (other.tag == "banana" ))
        {
            bool hasBeenDuplicated = other.gameObject.GetComponentInChildren<CollisionHandler>().hasBeenDuplicated;
            if (!hasBeenDuplicated)
            {
                other.gameObject.GetComponentInChildren<CollisionHandler>().hasBeenDuplicated = true;
                Instantiate(tomato, self.transform.position - new Vector3(0, 0.25f, 0), transform.rotation);
            }
        }
    }
}
