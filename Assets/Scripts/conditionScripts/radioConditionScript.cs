using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// This script needs to be attached to each toggle group
public class radioConditionScript : MonoBehaviour{
	// Vars
	public Button confirmButton;
	private ToggleGroup current_toggleGroup;

    // Start is called before the first frame update
    void Start(){
        current_toggleGroup = this.GetComponent<ToggleGroup>();
    }

    // Update is called once per frame
    void Update(){
    	// Get current trial duration
    	float trialDuration = Time.time - QP_controller.trialStart;

    	// Check conditions to make button interactable
        if(current_toggleGroup.AnyTogglesOn() & trialDuration >= QP_controller.minDuration){
        	confirmButton.interactable = true;
        } else {
        	confirmButton.interactable = false;
        }
    }
}
