using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{

    public static Player Instance {get; private set;}

    [SerializeField] private float movingSpeed = 5f;



    private Rigidbody2D rb;

    private float minMovingSpeed = 0.1f;
    private bool isRun = false;
    private void Awake(){
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate(){
       HandleMovement();
    }


    private void HandleMovement(){
         Vector2 inputVector = GameInput.Instance.GetMovementVector(); 
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * movingSpeed * Time.fixedDeltaTime);

        if (Mathf.Abs(inputVector.x)> minMovingSpeed || Math.Abs(inputVector.y)> minMovingSpeed ){
            isRun = true;
        } else {
            isRun = false;
        }
    }

    public bool IsRun(){
        return isRun;
    }

    public Vector3 GetPlayerScreenPosition(){
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
}
