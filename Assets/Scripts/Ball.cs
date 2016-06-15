using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

	public Paddle paddle;
	private bool gameStarted = false;
	private Vector3 paddleVector;

	// Use this for initialization
	void Start ()
	{
		//Put the paddle right under the ball during game start
		//by using relative to paddle position
		paddleVector = this.transform.position - paddle.transform.position;

		//Put the paddle right under the ball during game start
		//by using manually assigned values for the paddleVector
		//paddleVector = new Vector3(-0.4f, 0.3f, 0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gameStarted) {
			this.transform.position = paddle.transform.position + paddleVector;
			if (Input.GetMouseButtonDown (0)) {
				print ("Mouse clicked!");
				gameStarted = true;
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (2f, 10f);
			}
		}
	}
}
