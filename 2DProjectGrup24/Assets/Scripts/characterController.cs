using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float moveSpeed = 5f; //Karakter hareket kuvveti
    [SerializeField] float JumpForce = 8f; // Zýplama kuvveti
    private Animator animator; //Animator bileþeni
    private SpriteRenderer spriteRenderer; //SpriteRenderer bileþeni

    /*
    [Header("Animasyon")]
    [SerializeField] AnimationClip Jump;
    [SerializeField] AnimationClip Fall;
    */

    bool GroundCheck = false;
    bool canIJump; //Zýplama kontrolü
    Rigidbody2D rb; //Rigidbody
    private float horizontalMovement; //Yatayda hareket

    void Start()
    {//2
        animator = transform.GetComponentInChildren<Animator>(); //Childden animator çektik
        rb = GetComponent<Rigidbody2D>(); //Rigidbody kullandýk.
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>(); //Childden spriteRenderer aldýk.
    }

    void Update()
    {//3
        MoveController(); //Hareket kontrolünü kullandýk.
    }

    private void FixedUpdate()
    {//6
        rb.velocity = new Vector3(horizontalMovement * moveSpeed, rb.velocity.y, 0f); //velocity iþlemleri
        animator.SetBool("Fall", !GroundCheck); //Zemine temas yoksa animasyonu uygula
        if(rb.velocity.y < 0.02f)
        { //velocity kontrolü
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Force);
            animator.SetBool("Jump", false); //Atlama animasyonu devre dýþý
            if (GroundCheck) //Zemin kontrolü
            {
                animator.SetBool("Fall", false);
            }
        }
    }

    void MoveController()
    {//4
        horizontalMovement = Input.GetAxisRaw("Horizontal"); //Klavyeden yatay hareketleri aldýk

        if (Input.GetKeyDown(KeyCode.W))
        {//W'ya basýldýðýnda
            if (GroundCheck){ //GroundCheck kontrolü yapacak.
                rb.velocity = new Vector3(rb.velocity.x, JumpForce, 0); //Zýplama yönü tanýmladýk.
                animator.SetBool("Jump", true); //Zýplama animasyonu verdik.
            }
        }

        Vector3 move = new Vector3(horizontalMovement, 0f, 0f); //Hareket vektörü oluþturuldu

        if (horizontalMovement != 0f) //Run animasyon kontrolü
        {
            animator.SetBool("Run", true); //Hareket olursa Run true
            if(horizontalMovement < 0f)
            {
                spriteRenderer.flipX = true; //Sol tarafa hareket varsa sprite çevir
            }
            else
            {
                spriteRenderer.flipX = false; // Sað tarafa hareket ediliyorsa sprite çevirme
            }
        }
        else
        {
            animator.SetBool("Run", false); //Hareket edilirse, Run'u false yap.
        }
    }

    /*
    private bool GroundCheck()
    {//5 - Zemin kontrolü
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
