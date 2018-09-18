using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public float playerSpeed = 4f;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 inputVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        playerBody.velocity = inputVelocity * playerSpeed;
    }
}
