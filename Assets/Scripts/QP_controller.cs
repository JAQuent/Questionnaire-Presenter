using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF; 
using System.IO;
using System.Linq;

public class QP_controller : MonoBehaviour{
	// Public static variable of trial start
	public static float trialStart; // Trail start (used by the conditition scripts).
	public static float minDuration = 0.0f; // Minimum duration for each question (used by the condition scripts).

	// UXF session
	public Session session;

    // EndScreen
    public GameObject endScreen;

    // HTTPpost script
    public UXF.HTTPPost HTTPPostScript;

	// Public vars
    [Header("Question types")]
    public GameObject message;
    public GameObject slider;
    public GameObject dropdown;
    public GameObject textShort;
    public GameObject textLong;
    public GameObject numberInteger;
    public GameObject numberDecimal;
    public GameObject radio;
    public GameObject checkmark;

	[Header("Other parameters")]
    public string splitCharacter = "|";

    // Private vars
    private Trial trial; // UXF variable to get the trial information.
    private string trialType; // What kind of trial (e.g. canvas) is to be used. 
    private GameObject currentCanvas; // The variable where the current canvas is saved. 
    private string questionText; // Variable for the question.
    private string options; // Variable where the options from the trial settings are saved.
    private int trialNum;
    private int totalTrialNum;
    private Dropdown m_Dropdown;
    private int numOptions; // Used to store the number of options (relevant for both radio & checkmark trials).
    private GameObject current_toggleGroup_gameObject; // Used to access the current toogle group on radio trials.
    private GameObject current_checkmark_gameObject; // Used to access the check mark toggles on checkmark trials.
    // End screen variables
    private bool startEndCountDown; // If true it starts the end countdown
    private float endCountDown = 60; // End countdown if zero, application closes. 
    private Text endScreenText; // Text component of the end screen
    private string endMessage; // String for the end message that is used.
    private bool useHTTPPost = false; // Is HTTPPost to be used? If so it needs input from the .json

    // Every update check if esacpe is pressed
    void Update(){
    	// Stop Experiment
        if(Input.GetKey(KeyCode.Escape)){
            // Log entry
            Debug.Log("Session manually ended " + System.DateTime.Now);

            // Close application
            TheEnd();
        }

        // End countdown
        if(startEndCountDown){
            endCountDown -= Time.deltaTime;
            endScreenText.text = endMessage + Mathf.Round(endCountDown);

            // Quit if end count down over
            if(endCountDown <= 0){
                Application.Quit();
            }
        }
    }

    /// <summary>
    /// Method to start the experiment. Attach to event "On Session Begin"
    /// </summary>
    public void StartExperiment(){
        // Get endCountDown & countdown message
        endCountDown = session.settings.GetFloat("endCountDown");
        endMessage = session.settings.GetString("endMessage");

        // Check if HTTPPost needs to be set.
        useHTTPPost = session.settings.GetBool("useHTTPPost");
        if(useHTTPPost){
            configureHTTPPost();
        }

        // Print system time
        Debug.Log("Session start time " + System.DateTime.Now);

        // Which platform is used?
        whichPlatform();

    	// Begin first trial
        session.BeginNextTrial(); 
    }

    /// <summary>
    /// Method to configure the HTTPPost script. Needs public UXF.HTTPPost HTTPPostScript;
    /// </summary>
    public void configureHTTPPost(){
        string url = session.settings.GetString("url");
        string username = session.settings.GetString("username");
        string password = session.settings.GetString("password");

        // Set the variables
        HTTPPostScript.url = url;
        HTTPPostScript.username = username;
        HTTPPostScript.password = password;
        HTTPPostScript.active = true;
    }

    /// <summary>
    /// Method to set-up the trial. Attach to event "On Trial Begin"
    /// </summary>
    public void SetUpTrial(){
    	// Get current trial
		trial = session.CurrentTrial;
		trialNum =  session.currentTrialNum;

		// General trial settings
		// Get current trial & general info
		trialType = trial.settings.GetString("trialType");
		questionText = trial.settings.GetString("question");
		minDuration = trial.settings.GetFloat("minimumDuration");

		// Select correct method
		if(trialType == "message"){
			SetUpTrial_message();
		} else if(trialType == "slider"){
			SetUpTrial_slider();
		} else if(trialType == "dropdown"){
			SetUpTrial_dropdown();
		} else if(trialType == "textShort"){
			SetUpTrial_textShort();
		} else if(trialType == "textLong"){
			SetUpTrial_textLong();
		} else if(trialType == "numberInteger"){
			SetUpTrial_numberInteger();
		} else if(trialType == "numberDecimal"){
			SetUpTrial_numberDecimal();
		} else if(trialType == "radio"){
			SetUpTrial_radio();
		} else if(trialType == "checkmark"){
			SetUpTrial_checkmark();
		} else {
			Debug.Log("ERROR: [" + trialType + "] is not one of response types available. Please choose one of: message, slider, dropdown, textShort, textLong, numberInteger, numberDecimal, radio & checkmark");
		}
    }

    /// <summary>
    /// Method to end trial. Attach to button
    /// </summary>
    public void EndTrial(){
    	// Log entry
    	Debug.Log("Confirm button clicked.");

    	// Add input variable to the results
    	session.CurrentTrial.result["trialType"] = trial.settings.GetString("trialType");
    	session.CurrentTrial.result["question"] = trial.settings.GetString("question").Replace(",", "_"); // Replace so it can be saved as .csv
    	session.CurrentTrial.result["options"] = trial.settings.GetString("options").Replace(",", "_"); // Replace so it can be saved as .csv
    	session.CurrentTrial.result["minimumDuration"] = trial.settings.GetFloat("minimumDuration");

    	////////////////////////////////////////////////////
    	// Save data according the trialType
		if(trialType == "slider"){
			// Save data
    		session.CurrentTrial.result["value"] = currentCanvas.transform.GetChild(3).gameObject.GetComponent<Slider>().value;
		} else if(trialType == "dropdown"){
			//get the selected index
			int menuIndex = m_Dropdown.value;

			//get all options available within this dropdown menu
        	List<Dropdown.OptionData> menuOptions = m_Dropdown.options;
 
       		//get the string value of the selected index
	        session.CurrentTrial.result["value"] = menuOptions[menuIndex].text;
		} else if(trialType == "textShort" | trialType == "textLong" | trialType == "numberInteger"  | trialType == "numberDecimal" ){
			session.CurrentTrial.result["value"] = currentCanvas.transform.Find("Input").gameObject.GetComponent<UnityEngine.UI.InputField>().text;
		} else if(trialType == "radio"){
			// Get the current toggle group
			ToggleGroup current_toogleGroup = current_toggleGroup_gameObject.GetComponent<ToggleGroup>();

			// Get the selected toogle
		    Toggle selectedToggle = current_toogleGroup.ActiveToggles().FirstOrDefault();
		    
		    // Get all individual toogles from this toogle group
		    var toogles = current_toogleGroup.GetComponentsInChildren<Toggle>();

		    // Loop through all toogles to find the one that is selected & set all inactive
		    int toogleIndex = -1;
		    for(int i = 0; i < toogles.Length; i++){
	    		if(toogles[i] == selectedToggle){
	    			toogleIndex = i + 1; // set toogleIndex to i + 1 
	    		}
	    		// Set inactive
	    		toogles[i].isOn = false;
    		}

            // Inactive the toggle group
            current_toggleGroup_gameObject.SetActive(false);
    		
    		// save to results
    		session.CurrentTrial.result["value"] = toogleIndex;
		} else if(trialType == "checkmark"){
			// Intialise result string
			string resultString = "";

			// Loop through all options by checking if one and then set to inactive
			for(int i = 0; i < numOptions; i++){
	    		// Get game object for the checkmark point
	    		GameObject checkmarkPoint = current_checkmark_gameObject.transform.GetChild(i).gameObject;

	    		// Get the toggle of that checkmark point
	    		Toggle selectedToggle = checkmarkPoint.GetComponent<Toggle>();

	    		// Add delimiter if it not zero
	    		if(i > 0){
	    			resultString = resultString + "|";
	    		} 

	    		// Add true/false for each checkmark toogle
	    		resultString = resultString + selectedToggle.isOn;

	    		// Set unselected
	    		selectedToggle.isOn = false;

	    		// Set inactive
	    		checkmarkPoint.SetActive(false);
    		}

    		// save to results
    		session.CurrentTrial.result["value"] = resultString;
		}
		////////////////////////////////////////////////////

		// Deactivate the current canvas
    	currentCanvas.SetActive(false);

		// Check if last trial & if not start next question
		if(trial != trial.session.LastTrial){
			// End trial
			session.EndCurrentTrial();

			// Begin new trial
			session.BeginNextTrial();
			Debug.Log("Next trial started.");
		} else {
			// End trial
			session.EndCurrentTrial();
		}
    }

    /// <summary>
    /// This sets up a message trial.
    /// </summary>
    void SetUpTrial_message(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = message;
		currentCanvas.SetActive(true);

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");
    }

    /// <summary>
    /// This sets up a slider trial.
    /// </summary>
    void SetUpTrial_slider(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = slider;
		currentCanvas.SetActive(true);

        // Get information from options
        options = trial.settings.GetString("options");

        // Get the options from the string
        List<string> anchorStrings = splitString2list(options, splitCharacter);

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Set up the anchors
		Text leftAnchor = currentCanvas.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>();
        leftAnchor.text = anchorStrings[0].Replace("\\n", "\n");
        Text rightAnchor = currentCanvas.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>();
        rightAnchor.text = anchorStrings[1].Replace("\\n", "\n");

        // Reset value
        currentCanvas.transform.GetChild(3).gameObject.GetComponent<Slider>().value = 0.5f;
    }

    /// <summary>
    /// This sets up a dropdown trial.
    /// </summary>
    void SetUpTrial_dropdown(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = dropdown;
		currentCanvas.SetActive(true);

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Get options from trial setting
		string options = trial.settings.GetString("options");

		// Get the options from the string
    	List<string> dropdownOptions = splitString2list(options, splitCharacter);

        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = currentCanvas.transform.GetChild(1).gameObject.GetComponent<Dropdown>();

        //Clear the old options of the Dropdown menu
       	m_Dropdown.ClearOptions();

       	// Add empty option at the beginning, which is used as the default & for the condition script.
       	m_Dropdown.AddOptions(new List<string>{""});

        //Add the options created in the List above
        m_Dropdown.AddOptions(dropdownOptions);
    }

    /// <summary>
    /// This sets up a textShort trial.
    /// </summary>
    void SetUpTrial_textShort(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = textShort;
		currentCanvas.SetActive(true);

        // Reset input field
        currentCanvas.transform.Find("Input").gameObject.GetComponent<UnityEngine.UI.InputField>().text = "";

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Get the placeholder (saved in options) from the trial settings
		options = trial.settings.GetString("options");

		// Set up place holder by getting first child of second child of the game object
		Text placeholder = currentCanvas.transform.GetChild(1).Find("Placeholder").gameObject.GetComponent<UnityEngine.UI.Text>();
		placeholder.text = ""; // Reset in case it is empty
        placeholder.text = options;
    }

    /// <summary>
    /// This sets up a textShort trial.
    /// </summary>
    void SetUpTrial_textLong(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = textLong;
		currentCanvas.SetActive(true);

        // Reset input field
        currentCanvas.transform.Find("Input").gameObject.GetComponent<UnityEngine.UI.InputField>().text = "";

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Get the placeholder (saved in options) from the trial settings
        options = trial.settings.GetString("options");

		// Set up place holder by getting first child of second child of the game object
		Text placeholder = currentCanvas.transform.GetChild(1).Find("Placeholder").gameObject.GetComponent<UnityEngine.UI.Text>();
		placeholder.text = ""; // Reset in case it is empty
        placeholder.text = options;
    }

    /// <summary>
    /// This sets up a numberInteger trial.
    /// </summary>
    void SetUpTrial_numberInteger(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = numberInteger;
		currentCanvas.SetActive(true);

        // Reset input field
        currentCanvas.transform.Find("Input").gameObject.GetComponent<UnityEngine.UI.InputField>().text = "";

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Get the placeholder (saved in options) from the trial settings
        options = trial.settings.GetString("options");

		// Set up place holder by getting first child of second child of the game object
		Text placeholder = currentCanvas.transform.GetChild(1).Find("Placeholder").gameObject.GetComponent<UnityEngine.UI.Text>();
		placeholder.text = ""; // Reset in case it is empty
        placeholder.text = options;
    }

     /// <summary>
    /// This sets up a numberDecimal trial.
    /// </summary>
    void SetUpTrial_numberDecimal(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = numberDecimal;
		currentCanvas.SetActive(true);

        // Reset input field
        currentCanvas.transform.Find("Input").gameObject.GetComponent<UnityEngine.UI.InputField>().text = "";

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Get the placeholder (saved in options) from the trial settings
        options = trial.settings.GetString("options");

		// Set up place holder by getting first child of second child of the game object
		Text placeholder = currentCanvas.transform.GetChild(1).Find("Placeholder").gameObject.GetComponent<UnityEngine.UI.Text>();
		placeholder.text = ""; // Reset in case it is empty
        placeholder.text = options;
    }

    /// <summary>
    /// This function takes a string and splits it into list based on a character. Needs using System.Linq;
    /// </summary>
    List<string> splitString2list(string string2split, string splitCharacter){
    	// Needs using System.Linq;
    	List<string> list = new List<string>();
        list = string2split.Split(splitCharacter).ToList();
        return list;
    }

    /// <summary>
    /// This sets up a radio trial.
    /// </summary>
    void SetUpTrial_radio(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = radio;
		currentCanvas.SetActive(true);

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Get options from trial setting
		string options = trial.settings.GetString("options");

		// Get the options from the string
    	List<string> radioOptions = splitString2list(options, splitCharacter);

    	// Get number of options
    	numOptions = radioOptions.Count;

    	// Select the correcht toggle group
    	int toogleIndex = numOptions - 2; // Hacky way: Because the lowest toogle group has 3 options and the question comes before.
    	current_toggleGroup_gameObject = currentCanvas.transform.GetChild(toogleIndex).gameObject;

    	// Activate the toogleGroup
    	current_toggleGroup_gameObject.SetActive(true);

    	// Loop throup all options & add the correct label to the toogles
    	for(int i = 0; i < numOptions; i++){
    		Text label = current_toggleGroup_gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>();
    		label.text = radioOptions[i].Replace("\\n", "\n");
    	}
    }

    /// <summary>
    /// This sets up a checkmark trial.
    /// </summary>
    void SetUpTrial_checkmark(){
    	// Set current canvas so it can be deactivated later & activate
		currentCanvas = checkmark;
		currentCanvas.SetActive(true);

		// Set the question text
		Text question = currentCanvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        question.text = ""; // Reset in case there is no question
		question.text = questionText.Replace("\\n", "\n");

		// Get options from trial setting
		string options = trial.settings.GetString("options");

		// Get the options from the string
    	List<string> checkmarkOptions = splitString2list(options, splitCharacter);

    	// Get number of options
    	numOptions = checkmarkOptions.Count;

    	// Use the checkmarkQuestion parent object
    	current_checkmark_gameObject = currentCanvas.transform.GetChild(1).gameObject;

    	// Activate it
    	current_checkmark_gameObject.SetActive(true);

    	// Loop throup all options & add the correct label to the checkmarks
    	for(int i = 0; i < numOptions; i++){
    		// Get game object for the checkmark point
    		GameObject checkmarkPoint = current_checkmark_gameObject.transform.GetChild(i).gameObject;

    		// Set active
    		checkmarkPoint.SetActive(true);

    		// Get text label
    		Text label = checkmarkPoint.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>();
    		label.text = checkmarkOptions[i];
    	}
    }

    /// <summary>
    /// Function to end application. This needs to be attached to the On Session End Event of the UXF Rig.
    /// </summary>
    public void TheEnd(){
        // If useHTTPPost not used than quit immediately
        if(!useHTTPPost){
            Debug.Log("Application closed now.");
            Application.Quit();
        }

        // End session/trial if necessary
        if(session.InTrial){
            // End the trial
            session.EndCurrentTrial();  
        }
        if(!session.isEnding){
            // End the session
            session.End();
        }

        // Set end screen active but current canvas inactive
        endScreen.SetActive(true);
        currentCanvas.SetActive(false);

        // Start end countdown
        startEndCountDown = true;

        // Get text
        endScreenText = endScreen.transform.Find("Text").gameObject.GetComponent<Text>();
    }


    /// <summary>
    /// Start trial timer
    /// </summary>
    public void startTrialTimer(){
    	// Log trime
    	trialStart = Time.time;
    }

    /// <summary>
    /// Method to log which platform is used. # More info here https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
    /// </summary>
    void whichPlatform(){
        #if UNITY_EDITOR
            Debug.Log("Platform used is UNITY_EDITOR");
        #elif UNITY_STANDALONE_OSX
            Debug.Log("Platform used is UNITY_STANDALONE_OSX");
        #elif UNITY_STANDALONE_WIN
            Debug.Log("Platform used is UNITY_STANDALONE_WIN");
        #endif
    }
}