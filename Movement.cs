using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _jumpForce = 0f;
    private bool _facingRight = true;
    private Rigidbody2D _rigidbody;

    [Space]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask IsGround;
    [SerializeField] private float GroundRadius = 0.5f;
    private bool OnGround = false;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float inputAxisY = _rigidbody.velocity.y;
        float movement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            _rigidbody.AddForce(new Vector3(0, _jumpForce, 0));
        }

        OnGround = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, IsGround);

        _animator.SetBool("Jump", !OnGround);
        _animator.SetFloat("Speed", Mathf.Abs(movement));
        _animator.SetFloat("VelocityX", inputAxisY);

        transform.position += new Vector3(movement, 0, 0) * _speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (inputHorizontal > 0 && !_facingRight)
        {
            Flip();
        }

        if (inputHorizontal < 0 && _facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        _facingRight = !_facingRight;
    }
}
