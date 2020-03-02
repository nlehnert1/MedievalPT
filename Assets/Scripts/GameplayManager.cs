using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject BananaPedestal;
    public GameObject TomatoPedestal;
    public GameObject Jester;
    public GameObject tomato;
    public GameObject banana;

    // Start is called before the first frame update
    void Start()
    {
        BananaPedestal.SetActive(true);
        TomatoPedestal.SetActive(true);
        //Instantiate(BananaPedestal);
        //Instantiate(TomatoPedestal);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] tomatoes = GameObject.FindGameObjectsWithTag("tomato");
        GameObject[] bananas = GameObject.FindGameObjectsWithTag("banana");
        if(Jester.GetComponent<JesterMover>().hitByBanana)
        {
            //Instantiate(TomatoPedestal);
            TomatoPedestal.SetActive(true);
            foreach(GameObject banana in bananas) {
                banana.SetActive(false);
            }
            foreach(GameObject tomato in tomatoes) {
                tomato.SetActive(true);
            }
            Instantiate(tomato, TomatoPedestal.transform.position + new Vector3(0, 0.25f, 0), transform.rotation);
            //Destroy(BananaPedestal);
            BananaPedestal.SetActive(false);
            Jester.GetComponent<JesterMover>().hitByBanana = false;
        }
        if(Jester.GetComponent<JesterMover>().hitByTomato)
        {
            foreach(GameObject banana in bananas) {
                banana.SetActive(true);
            }
            foreach(GameObject tomato in tomatoes) {
                tomato.SetActive(false);
            }
            //Instantiate(BananaPedestal);
            BananaPedestal.SetActive(true);
            Instantiate(banana, BananaPedestal.transform.position + new Vector3(0, 0.25f, 0), transform.rotation);
            //Destroy(TomatoPedestal);
            TomatoPedestal.SetActive(false);
            Jester.GetComponent<JesterMover>().hitByTomato = false;
        }
    }
}
