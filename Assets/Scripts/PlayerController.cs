using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb; // Stores reference to rigid body
    private int count;
    private float movementX;
    private float movementY;

    private bool canDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    public void OnJump(InputValue movementValue)
    {
        Debug.Log("Jump!");
        if(GetComponent<Rigidbody>().transform.position.y <= 0.5f) {
            Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
            GetComponent<Rigidbody>().AddForce (jump);
        } else if (canDoubleJump) {
            Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
            GetComponent<Rigidbody>().AddForce (jump);
            canDoubleJump = false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        if(GetComponent<Rigidbody>().transform.position.y <= 0.5f) {
            canDoubleJump = true;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }
}