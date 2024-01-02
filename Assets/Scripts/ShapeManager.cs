using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private bool canRotate = true;

    #region Movements

    public void MoveLeft()
    {
        transform.Translate(Vector3.left, Space.World);
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right, Space.World);
    }

    public void MoveDown()
    {
        transform.Translate(Vector3.down, Space.World);
    }

    public void MoveUp()
    {
        transform.Translate(Vector3.up, Space.World);
    }

    public void TurnRight()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    public void TurnLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }

    #endregion

    public void RotateClockwise(bool clockwise)
    {
        if (clockwise) TurnRight();
        else TurnLeft();
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            MoveDown();
            yield return new WaitForSeconds(0.25f);
        }
    }
}