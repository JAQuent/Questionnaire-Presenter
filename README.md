Questionnaire Presenter - A standalone program to collect questionnaire
data made with Unity3D
================

[![DOI](https://zenodo.org/badge/500255247.svg)](https://zenodo.org/badge/latestdoi/500255247)

<p align="center">
<img src="nonUnity_folder/logo.png" width="100" height="100" >
</p>

Questionnaire Presenter is a free-standing application built with
Unity3D (version 2021.3.1f1c1) and the [Unity Experiment
Framework](https://github.com/immersivecognition/unity-experiment-framework)
that allows you to quickly & easily run questionnaires on a computer.
The user does not need to have any programming experience or even have
Unity3D install on the target machine. The questionnaire is constructed
by providing a simple .txt file with tab-separated values, which can be
created with spreadsheet program like Excel or LibreOffice. The
questionnaire then presents each question as a single item on a screen.
As of now, multi-item presentations are not supported (If that is what
you need, please check out
[VRQuestionnaireToolkit](https://github.com/MartinFk/VRQuestionnaireToolkit)).
Questionnaire Presenter was designed to be as lightweight and as simple
as possible so I decided to restrict it to one question per page. This
allows for a uniquely input system that is simple can be used to
construct the questionnaire.

Illustration how the application looks like:

<https://user-images.githubusercontent.com/17894303/194999455-ff7a3c1b-b1c9-4217-8cc3-6de378d22856.mp4>

**Contents**

-   [General introduction](#General-introduction)
-   [Question types](#Question-types)
    -   [Message](#Message)
    -   [Slider](#Slider)
    -   [Dropdown](#Dropdown)
    -   [TextShort](#TextShort)
    -   [TextLong](#TextLong)
    -   [NumberInterger](#NumberInterger)
    -   [NumberDecimal](#NumberDecimal)
    -   [Radio](#Radio)
    -   [Checkmark](#Checkmark)
-   [Example input files](#Example-input-files)
-   [Data saved](#Data-saved)
    -   [Main results](#Main-results)
    -   [Log](#log)
    -   [Tracker](#Tracker)
-   [Ideas for the future](#Ideas-for-the-future)
-   [How to cite](#How-to-cite)

# General introduction

As explained above, you simply need to copy your .txt file with
tab-separate values into the `StreamingAssets` folder of the the program
(the exactly depends on whether you use the Window or the Mac version of
Questionnaire Presenter). The .txt must contains the following columns
(please note that the names of the columns need to match exactly
including being case-sensitive).

-   `trialType`
-   `question`
-   `options`
-   `minimumDuration`

Optional but relevant for UXF (see UXF’s website for more details),
which will determine the order in which the questions are presented:

-   `trial_num`
-   `block_num`

All columns are included in the result file that is saved, which also
includes any other columns that are provided but not needed to construct
the questionnaire. For instance, one might want to include question IDs
for simpler analysis. In the following, I quickly introduce the
different question types, which are provide as the `trialType`.

# Question types

## Message

-   `trialType` = message
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = NA
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: NA

## Slider

-   `trialType` = slider
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Left and right anchors are provided with a `|` in the
    middle. For example: “agree\|disagree”
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: Value between 0 & 1.

## Dropdown

-   `trialType` = slider
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Dropdown options are provided with a `|` between each
    item (unlimited number). For example: “yes\|no\|maybe”
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: String of the chosen menu item.

## TextShort

-   `trialType` = textShort
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Place holder text. For example: “Enter your text here…”
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: String of typed response.

## TextLong

-   `trialType` = textLong
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Place holder text. For example: “Enter your text here…”
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: String of typed response.

## NumberInteger

-   `trialType` = numberInteger
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Place holder text. For example: “Enter the number here…”
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: The integer that was typed in.

## NumberDecimal

-   `trialType` = numberDecimal
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Place holder text. For example: “Enter the number here…”
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: The decimal number that was typed in.

## Radio

-   `trialType` = radio
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Radio options are provided with a `|` between each item
    (limit is 7 options, minimum is 3). For example:
    “agree\|\|disagree”. `\n` can be used for these.
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: The ordinal number (from left to right) of the chosen
option.

## Checkmark

-   `trialType` = checkmark
-   `question` = string included your message. `\n` Can be used a line
    break character.
-   `options` = Checkmark options are provided with a `|` between each
    item (limit is 9 options). For example: “windows\|mac\|unix/linux”
-   `minimumDuration` = Minimum duration in seconds you want your
    participants to spend on this item before they can continue. Default
    is always zero seconds.

**Return**: True & False separated by `|` matching with the options.

# Example input files

Example input files can be found in the `example` folder.

## Example `QP_input.txt` file

| trial_num | block_num | trialType | question                      | options               | minimumDuration |
|-----------|-----------|-----------|-------------------------------|-----------------------|-----------------|
| 1         | 1         | message   | Welcome to this questionnaire | NA                    | 3               |
| 2         | 2         | slider    | Do you like this program?     | yes\|no               | 3               |
| 3         | 2         | textLong  | Tell me your life story.      | Type life story here… | 3               |
| 4         | 3         | dropdown  | Which platform did you use?   | Windows\|Mac          | 3               |

# Further input through session .json file

The most important input comes in form of the
`trial_specification_name`, which specifies the .txt file that is used
to construct the questionnaire. `targetFrameRate` allows you to set a
maximal number to cap the frames per second, which might be helpful to
reduce between device variability if so desired. `alignment` allows you
to change the text alignment with text anchors. (see
[here](https://docs.unity3d.com/2019.1/Documentation/ScriptReference/TextAnchor.html)
for more information). `confirmButtonLabel` is used to change the label
of the confirmation button.

The other input are really only relevant if HTTPPost (see [here for an
explanation](https://github.com/immersivecognition/unity-experiment-framework/wiki/HTTP-POST-setup))
is used to collect data remotely. If you want to do that, you can
specify the relevant server by setting url, username & password. If the
application is closed to early, it is possible that the data is not
completely send to the server which can lead to data loss. Therefore, if
HTTPPost is used, a end screen counting down from `endCountDown` along
with the message specified in `endMessage` is presented, which will wait
with closing the application until the time is over. It is probably
advisable to test how much time you need so that your data is actually
saved. Note the name of this file must start with `QP_` in order to be
recognised by UXF.

``` json
{
  "trial_specification_name": "QP_input.txt",
  "targetFrameRate": 60,
  "alignment": "LowerCenter", 
  "confirmButtonLabel": "Confirm",
  "endCountDown" : 60,
  "endMessage": "Thank you for completing the task.\n\nPlease wait briefly before you close the application.\n\nSeconds remaining: ",
  "useHTTPPost": false,
  "url": "http://127.0.0.1:5000/form",
  "username": "susan",
  "password": "hello"
}
```

## Customisation of the UXF Startup panel using a .json file

The UXF Startup panel can be customised by changing values in this the
.json file called `startupText.json` that is also found the
`StreamingAssets` folder and looks like this:

``` json
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

All that needs to be done is to edit the strings in this file to display
your information instead of the standard text.

# Data saved

## Main results

Here is quick guide what data is saved by Questionnaire Presenter. For
more information please also check [Unity Experiment
Framework](https://github.com/immersivecognition/unity-experiment-framework)\]
but the main result can be found in `trail_results.csv`. Here is the
data that was generated in the video shown above;

``` r
# Libraries
library(knitr)

# Load main results
data <- read.csv("QP_my_experiment/subject1/S001/trial_results.csv", header = TRUE)

# Display as table 
kable(data)
```

| experiment       | ppid     | session_num | trial_num | block_num | trial_num_in_block | start_time | end_time | trialType     | question                                                                       | options                                                                                  | minimumDuration | value                                                     | controller_mouse_screen_location_0                                       |
|:-----------------|:---------|------------:|----------:|----------:|-------------------:|-----------:|---------:|:--------------|:-------------------------------------------------------------------------------|:-----------------------------------------------------------------------------------------|----------------:|:----------------------------------------------------------|:-------------------------------------------------------------------------|
| QP_my_experiment | subject1 |           1 |         1 |         1 |                  1 |   10.53183 | 17.72592 | message       | This is message with a comma\_ which is something you can use in the question. | NA                                                                                       |               2 |                                                           | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T001.csv |
| QP_my_experiment | subject1 |           1 |         2 |         1 |                  2 |   17.72592 | 23.28396 | slider        | Is this useful?                                                                | yes\|no                                                                                  |               2 | 0.104167                                                  | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T002.csv |
| QP_my_experiment | subject1 |           1 |         3 |         1 |                  3 |   23.28396 | 29.31527 | dropdown      | Select your option:                                                            | this\|no this\|or this?                                                                  |               2 | no this                                                   | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T003.csv |
| QP_my_experiment | subject1 |           1 |         4 |         1 |                  4 |   29.31527 | 40.41155 | textShort     | Please type your response to this question:                                    | Type here…                                                                               |               2 | Here is my response!                                      | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T004.csv |
| QP_my_experiment | subject1 |           1 |         5 |         1 |                  5 |   40.41155 | 58.45943 | textLong      | Please type your response to this question but with more space:                | Type here…                                                                               |               2 | Here!                                                     | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T005.csv |
| QP_my_experiment | subject1 |           1 |         6 |         1 |                  6 |   58.45943 | 63.27694 | numberInteger | Give me an integer number                                                      | Type here…                                                                               |               2 | 42                                                        | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T006.csv |
| QP_my_experiment | subject1 |           1 |         7 |         1 |                  7 |   63.27694 | 69.70856 | numberDecimal | Give me an floating point number                                               | Type here…                                                                               |               2 | 0.37828                                                   | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T007.csv |
| QP_my_experiment | subject1 |           1 |         8 |         1 |                  8 |   69.70856 | 75.54406 | radio         | You can only select on option:                                                 | this\|no this\|or this?\|I don’t this                                                    |               2 | 4                                                         | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T008.csv |
| QP_my_experiment | subject1 |           1 |         9 |         1 |                  9 |   75.54406 | 82.25333 | checkmark     | Now\_ you’re allowed to select multiple options.                               | Option 1\|Option 2\|Option 3\|Option 4\|Option 5\|Option 6\|Option 7\|Option 8\|Option 9 |               2 | False\|True\|False\|True\|False\|True\|False\|True\|False | QP_my_experiment/subject1/S001/trackers/controller_mouse_screen_T009.csv |

Here is what the most important columns that are unique to this
application mean:

-   *ppid* = The participant ID.
-   *trailType* = Question type used in this trial.  
-   *question* = The question that was used in this trial.
-   *value* = The response given by the participant. Please look above
    for the different value types.

Note due to UXF’s API “,” in responses & input are converted into “\_”.

For the most part the way to analyse the response should be fairly
obvious. The only tricky bit might be to analyse radio & checkmark
questions. Here is short demonstration to do this.

``` r
# How to analyse the response to a radio question
opt1  <- data[data$trialType == "radio", 'options'] # Get the options
# Split the string by | and then use value as the index for opt1
resp1 <- strsplit(opt1, "\\|")[[1]][as.numeric(data[data$trialType == "radio", 'value'])] 

# How to analyse the response to a checkmark question
opt2  <- data[data$trialType == "checkmark", 'options'] # Get the options
opt2  <- strsplit(opt2, "\\|")[[1]] # Split the options into separate strings
# Do the same for the response
resp2 <- data[data$trialType == "checkmark", 'value'] # Get the response
resp2 <- strsplit(resp2, "\\|")[[1]] # Split the options into separate strings
resp2 <- ifelse(resp2 == "True", TRUE, FALSE)
# Now use this a boolean index and paste together
resp2 <- paste(opt2[resp2], collapse = ", ")
```

In the example data (see video), the participants responded with *I
don’t this* in the radio question and with *Option 2, Option 4, Option
6, Option 8* in the checkmark question.

## Log

In the log you find the time stamps for every button press. Furthermore,
it provides information but which platform is used (Windows/Mac), at
which system time the experiment started and which version was used.
Here is an example:

-   10.53183,Log,Session start time 10/10/2022 4:49:43 PM,
-   10.53183,Log,Application Version : 1.0.0,
-   10.53183,Log,Platform used is UNITY_STANDALONE_WIN,

## Tracker

Additionally, UXF mouse tracker is enabled that tracks the mouse
movement during te trials. This data can be analysed like this:

``` r
# Load libraries for custom_rbinder_addIndexColumn & plotting
library(assortedRFunctions) # Install with devtools::install_github("JAQuent/assortedRFunctions", upgrade = "never")
library(ggplot2)

# Task folder
taskFolder <- "QP_my_experiment/"
subjID     <- "subject1"

# Not this code will only work correctly if the folder only contains position tracker
# Get all files
path2tracker <- paste0(taskFolder, subjID, '/S001/trackers/') 
allTrackers  <- paste0(path2tracker, list.files(path2tracker))
  
# Get list of DFs
tempTracker <- lapply(allTrackers, read.csv)
  
# Add index as subject and then bind together
questions_mouseTracker <- custom_rbinder_addIndexColumn(tempTracker, "trial")

# Add resolution to tracker data
questions_mouseTracker$width      <- 1920 
questions_mouseTracker$height     <- 1080 
questions_mouseTracker$norm_pix_x <- questions_mouseTracker$pix_x/questions_mouseTracker$width
questions_mouseTracker$norm_pix_y <- questions_mouseTracker$pix_y/questions_mouseTracker$height

# Plot
ggplot(questions_mouseTracker,aes(norm_pix_x, norm_pix_y)) + 
  geom_bin2d() +
  scale_fill_gradientn(colours = rev(rainbow(10))) +
  coord_cartesian(xlim = c(0, 1), ylim = c(0, 1), expand = FALSE) +
  labs(title = "Mouse position as heat map during the trials", y = "y position", x = "x position")
```

![](README_files/figure-gfm/mouse_tracker-1.png)<!-- -->

# Ideas for the future

-   A way to introduce a participant check list.
-   A way to further customise the visual appearance (e.g. change
    background on trials to highlight different questionnaires),  
-   Currently, any extra columns in the input .csv file are not copied
    to the data making adding supplementary information to the data a
    bit more cumbersome. Even though it would be a useful feature
    because one could question IDs and many other things, the issue is
    the fact that QP allows “,” to be used in the strings for the
    questions etc. interferes with the `trial_results.csv`. For now, I
    therefore simply disabled this function and suggest to add this
    information at a later stage.
-   Add resolution to the log.

Feedback or help is always welcome!

# How to cite

Quent, J. A. (2022). Questionnaire Presenter - A standalone program to
collect questionnaire data made with Unity3D (Version 1.0.0) \[Computer
software\]. <https://doi.org/10.5281/zenodo.7180407>
