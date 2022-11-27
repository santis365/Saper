using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRenderer : MonoBehaviour
{
    public Text timer;

    // Start is called before the first frame update
    void Start()
    {
        if(KafelekController.iloscPozostalychDoOdkrycia==0)
        {
            timer.text = "Gratulację!      Twój czas to:" + GameManager.sekundy;
        }
        if (KafelekController.bombClicked)
        {
            timer.text = "BUM!!!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
