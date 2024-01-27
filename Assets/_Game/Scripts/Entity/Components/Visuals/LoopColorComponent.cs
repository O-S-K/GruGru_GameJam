using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopColorComponent : MonoBehaviour
{
    private IEnumerator Start()
    {
        var duck = GetComponent<SpriteRenderer>();
        while (true)
        {
            duck.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            duck.color = Color.magenta;
        }
    }
}
