using UnityEngine;
using System.Collections;

//Make sure an AudioSource component on the GameObject where the script is
//added.
[RequireComponent(typeof(AudioSource),typeof(Animation))]

public class Brick : MonoBehaviour
{

	public int timesHit;
	public int maxHit;
	private bool brickIsDestroyed = false;

	//Define audio clips and pitch
	public AudioClip sound;
	public float pitchStep = 0.05f;
	public float maxPitch = 1.3f;

	//Make the current pitch value global
	public static float pitch = 1f;

	//Falling variable
	public bool fallDown = false;
	[HideInInspector]
	public bool
		blockIsDestroyed = false;
	private Vector2 velocity = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
		timesHit = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (fallDown && velocity != Vector2.zero) {
			Vector2 pos = (Vector2)transform.position;
			pos += velocity * Time.deltaTime;
		}
	}

	void OnBecameInvisible ()
	{
		blockIsDestroyed = true;
		Destroy (gameObject);
	}

	IEnumerator OnCollisionExit2D (Collision2D c)
	{

		if (timesHit == maxHit) {
			print ("Destroyed on Exit");

			print ("Play Woggle animation");
			//Disable the 2D collider of the brick
			GetComponent<Collider2D> ().enabled = false;
			//Play the Woggle animation
			GetComponent<Animation> ().Play ();

			//Wait for the woggle animation to finish
			//before executing the next line
			yield return new WaitForSeconds (GetComponent<Animation> () ["Woggle"].length);

			//The woggle animation has finished, now
			//decide what to do, either fall down or
			//just dissapear
			if (fallDown) {
				print ("Brick is falling");
				//Falldown to the direction the ball hit it, with a random
				//speed and plus a little downwards "gravity"
				velocity = new Vector2 (0, Random.Range (1, 12.0f) * -1);
			} else {
				GetComponent<Renderer> ().enabled = false;
			}

			Destroy (gameObject);

		} else {
			print ("Max Hit is not reached!");
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		print ("Brick got hit!");
		timesHit++;

		//Increase the pitch
		pitch += pitchStep;

		if (pitch > maxPitch) {
			//Reset pitch back to 1
			pitch = 1;
		}

		//Apply the pitch
		GetComponent<AudioSource> ().pitch = pitch;

		//Play it once for this collision hit
		GetComponent<AudioSource> ().PlayOneShot (sound);

		StartCoroutine (OnCollisionExit2D (col));
	}
}
