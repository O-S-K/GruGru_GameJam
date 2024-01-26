using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private float healthChangeDelay = .5f;
    private float _timeSinceLastChange = float.MaxValue;

    public HealthBarShrinkTransform HealthDisplay;

    [SerializeField] private UnityEvent<Vector2, int, DamagePopup.ETypeDamage> onHit;
    [SerializeField] private UnityEvent<int> onHeal;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent onInvincibilityEnd;


    public UnityEvent<Vector2, int, DamagePopup.ETypeDamage> OnHit => onHit;
    public UnityEvent<int> OnHeal => onHeal;
    public UnityEvent OnDeath => onDeath;
    public UnityEvent OnInvincibilityEnd => onInvincibilityEnd;


    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDie { get; set; }
    public bool IsBlockDamage { get; set; }


    public void Initialize(int value)
    {
        MaxHealth = value;
        CurrentHealth = MaxHealth;
        IsDie = false;
        IsBlockDamage = false;
        HealthDisplay.Initialize(this);
    }

    public void SetMaxHeath(int value)
    {
        MaxHealth = value;
    }

    private void Update()
    {
        if (IsDie && IsBlockDamage) return;

        if (_timeSinceLastChange < healthChangeDelay)
        {
            _timeSinceLastChange += Time.deltaTime;
            if (_timeSinceLastChange >= healthChangeDelay)
            {
                onInvincibilityEnd.Invoke();
            }
        }
    }

    public void SetBlockDamage(bool isSet, float timeBlock)
    {
        IsBlockDamage = isSet;
        Invoke(nameof(DisableBlockDamage), timeBlock);
    }

    private void DisableBlockDamage()
    {
        IsBlockDamage = false;
    }

    public float GetHealthPercent()
    {
        return (float)CurrentHealth / MaxHealth;
    }

    public bool TakeDamage(Transform target, float value)
    {
        if (IsDie || IsBlockDamage || value == 0 || _timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        _timeSinceLastChange = 0f;
        CurrentHealth += value;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;


        if (value > 0)
        {
            OnHeal?.Invoke((int)value);
        }
        else
        {
            var dir = (target.position - transform.position).normalized;
            OnHit?.Invoke(dir, (int)value, DamagePopup.ETypeDamage.Nomal);
        }

        if (CurrentHealth <= 0.01f)
        {
            Death();
        }

        return true;
    }

    public virtual void Death()
    {
        if (IsDie) return;
        IsDie = true;
        IsBlockDamage = true;
        onDeath.Invoke();
        HealthDisplay.gameObject.SetActive(false);
    }

    public void Hit(bool was)
    {

    }

    public bool WasHit()
    {
        return false;
    }
}