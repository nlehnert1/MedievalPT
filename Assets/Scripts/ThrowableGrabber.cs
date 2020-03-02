using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ThrowableGrabber : MonoBehaviour
{
    private SteamVR_Action_Vector2 controllerInput = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("default", "grab");
    Hand attachedHand;
    private SteamVR_Input_Sources hand;
    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(interactable.attachedToHand.handType);
        //if(interactable.attachedToHand.handType == )
    }
}
