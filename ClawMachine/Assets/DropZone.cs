using UnityEngine;
using System.Collections;

public class DropZone : MonoBehaviour
{
    private int cowboyCount = 0;
    private int armyCount = 0;
    private int destroyed = 0;
    

    void OnTriggerEnter(Collider other)
    {
        GameObject fallThrough = other.gameObject.transform.root.gameObject;
        if (fallThrough.CompareTag("Cowboy"))
        {
            cowboyCount++;
        }
        else if (fallThrough.CompareTag("Soldier"))
        {
            armyCount++;
        }
      //  Debug.Log(cowboyCount);
      //  Debug.Log(armyCount);
        destroyed++;
      //  Debug.Log(destroyed);
        Destroy(fallThrough, 1);
    }
}