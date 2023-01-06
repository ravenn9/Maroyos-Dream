using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] public float speed = 7f;
    [SerializeField] public float jumpForce = 20f;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    bool grounded;
    bool started;
    private bool jumping;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); //caching
        _animator = GetComponent<Animator>();
        grounded = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (started && grounded) 
            {
                _animator.SetTrigger("Jump");
                grounded = false;
                jumping = true;
            }
            else 
            {
                _animator.SetBool("GameStarted", true);
                started = true;
            }
        }       
            _animator.SetBool("Grounded", grounded);

    }

    private void FixedUpdate()
    {
        if (started) 
        {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }
        if (jumping) 
        {
            _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
            grounded = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
