using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PathObjectsParent : MonoBehaviour
{
    public PathPoints[] commonPathPoints;
    public PathPoints[] redPathPoints;
    public PathPoints[] greenPathPoints;
    public PathPoints[] bluePathPoints;
    public PathPoints[] yellowPathPoints;

    [Header("Scale And Position difference")]
    public float[] scales;
    public float[] positionsDifference; 

}
