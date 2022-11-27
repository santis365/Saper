using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static int iloscBomb=10;
    public static int szerokosc=10;
    public static int wysokosc=10;
    public GameObject inputBomb;
    public GameObject inputSzerokosc;
    public GameObject inputWysokosc;
    
    public void SaveBomb()
    {
    
        int.TryParse(inputBomb.GetComponent<Text>().text, out int result);
        if (result == 0)
        {
            iloscBomb = 10;
        }
        else if(result>szerokosc*wysokosc)
        {
            iloscBomb = szerokosc * wysokosc;
        }
        else
            iloscBomb = result;
    }
    public void SaveSzerokosc()
    {
        int.TryParse(inputSzerokosc.GetComponent<Text>().text, out int result);
        if (result == 0)
        {
            szerokosc = 10;
            return;
        }
        if (result > 30)
        {
            szerokosc = 30;
            return;
        }
            
        if (result < 5)
        {
            szerokosc = 5;
            return;
        }
            szerokosc = result;
    }
    public void SaveWysokosc()
    {
        int.TryParse(inputWysokosc.GetComponent<Text>().text, out int result);
        if(result==0)
        {
            wysokosc = 10;
            return;
        }
        if (result > 30)
        {
            wysokosc = 30;
            return;
        }
            
        if (result < 5)
        {
            wysokosc = 5;
            return;
        }
            wysokosc = result;
    }
}
