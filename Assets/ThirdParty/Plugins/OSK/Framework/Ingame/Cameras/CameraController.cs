using DG.Tweening;
using UnityEngine;


public class CameraController : OSK.SingletonMono<CameraController>
{

    /* ------------------------ Inspector Assigned Fields ----------------------- */

    public Camera cameraMain;
    private Tweener camShake;

    /* ------------------------------ Unity Events ------------------------------ */


    public void ShakeCamera(float dur, float stre, int va, Ease easeStart, float timeOut, Ease easeOut)
    {
        if (camShake != null) camShake.Kill();
        camShake = cameraMain.transform.DOShakePosition(dur, stre, va)
            .SetEase(easeStart).OnComplete(() =>
            {
                cameraMain.transform.DOLocalMove(Vector3.zero, timeOut).SetEase(easeOut);
            });
    }
}
