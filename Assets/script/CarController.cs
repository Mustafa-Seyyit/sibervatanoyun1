using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float moveSpeed = 15f;


    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed*Time.deltaTime); 
    }

}
