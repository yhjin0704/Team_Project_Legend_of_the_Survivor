using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Target;

    float OffsetX;
    float OffsetY;

    //public float minX, maxX, minY, maxY;

    void Start()
    {
        if (Target == null)
            return;

        OffsetX = transform.position.x - Target.position.x;
        OffsetY = transform.position.y - Target.position.y;
    }

    void Update()
    {
        if (Target == null)
            return;

        Vector3 pos = transform.position;

        pos.x = Target.position.x + OffsetX;
        pos.y = Target.position.y + OffsetY;

        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
