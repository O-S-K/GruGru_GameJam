using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
