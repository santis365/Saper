
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// ctrl+t - wyszukiwanie po polach, klasach
// ctrl+r, r lub F2 - [r]ename
// ctrl+l - wycina linijkę
// ctrl+k, d, formatuje (w sensie ładnie układa, nie usuwa xd) dokument, w którym się obecnie jest
// ctrl+k, c - [c]omment, komentarz tego, co zaznaczone
// ctrl+k, u - [u]ncomment, odkomentowywuje to, co zaznaczone
// ctrl+lewy przycisk myszki na jakimś polu - przenosi do miejsca deklaracji
// prawy przycisk myszki na polu/klasie/metodzie/czymśtam - Find All References
// ctrl+f - [f]ind, ale nie obiekt czy klasę, tylko słowo czy zdanie czy coś takiego. no i tam jest też replace

public class GameManager : MonoBehaviour
{
    public GameObject bomba = null;
    public GameObject kafelek0 = null;
    public GameObject kafelek1 = null;
    public GameObject kafelek2 = null;
    public GameObject kafelek3 = null;
    public GameObject kafelek4 = null;
    public GameObject kafelek5 = null;
    public GameObject kafelek6 = null;
    public GameObject kafelek7 = null;
    public GameObject kafelek8 = null;
    public GameObject kafelekGlowny = null;
    public static int szerokosc;
    public static int wysokosc;
    public static int iloscBomb;
    public Vector3 wsp;
    public List<Vector3> polozenieBomb = new List<Vector3>();
    public static Vector3 polozenieBomby;
    static public KafelekModel[,] mapa;
    public float startTime;
    public Text timerText;
    public static string sekundy;
    private Rigidbody body;

    public void BombGenerator(int iloscBomb, int szerokosc ,int wysokosc)
    {
        bool bombGenerated;
        Vector3 bomb = new Vector3();
        for (int i = 0; i < iloscBomb; i++)
        {
            bomb.x = Random.Range(1, szerokosc + 1);
            bomb.y = Random.Range(1, wysokosc + 1);
            do
            {
                bomb = new Vector3(Random.Range(1, szerokosc + 1), Random.Range(1, wysokosc + 1), 0);
                bombGenerated = polozenieBomb.Exists(w => w.Equals(bomb));
            } while (bombGenerated);
            polozenieBomb.Add(bomb);
        }
    }
    public void MapGenerator(int szerokosc, int wysokosc)
    {
        KafelekModel doStworzeniaMapy = new KafelekModel();
        for (int j = 0; j < wysokosc + 2; j++)
        {
            for (int i = 0; i < szerokosc + 2; i++)
            {
                doStworzeniaMapy = new KafelekModel();
                doStworzeniaMapy.wspolrzedne = new Vector3(i, j, 0);
                doStworzeniaMapy.liczbaBombObok = 0;
                if (polozenieBomb.Exists(w => w.Equals(doStworzeniaMapy.wspolrzedne)))
                {
                    doStworzeniaMapy.isBomb = true;
                }
                mapa[i, j] = doStworzeniaMapy;
            }
        }
        for (int j = 1; j < wysokosc + 1; j++)
        {
            for (int i = 1; i < szerokosc + 1; i++)
            {
                if (mapa[i, j].isBomb == false)
                {
                    if (mapa[i - 1, j].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                    if (mapa[i - 1, j - 1].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                    if (mapa[i, j - 1].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                    if (mapa[i + 1, j - 1].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                    if (mapa[i + 1, j].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                    if (mapa[i + 1, j + 1].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                    if (mapa[i, j + 1].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                    if (mapa[i - 1, j + 1].isBomb == true)
                        mapa[i, j].liczbaBombObok++;
                }
                /*if(doStworzeniaMapy.isBomb)
                {
                     mapa[i - 1, j].iloscBombObok++;  
                     mapa[i - 1, j - 1].iloscBombObok++;
                     mapa[i , j - 1].iloscBombObok++;
                     mapa[i + 1, j - 1].iloscBombObok++;
                     mapa[i + 1, j].iloscBombObok++;         //dziala
                     mapa[i + 1, j + 1].iloscBombObok++;     //dziala
                     mapa[i, j + 1].iloscBombObok++;         //dziala
                     mapa[i - 1, j + 1].iloscBombObok++;     //dziala
                }         
                */
            }
        }
    }
    public void WorldGenerator()
    {
        Quaternion rotation = Quaternion.Euler(0, -90, 0);
        KafelekController kafelekControllerInstance;
        GameObject spawner;
        spawner = new GameObject();
        for (int j = 1; j < wysokosc + 1; j++)
        {
            for (int i = 1; i < szerokosc + 1; i++)
            {
                
                if (mapa[i, j].isBomb)
                {
                    spawner = bomba;
                }
                if (mapa[i, j].isBomb == false)
                {
                    switch (mapa[i, j].liczbaBombObok)
                    {
                        case 1:
                            spawner = kafelek1;
                            break;
                        case 2:
                            spawner = kafelek2;
                            break;
                        case 3:
                            spawner = kafelek3;
                            break;
                        case 4:
                            spawner = kafelek4;
                            break;
                        case 5:
                            spawner = kafelek5;
                            break;
                        case 6:
                            spawner = kafelek6;
                            break;
                        case 7:
                            spawner = kafelek7;
                            break;
                        case 8:
                            spawner = kafelek8;
                            break;
                        default:
                            spawner = kafelek0;
                            break;
                    }
                }
                kafelekControllerInstance = Instantiate(spawner, mapa[i, j].wspolrzedne, rotation).GetComponent<KafelekController>();
                kafelekControllerInstance.model = mapa[i, j]; // kafelekController.model = kafelekModel;
                kafelekControllerInstance.model.controller = kafelekControllerInstance; // kafelekModel.controller = kafelekController;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        kafelekGlowny.GetComponent<Rigidbody>().useGravity = false;
        startTime = Time.time;
        iloscBomb = 10;
        szerokosc = 10;
        wysokosc = 10;
        if (Options.iloscBomb > 0)
            iloscBomb = Options.iloscBomb;
        if (Options.szerokosc > 0)
            szerokosc = Options.szerokosc;       
        if (Options.wysokosc > 0)
            wysokosc = Options.wysokosc;
            mapa = new KafelekModel[szerokosc + 2, wysokosc + 2];
        BombGenerator(iloscBomb, szerokosc, wysokosc);
        MapGenerator(szerokosc, wysokosc);
        WorldGenerator();
    }
    // Update is called once per frame
    void Update()
    {
        if(KafelekController.bombClicked==false)
        {
            float t = Time.time - startTime;
            sekundy = t.ToString("f0");
            timerText.text = sekundy;

        }
        if (KafelekController.bombClicked == true)
        {
            //GameObject[] targets = GameObject.FindGameObjectsWithTag("Kafelek");

            //for (int i = 0; i < targets.Length; i++)
            //{
            //    Rigidbody rbComp = targets[i].GetComponent<Rigidbody>();
            //    rbComp.useGravity = true;
            //}


        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            Debug.Log("Quit");
        }

    }
}
