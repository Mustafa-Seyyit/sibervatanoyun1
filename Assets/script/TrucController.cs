using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrucController : MonoBehaviour
{
    public List<GameObject> golds;
    public GameObject GoldsParent;
    private int currentGold; // şuanda açık olan altın sayısı 

    private void Start()
    {
        golds = new List<GameObject>();  // listeyi intalize ettik
        foreach (Transform gold in GoldsParent.transform)
        {
            golds.Add(gold.gameObject);
            gold.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player") return;

        var Player = other.gameObject.GetComponent<playercontroller>();

        var gold = Player.LoadGoldsToTruck();

        currentGold += gold; //bu altınları mevcut altın sayısına ekledik 

        for (int i=0; i< currentGold; i++)
        {
            golds[i].SetActive(true);  //sonrasında arabadaki altınlari açtım.
        }

        
    }
}
