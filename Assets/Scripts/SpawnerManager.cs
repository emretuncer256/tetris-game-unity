using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] ShapeManager[] allShapes;


    public ShapeManager GenerateShape()
    {
        int randomIndex = Random.Range(0, allShapes.Length);
        ShapeManager shape = Instantiate(
            allShapes[randomIndex],
            transform.position,
            Quaternion.identity
        ) as ShapeManager;

        shape.transform.parent = this.transform;

        return (shape != null) ? shape : null;
    }
}