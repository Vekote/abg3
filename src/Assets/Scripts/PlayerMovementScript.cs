using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour
{
    public float movementSpeed = 50f;
    public float jumpForce = 1000f;
	public float rotationSpeed = 100f;
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
        Vector3 inputVelocity = new Vector3(
			Input.GetAxisRaw("Vertical") * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) +
			Input.GetAxisRaw("Horizontal") * Mathf.Sin((transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad),
			0,
			Input.GetAxisRaw("Vertical") * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + 
			Input.GetAxisRaw("Horizontal") * Mathf.Cos((transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad)
		).normalized;
        movementDirection = inputVelocity * movementSpeed;
        if (isGrounded || hasDoubleJumpLeft)
        {
			if (Input.GetButtonDown ("Jump")) {
				Instantiate (jumpDustCloud, transform.position, jumpDustCloud.transform.rotation);
				hasDoubleJumpLeft = false;
				movementDirection.y = jumpForce;
			}
        }
		if (Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
		if (Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        playerBody.AddForce(movementDirection); 
    }
}
