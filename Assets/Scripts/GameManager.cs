using UnityEngine;
using System.Collections;

//All possible game states
public enum GameState
{
	NotStarted,
	Playing,
	Completed,
	Failed
}

//Require an audio source for the object
[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour
{
	//Sounds to be played when one of the game states is entered
	public AudioClip StartSound;
	public AudioClip FailedSound;

	//Starts the game with "NotStarted" game state
	private GameState currentState = GameState.NotStarted;

	//Number of all bricks and balls available in the game
	//Used to track how many of them are left
	private Brick[] allBrick;
	private Ball[] allBall;
	private Paddle paddle;

	public float Timer = 0.0f;
	private int minutes;
	private int seconds;
	public string formattedTime;

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
