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

    // Start is called before the first frame update
    void Start()
    {
        basemomentspeed = movementspeed;
        
        rb = GetComponent<Rigidbody>();   // aynı obje üzerinde rigidbody e ulaşmak için rigidbody i kullandık
        animator = GetComponent<Animator>(); //animatöre ulaştık 
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //yatay eksende giriş aldık 
        var vertical = Input.GetAxis("Vertical");       // dikey eksende giriş aldık

        //2 boyutlu olan yataty ve dikey eksenindeki girişleri 3 boyutlu ya çevirdik
        //x ve y dekseni uzerindeki girişleri x ve z eksenine göre cevirdik 

        var movementDirection = new Vector3(horizontal, 0, vertical);
        //movement directions 0 degilse isRunning animator parametresine true ata eger 0 ise false ata 
        animator.SetBool("isrunning ", movementDirection !=Vector3.zero);
        // carru degerim 0 degilde iscarrying animator parametresine true ata 
        animator.SetBool("isCarrying", carry!=0);
        
       

        if(movementDirection== Vector3.zero)
        {
            Debug.Log(" su an input yok");
                return;
        }



        // fiziksel olarak hizimiz yan eksenimiz ile hareket hizini carparak hareket ettirdik

        rb.velocity = movementDirection * movementspeed;


        // fiziksel olarak hizimizi yan eksenimiz ile hareket hizini carparak hareket ettirdik 
        var rotationDirection= Quaternion.LookRotation(movementDirection);

        //karakterin rotation degerini katdettiğim rotation değerine smototh bir geçiş sağlar 
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationspeed * Time.deltaTime);

    }

    public bool CollecGold()
    {
        if (carry == CarryLimit) return false;
        
            goldlist[carry].gameObject.SetActive(true);
            carry++;

            movementspeed -= reduceSpeed;

            return true;
    }

    public int LoadGoldsToTruck()
    {

        var carryingGold = carry;

        if (carryingGold == 0) return 0;


        carry = 0;
        foreach (var gold in goldlist)
        {
            gold.SetActive(false);

        }

        carry = 0;
        movementspeed = basemomentspeed; // hareket hızını default değer set ediyoruz.
        //movemomentspeed += carryingGold * reducespeed; yukarıdaki satırla aynı işleve sahipdir.

        return carryingGold;
    }

}
