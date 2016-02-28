using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class masterScript : MonoBehaviour {


    private GroundHit groundHit;
    private DropZone dropZone;
    private PowerupSpawn powerupSpawn;
    private CraneController craneController;
    private int points;
    private float startTime, currentTime;

    public int BLUE_DEDUCTION, RED_DURATION;
    public float GREEN_TIME_ADDITION;
    public float durationTime;
    public Text scoreText, gameOverText, remainingTimeText;


	// Use this for initialization
	void Start () {
        startTime = Time.time;
        groundHit = GetComponentInChildren<GroundHit>();
        dropZone = GetComponentInChildren<DropZone>();
        powerupSpawn = GetComponentInChildren<PowerupSpawn>();
        craneController = GetComponentInChildren<CraneController>();

        gameOverText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;
        checkFlags();
        SetText();
	}

    void SetText()
    {
        scoreText.text = "Score: " + dropZone.score;
        Debug.Log("CURRENT: " + currentTime);
        Debug.Log("START:" + startTime);
        Debug.Log("DURATION:" + durationTime);
        remainingTimeText.text = "Time Remaining: " + (int)(durationTime + startTime - currentTime);
        if (currentTime - startTime > durationTime)
        {
            gameOverText.text = "GAME OVER!";
            remainingTimeText.text = "Time Remaining: 0";
            this.gameObject.SetActive(false);
        }
    }

    void checkFlags()
    {
        if (groundHit.blueOn) {
            groundHit.blueOn = false;
            dropZone.score -= BLUE_DEDUCTION;
        }
        else if (groundHit.greenOn){
            groundHit.greenOn = false;
            durationTime += GREEN_TIME_ADDITION;
        }  else if (groundHit.redOn){
            groundHit.greenOn = false;
            dropZone.doublePoints(RED_DURATION);
        }
    }
}