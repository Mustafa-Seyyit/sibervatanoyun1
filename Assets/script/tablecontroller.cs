using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tablecontroller : MonoBehaviour
{
    public GameObject goldobject;

    public bool IsGoldectable => goldobject.activeSelf; // active self değişkenin işaret ettiği nesnenin aktif olup olmadığını kontrol eder.


    private void OnCollisionEnter(Collision other)
    {

        if (!IsGoldectable) return;

        if (other.gameObject.tag!="Player")    return;

        var Player = other.gameObject.GetComponent<playercontroller>();
      

        if (Player.CollecGold())
        {
            goldobject.SetActive(false);
            Invoke(nameof(ReloadGold), Random.Range(5f, 15f)); //5 il 15 saniye arasında reloadGolf fonksiyonunu çalıştırır.
        }
    }


    private void ReloadGold()
    {
        goldobject.SetActive(true);
    }
   
}
