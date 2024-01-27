using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalallaxBackground : MonoBehaviour
{
    public Vector2 targetStart;
    public Vector2 targetEnd;

    public float speed;

    public Transform[] contents;


    private void Update()
    {
        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i].transform.position.x <= targetEnd.x)
            {
                contents[i].transform.position = targetStart;
            }
        }
    }
}
