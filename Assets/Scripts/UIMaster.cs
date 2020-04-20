using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public JesterMover jester;

    public void ChangeDiffiulty(int maxDist) {
        //Debug.Log(maxDist);
        float newXOffset = 0;
        float newZOffset = 7;
        float newZMax = 0;
        switch (maxDist) {
            case 0:
                newZMax = 2;
                newZOffset = 3.5f;
                newXOffset = 1;
                break;
            case 1:
                newZMax = 5;
                newZOffset = 2.5f;
                newXOffset = 2.5f;
                break;
            case 2:
                newZMax = 7;
                newZOffset = 1;
                newXOffset = 3.5f;
                break;
        }
        jester.zMax = newZMax;
        jester.xOffset = newXOffset;
        jester.zOffset = newZOffset;
    }
}
