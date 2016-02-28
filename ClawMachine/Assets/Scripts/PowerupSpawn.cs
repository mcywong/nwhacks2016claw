using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerupSpawn : MonoBehaviour
{

    public GameObject FreezeBlue;
    public GameObject TimeGreen;
    public GameObject FeverRed;

    public float xmin;
    public float xmax;
    public float zmin;
    public float zmax;
  //  public int i;
 //   public float gameTime;

 //   private float startTime;
    private float currentTime;
    private int powerupnumber;
    public int numofpowers;
 //   public Text powerup;
 //   public Text checkingtime;
 //   public Text testingloop;
    private int accessflag;
    
    // Use this for initialization
    void Start()
    {
 //       startTime = Time.Time; 
 //       powerup.text = "";
 //       checkingtime.text = "";
 //       testingloop.text = "";
 //       i = 0;
        accessflag = 0;
    }

    void Update()
    {
        currentTime = Time.time;

       /*currentTime - startTime > gameTime)
        {
            displayEnd();

        }*/

//        checkingtime.text = "currenttime:" + currentTime.ToString();
        if (accessflag == 1)
        {
            if ((int)currentTime % 5 == 1)
                accessflag = 0;
        }
    
        if ( ((int)currentTime % 5 == 0) && accessflag == 0)
        {
            accessflag = 1;
//            i++;
//           testingloop.text = "#timeacc:" + i.ToString();
            powerupnumber = Random.Range(0, numofpowers);
            SpawnPower(powerupnumber);

        }
    }



    void SpawnPower(int n) {
        Vector3 spawnPosition = new Vector3(Random.Range(xmin, xmax), 83.7f, Random.Range(zmin, zmax));
        Quaternion spawnRotation = Quaternion.identity;
        if (n == 0)
            Instantiate(FreezeBlue, spawnPosition, spawnRotation);
        else if (n == 1)
            Instantiate(TimeGreen, spawnPosition, spawnRotation);
        else
            Instantiate(FeverRed, spawnPosition, spawnRotation);
    }
}