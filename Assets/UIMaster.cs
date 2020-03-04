using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public static void ChangeDiffiulty(int maxDist) {
        int newXOffset = 0;
        int newZOffset = 7;
        int newZMax = 0;
        switch (maxDist) {
            case 0:
                newZMax = 4;
                newZOffset = 2;
                newXOffset = 7;
                break;
            case 1:
                newZMax = 10;
                newZOffset = 5;
                newXOffset = 5;
                break;
            case 2:
                newZMax = 14;
                newZOffset = 7;
                newXOffset = 2;
                break;
            default:
                newZOffset = 0;
                newXOffset = 7;
                break;
        }
        JesterMover.zMax = newZMax;
        JesterMover.xOffset = newXOffset;
        JesterMover.zOffset = newZOffset;
    }
}
