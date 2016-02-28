using UnityEngine;
using System.Collections;

public class DropZone : MonoBehaviour
{
    public int score = 0;
    private int cowboyCount = 0;
    private int armyCount = 0;
    private int destroyed = 0;
    private int endTime, currentTime;
    private bool poweredUp = false;

    void Update()
    {
        currentTime = (int)Time.time;
        if (poweredUp && currentTime >= endTime)
        {
            poweredUp = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject fallThrough = other.gameObject.transform.root.gameObject;
        if (fallThrough.CompareTag("Cowboy"))
        {
            cowboyCount++;
            score += 200;
        }
        else if (fallThrough.CompareTag("Soldier"))
        {
            armyCount++;
            score += 100;
        }
      //  Debug.Log(cowboyCount);
      //  Debug.Log(armyCount);
        destroyed++;
      //  Debug.Log(destroyed);
        Destroy(fallThrough, 1);
    }

    public void doublePoints(int duration)
    {
        endTime = (int)Time.time + duration;
        this.poweredUp = true;
    }
}