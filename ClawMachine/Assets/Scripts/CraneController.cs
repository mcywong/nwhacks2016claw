using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour
{

    public GameObject craneClosed;
    public float downSpeed, upSpeed, maxHeight;
    private Rigidbody rb;
    private int count = 0;
    private Collision collision;

    // Use this for initialization
    void Start()
    {
        craneClosed.SetActive(false);
        rb = GetComponentInChildren<Rigidbody>();
        stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string input = Input.inputString;
        if (input == "z")
        {
            normal();
        }
        else if (input == "x")
        {
            stop();
        }
        else if (input == "c")
        {
            if (collision != null)
            {
                dropObject(collision);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        rb.GetComponent<Renderer>().enabled = false;
        craneClosed.SetActive(true);
        collision = other;
        count++;
        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
        rb.isKinematic = true;
        GameObject grabbed = other.gameObject;
        other.rigidbody.isKinematic = true;
        grabbed.transform.parent = this.transform;
        rb.isKinematic = false;
        rb.AddForce(up * upSpeed);
        if (transform.position.y > maxHeight)
        {
            stop();
        }
    }

    void dropObject(Collision other)
    {
        rb.GetComponent<Renderer>().enabled = true;
        craneClosed.SetActive(false);
        rb.detectCollisions = false;
        other.transform.parent = null;
        other.rigidbody.isKinematic = false;
        stop();
    }
    void normal()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.detectCollisions = true;
        Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
        rb.AddForce(down * downSpeed);
    }
    void stop()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}