using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

/* Alright so this bad boy is the Game Manager.
 * It maintains all the gameplay stuff.
 * That means it keeps track of 
 * deaths, 
 * score,
 * time left in the game,✓ 
 * the two teams(?),
 * and more! Hooray!
 *  It also knows all the rules of the game.
 *  (Actually teams track their own score)
 */
public class GameManager : NetworkBehaviour {

    public int gameLength;      // Length, in seconds, of a match.
    public int teamSize;        // Number of players per team
    public int numberOfTeams;   // Number of teams (I think this is intended for FFA maybe?)
    public int goalScore;       // Score at which the team that achieves it becomes the victor.

    public Text timer;          // The UI Game Timer

    private int timeLeft;       // Time (in seconds) left in the match.
    private float endTime;      // Time (in ticks?) that the game will end if the goalScore is not reached.

	// THE NEXT FOUR VARIABLES ARE TO BE DELETED.
	public int teamOrangeScore;     // Team Orange's Score
	public PlayerBehavior player;   // A single player (???)
	public TestTurret enemy;        // A single enemy (???)
	public int teamBlueScore;       // Team Blue's score. 

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	// Begins the game.
	void Start () {
        endTime = Time.time + gameLength;   // The system time when the game will end.
		teamOrangeScore = 0;
		teamBlueScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRespawns ();
        UpdateTimer();          // Note: this part actually works.
		UpdateTeamScores ();
	}

    // Updates the overall game timer
    // and converts the time left in seconds to minutes and seconds.
    void UpdateTimer()
    {
        timeLeft = (int)(endTime - Time.time);
		if (timeLeft < 0) {
//			endGame ();
			return;
		}

        int minutes = timeLeft / 60;
        int seconds = timeLeft % 60;
        string time;
        if (seconds < 10)
            time = (minutes + ":0" + seconds);
        else
            time = (minutes + ":" + seconds);
        timer.text = (time);
    }

    // This is some real spaghetti code if I do say so myself.
	void UpdateTeamScores()
	{
		if (player.scorePing()) // So this is basically guaranteed to be a null reference.
			teamOrangeScore++;  // also i feel like team score should be stored somewhere else.
		if (enemy.scorePing())
			teamBlueScore++;
	}

	void UpdateRespawns()
	{
/*		player = GameObject.FindObjectOfType<PlayerBehavior> ();
		enemy = GameObject.FindObjectOfType<TestTurret>();
//		if (player = null)
//			Instantiate (playerPrefab, Vector3 (6.5, 0, 0), Quaternion.AngleAxis(0, Vector3.forward));
		if (enemy = null)
			Instantiate (enemyPrefab, Vector3 (-6.5, 0, 0), Quaternion.AngleAxis(0, Vector3.forward));
*/	}
}
