using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 pozycja;
    public GameObject kamera;
    float odleglosc;
    public AnimationCurve curve;
    public static bool startShaking = false;
    public void MoveCamera()
    {
        if(Options.szerokosc==0 &Options.wysokosc==0)
        {
            SetCamera(10,10);
        }
        else if(Options.szerokosc == 0)
        {
            SetCamera(10, Options.wysokosc);       
        }
        else if(Options.wysokosc == 0)
        {
            SetCamera(Options.szerokosc, 10);
        }
        else
        { 
            SetCamera(Options.szerokosc, Options.wysokosc);
        }
        
    }
    public void SetCamera(float szerokosc, float wysokosc)
    {
        float odleglosc;
        odleglosc = -(szerokosc * 0.8f+5 + 2.5f * wysokosc+5);
        pozycja = new Vector3((szerokosc / 2) + 0.5f, (wysokosc / 2) + 0.5f, odleglosc);
        kamera.transform.position = pozycja;
    }
    IEnumerator Shake()
    {

        float duration = 1f;
        Vector3 startPostion=transform.position;
        float elapsedTime = 0f;
        startShaking = false;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = startPostion + Random.insideUnitSphere * strength;
            yield return null;
        }
    }
    void Start()
    {
        MoveCamera();
    }
    void Update()
    {
        if (startShaking==true)
        {
            StartCoroutine(Shake());
        }
     
    }
}
