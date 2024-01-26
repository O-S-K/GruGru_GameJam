using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateSortingLayerSprite : MonoBehaviour
{
    [SerializeField] private int sortingOrderBase = 10;
    [SerializeField] private bool runOnlyOnce = true;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y);
            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
