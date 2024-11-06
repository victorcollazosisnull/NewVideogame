using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 2f; 
    public float distance = 2f; 
    private Vector3 startPosition; 
    private bool movingRight = true; 

    private void Start()
    {
        startPosition = transform.position; 
    }
    private void OnEnable()
    {
        transform.position = startPosition; 
    }


    private void Update()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        float newPosX = transform.position.x + (movingRight ? speed * Time.deltaTime : -speed * Time.deltaTime);

        if (newPosX > startPosition.x + distance)
        {
            movingRight = false; 
        }
        else if (newPosX < startPosition.x - distance)
        {
            movingRight = true;
        }

        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }
}