using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playercontroller : MonoBehaviour
{
    public float movementspeed = 5f; //hareket hızı
    public float rotationspeed = 10f; // oyuncu karakterinin dönüş hızını depolamak için bir değişken tanımladık 
    public Rigidbody rb;      //fizik işlemleri için rigidbody i tanımlama
    private Animator animator; // animasyon işlemleri için animatörü tanımladık

    // Start is called before the first frame update
    void Start()
    {
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

        animator.SetBool("isrunning ", movementDirection !=Vector3.zero); 
        
       

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
}
