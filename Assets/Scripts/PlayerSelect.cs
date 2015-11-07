using UnityEngine;
using System.Collections;

public static class PlayerSelect {
	static bool isFemale = false; // Static variable for is the player selects the female character
	static bool isMale = false; // Static variable for is the player selects the male character

	// Returns the value of the isFemale boolean
	public static bool getFemale() {
		return isFemale;
	}
	// sets isFemale to the boolean passed in
	public static void setFemale(bool b) {
		isFemale = b;
	}

	// Returns the value of the isMale boolean
	public static bool getMale() {
		return isMale;
	}
	// sets isMale to the boolean passed in
	public static void setMale(bool b) {
		isMale = b;
	}
}