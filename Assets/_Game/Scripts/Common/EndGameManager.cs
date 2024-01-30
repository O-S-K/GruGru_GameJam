using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public Duck duck;
    public Baby baby;

    public Transform dady;
    public Transform momy;


    public Transform pointMoveEnd;
    public Transform pointBabyMoveEnd;

    public GameObject shit;

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    public GameObject sound1;
    public GameObject sound2;
    public GameObject soundNemShit;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }


    private void Start()
    {
        sound1.SetActive(true);
        text1.gameObject.SetActive(true);
        duck.Fly();
        duck.transform.parent.DOMove(pointMoveEnd.position, 6).SetEase(Ease.Linear).OnComplete(() =>
        {
            duck.Idle();
            baby.transform.parent = null;

            DOVirtual.DelayedCall(2, () =>
            {
                baby.transform.position = new Vector3(3f, -3.76f, 40.5f);
                DOVirtual.DelayedCall(2, () =>
                {
                    // attack
                    text1.gameObject.SetActive(false);
                    text2.gameObject.SetActive(true);

                    soundNemShit.SetActive(true);
                    shit.transform.position = baby.transform.position;
                    shit.transform.DOMove(new Vector3(15f, 0, 0), 2.5f).SetUpdate(UpdateType.Fixed);

                    dady.transform.DOMove(new Vector3(15f, 0, 0), 1f).SetUpdate(UpdateType.Fixed).SetDelay(0.12f);
                    momy.transform.DOMove(new Vector3(15f, 0, 0), 1f).SetUpdate(UpdateType.Fixed).SetDelay(0.125f);

                    baby.Attack();

                    DOVirtual.DelayedCall(2, () =>
                    {
                        sound1.SetActive(false);
                        soundNemShit.SetActive(false);
                        sound2.SetActive(true);


                        baby.transform.parent = duck.transform.parent;
                        baby.Idle();
                        baby.transform.localPosition = new Vector3(-0.171f, 0.149f, 0);

                        DOVirtual.DelayedCall(0.5f, () =>
                        {
                            text1.gameObject.SetActive(false);

                            DOVirtual.DelayedCall(1.5f, () =>
                            {
                                text2.gameObject.SetActive(false);
                            });

                            duck.Fly();
                            duck.transform.parent.transform.DOMove(new Vector3(15, 0, 0), 3).SetEase(Ease.Linear);
                            FindObjectOfType<FadeUI>().FadeIn(1, 2, () =>
                            {
                                text3.gameObject.SetActive(true);
                            });
                        });
                    });
                });
            });
        });
    }
}
