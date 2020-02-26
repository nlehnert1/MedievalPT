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
        Instantiate(tomato, self.transform);
    }
}
