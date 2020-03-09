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
    public GameObject BananaPrompt;
    public GameObject TomatoPrompt;

    // Start is called before the first frame update
    void Start()
    {
        BananaPedestal.SetActive(true);
        TomatoPedestal.SetActive(true);
        BananaPrompt.SetActive(false);
        TomatoPrompt.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] tomatoes = GameObject.FindGameObjectsWithTag("tomato");
        GameObject[] bananas = GameObject.FindGameObjectsWithTag("banana");
        if(!BananaPrompt.activeInHierarchy && Jester.GetComponent<JesterMover>().shouldBeHitByBanana)
        {
            //TomatoPedestal.SetActive(true);
            TomatoPrompt.SetActive(true);
            foreach(GameObject banana in bananas) {
                banana.SetActive(false);
            }
            foreach(GameObject tomato in tomatoes) {
                tomato.SetActive(true);
            }
            Instantiate(tomato, TomatoPedestal.transform.position + new Vector3(0, 0.25f, 0), transform.rotation);
            BananaPrompt.SetActive(false);
            //BananaPedestal.SetActive(false);
            Jester.GetComponent<JesterMover>().hitByBanana = false;
        }
        if(!TomatoPrompt.activeInHierarchy && Jester.GetComponent<JesterMover>().shouldBeHitByTomato)
        {
            foreach(GameObject banana in bananas) {
                banana.SetActive(true);
            }
            foreach(GameObject tomato in tomatoes) {
                tomato.SetActive(false);
            }
            //BananaPedestal.SetActive(true);
            BananaPrompt.SetActive(true);
            TomatoPrompt.SetActive(false);
            Instantiate(banana, BananaPedestal.transform.position + new Vector3(0, 0.25f, 0), transform.rotation);
            //TomatoPedestal.SetActive(false);
            Jester.GetComponent<JesterMover>().hitByTomato = false;
        }
    }
}
