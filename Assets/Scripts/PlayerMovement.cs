using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float direction;
    public float Jforce = 8.5f;
    public LayerMask layerMask;
    public bool grounded;
    public bool DJump;
    public SpriteRenderer _changesprite;
    public Slider _slider;
    public int color;
    public int vida = 10;
    Rigidbody2D myRB2D;
    public GameObject bloquear;
    public static event Action OnHeartCollected;
    public static event Action OnLose;
    public static event Action OnWin;
    private void Awake()
    {
        myRB2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Inputs();
        CheckGround();
    }
    private void FixedUpdate()
    {
        myRB2D.velocity = new Vector2(direction * speed, myRB2D.velocity.y);
    }

    void Inputs()
    {
        direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || DJump))
        {
            if (!grounded)
            {
                DJump = false;
            }

            myRB2D.velocity = new Vector2(myRB2D.velocity.x, Jforce);
        }

    }

    void CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red);

        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMask))
        {
            grounded = true;
            DJump = true;
        }
        else
        {
            grounded = false;
        }
    }
    /*
    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (grounded || DJump)
            {
                if (!grounded)
                {
                    DJump = false;
                }

                myRB2D.velocity = new Vector2(myRB2D.velocity.x, Jforce);
            }
        }
    }
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Die"))
        {
            if (OnLose != null)
            {
                OnLose();
            }
        }
        if (collision.tag == ("Win"))
        {
            SceneManager.LoadScene("Nivel 2");
        }
        if (collision.tag == ("Win2"))
        {
            if (OnWin != null)
            {
                OnWin();
            }
        }
        if (collision.tag == "Blue")
        {
            bloquear.SetActive(true);
            if (color == 3)
            {
                Debug.Log("Objeto del mismo color detectado");
            }
            else
            {
                Shake.Instance.CameraMovement(2, 2, 2);
                vida = vida - 1;
                _slider.value = vida;
                Debug.Log("-1 de vida");
            }
        }
        if (collision.tag == ("Red"))
        {
            bloquear.SetActive(true);
            if (color == 1)
            {
                Debug.Log("Objeto del mismo color detectado");
            }
            else
            {
                Shake.Instance.CameraMovement(2, 2, 2);
                vida = vida - 2;
                _slider.value = vida;
                Debug.Log("-2 de vida");
            }
        }
        if (collision.CompareTag("Green"))
        {
            bloquear.SetActive(true);
            if (color == 2)
            {
                Debug.Log("Objeto del mismo color detectado");
            }
            else
            {
                Shake.Instance.CameraMovement(3, 4, 2);
                vida = vida - 3;
                _slider.value = vida;
                Debug.Log("-3 de vida");
            }
        }
        if (collision.CompareTag("Life"))
        {
            vida = vida + 1;
            _slider.value = vida;
            Debug.Log("+1 de vida");
            if (OnHeartCollected != null)
            {
                OnHeartCollected(); 
            }
            Destroy(collision.gameObject); 
        }
        if (vida <= 0)
        {
            if (OnLose != null)
            {
                OnLose();
            }    
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Green"))
        {
            bloquear.SetActive(false);
        }
        if (collision.CompareTag("Red"))
        {
            bloquear.SetActive(false);
        }
        if (collision.CompareTag("Blue"))
        {
            bloquear.SetActive(false);
        }
    }
        /*
    public void OnChangeColorRed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _changesprite.color = Color.red;
            color = 1;
        }
    }

    public void OnChangeColorBlue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _changesprite.color = Color.blue;
            color = 3;
        }
    }

    public void OnChangeColorGreen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _changesprite.color = Color.green;
            color = 2;
        }
    }
        */
    public void rojo()
    {
        _changesprite.color = Color.red;
        color = 1;
    }
    public void verde()
    {
        _changesprite.color = Color.green;
        color = 2;
    }
    public void blue()
    {
        _changesprite.color = Color.blue;
        color = 3;
    }

}
