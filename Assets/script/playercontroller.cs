using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playercontroller : MonoBehaviour
{
    public float movementspeed = 5f; //hareket hızı
    public float rotationspeed = 10f; // oyuncu karakterinin dönüş hızını depolamak için bir değişken tanımladık 
    public Rigidbody rb;      //fizik işlemleri için rigidbody i tanımlama
    private Animator animator; // animasyon işlemleri için animatörü tanımladık
    public List<GameObject> goldlist; //karakterin elindeki altınları tuttugumuz liste
    public int carry;  //kaç tane altın  tuttuğunu gösterir karakterin

    public float reduceSpeed =  0.5f; // altın tasıdıkca azalacak olan hız miktarı 
    private float basemomentspeed;    // oyuna başladığımızda karakterin hareket hızı

    public int CarryLimit => goldlist.Count;  //tasima işlemi

    public Transform boneParent;  //kemiklerin parenti 

    public bool CanMove = true;

    public Transform spinepozition;
    
    void Start()
    {
        basemomentspeed = movementspeed;  //base hızın değerini aldık 
        
        rb = GetComponent<Rigidbody>();   // aynı obje üzerinde rigidbody e ulaşmak için rb değişkenini kullandık
        animator = GetComponent<Animator>(); //animatöre ulaştık

        Ragdoll(false);  // oyun başladığında Ragdoll kapalı olucak
    }

    
    void Update()
    {
        if (!CanMove) return;

        float horizontal = Input.GetAxis("Horizontal"); //yatay eksende giriş aldık 
        var vertical = Input.GetAxis("Vertical");       // dikey eksende giriş aldık

        //2 boyutlu olan yataty ve dikey eksenindeki girişleri 3 boyutlu ya çevirdik
        //x ve y dekseni uzerindeki girişleri x ve z eksenine göre cevirdik 
        var movementDirection = new Vector3(horizontal, 0, vertical);

        //movement directions 0 degilse isRunning animator parametresine true ata eger 0 ise false ata 
        animator.SetBool("isrunning ", movementDirection !=Vector3.zero);

        // carry degerim 0 degilde iscarrying animator parametresine true ata 
        animator.SetBool("isCarrying", carry!=0);


        // yukaridakinin movement directionu ile degil de rigidbodydeki hizi kullanarak yaptigimiz sey. ikisi de ayni.
        // animator.SetBool("isRunning",rb.velocity != Vector3.zero);


        if (movementDirection== Vector3.zero)  //input yoksa 
        {
            Debug.Log(" su an input yok");
                return;
        }



        // fiziksel olarak hizimiz yan eksenimiz ile hareket hizini carparak hareket ettirdik

        rb.velocity = movementDirection * movementspeed;


        // movement direction yonunu rotation olarak kaydet
        var rotationDirection = Quaternion.LookRotation(movementDirection);

        //karakterin rotation degerini katdettiğim rotation değerine smototh bir geçiş sağlar 
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationspeed * Time.deltaTime);

    }

    public bool CollecGold()        // table scriptinde cagirilacak olan altin toplama fonksiyonu.
    {
        if (carry == CarryLimit) return false;      // eger tasidigim altin sayisi tasima limitime esit ise fonksiyon false deger return etsin.


        // 0 1 2 -> cary degerlerimi listenin indexi olarak kullanip o indexteki altinlari aktif yaptik.
        goldlist[carry].gameObject.SetActive(true);
            carry++;     // carry değerini 1 arttırıyoruz

            movementspeed -= reduceSpeed;  //hareket hızımızı azalttık 

            return true;     // butun islem basarili bir sekilde gerceklestigi icin true return ediyoruz.
    }

    public int LoadGoldsToTruck()
    {

        var carryingGold = carry; //topladığımız altın sayısını kopyaladık 

        if (carryingGold == 0) return 0; //eğer altın taşımıyorsak uğraşma 


        foreach (var gold in goldlist)
        {
            gold.SetActive(false);   // elimizdeki butun altinlari kapattik

        }

        carry = 0;  //taşıdığımız altın sayısını sıfırladık 

        movementspeed = basemomentspeed; // hareket hızını default değer set ediyoruz.
        //movemomentspeed += carryingGold * reducespeed; yukarıdaki satırla aynı işleve sahipdir.

        return carryingGold;      // tasidigimiz altin sayisini return ettik.
    }

    public void Ragdoll(bool isActive)
    {
        animator.enabled = !isActive;

        var colliders = boneParent.GetComponentsInChildren<Collider>(); // kemiklerdeki colliderların hepsine eriştrik
        var rigidbodies = boneParent.GetComponentsInChildren<Rigidbody>(); //kemiklerdeki  rigidbodylere eriş 

        foreach (var coll in colliders)
            coll.enabled = isActive;

        foreach (var rig in rigidbodies)
            rig.isKinematic = !isActive;

        GetComponent<Collider>().enabled = !isActive;

        CanMove = !isActive; //ragdoll olduğu zaman hareketi engelle
        
        if (isActive==false)
        {
            StartCoroutine(CloseRagdoll());
        }
    }

    public IEnumerator CloseRagdoll()
    {
        yield return new WaitForSeconds(3f);
        Ragdoll(false);
        transform.position = new Vector3(spinepozition.position.x, 0, spinepozition.position.y);
    }
}

