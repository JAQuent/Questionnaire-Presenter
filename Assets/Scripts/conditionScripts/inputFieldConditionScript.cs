using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is a general condition script for input fields
// The conditions are minimum duration and non-empty field
public class inputFieldConditionScript : MonoBehaviour{
	// Vars
	public Button confirmButton;
	public InputField m_inputField;

    // Update is called once per frame
    void Update(){
        // Get current trial duration
    	float trialDuration = Time.time - QP_controller.trialStart;

    	// Check conditions to make button interactable
        if(m_inputField.text != "" & trialDuration >= QP_controller.minDuration){
        	confirmButton.interactable = true;
        } else {
        	confirmButton.interactable = false;
        }
    }
}
