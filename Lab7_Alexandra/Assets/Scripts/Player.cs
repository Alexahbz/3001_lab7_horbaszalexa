using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) && transform.position.y <= 5.2f)
        {
            moveY = moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -moveSpeed * Time.deltaTime;
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = moveSpeed * Time.deltaTime;
            
        }
        transform.Translate(moveX, moveY, 0f);
    }
}
