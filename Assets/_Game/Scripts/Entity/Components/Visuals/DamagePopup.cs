using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public enum ETypeDamage
    {
        None,
        Nomal,
        CriticalHit,
        BuffHeal,
        UseStamina
    }
    public TextMeshPro textMesh;

    private float disappearTimerMax = 0.25f;
    private float disappearTimer;
    private int sortingOrder;


    float increaseScaleAmount = 1f;
    float decreaseScaleAmount = 1f;

    private Color textColor;
    private Vector3 moveVector;

    public void Setup(int damageAmount, ETypeDamage typeDamage)
    {
        string value = damageAmount > 0 ? $"+{damageAmount}" : $"{damageAmount}";
        textMesh.SetText(value);

        switch (typeDamage)
        {
            case ETypeDamage.Nomal:
                textMesh.fontSize = 25;
                textColor = Color.red;
                break;
            case ETypeDamage.CriticalHit:
                textMesh.fontSize = 30;
                textColor = Color.yellow;
                break;
            case ETypeDamage.BuffHeal:
                textMesh.fontSize = 27;
                textColor = Color.green;
                break;
            case ETypeDamage.UseStamina:
                textMesh.fontSize = 27;
                textColor = Color.green;
                break;
        }

        textMesh.color = textColor;
        disappearTimer = disappearTimerMax;

        increaseScaleAmount = 1f;
        decreaseScaleAmount = 1f;

        sortingOrder++;
        textMesh.sortingOrder = 1000 + sortingOrder;
        moveVector = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1));
        transform.localScale = Vector3.one;
    }

    public void Setup(string text, Color color, float size = 25, float timeShow = 0)
    {
        textMesh.SetText(text);
        textMesh.fontSize = size;

        textMesh.color = color;
        disappearTimer = disappearTimerMax + timeShow;

        sortingOrder++;
        textMesh.sortingOrder = 1000 + sortingOrder;
        moveVector = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0.5f, 0.7f));
        transform.localScale = Vector3.one;

        increaseScaleAmount = 0.35f;
        decreaseScaleAmount = 0.5f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * Random.Range(-15, 15f) * Time.deltaTime;

        if (disappearTimer > disappearTimerMax * .5f)
        {
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // Start disappearing
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0.1)
            {
                OSK.PoolManager.Instance.Despawn(this);
            }
        }
    }
}

