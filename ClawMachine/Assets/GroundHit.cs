using UnityEngine;
using System.Collections;

public class GroundHit : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject craneClosed;
    public float upSpeed, maxHeight;
    public bool blueOn, greenOn, redOn;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FreezeBlue"))
        {
            blueOn = true;
            Destroy(other);
        }

        else if (other.gameObject.CompareTag("TimeGreen"))
        {
            greenOn = true;
            Destroy(other);
        }
        else
        {
            redOn = true;
            Destroy(other);
        }

        if (other.gameObject.tag == "PurplePlatform" || other.gameObject.tag == "DropZone")
        {
            rb.GetComponentInParent<Renderer>().enabled = false;
            craneClosed.SetActive(true);
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            rb.GetComponentInParent<Rigidbody>().isKinematic = true;
            rb.GetComponentInParent<Rigidbody>().AddForce(up * upSpeed);
            if (transform.parent.position.y > maxHeight)
            {
                stop();
            }
        }
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