using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EndOfHeroes : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject[] bottomPos;


    public static string[] suits = new string[] { "Light", "Mid", "Heavy", "Power", "Balk" };
    public static string[] number = new string[] { "1", "2", "3", "4" };
    public List<string>[] bottoms;

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();


    public List<string> deck;
        
    void Start()
    {
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3 };
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        foreach (string card in deck)
        {
            //Debug.Log(card);
        }
        EndOfHeroesSort();
        StartCoroutine(EndOfHeroesDeal());
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string n in number)
            {
                newDeck.Add(s + n);
            }
        }

        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    IEnumerator EndOfHeroesDeal()
    {
        for (int i = 0; i < 4; i++)

        {
            float yOffset = -0.2f;
            float zOffset = 0.0f;
            //string card = bottoms[i];
            foreach (string card in bottoms[i])

            {
                yield return new WaitForSeconds(0.25f);
                GameObject newCard = Instantiate(cardPrefab, new Vector3( bottomPos[i].transform.position.x - yOffset, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), transform.rotation * Quaternion.Euler (15f, 257f, 7f), bottomPos[i].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().faceUp = true;
            }
        }
    }
    
    void EndOfHeroesSort()
    {
        for (int i = 0; i < 1; i++)
        {
            for (int j = i; j < 4; j++)
           {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }
}
