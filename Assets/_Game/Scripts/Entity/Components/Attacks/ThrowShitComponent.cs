using UnityEngine;

public class ThrowShitComponent : MonoBehaviour, IAttack
{
    private BaseCharacter entity;
    public Projectile projectile;
    public Transform pointSpawn;

    public float speed = 5;
    public float timeDestroyed = 3;
    public float fireRate = 1;
    private float currentFireRate;

    private void Awake()
    {
        entity = GetComponent<BaseCharacter>();
    }


    private void Start()
    {
        currentFireRate = fireRate;
    }

    public void Attack(Entity target)
    {
        if(currentFireRate >= fireRate)
        {
            currentFireRate = 0;

            var bullet = Instantiate(projectile, pointSpawn.position, Quaternion.identity);
            Vector2 direction = (target.transform.position - transform.position).normalized;

            bullet.Init(this.entity, direction);
            bullet.transform.parent = null;
            bullet.GetRig().AddForce(direction * speed * 100);
            bullet.Destroyd(timeDestroyed);
        }
        else
        {
            currentFireRate += Time.deltaTime;
        }
       
    }
}
