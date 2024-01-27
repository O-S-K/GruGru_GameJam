using UnityEngine;

public class Parallax2D : MonoBehaviour
{
    public Camera cam;
    public float parallaxEffect;
    private float length;
    private float startPos;

    private void Awake()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + (length / 2))
        {
            startPos += length;
        }
        else if (temp < startPos - (length / 2))
        {
            startPos -= length;
        }
    }
}
