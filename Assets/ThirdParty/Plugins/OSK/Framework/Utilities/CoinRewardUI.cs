using DG.Tweening;
using UnityEngine;

public class CoinRewardUI : MonoBehaviour
{
    [SerializeField] private Transform pileOfCoins;
    [SerializeField] private Transform iconCoinEar;

    private void Update()
    {
        for (int i = 1; i  < transform.childCount; i++)
        {
            transform.GetChild(i).localScale = Vector3.one;
        }
    }

    public void DropCoin(int coinsAmount)
    {
        var groupCoins = new GameObject();
        groupCoins.transform.parent = pileOfCoins;
        groupCoins.transform.localPosition = Vector3.zero;
        groupCoins.AddComponent<RectTransform>();
        //groupCoins.GetComponent<RectTransform>().localScale = Vector3.zero;

        var delay = 0f;
        for (int i = 0; i < coinsAmount; i++)
        {
            var coinClone = Instantiate(pileOfCoins.transform.GetChild(0));
            coinClone.parent = groupCoins.transform;
            var xRand = pileOfCoins.position.x + Random.Range(200, -200);
            var yRand = pileOfCoins.position.y + Random.Range(-200, -500);

            coinClone.position = new Vector2(xRand, yRand);
            coinClone.gameObject.SetActive(true);
        }

        // type move 1
        for (int i = 1; i < groupCoins.transform.childCount; i++)
        {
            var gc = groupCoins.transform.GetChild(i);
            gc.DOScale(1f, 0.3f).SetDelay(delay + 0.125f).SetEase(Ease.OutBack);
            gc.GetComponent<RectTransform>().DOAnchorPos(iconCoinEar.localPosition, 0.5f).SetDelay(delay + 0.25f).SetEase(Ease.InBack);
            gc.DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.25f).SetEase(Ease.Flash);
            //gc.DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);
            delay += 0.025f;
        }

        // type move 2
        //for (int i = 1; i < groupCoins.transform.childCount; i++)
        //{
        //    var gc = groupCoins.transform.GetChild(i);
        //    gc.DOScale(1f, 0.1f).SetEase(Ease.OutBack);
        //    gc.DORotate(Vector3.zero, 0.25f).SetEase(Ease.Flash);
        //    gc.GetComponent<RectTransform>().DOJumpAnchorPos(iconCoinEar.localPosition, 200, 1, 1f);
        //}
        Destroy(groupCoins, 2f);
    }
}
