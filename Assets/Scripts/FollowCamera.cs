using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Target;

    float OffsetX;
    float OffsetY;

    //public float minX, maxX, minY, maxY;

    void Awake()
    {
        if (Target == null)
            return;
    }

    void Update()
    {
        if (Target == null)
            return;

        Vector3 pos = transform.position;

        pos.x = Target.position.x;
        pos.y = Target.position.y;

        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }

    public void SetTarget()
    {
        Target = GameManager.Instance.PlayerGameObject.transform;
    }
}
