using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KafelekController : MonoBehaviour
{


    private KafelekState state = KafelekState.Zakryty;
    public KafelekModel model;
    public float szybkoscKrecenia;
    public static bool bombClicked = false;
    public static int iloscDoOdkrycia;
    public static int iloscPozostalychDoOdkrycia;
    public GameObject restart = null;


    public enum KafelekState
    {
        Zakryty,
        Odkryty,
        Flaga,
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch (state)
            {
                case KafelekState.Zakryty:
                    {
                        Vector3 kierunek = new Vector3(0, -1, 0);
                        transform.Rotate(kierunek, szybkoscKrecenia);
                        state = KafelekState.Flaga;
                        break;
                    }
                case KafelekState.Odkryty:
                    {
                        break;
                    }
                case KafelekState.Flaga:
                    {
                        Vector3 kierunek = new Vector3(0, 1, 0);
                        transform.Rotate(kierunek, szybkoscKrecenia);
                        state = KafelekState.Zakryty;
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            switch (state)
            {
                case KafelekState.Zakryty:
                    {
                        Uncover();
                        if (model.isBomb)
                        {
                            iloscPozostalychDoOdkrycia++;
                            GameManager.polozenieBomby = transform.position;
                            bombClicked = true;
                            BoxCollider collider;
                            //Vector3 kierunekWybuchu = new Vector3(1000, 1000, 1000);
                            // this.GetComponent<Rigidbody>().AddForceAtPosition(kierunekWybuchu, GameManager.polozenieBomby);
                            collider = this.GetComponent<BoxCollider>();
                            collider.size = new Vector3(3, 3, 1);
                            Debug.Log("BOMBA!!");
                            CameraController.startShaking = true;
                            StartCoroutine(ExecuteAfterTime(5));

                            break;
                        }
                        if(model.liczbaBombObok==0)
                        {
                            UncoverNeighbours();
                            break;
                        }
                        break;
                    }

                case KafelekState.Odkryty:
                    {
                        break;
                    }
                case KafelekState.Flaga:
                    {

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
    private void Uncover()
    { 
        state = KafelekState.Odkryty;
        Vector3 kierunek = new Vector3(0, 1, 0);
        transform.Rotate(kierunek, szybkoscKrecenia);
        iloscPozostalychDoOdkrycia--;
        Debug.Log(iloscPozostalychDoOdkrycia);
        if (iloscPozostalychDoOdkrycia==0)
        {
            Debug.Log("Wygrales!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
       
    }
    public void TryToUncover()
    {
        if (state == KafelekState.Zakryty)
        {
            Uncover();
            if(model.liczbaBombObok==0)
            {
                UncoverNeighbours();
            }      
        }
    }
    private void UncoverNeighbours()
    {
        int x = (int)model.wspolrzedne.x;
        int y = (int)model.wspolrzedne.y;
        if (x - 1 > 0)
            GameManager.mapa[x - 1, y].controller.TryToUncover(); 
        if (x - 1 > 0 && y - 1 > 0)
            GameManager.mapa[x - 1, y - 1].controller.TryToUncover();
        if (y - 1 > 0)
            GameManager.mapa[x, y - 1].controller.TryToUncover();
        if (x + 1 <= GameManager.szerokosc && y - 1 > 0)
            GameManager.mapa[x + 1, y - 1].controller.TryToUncover();
        if (x + 1 <= GameManager.szerokosc)
            GameManager.mapa[x + 1, y].controller.TryToUncover();
        if (x + 1 <=GameManager.szerokosc && y + 1 <= GameManager.wysokosc)
            GameManager.mapa[x + 1, y + 1].controller.TryToUncover();
        if (y + 1 <= GameManager.wysokosc)
            GameManager.mapa[x, y + 1].controller.TryToUncover();
        if (x - 1 > 0 && y + 1 <=GameManager.wysokosc)
            GameManager.mapa[x - 1, y + 1].controller.TryToUncover();
    }



    // Start is called before the first frame update
    void Start()
    {
        bombClicked = false;
        iloscDoOdkrycia = GameManager.szerokosc * GameManager.wysokosc - GameManager.iloscBomb;
        iloscPozostalychDoOdkrycia = iloscDoOdkrycia;
        szybkoscKrecenia = 90f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
