using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMove : Interaction
{
    public Transform objectToMove;
    public Transform moveToPos;

    bool isMoving = false;

    public override void Activate()
    {
        isMoving = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            objectToMove.position = Vector3.Lerp(objectToMove.position, moveToPos.position, Time.deltaTime);
        }
    }
}
