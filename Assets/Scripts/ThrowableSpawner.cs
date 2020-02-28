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
        GameObject[] tomatoes = GameObject.FindGameObjectsWithTag("tomato");
        GameObject[] bananas = GameObject.FindGameObjectsWithTag("banana");
        if ((other.tag == "tomato" && tomatoes.Length < 5) || (other.tag == "banana" && bananas.Length < 5))
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
