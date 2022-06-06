using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//// This script can be attaced to any Game object and will controll the text of the UXF start up panel.
public class UXF_UI_controllScript : MonoBehaviour{
	// Public vars
	public string fileNameForStartUpText = "startupText.json";

    // You need to set-up all variables that you want to get from the .json file.
	// The variable names have to correspond to the input names in that file. 
	[System.Serializable]
    public class JSONDataClass {
    	public string chromeBar;
    	public string instructionsPanelContent1;
        public string instructionsPanelContent2;
        public string expSettingsprofile;
        public string localPathElement;
        public string localPathElement_placeHolder;
        public string participantID;
        public string participantID_placeholder;
        public string sessionNumber;
        public string termsAndConditions;
        public string beginButton;
    }

	// Private vars
	private Text chromeBar;
	private Text instructionsPanelContent1; // Heading
	private Text instructionsPanelContent2; // Main text
	private Text expSettingsprofile; 
	private Text localPathElement;
	private Text localPathElement_placeHolder;
	private Text participantID;
	private Text participantID_placeholder;
	private Text sessionNumber;
	private Text beginButton;
	private JSONDataClass JSONData;

    // Start is called before the first frame update
    void Start(){
    	// Get the Startup Text from the JSON file
    	GetDataFromJSON(fileNameForStartUpText);

    	////////////////////////////////////////////
    	// Go through all elements and update the text
    	chromeBar = GameObject.Find("Chrome Bar/Text").GetComponent<UnityEngine.UI.Text>();
    	chromeBar.text = JSONData.chromeBar; //"Startup";

    	instructionsPanelContent1 = GameObject.Find("Instructions Panel Content/Text (1)").GetComponent<UnityEngine.UI.Text>();
    	instructionsPanelContent1.text = JSONData.instructionsPanelContent1; //"Welcome to UXF! ";

    	instructionsPanelContent2 = GameObject.Find("Instructions Panel Content/Text (2)").GetComponent<UnityEngine.UI.Text>();
    	instructionsPanelContent2.text = JSONData.instructionsPanelContent2; //"You could use this space to display some instructions to the researcher or the participant.";

    	expSettingsprofile = GameObject.Find("SettingsElement/Title Box/Title").GetComponent<UnityEngine.UI.Text>();
    	expSettingsprofile.text = JSONData.expSettingsprofile; //"Experiment settings profile";

    	localPathElement = GameObject.Find("LocalPathElement/Title Box/Title").GetComponent<UnityEngine.UI.Text>();
    	localPathElement.text = JSONData.localPathElement; //"Local data save directory";

    	localPathElement_placeHolder = GameObject.Find("LocalPathElement/InputField/Placeholder").GetComponent<UnityEngine.UI.Text>();
    	localPathElement_placeHolder.text = JSONData.localPathElement_placeHolder; //"Press browse button to select...";

    	participantID = GameObject.Find("PPIDElement/Title Box/Title").GetComponent<UnityEngine.UI.Text>();
    	participantID.text = JSONData.participantID; //"Participant ID";

    	participantID_placeholder = GameObject.Find("PPIDElement/InputField/Placeholder").GetComponent<UnityEngine.UI.Text>();
    	participantID_placeholder.text = JSONData.participantID_placeholder; //"Enter text...";

    	sessionNumber = GameObject.Find("SessionNumDropdown/Title Box/Title").GetComponent<UnityEngine.UI.Text>();
    	sessionNumber.text = JSONData.sessionNumber; //"Session number";

    	// This has to be set-up in the UIController script because it would be overwritten otherwise.
    	var termsAndConditions = GameObject.Find("[UXF_UI]").GetComponent<UXF.UI.UIController>();
    	termsAndConditions.termsAndConditions = JSONData.termsAndConditions; //"Please tick if you understand the instructions and agree for your data to be collected and used for research purposes.<color=red>*</color>";

    	beginButton = GameObject.Find("Begin Button/Text").GetComponent<UnityEngine.UI.Text>();
    	beginButton.text = JSONData.beginButton; //"Begin session";
    }

    /// <summary>
    /// Method to read in the JSON file that is placed in the StreamingAssets folder for the file that is provided
    /// </summary>
    void GetDataFromJSON(string fileName){
    	// Get path
        string path2file = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath, fileName));

        // Get JSON input
        var sr = new StreamReader(path2file);
        var fileContents = sr.ReadToEnd();
        sr.Close();

        // Get instruction data profile
        JSONData = JsonUtility.FromJson<JSONDataClass>(fileContents);
    }
}
