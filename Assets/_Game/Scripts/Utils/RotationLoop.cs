using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLoop : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.forward * 1000 * Time.deltaTime);
    }
}
