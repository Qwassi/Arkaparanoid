using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private Brick[] allBricks;
	private Ball[] allBalls;
	private Paddle paddle;
	public float Timer = 0.0f;
	private int minutes;
	private int seconds;
	public string formattedTime;

	// Use this for initialization
	void Start ()
	{
		GetComponent<AudioSource>().volume = 0.009f;
		Time.timeScale = 1;

		//Find all bricks in the game scene
		allBricks = FindObjectsOfType (typeof(Brick)) as Brick[];

		//Find all balls in the game scene
		allBalls = FindObjectsOfType (typeof(Ball)) as Ball[];

		//Find the paddle in the game scene
		paddle = GameObject.FindObjectOfType<Paddle> ();

		//Print out game objects info
		print ("Number of bricks: " + allBricks.Length);
		print ("Number of balls: " + allBalls.Length);
		print ("Paddle: " + paddle);

		//Change the UI Text in-game
		ChangeText ("Click to begin!");

		//Prepare the start of the level
		SwitchState (GameState.NotStarted);


	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (currentState) {

		case GameState.NotStarted:

			//Change the UI Text in-game
			ChangeText ("Click to begin!");

			//Checks if player clicks the mouse button
			if (Input.GetMouseButtonDown (0)) {
				SwitchState (GameState.Playing);
			}
			break;

		case GameState.Playing:

			Timer += Time.deltaTime;
			minutes = Mathf.FloorToInt (Timer / 60F);
			seconds = Mathf.FloorToInt (Timer - minutes * 60);
			formattedTime = string.Format ("{0:0}:{1:00}", minutes, seconds);

			ChangeText ("Time: " + formattedTime);
			//Uncomment below to check the number of fps
			//the game is running
			print (formattedTime);

			bool allBlocksDestroyed = false;

			if (FindObjectOfType (typeof(Ball)) == null) {
				SwitchState (GameState.Failed);
			}

			if (allBlocksDestroyed) {
				SwitchState (GameState.Completed);
			}
			break;

		case GameState.Failed:

			print ("Game Over!");
			ChangeText ("Game Over!");
			break;

		case GameState.Completed:

			bool allBlocksDestroyedFinal = false;

			Ball[] others = FindObjectsOfType (typeof(Ball)) as Ball[];

			//Destroy remaining balls
			foreach (Ball other in others) {
				Destroy (other.gameObject);
			}
			break;
		}
	}

	public void ChangeText (string text)
	{
		GameObject canvas = GameObject.Find ("Canvas");
		Text[] textValue = canvas.GetComponentsInChildren<Text>();
		textValue[0].text = text;
	}

	public void SwitchState (GameState newState)
	{
		currentState = newState;

		switch (currentState) {

		default:

		case GameState.NotStarted:

			break;

		case GameState.Playing:

			GetComponent<AudioSource> ().PlayOneShot (StartSound);
			break;

		case GameState.Completed:

			GetComponent<AudioSource> ().PlayOneShot (StartSound);
			break;

		case GameState.Failed:

			GetComponent<AudioSource> ().PlayOneShot (FailedSound);
			break;
		}
	}
}
