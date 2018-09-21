using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float jumpForce = 100f;
    public GameObject jumpDustCloud;
    
    private Rigidbody playerBody;
    private bool isGrounded;
    private bool hasDoubleJumpLeft;

    private Vector3 movementDirection = Vector3.zero;

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
            hasDoubleJumpLeft = true;
        }
    }

        void Update()
    {	
		
        Vector3 inputVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        movementDirection = inputVelocity * movementSpeed;
        if (isGrounded || hasDoubleJumpLeft)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Instantiate(jumpDustCloud, transform.position, jumpDustCloud.transform.rotation);
                hasDoubleJumpLeft = false;
                movementDirection.y = jumpForce;
            }
        }
        playerBody.AddForce(movementDirection); 
    }
}
