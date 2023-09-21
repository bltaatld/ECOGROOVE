using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEffectManager : MonoBehaviour
{
    public Light2D globalLight;
    private float originalLightIntensity;
    public bool isLightNoteHit;
    public bool isTimerRunning = false;
    public float timer = 0.0f;
    public float timerInterval = 10.0f; // 10�ʸ��� ����
    public float backTimerInterval = 3f; // 10�ʸ��� ����

    private void Start()
    {
        originalLightIntensity = globalLight.intensity;
        StartCoroutine(LightIntensityDecreaseCoroutine());
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;

            if (timer <= backTimerInterval)
            {
                globalLight.intensity += 0.1f;

                if (globalLight.intensity >= originalLightIntensity)
                {
                    timer = 0.0f;
                    isLightNoteHit = false;
                    isTimerRunning = false;
                    StartCoroutine(LightIntensityDecreaseCoroutine());
                }
            }
        }

        if (isLightNoteHit)
        {
            isTimerRunning = true;
        }
    }

    private IEnumerator LightIntensityDecreaseCoroutine()
    {
        while (true)
        {
            // 10�� ���� globalLight�� intensity�� 0.1���� ���ҽ�Ŵ
            float elapsedTime = 0.0f;
            float startIntensity = globalLight.intensity;
            float targetIntensity = 0.03f;

            while (elapsedTime < timerInterval)
            {
                float t = elapsedTime / timerInterval;
                globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(timerInterval);
        }
    }
}