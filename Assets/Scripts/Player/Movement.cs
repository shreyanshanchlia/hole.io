using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private void Update()
    {
        // Get the input from the player and move the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);
    }
    void Move(float horizontal, float vertical)
    {
        // Move the player
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.position += movement * (speed * Time.deltaTime);
    }
}
