<p align="center">
  <img src="nonUnity_folder/logo.png" width="100" height="100" >
</p>

# Questionnaire Presenter
is a free-standing application built with Unity3D (version 2021.3.1f1c1) and the [Unity Experiment Framework](https://github.com/immersivecognition/unity-experiment-framework) that allows you to quickly & easily run questionnaires on a computer. The user does not need to have any programming experience or even have Unity3D install on the target machine. The questionnaire is constructed by providing a simple .txt file with tab-separated values, which can be created with spreadsheet program like Excel or LibreOffice. The questionnaire then presents each question as a single item on a screen. As of now, multi-item presentations are  not supported (If that is what you need, please check out [VRQuestionnaireToolkit](https://github.com/MartinFk/VRQuestionnaireToolkit)). Questionnaire Presenter was designed to be was lightweight and as simple as possible so I decided to restrict it to one question per page. This allows for a uniquely simple input system that is used to construct the questionnaire. 

**Contents**
- [General introduction](#General-introduction)
- [Question types](#Question-types)
  - [Message](#Message)
  - [Slider](#Slider)
  - [Dropdown](#Dropdown)
  - [TextShort](#TextShort)
  - [TextLong](#TextLong)
  - [NumberInterger](#NumberInterger)
  - [NumberDecimal](#NumberDecimal)
  - [Radio](#Radio)
  - [Checkmark](#Checkmark)
- [Example .txt file](#Example_.txt_file)
- [Customisation using a .json file to the UXF Start-Up Panel](#Customisation-using-a-.json-file-to-the-UXF-Start-Up-Panel)
- [Data saved](#Data-saved)
- [Ideas for the future](#Ideas-for-the-future)

# General introduction

As explained above, you simply need to copy your .txt file with tab-separate values into the `StreamingAssets` folder of the the program (the exactly depends on whether you use the Window or the Mac version of Questionnaire Presenter). The .txt must contains the following columns (please note that the names of the columns need to match exactly including being case-sensitive).

- `trialType`
- `question`
- `options`
- `minimumDuration`

Optional but relevant for UXF (see UXF's website for more details), which will determine the order in which the questions are presented:

- `trial_num`
- `block_num`

All columns are included in the result file that is saved, which also includes any other columns that are provided but not needed to construct the questionnaire. For instance, one might want to include question IDs for simpler analysis. In the following, I quickly introduce the different question types, which are provide as the `trialType`.

# Question types
## Message
- `trialType` = message
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` = NA
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## Slider
- `trialType` = slider
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` = Left and right anchors are provided with a `|` in the middle. For example:  "agree|disagree"
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## Dropdown
- `trialType` = slider
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` = Dropdown options are provided with a `|`  between each item (unlimited number). For example: "yes|no|maybe"
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## TextShort
- `trialType` = textShort
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` =  Place holder text. For example: "Enter your text here..."
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## TextLong
- `trialType` = textLong
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` =  Place holder text. For example: "Enter your text here..."
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## NumberInteger
- `trialType` = numberInteger
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` =  Place holder text. For example: "Enter the number here..."
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## NumberDecimal
- `trialType` = numberDecimal
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` =  Place holder text. For example: "Enter the number here..."
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## Radio
- `trialType` = radio
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` =  Radio options are provided with a `|`  between each item (limit is 7 options, minimum is 3). For example: "agree||disagree". `\n` can be used for these. 
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 

## Checkmark
- `trialType` = checkmark
- `question` = string included your message. `\n` Can be used a line break character. 
- `options` =  Checkmark options are provided with a `|`  between each item (limit is 9 options). For example: "windows|mac|unix/linux"
- `minimumDuration` = Minimum duration in seconds you want your participants to spend on this item before they can continue. Default is always zero seconds. 


# Example .txt file
| trial_num | block_num | trialType | question                      | options                 | minimumDuration |
| --------- | --------- | --------- | ----------------------------- | ----------------------- | --------------- |
| 1         | 1         | message   | Welcome to this questionnaire | NA                      | 3               |
| 2         | 2         | slider    | Do you like this program?     | yes\|no                 | 3               |
| 3         | 2         | textLong  | Tell me your life story.      | Type life story here... | 3               |
| 4         | 3         | dropdown  | Which platform did you use?   | Windows\|Mac            | 3               |

# Further input through session .json file
The most important input comes in form of the `trial_specification_name`, which specifies the .txt file that is used to construct the questionnaire. `targetFrameRate` allows you to set a maximal number to cap the frames per second, which might be helpful to reduce between device variability if so desired. `alignment` allows you to change the text alignment with text anchors. (see [here](https://docs.unity3d.com/2019.1/Documentation/ScriptReference/TextAnchor.html) for more information). The other input are really only relevant if HTTPPost (see [here for an explanation](https://github.com/immersivecognition/unity-experiment-framework/wiki/HTTP-POST-setup)) is used to collect data remotely. If you want to do that, you can specify the relevant server by setting url, username & password. If the application is closed to early, it is possible that the data is not completely send to the server which can lead to data loss. Therefore, if HTTPPost is used, a end screen counting down from `endCountDown` along with the message specified in `endMessage` is presented, which will wait with closing the application until the time is over. It is probably advisable to test how much time you need so that your data is actually saved. Note the name of this file must start with `QP_` in order to be recognised by UXF.

```json
{
  "trial_specification_name": "QP_input.txt",
  "targetFrameRate": 60,
  "alignment": "LowerCenter", 
  "endCountDown" : 60,
  "endMessage": "Thank you for completing the task.\n\nPlease wait briefly before you close the application.\n\nSeconds remaining: ",
  "useHTTPPost": false,
  "url": "http://127.0.0.1:5000/form",
  "username": "susan",
  "password": "hello"
}
```

# Customisation of the UXF Startup panel using a .json file 
The UXF Startup panel can be customised by changing values in this the .json file called `startupText.json` that is also found the `StreamingAssets` folder and looks like this:

```json
{
    "chromeBar": "Questionnaire Presenter (version 0.1)",
    "instructionsPanelContent1": "Welcome to this part of this study",
    "instructionsPanelContent2": "You can add instructions here.",
    "expSettingsprofile": "Experiment settings profile",
    "localPathElement": "Local data save directory",
    "localPathElement_placeHolder": "Press browse button to select...",
    "participantID": "Participant ID",
    "participantID_placeholder": "Enter text...",
    "sessionNumber": "Session number",
    "termsAndConditions": "Please tick if you understand the instructions and agree for your data to be collected and used for research purposes.<color=red>*</color>",
    "beginButton": "Begin session"
}
```

All that needs to be done is to edit the strings in this file to display your information instead of the standard text. 

# Data saved
_Explanation coming soon_.

Note due to UXF internal workings “,” in responses & input are converted into “\_”.

## Log 

In the log you find the time stamps for every button press and every time the minimum duration is over. Furthermore, it provides information but which platform is used (Windows/Mac) and at which system time the experiment started. 

# Ideas for the future
- Participant check list
- Change background on trials? To indicate different questionnaires. 
- Currently, any extra columns are not copied to the data so that during analysis supplementary information has to be added. Even though it would be a useful feature because one could question IDs and many other things, the issue is the fact that QP allows "," to be used in the strings for the questions etc. interferes with the `trial_results.csv`. For now, I therefore simply disabled this function and suggest to add this information at a later stage. 
