using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
       private Rigidbody rb; 
              
       private int count;

       private float movementX;
       private float movementY;

       public float speed = 0; 

       public TextMeshProUGUI countText;
       
       public GameObject winTextObject;

       public bool grounded;

       public int jumps = 2;

       public Vector3 jump;


       void Start()
       {
              rb = GetComponent<Rigidbody>();

              count = 0; 

              SetCountText();

              winTextObject.SetActive(false);
              jump = new Vector3(0.0f, 2.0f, 0.0f);
       }
 
       void OnMove(InputValue movementValue)
       {
              Vector2 movementVector = movementValue.Get<Vector2>();

              movementX = movementVector.x; 
              movementY = movementVector.y;          
       }

       private void FixedUpdate() 
       {
              Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

              rb.AddForce(movement * speed);

              if (Input.GetKeyDown("space") && (jumps > 0))
              {
                     rb.AddForce(jump*5, ForceMode.Impulse);
                     jumps -= 1;
              }
       }

       void OnCollisionEnter(Collision other)
       {
              if (other.gameObject.name == "Ground")
              {
                     grounded = true;
                     jumps=2;
              }
              else
              {
                     grounded = false;
                     if (jumps != 0)
                     {
                            jumps = 1;
                     }
              }
       }
       void OnTriggerEnter(Collider other)
       {     
              if (other.gameObject.CompareTag("PickUp")) 
              {
                     other.gameObject.SetActive(false);

                     count = count + 1;

                     SetCountText();
              }

       }
       void SetCountText()
       {
              countText.text = "Count: " + count.ToString();

              if (count >= 5)
              {
                     winTextObject.SetActive(true);
              }
       }
}