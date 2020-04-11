using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private int numjumps;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        numjumps = 0;

        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool jump = Input.GetKeyDown(KeyCode.Space);
        Vector3 movement;

        if (jump && numjumps > 0){
            numjumps--;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            movement = new Vector3(moveHorizontal, 30.0f, moveVertical);
    
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        }
        rb.AddForce(movement * speed);

     


    }
    private void OnCollisionEnter(Collision collision)
    {
        numjumps = 2;
    }

    private void OnCollisionExit(Collision collision)
    {
        numjumps = 1;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}