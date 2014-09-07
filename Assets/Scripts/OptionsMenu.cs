﻿using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	private class Option{

		public string trueLabel;
		public string falseLabel;
		public bool enabled;
		public GUIStyle gs;

		public Option(string t, string f, bool e, GUIStyle g){
			trueLabel = t;
			falseLabel = f;
			enabled = e;
			gs = g;
		}

	}

	public Font chewy;

	public Texture options;
	public Texture yesTutorial;
	public Texture noTutorial;
	public Texture yesProfanity;
	public Texture noProfanity;
	public Texture moreVioLence;
	public Texture lessViolence;
	public Texture healthBarNumbers;
	public Texture noHealthBarNumbers;
	public Texture credits;
	public Texture mainMenu;
	public Texture2D buttonbg;
	public bool tutorial;
	public bool profanity;
	public bool violenceSoundtrack;
	public bool showNumericHealth;
	public bool easyMode;

	private GUIStyle optionsHeader;
	private GUIStyle otherText;
	private GUIStyle buttonStyle;
	const string style = "<color=#ffffff><b><i>";
	const string endStyle = "</i></b></color>";
	private Queue optionList;

	// Use this for initialization
	void Start () {
		tutorial = PlayerPrefs.GetInt("ShowTutorial") == 1;
		profanity = PlayerPrefs.GetInt ("Profanity") == 1;
		violenceSoundtrack = PlayerPrefs.GetInt ("ViolenceMusic") == 1;
		showNumericHealth = PlayerPrefs.GetInt("HealthBarNumbers") == 1;
		easyMode = PlayerPrefs.GetInt ("EasyMode") == 1;

		optionsHeader = new GUIStyle();
		optionsHeader.fontSize = Screen.width/10;
		optionsHeader.font = chewy;


		otherText = new GUIStyle();
		otherText.font = chewy;
		//otherText.fontStyle = FontStyle.BoldAndItalic;
		otherText.richText = true;

		buttonStyle = new GUIStyle();
		buttonStyle.stretchWidth = true;
		buttonStyle.stretchHeight = true;
		buttonStyle.fixedWidth = yesProfanity.width * Screen.width/800;
		buttonStyle.fixedHeight = yesProfanity.height * Screen.height/1200;

		InitializeList();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.E)){
			int x = PlayerPrefs.GetInt ("EasyMode") + 1;
			x %= 2;
			PlayerPrefs.SetInt ("EasyMode", x);
			print ("Difficulty set to: " + (PlayerPrefs.GetInt ("EasyMode") == 0?"hard":"easy"));
		}
	}

	void OnGUI(){
		int x = Screen.width;
		int y = Screen.height;
		int buttonWidth  = Mathf.RoundToInt(x * 2.0f/3.0f);
		int buttonHeight = Mathf.RoundToInt(y * 1.0f/6.0f);
		Texture tutorialTexture  = (tutorial?yesTutorial:noTutorial);
		Texture profanityTexture = (profanity?yesProfanity:noProfanity);
		Texture violenceTexture  = (violenceSoundtrack?moreVioLence:lessViolence);
		Texture numericHealthTexture = (showNumericHealth?healthBarNumbers:noHealthBarNumbers);

		//draw options
		GUI.Box(new Rect(Screen.width/2 - buttonWidth/2, 0, options.width, options.height), options, buttonStyle);

		//option to allow profanity in game
		GUI.Label (new Rect(Screen.width/2 - buttonWidth/2, Screen.height/7,
		                   buttonWidth, buttonHeight), style + "I'm an adult" + (profanity?" God damn it!":"!") + endStyle, otherText);
		if(GUI.Button (new Rect(Screen.width/2 - buttonWidth/2, Screen.height/7 + 25,
		                        buttonWidth, buttonHeight), profanityTexture, buttonStyle)){
			profanity = !profanity;
			//store this result
			PlayerPrefs.SetInt ("Profanity", (profanity?1:0));
		}


		//option to override our music with vio-lence
		GUI.Label (new Rect(Screen.width/2 - buttonWidth/2, 2 * Screen.height/7,
		                    buttonWidth, buttonHeight), style + (violenceSoundtrack?"I want more Vio-Lence!":"Bang that head that doesn't bang") + endStyle, otherText);
		if(GUI.Button (new Rect(Screen.width/2 - buttonWidth/2, 2 * Screen.height/7 + 25,
		                        buttonWidth, buttonHeight), violenceTexture, buttonStyle)){
			violenceSoundtrack = !violenceSoundtrack;
			//store this result
			PlayerPrefs.SetInt ("ViolenceMusic", (violenceSoundtrack?1:0));
		}


		//show tutorial setting
		GUI.Label (new Rect(Screen.width/2-buttonWidth/2, 3 * Screen.height/7, 
		                    buttonWidth, buttonHeight), style + (tutorial?"Help me play this complicated game!":"I got it") + endStyle, otherText);
		if(GUI.Button (new Rect(Screen.width/2-buttonWidth/2, 3 * Screen.height/7 + 25, 
		                        buttonWidth, buttonHeight), tutorialTexture, buttonStyle)){
			tutorial = !tutorial;
			//store this result
			PlayerPrefs.SetInt("ShowTutorial", (tutorial?1:0));
		}


		//show numeric health values setting
		GUI.Label(new Rect(Screen.width/2 - buttonWidth/2, 4 * Screen.height/7,
		                   numericHealthTexture.width, numericHealthTexture.height), style + (showNumericHealth?"Quantify my drunkenness":"I don't need no stinkin' numbers") + endStyle, otherText);
		if(GUI.Button (new Rect(Screen.width/2 - buttonWidth/2, 4 * Screen.height/7 + 25,
		                        buttonWidth, buttonHeight), numericHealthTexture, buttonStyle)){
			showNumericHealth = !showNumericHealth;
			PlayerPrefs.SetInt(	"HealthBarNumbers", (showNumericHealth?1:0));
		}


		//show credits
		GUI.Label (new Rect(Screen.width/2 - buttonWidth/2, 5 * Screen.height/7,
		                    tutorialTexture.width, tutorialTexture.height), style + "Who made this awesome game?" + endStyle, otherText);
		if(GUI.Button (new Rect(Screen.width/2 - buttonWidth/2, 5 * Screen.height/7 + 25,
		                       buttonWidth, buttonHeight), credits, buttonStyle)){
			Application.LoadLevel("Credits");
		}


		//return to main
		GUI.DrawTexture (new Rect(0, 6*Screen.height/7+25, Screen.width, Screen.height/7-25), buttonbg);
		if(GUI.Button (new Rect(Screen.width/2 - buttonWidth/2, 6*Screen.height/7 + 25,
		                        buttonWidth, buttonHeight), mainMenu, buttonStyle)){
			Application.LoadLevel ("Main_Menu");
		}
	}	

	void InitializeList(){
		optionList = new Queue(9);
	}
}
