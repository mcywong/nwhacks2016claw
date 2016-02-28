using UnityEngine;
using System.Collections;

//Myo Armband
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;

public class MyoCraneController : MonoBehaviour
{
    public GameObject craneClosed;
    public float downSpeed, upSpeed, maxHeight;
    private Rigidbody rb;
    private int count = 0;
    private Collision collision;

    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    public float speed;
    private float lastTime;

    // Use this for initialization
    void Start()
    {
        lastTime = 0;
        craneClosed.SetActive(false);
        rb = GetComponentInChildren<Rigidbody>();
        stop();
        lastTime = 0;
    }

    // Update is called once per frame.
    void Update()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        float currentTime = Time.time;

        if (currentTime - lastTime > 0.75)
        {
            lastTime = currentTime;
            // Vibrate the Myo armband when a fist is made.
            if (thalmicMyo.pose == Pose.Fist)
            {
                transform.Translate(10, 0, 0);
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                transform.Translate(-10, 0, 0);
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.WaveIn)
            {
                transform.Translate(0, 0, -10);
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.WaveOut)
            {
                transform.Translate(0, 0, 10);
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.DoubleTap)
            {
                normal();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else
            {
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }
    }

    /*
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
    */

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

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}