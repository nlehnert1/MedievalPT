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
        if (other.tag == "tomato" || other.tag == "banana")
        {
            Instantiate(tomato, self.transform.position - new Vector3(0, 0.25f, 0), transform.rotation);
        }
    }
}
