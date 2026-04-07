using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bHelper : MonoBehaviour
{
    public static Vector3 GetDir(Vector3 v1, Vector3 v2)
    {
        float x = v2.x - v1.x;
        float y = v2.y - v1.y;
        float z = v2.z - v1.z;
        
        return new Vector3(x, y, z); 
    }

    public static Vector3 GetNormalizedDir(Vector3 v1, Vector3 v2)
    {
        float x = v2.x - v1.x;
        float y = v2.y - v1.y;
        float z = v2.z - v1.z;
        Vector3 dir = new Vector3(x, y, z);
        
        return dir.normalized;
    }

    public static bool AtPositionInRange(Transform target, Vector3 position, float range)
    {
        bool inXRange, inYRange, inZRange;

        inXRange = target.transform.position.x >= position.x - range &&
                   target.transform.position.x <= position.x + range;
        
        inYRange = target.transform.position.y >= position.y - range &&
                   target.transform.position.y <= position.y + range;

        inZRange = target.transform.position.z >= position.z - range &&
                   target.transform.position.z <= position.z + range;

        return (inXRange && inYRange && inZRange);
    }
}
