using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour
{
    public float playerSpeed = 4f;
    public GameObject jumpDustCloud;
    private Rigidbody2D playerBody;
    private Transform transform;
    private Vector3 defaultScale;
    private bool isAirborne;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        defaultScale = transform.localScale;
    }

    void Update(){
        if (!isAirborne && Input.GetButtonDown("Jump"))
        {
            Instantiate(jumpDustCloud, transform.position, jumpDustCloud.transform.rotation);
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isAirborne)
            transform.localScale = defaultScale * 1.5f;
        else transform.localScale = defaultScale;

        Vector2 inputVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        playerBody.velocity = inputVelocity * playerSpeed;
    }

    private void Jump()
    {
        isAirborne = true;
        gameObject.layer = 8;
        Invoke("SetAirborneToFalse", 1);
    }

    private void SetAirborneToFalse()
    {
        gameObject.layer = 1;
        isAirborne = false;
    }
}
