using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float dashTimer;
    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    private Vector3 dashDirection;
    private float currentDashSpeed;
    public float speed;
    public float dashSpeed;
    public float dashCountThreshold;
    public float cooldownDash;
    EnemyBehaviour Enemy;
    public KeyCode attack, Jump;
    public bool isGrounded;
    public float JumpForce;
    public enum State
    {
        Normal,
        Dashing,
    }
    private State state;

    void Start()
    {
        speed = 5;
        dashSpeed = 20;
        dashCountThreshold = 60;
        cooldownDash = 50;
        dashTimer = 0;
        rb = GetComponent<Rigidbody>();
        state = State.Normal;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void FixedUpdate()
    {
        dashTimer--;
        switch (state)
        {
            case State.Normal:
                moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    lastMoveDirection = moveDirection;
                    rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, 0);

                    if (Input.GetKeyDown(Jump) && isGrounded)
                    {
                        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                        isGrounded = false;
                    }
                    Vector3 tmpPos = transform.position;
                    tmpPos.x = Mathf.Clamp(tmpPos.x, -12.5f, 26.5f);
                    tmpPos.y = Mathf.Clamp(tmpPos.y, -0.5f, 19.2f);
                    transform.position = tmpPos;
                }

                if (Input.GetKeyDown(attack))
                {
                    if (dashTimer <= 0)
                    {
                        dashDirection = lastMoveDirection;
                        currentDashSpeed = dashSpeed;
                        dashTimer = cooldownDash;
                        state = State.Dashing;
                    }
                }
                break;

            case State.Dashing:
                currentDashSpeed -= currentDashSpeed * Time.deltaTime;
                rb.velocity = dashDirection * currentDashSpeed;
                if (currentDashSpeed < dashCountThreshold)
                {
                    state = State.Normal;
                    dashTimer = cooldownDash;
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
            other.gameObject.GetComponent<EnemyBehaviour>().Health -= 1;
    }
}
