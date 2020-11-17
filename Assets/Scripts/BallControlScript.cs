using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControlScript : MonoBehaviour {

	Rigidbody2D rb;

	[Range(0.2f, 2f)]
	public float moveSpeedModifier = 0.5f;

	// Direction variables that read acceleration input to be added
	// as velocity to Rigidbody2d component
	float dirX, dirY;

	Animator anim;
	static bool isDead;
	static bool moveAllowed;
	static bool youWin;
	static bool keyPickedUp;
	static bool showKeyTip;

	[SerializeField]
	GameObject winText = default;
	[SerializeField]
	GameObject noKeyTip = default;
  [SerializeField]
	GameObject deathText = default;
  [SerializeField]
	GameObject keyUI = default;

	void Start () {
		winText.gameObject.SetActive(false);
		noKeyTip.gameObject.SetActive(false);
    deathText.gameObject.SetActive(false);
    keyUI.gameObject.SetActive(false);
		youWin = false;
		moveAllowed = true;
		isDead = false;
		keyPickedUp = false;
		showKeyTip = false;
		// Getting Rigidbody2D component of the ball game object
		rb = GetComponent<Rigidbody2D> ();
		// Getting Animator component of the ball game object
		anim = GetComponent<Animator> ();
		// Set BallAlive animation
		anim.SetBool ("BallDead", isDead);
	}

	// Update is called once per frame
	void Update () {
		// Getting devices accelerometer data in X and Y direction
		// multiplied by move speed modifier
		dirX = Input.acceleration.x * moveSpeedModifier;
		dirY = Input.acceleration.y * moveSpeedModifier;

		if (isDead) {
			rb.velocity = new Vector2 (0, 0);
      moveAllowed = false;
      keyUI.gameObject.SetActive(false);
      deathText.gameObject.SetActive(true);
			//anim.SetBool ("BallDead", isDead);
			Invoke ("RestartScene", 3f);
		} else if (youWin) {
      rb.velocity = new Vector2 (0, 0);
			winText.gameObject.SetActive(true);
			moveAllowed = false;
			//anim.SetBool("BallDead", true);
			// Restart scene to play again in 2 seconds
      keyUI.gameObject.SetActive(false);
			Invoke ("RestartScene", 6f);
		}
    if(keyPickedUp) {
      keyUI.gameObject.SetActive(true);
    }
    if(showKeyTip) {
			noKeyTip.gameObject.SetActive(true);
		}
	}

	void FixedUpdate()
	{
		// Setting a velocity to Rigidbody2D component according to accelerometer data
		if (moveAllowed)
			rb.velocity = new Vector2 (rb.velocity.x + dirX, rb.velocity.y + dirY);
	}

	// Method is invoked by DeathHoleScript when ball touches deathHole collider
	public static void setIsDeadTrue()
	{
		// Setting isDead to true
		isDead = true;
	}

	// Method is invoked by exit hole game object when ball thouches its collider
	public static void setYouWinToTrue()
	{
		youWin = true;
	}

	// Method to restart current scene
	void RestartScene()
	{
		SceneManager.LoadScene("Puzzle_Scene");
	}

	public static void PickUpKey()
	{
		keyPickedUp = true;
	}

	public static void OpenLocker()
	{
    showKeyTip = false;
		if (keyPickedUp)
		{
			youWin = true;
		} else
		{
			showKeyTip = true;
		}

	}
}
