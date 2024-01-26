using TMPro;
using UnityEngine;

public class HealthBarShrinkTransform : MonoBehaviour
{
    public bool isShrinkFake = true;
    public float damageShrinkTimer = .6f;
    public float shrinkSpeed = 0.1f;

    public bool isPlayer = false;
    public bool isHideBar = true;

    public TextMeshPro textStats;
    //private Transform _barLv;


    private HealthSystem _health;
    [SerializeField] private Transform _bar;
    [SerializeField] private Transform _damagedBar;
    [SerializeField] private Transform _pivotBar;

    private float _xScale;
    private float _damagedHealthShrinkTimer;

    private bool _isShowBar;
    private float _timeCheckHideBar;


    private void Start()
    {
        _xScale = 1;
        _timeCheckHideBar = 5;

        if (isHideBar)
        {
            _isShowBar = false;
            HideBar();
        }

        _damagedBar.localScale = Vector3.one;
        _bar.localScale = Vector3.one;

        //if (isPlayer)
        //{
        //    textStats.text = ((int)_health.CurrentHealth).ToString();
        //}
        //else
        //{
        //    if (transform.parent.GetComponent<EnemyController>() != null)
        //    {
        //        textStats.text = transform.parent.GetComponent<EnemyController>().level.ToString();
        //    }
        //}
    }

    //#if UNITY_EDITOR
    //        private void OnValidate()
    //        {
    //            if (transform.parent.GetComponent<EnemyController>() != null)
    //            {
    //                var e = transform.parent.GetComponent<EnemyController>();
    //                if (e.TypeEnemy == ETypeEnemy.Creep)
    //                {
    //                    transform.localScale = new Vector3(1.5f, 0.25f, 1);
    //                }
    //                else if (e.TypeEnemy == ETypeEnemy.MiniBoss)
    //                {
    //                    transform.localScale = new Vector3(1.75f, 0.26f, 1);
    //                }
    //                else
    //                {
    //                    transform.localScale = new Vector3(2f, 0.275f, 1);
    //                }
    //            }
    //            else
    //            {
    //                transform.localScale = new Vector3(1.75f, 0.25f, 1);
    //            }
    //        }
    //#endif


    private void Update()
    {
        _damagedHealthShrinkTimer -= Time.deltaTime;
        if (_damagedHealthShrinkTimer < 0)
        {
            if (_bar.localScale.x < _damagedBar.localScale.x)
            {
                _xScale = Mathf.Lerp(_damagedBar.localScale.x, _bar.localScale.x, shrinkSpeed * Time.deltaTime);
                _damagedBar.localScale = new Vector3(_xScale, 1, 1);
            }
        }

        if (isHideBar)
        {
            if (_isShowBar)
            {
                _timeCheckHideBar -= Time.deltaTime;
                if (_timeCheckHideBar < 0)
                {
                    _timeCheckHideBar = 5;
                    _isShowBar = false;
                    HideBar();
                }
                ShowBar();
            }
            else
            {
                HideBar();
            }
        }
    }

    public void Initialize(HealthSystem health)
    {
        _health = health;
        _health.OnHit.AddListener(OnDamaged);
        _health.OnHeal.AddListener(OnHealed);
    }

    protected void OnHealed(int hp)
    {
        _isShowBar = true;
        _timeCheckHideBar = 5;
        UpdateHeathBar(_health.GetHealthPercent());
        _damagedBar.localScale = new Vector3(_bar.localScale.x, 1, 1);
    }

    protected void OnDamaged(Vector2 dir, int damage, DamagePopup.ETypeDamage typeDamage)
    {
        _isShowBar = true;
        _timeCheckHideBar = 5;
        _damagedHealthShrinkTimer = damageShrinkTimer;
        UpdateHeathBar(_health.GetHealthPercent());
    }

    private void UpdateHeathBar(float healthNormalized)
    {
        _bar.localScale = new Vector3(healthNormalized, 1, 1);
        if (isPlayer) textStats.text = ((int)_health.CurrentHealth).ToString();
    }

    public void ShowBar()
    {
        if (!_pivotBar.gameObject.activeInHierarchy)
        {
            _pivotBar.gameObject.SetActive(true);
        }
    }

    public void HideBar()
    {
        if (_pivotBar.gameObject.activeInHierarchy)
        {
            _pivotBar.gameObject.SetActive(false);
        }
    }
}
