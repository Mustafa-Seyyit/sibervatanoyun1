using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrucController : MonoBehaviour
{
    public List<GameObject> golds; // truck da bulunacak altınları tuttuğumuz liste 
    public GameObject GoldsParent; // önceden yerleştirdiğimiz altınların parent i
    private int currentGold; // şuanda açık olan altın sayısı 

    private void Start()
    {
        golds = new List<GameObject>();  // listeyi intalize ettik
        foreach (Transform gold in GoldsParent.transform)    // gold parent'indaki butun childlara ulastik
        {
            golds.Add(gold.gameObject);  //bunları listeye ekledik 
            gold.gameObject.SetActive(false);  //ve görünürlüğünü kapattık 
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player") return; // carpisma objesinin (other) tag'ini kontrol et

        var Player = other.gameObject.GetComponent<playercontroller>();   // other objesinden karakter kontrol scriptine ulas.

        var gold = Player.LoadGoldsToTruck();   // playerda kac tane altin oldugunun sayisini aldik.

        currentGold += gold; //bu altınları mevcut altın sayısına ekledik 

        for (int i=0; i< currentGold; i++)
        {
            golds[i].SetActive(true);  //sonrasında arabadaki altınlari açtım.
        }

        
    }
}
