using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("VELOCIDAD Y SALTO")]
    public float velMovement = 5f; //Velocidad del personaje 
    public float fuerzaJump = 7f; //Fuersa de salto

    private bool enElSuelo = false; //Indicador detencción dle suelo

    private Rigidbody2D rb;
    private Animator animator;

    //Movimiento player
    float movimientoH;

    //Referenciamos al personaje si luego pensamos usarlo en ciertos puntos del mapa
    private Transform playerTransform;


    void Start()
    {
        // Obtener las referencias a los componentes Rigidbody2D y Animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Verificar que los componentes han sido encontrados
        if (rb == null)
        {
            Debug.LogError("No se encontró el componente Rigidbody2D en el objeto " + gameObject.name);
        }

        if (animator == null)
        {
            Debug.LogError("No se encontró el componente Animator en el objeto " + gameObject.name);
        }

    }


    void Update()
    {
        //Movimiento Horizontal del player
        movimientoH = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimientoH * velMovement, rb.velocity.y);
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoH));


        //Flip
        if (movimientoH > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);//Movimienot hacia la derecha

        }
        else if (movimientoH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);//Movimienot hacia la izquierda
        }


        //Salto

        if (Input.GetButton("Jump") && enElSuelo)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(new Vector2(0f, fuerzaJump), ForceMode2D.Impulse);
            enElSuelo = false;
        }

    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Detectar el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = true;
            Debug.Log("Estoy Tocando el suelo");
        }
    }

}
