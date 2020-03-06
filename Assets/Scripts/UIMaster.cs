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
                newZMax = 4;
                newZOffset = 7;
                newXOffset = 2;
                break;
            case 1:
                newZMax = 10;
                newZOffset = 5;
                newXOffset = 5;
                break;
            case 2:
                newZMax = 14;
                newZOffset = 2;
                newXOffset = 7;
                break;
        }
        jester.zMax = newZMax;
        jester.xOffset = newXOffset;
        jester.zOffset = newZOffset;
    }
}
