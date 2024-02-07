using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RedFlash : MonoBehaviour
{
    [SerializeField] private Volume volume;
    public static RedFlash i;
    private void Awake()
    {
        i = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Flash();
        }
    }
    public void Flash()
    {
        float endingValue = 0.35f;
        float startTime = 0.2f;
        float endTime = 0.15f;
        float waitTime = 0.15f;
        ToggleVignette(true);
        LeanTween.value(gameObject, ModifyIntensity, 0, endingValue, startTime).setOnComplete(() =>
        {
            FunctionTimer.Create(() => LeanTween.value(gameObject, ModifyIntensity, endingValue, 0, endTime).setOnComplete(() => ToggleVignette(false)), waitTime);
            
        });

        void ModifyIntensity(float intensity)
        {
            ModifyVignetteIntensity(intensity);
        }

    }
    private void ToggleVignette(bool enable)
    {
        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            if (enable)
            {
                vignette.active = true;
                return;
            }
            vignette.active = false;
        }
    }
    private void ModifyVignetteIntensity(float intensity)
    {
        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            vignette.intensity.value = intensity;
        }
    }
}
