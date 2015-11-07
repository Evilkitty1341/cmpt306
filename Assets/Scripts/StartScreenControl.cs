using UnityEngine;
using System.Collections;

public class StartScreenControl : MonoBehaviour {
	bool isStartButton = false;
	bool isQuitButton = false;
	GameObject female; 
	GameObject male;

	// Called whenever a mouse collides with something on the screen
	void OnMouseEnter() {
		female = GameObject.FindWithTag("Female"); // If the mouse is over the female character, save the game object
		male = GameObject.FindWithTag("Male"); // If the mouse is over the  male character, save the  game object

		// Check if the object is text
		if (transform.GetComponent<TextMesh> () != null) {
			// When the mouse os hovering over text, change its color to green
			transform.GetComponent<TextMesh> ().color = Color.green;
			// If hovering over the "Start Game" or "Quit" button, set their respective variables to true
			if (transform.GetComponent<TextMesh> ().text == "Start Game")
				isStartButton = true;
			if (transform.GetComponent<TextMesh> ().text == "Quit") 
				isQuitButton = true;
		}

		// Check if the object is a sprite
		if (transform.GetComponent<SpriteRenderer> () != null)
			// If the mouse os over a sprite, change its color to green
			transform.GetComponent<SpriteRenderer> ().color = Color.green;
	}

	// Called whenever the mouse leaves a collidable area/button
	void OnMouseExit() {
		// Check if the object is text, and if it is, change its color back to white and set respective value to false
		if (transform.GetComponent<TextMesh> () != null) {
			transform.GetComponent<TextMesh> ().color = Color.white;
			if (transform.GetComponent<TextMesh> ().text == "Start Game")
				isStartButton = false;
			if (transform.GetComponent<TextMesh> ().text == "Quit")
				isQuitButton = false;
		}
		// Check if the object is a sprite, and if it is, change its color back to white
		if (transform.GetComponent<SpriteRenderer> () != null) {
			if (tag == "Female" && !PlayerSelect.getFemale())
				transform.GetComponent<SpriteRenderer> ().color = Color.white;
			if (tag == "Male" && !PlayerSelect.getMale())
				transform.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

	// When the mouse button is clicked and then released, this gets called
	void OnMouseUp() {	
		// Check if the player has selected the female character
		if (tag == "Female") {
			// Call the function to set the static methods for which character has been seleced 
			// and ensure that only the female is selected and the male is not
			PlayerSelect.setFemale(!PlayerSelect.getFemale());
			PlayerSelect.setMale(false);
			// Set male color back to white in case this had been previously selected and the player changed their mind
			male.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		// Check if the player has selected the male character
		if (tag == "Male") {
			// Call the function to set the static methods for which character has been seleced 
			// and ensure that only the male is selected and the female is not
			PlayerSelect.setMale(!PlayerSelect.getMale());
			PlayerSelect.setFemale(false);
			// Set female color back to white in case this had been previously selected and the player changed their mind
			female.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		// The player can select the "Quit" button at any point to exit the application
		// If the player wants to start the game, ensure a character has been chosen and load the corresponding level
		if (isStartButton && PlayerSelect.getFemale ()) {
			Application.LoadLevel (1);
		} else if (isStartButton && PlayerSelect.getMale ()) {
			Application.LoadLevel (2);
		} else if (isQuitButton) {
			Application.Quit ();
		}
	}
}
