using UnityEngine;
using System.Collections;

/*
 *******************************
 ***** EXPLODING ASTEROIDS *****
 *******************************
 *
 * This is an example of how to access the explode function of an asteroid
 * Attach this script to an object that has the Asteroid component already attached
 * and press X or the key you select in the editor when running the game.
 */

public class ExplosionTest : MonoBehaviour {
	
	Asteroid a; // declare an object of type Asteroid
	public KeyCode kc = KeyCode.X; // For testing purposes select a key to press to make the asteroid explode
	
	void Start () {
		// Get the asteroid component. This gives a reference to the script to access its functions.
		a = GetComponent<Asteroid>();
		Debug.Log("Press "+kc.ToString()+" to see "+a.name+" explode.");
	}
	
	void Update () {
	
	}
	
	void OnGUI()
	{
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == kc)
		{
			Debug.Log("explode!");
			a.explode (); // when the key is pressed, this asteroid explodes.
		}
	}
}
