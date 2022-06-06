using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is a general script that only looks for the minimum time passed to make button interactable.
public class timeOnlyConditionScript : MonoBehaviour{
	// Vars
	public Button confirmButton;

    // Update is called once per frame
    void Update(){
    	// Get current trial duration
    	float trialDuration = Time.time - QP_controller.trialStart;

    	// Check conditions to make button interactable
        if(trialDuration >= QP_controller.minDuration){
        	confirmButton.interactable = true;
        } else {
        	confirmButton.interactable = false;
        }
    }
}
