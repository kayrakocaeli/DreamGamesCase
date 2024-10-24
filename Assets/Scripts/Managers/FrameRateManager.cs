using System;
using System.Collections;
using System.Threading;
using UnityEngine;
public class FrameRateManager : MonoBehaviour
{
    [SerializeField] private int maxRate = 9999;
    [SerializeField] private float targetFrameRate = 60;
    [SerializeField] private float currentFrameTime;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = maxRate;
        currentFrameTime= Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
    }

    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / targetFrameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            if (sleepTime > 0)
            {
                Thread.Sleep((int)(sleepTime*1000));
            }

            while (t<currentFrameTime)
            {
                t = Time.realtimeSinceStartup;
            }
        }

        
    }
}
