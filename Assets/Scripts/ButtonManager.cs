using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour {

	public GameObject Object;
	
	public GameObject Other1;
	
	public GameObject Other2;

	private bool showing;



	// Update is called once per frame
	public void OnClick () {

		
	
			//  check if the GUI is actived
			showing = Object.activeInHierarchy;

			//set showing to true if false, if false turn it to true
			showing = !showing;
			
			Object.SetActive (showing);
			
			//if other GUI actived turn it off
			if (Other1.activeInHierarchy) {
				Other1.SetActive (false);
				
			}
			
			if (Other2.activeInHierarchy) {
				Other2.SetActive (false);
			}
			
			
			
			

	}
}

