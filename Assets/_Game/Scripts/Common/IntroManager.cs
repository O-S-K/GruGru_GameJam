using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Duck duck;
    public Baby baby;

    public SpriteRenderer tile;

    private void Start()
    {
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {

        tile.DOFade(0, 0);
        tile.gameObject.SetActive(false);


        yield return new WaitForSeconds(1);
        baby.gameObject.SetActive(false);
        duck.transform.parent.transform.position = Vector3.zero;
        duck.Sprite().flipX = true;
        duck.Fly();


        DOVirtual.DelayedCall(3, () =>
        {
            tile.gameObject.SetActive(true);
            tile.DOFade(1, 1f);
        });


        duck.transform.parent.transform.DOMove(baby.transform.position, 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            duck.Sprite().flipX = false;
            baby.gameObject.SetActive(true);
            baby.transform.parent = duck.transform.parent;
            baby.transform.localPosition = new Vector3(-0.165f, 0.167f, 0);

            DOVirtual.DelayedCall(2, () =>
            {  
                duck.transform.parent.transform.DOMove(Vector3.right * 10, 6).SetEase(Ease.Linear);
                FindObjectOfType<FadeUI>().FadeIn(1, 4, () =>
                {
                    SceneManager.LoadSceneAsync(1);
                });
            });
        });
    }
}
