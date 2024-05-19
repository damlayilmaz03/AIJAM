using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    
    private bool isGrounded;
    Animator anim;
    public AudioClip jumpSound; 
    private AudioSource audioSource;
    void Start()
    {
        
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isGrounded = true;
    }

    void Update()
    {
        
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetTrigger("walk");
            isGrounded = true;
        }
    }
    void Jump()
    {
        anim.SetTrigger("jump");
        audioSource.PlayOneShot(jumpSound);
        transform.position += Vector3.up * jumpForce;
        isGrounded =false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
