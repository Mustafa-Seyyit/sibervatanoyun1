using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> CarPrefabs;

    private float mintime=1f, maxtime=3f;

    private float timer;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Random.Range(mintime, maxtime);  //oluşucakak İLK arabanın rastgele süresi 
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            timer = 0; // bu kosuiun tek bir kere çalışması için ifin içine girer girmez timer ı 0 a eşitledim. 
            var car = CarPrefabs[Random.Range(0, CarPrefabs.Count)]; //listeden rastgele araba seç

            //oluşucak arabayi bu scritin baglı olduğu transformu parebt yapmak için en sona transform yazdim. 
            var spawnedCar=Instantiate(car, transform.position, transform.rotation, transform);

            spawnedCar.AddComponent<CarController>();

            Destroy(spawnedCar.gameObject, 4.5f);

            spawnTime = Random.Range(mintime, maxtime);   // oluşucak ilk hata dan sonraki arabaların rastgele süresi 


        }
    }

}
