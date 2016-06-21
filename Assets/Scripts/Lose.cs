using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour
{

	private Ball ball;
	private GameManager gameManager;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	//Custom coroutine method
	IEnumerator Pause ()
	{
		print ("Before waiting for 2 seconds");
		gameManager = FindObjectOfType<GameManager>();
		gameManager.SwitchState(GameState.Failed);

		//The yield statement is a special kind of return, 
		//that ensures that the function will continue from 
		//the line AFTER the yield statement NEXT TIME IT IS CALLED.
		yield return new WaitForSeconds (2);

		//Find the ball and reset game start
		ball = GameObject.FindObjectOfType<Ball> ();
		ball.gameStarted = false;

		//Reload game scene
		Application.LoadLevel (Application.loadedLevel);

		print ("After waiting for 2 seconds");

	}

	void OnTriggerEnter2D (Collider2D trigger)
	{
		print ("Bottom collider triggered!");

		//Wait before restarting level
		StartCoroutine(Pause ());
	}
}
