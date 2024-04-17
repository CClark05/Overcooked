using System;
using UnityEngine;
using UnityEngine.U2D;
using PixelPerfectCamera = UnityEngine.Rendering.Universal.PixelPerfectCamera;

public class CameraMovement : MonoBehaviour
{
    private PixelPerfectCamera pixelPerfectCam;
    private Camera cam;
    private float originalSize;

    private void Awake()
    {
        pixelPerfectCam = GetComponent<PixelPerfectCamera>();
        cam = GetComponent<Camera>();
        originalSize = GetComponent<Camera>().orthographicSize;
    }

    private void Start()
    {
        SetSize(4.8f);
    }
    

    public void ZoomOut(float time)
    {
        pixelPerfectCam.enabled = false;
        LeanTween.value(gameObject, f => GetComponent<Camera>().orthographicSize = f, cam.orthographicSize, originalSize, time).setOnComplete(() => pixelPerfectCam.enabled = true).setEaseOutQuart();
    }

    public void ZoomIn(float time, float amount)
    {
        pixelPerfectCam.enabled = false;
        LeanTween.value(gameObject, f => GetComponent<Camera>().orthographicSize = f, originalSize, amount, time).setEaseInSine();
    }
    public void SetSize(float size)
    {
        pixelPerfectCam.enabled = false;
        cam.orthographicSize = size;
    }
}