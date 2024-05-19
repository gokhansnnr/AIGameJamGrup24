using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float moveSpeed = 5f; //Karakter hareket kuvveti
    [SerializeField] float JumpForce = 8f; // Z�plama kuvveti
    private Animator animator; //Animator bile�eni
    private SpriteRenderer spriteRenderer; //SpriteRenderer bile�eni

    /*
    [Header("Animasyon")]
    [SerializeField] AnimationClip Jump;
    [SerializeField] AnimationClip Fall;
    */

    bool GroundCheck = false;
    bool canIJump; //Z�plama kontrol�
    Rigidbody2D rb; //Rigidbody
    private float horizontalMovement; //Yatayda hareket

    void Start()
    {//2
        animator = transform.GetComponentInChildren<Animator>(); //Childden animator �ektik
        rb = GetComponent<Rigidbody2D>(); //Rigidbody kulland�k.
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>(); //Childden spriteRenderer ald�k.
    }

    void Update()
    {//3
        MoveController(); //Hareket kontrol�n� kulland�k.
    }

    private void FixedUpdate()
    {//6
        rb.velocity = new Vector3(horizontalMovement * moveSpeed, rb.velocity.y, 0f); //velocity i�lemleri
        animator.SetBool("Fall", !GroundCheck); //Zemine temas yoksa animasyonu uygula
        if(rb.velocity.y < 0.02f)
        { //velocity kontrol�
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Force);
            animator.SetBool("Jump", false); //Atlama animasyonu devre d���
            if (GroundCheck) //Zemin kontrol�
            {
                animator.SetBool("Fall", false);
            }
        }
    }

    void MoveController()
    {//4
        horizontalMovement = Input.GetAxisRaw("Horizontal"); //Klavyeden yatay hareketleri ald�k

        if (Input.GetKeyDown(KeyCode.W))
        {//W'ya bas�ld���nda
            if (GroundCheck){ //GroundCheck kontrol� yapacak.
                rb.velocity = new Vector3(rb.velocity.x, JumpForce, 0); //Z�plama y�n� tan�mlad�k.
                animator.SetBool("Jump", true); //Z�plama animasyonu verdik.
            }
        }

        Vector3 move = new Vector3(horizontalMovement, 0f, 0f); //Hareket vekt�r� olu�turuldu

        if (horizontalMovement != 0f) //Run animasyon kontrol�
        {
            animator.SetBool("Run", true); //Hareket olursa Run true
            if(horizontalMovement < 0f)
            {
                spriteRenderer.flipX = true; //Sol tarafa hareket varsa sprite �evir
            }
            else
            {
                spriteRenderer.flipX = false; // Sa� tarafa hareket ediliyorsa sprite �evirme
            }
        }
        else
        {
            animator.SetBool("Run", false); //Hareket edilirse, Run'u false yap.
        }
    }

    /*
    private bool GroundCheck()
    {//5 - Zemin kontrol�
        return Physics2D.OverlapCircle(transform.position, .5f, LayerMask.GetMask("Ground"));
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GroundCheck = collision.gameObject.CompareTag("Ground") ? true : false;
        print(GroundCheck);
        if (collision.gameObject.CompareTag("Ground"))
        {
            GroundCheck = true;
        }
        else
        {
            GroundCheck = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
