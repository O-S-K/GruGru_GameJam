/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;
using System.Collections.Generic;

public class BloodParticleSystemHandler : MonoBehaviour
{
    public static BloodParticleSystemHandler Instance { get; private set; }

    [SerializeField] private LayerMask hitLayerMask;

    private MeshParticleSystem meshParticleSystem;
    private List<Single> singleList;

    private void Awake()
    {
        Instance = this;
        meshParticleSystem = GetComponent<MeshParticleSystem>();
        singleList = new List<Single>();
    }

    private void Update()
    {
        for (int i = 0; i < singleList.Count; i++)
        {
            Single single = singleList[i];
            single.Update();
            if (single.IsParticleComplete())
            {
                singleList.RemoveAt(i);
                i--;
            }
        }
    }

    public void SpawnBlood(int bloodParticleCount, Vector3 position, Vector3 direction)
    {
        for (int i = 0; i < bloodParticleCount; i++)
        {
            singleList.Add(new Single(position, ApplyRotationToVector(direction, Random.Range(-180, 180)), meshParticleSystem, hitLayerMask));
        }
    }


    public Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

     
    private class Single
    {

        private MeshParticleSystem meshParticleSystem;
        private LayerMask hitLayerMask;
        private Vector3 position;
        private Vector3 direction;
        private int quadIndex;
        private Vector3 quadSize;
        private float moveSpeed;
        private float rotation;
        private int uvIndex;

        public Single(Vector3 position, Vector3 direction, MeshParticleSystem meshParticleSystem, LayerMask hitLayerMask)
        {
            this.position = position;
            this.direction = direction;
            this.meshParticleSystem = meshParticleSystem;
            this.hitLayerMask = hitLayerMask;

            quadSize = new Vector3(Random.Range(0.4f, 0.6f), Random.Range(0.6f, 0.8f));
            rotation = Random.Range(0, 360f);
            moveSpeed = Random.Range(0f, 50f);
            uvIndex = Random.Range(0, 8); 
            quadIndex = meshParticleSystem.AddQuad(position, rotation, quadSize, false, uvIndex);
        }

        public void Update()
        {
            float colliderSize = 1f;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(position + direction * colliderSize, direction, moveSpeed * Time.deltaTime, hitLayerMask);
            if (raycastHit2D.collider != null)
            {
                // Hit something, stop!
                moveSpeed = 0f;
                return;
            }

            position += direction * moveSpeed * Time.deltaTime;
            rotation += 360f * (moveSpeed / 10f) * Time.deltaTime;

            meshParticleSystem.UpdateQuad(quadIndex, position, rotation, quadSize, false, uvIndex);

            float slowDownFactor = Random.Range(15, 20);
            moveSpeed -= moveSpeed * slowDownFactor * Time.deltaTime; 
        }

        public bool IsParticleComplete()
        {
            return moveSpeed < 0.1f;
        }

    }

}
