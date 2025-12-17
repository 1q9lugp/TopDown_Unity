using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * MoveSpeed;
    }

    public void Move(InputAction.CallbackContext context) {
        anim.SetBool("isWalking", true);

        if (context.canceled) {
            anim.SetBool("isWalking", false);

            anim.SetFloat("LastInputX", moveInput.x);
            anim.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();

        anim.SetFloat("inputX", moveInput.x);
        anim.SetFloat("inputY", moveInput.y);
    }
}
