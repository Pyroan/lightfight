using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Okay so a Team will hold an array of Players. It will also
 * keep track of it's own score, as well as each player's score.
 * 
 */
public class Team : MonoBehaviour {

	private PlayerBehavior[] players;

	private int score = 0;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        score = updateScore(score);
	}

    /* Updates the TEAM's score.
	 * I'll be honest I have literaly no idea
	 * if this will work because scorePing()
	 * is kind of in the aether.
     */
	int updateScore(int score) {
		int newScore = score;
		for (int i = 0; i < players.Length; i++)
		{
		//	newScore += players[i].score();
		}
		return newScore;
    }

	public int getScore()
	{
		return score;
	}
}
