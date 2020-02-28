using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject BananaPedestal;
    public GameObject TomatoPedestal;
    public GameObject Jester;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(BananaPedestal);
        //Instantiate(TomatoPedestal);
    }

    // Update is called once per frame
    void Update()
    {
        if(Jester.GetComponent<JesterMover>().hitByBanana)
        {
            Instantiate(TomatoPedestal);
            Destroy(BananaPedestal);
            Jester.GetComponent<JesterMover>().hitByBanana = false;
        }
        if(Jester.GetComponent<JesterMover>().hitByTomato)
        {
            Instantiate(BananaPedestal);
            Destroy(TomatoPedestal);
            Jester.GetComponent<JesterMover>().hitByTomato = false;
        }
    }
}
