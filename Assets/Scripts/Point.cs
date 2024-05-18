using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point
{
    public Vector3 Position;
    public Quaternion Rotation;
    public float MoveSpeed;

    public Point(Vector3 position, Quaternion rotation, float moveSpeed)
    {
        Position = position;
        Rotation = rotation;
        MoveSpeed = moveSpeed;
    }
}
