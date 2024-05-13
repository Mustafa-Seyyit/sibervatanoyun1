using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tablecontroller : MonoBehaviour
{
    public GameObject goldobject;  //masadaki altın objesi 

    public bool IsGoldectable => goldobject.activeSelf; // active self değişkenin işaret ettiği nesnenin aktif olup olmadığını kontrol eder.


    private void OnCollisionEnter(Collision other)   // carpismaya basladigi zaman calisacak olan unity fonksiyonu
    {

        if (!IsGoldectable) return;   // eger masadaki altin objem kapali ise fonksiyondan cik.

        if (other.gameObject.tag!="Player")    return;   // carpisma objesinin (other) tag'ini kontrol et. karakter değilise çık fonksiyondan 

        var Player = other.gameObject.GetComponent<playercontroller>();    // other objesinden karakter kontrol scriptine ulas.


        if (Player.CollecGold())   // collect gold fonksiyonum eger calistiysa
        {
            goldobject.SetActive(false);   // altin objesini kapat.

            Invoke(nameof(ReloadGold), Random.Range(5f, 10f)); //5 il 10 saniye arasında reloadGolf fonksiyonunu çalıştırır.
        }
    }


    private void ReloadGold()     // altin objesini tekrar acan fonksiyon
    {
        goldobject.SetActive(true);    // altin objesini ac.
    }
   
}
