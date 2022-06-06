using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownConditionScript : MonoBehaviour{
	// Vars
	public Button confirmButton;
	public Dropdown m_Dropdown;

    // Update is called once per frame
    void Update(){
    	// Get current trial duration
    	float trialDuration = Time.time - QP_controller.trialStart;

    	//get the selected index
		int menuIndex = m_Dropdown.value;

		//get all options available within this dropdown menu
        List<Dropdown.OptionData> menuOptions = m_Dropdown.options;

    	// Check conditions to make button interactable
        if(menuOptions[menuIndex].text != "" & trialDuration >= QP_controller.minDuration){
        	confirmButton.interactable = true;
        } else {
        	confirmButton.interactable = false;
        }
    }
}
