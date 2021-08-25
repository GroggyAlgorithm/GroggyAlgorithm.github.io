using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour {
	private Camera mainCamera;
	Vector3 cameraPos;
	public float cameraMovementSpeed = 5f;

	private YieldInstruction fadeInstruction = new YieldInstruction();

	private Image levelImage;
	private TextMeshProUGUI levelText;

	//These are exclusively for the Menus
	private GameObject titleObject;
	//private Text titleText;
	private TextMeshProUGUI titleText;
	private TextMeshPro titleShader;
	private GameObject pressStartObject;
	private TextMeshProUGUI pressStartText;


	private GameObject playButtonObject;
	private TextMeshProUGUI playButtonText;
	private TextMeshProUGUI pressOptionsText;
	private TextMeshProUGUI pressExitText;


	[HideInInspector] public bool doingSetup = false;

	

	private GameObject buttonsObject;
	public float FadeRate = 0.5f;
	private float textElapsedTime = 0.0f;
	private float textMidTime = 0.75f;
	private float textEndTime = 1.25f;

	private float fadeElaspedTime = 0;
	private float fadeEndTime = 1.75f;
	private float textFadeElapsedTime = 0;


	private bool startGame = false;


	private int mainSelections = 3;
	private int mainButtonSelected = 1;

	private int playGameSelections = 2;
	private int playGameButtonSelected = 1;


	private int optionsSlections = 5;
	private int optionsButtonSelected = 1;

	private int exitSelections = 2;
	private int exitButtonSelected = 2;

	private float verticalInput;
	private float horizontalInput;


	private bool onEnterScreen = true;
	private bool onMainScreen = false;
	private bool onPlayGameScreen = false;
	private bool onOptionsScreen = false;
	private bool onExitScreen = false;
	private bool on1or2PlayerScreen = false;

	private GameObject MainScreen;
	private GameObject OptionsScreen;
	private TextMeshProUGUI optionsSoundText;
	private TextMeshProUGUI optionsGraphicsText;
	private TextMeshProUGUI optionsResolutionText;
	private TextMeshProUGUI optionsPassWordText;
	private TextMeshProUGUI optionsReturnText;
	private GameObject QuitScreen;
	private TextMeshProUGUI exQuitText;
	private TextMeshProUGUI exReturnText;
	private GameObject player1or2Screen;
	private TextMeshProUGUI SinglePlayerText;
	private TextMeshProUGUI CoopText;

	private float waitTime = 0f;
	[HideInInspector] private bool p2Mode = false;



	private int buttonClickCounter = 0;

	//public AudioClip menuMusic;
	private SoundManager soundManager;

	private void Awake() {
		var passedValues = GameObject.Find("PassedValues").GetComponent<PassedValues>();
		passedValues.p2Mode = false;
		passedValues.GameOver = false;
		passedValues.GameStarted = false;

		MainScreen = GameObject.Find("MainScreen");
		OptionsScreen = GameObject.Find("OptionsScreen");
		optionsSoundText = GameObject.Find("SoundText").GetComponent<TextMeshProUGUI>();
		optionsGraphicsText = GameObject.Find("GraphicsText").GetComponent<TextMeshProUGUI>();
		optionsResolutionText = GameObject.Find("ResolutionText").GetComponent<TextMeshProUGUI>();
		optionsPassWordText = GameObject.Find("PassWordText").GetComponent<TextMeshProUGUI>();
		optionsReturnText = GameObject.Find("ReturnText").GetComponent<TextMeshProUGUI>();

		QuitScreen = GameObject.Find("ExitScreen");
		exQuitText = GameObject.Find("exQuitText").GetComponent<TextMeshProUGUI>();
		exReturnText = GameObject.Find("exReturnText").GetComponent<TextMeshProUGUI>();

		titleObject = GameObject.Find("Title");
		//titleText = titleObject.GetComponent<Text>();
		titleText = titleObject.GetComponent<TextMeshProUGUI>();
		//titleText.color = new Color(1, 1, 1, 0);
		titleShader = titleObject.GetComponent<TextMeshPro>();
		//Need to get shader from title to add to dilate for entrance

		pressStartObject = GameObject.Find("pressStartButton");
		pressStartText = pressStartObject.GetComponentInChildren<TextMeshProUGUI>();


		playButtonObject = GameObject.Find("PlayButton");
		playButtonText = GameObject.Find("GameStart").GetComponent<TextMeshProUGUI>();
		pressOptionsText = GameObject.Find("Options").GetComponent<TextMeshProUGUI>();
		pressExitText = GameObject.Find("Exit").GetComponent<TextMeshProUGUI>();


		buttonsObject = GameObject.Find("Buttons");
		levelImage = GameObject.Find("LevelImage").GetComponent<Image>();

		levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
		levelText.text = "";

		player1or2Screen = GameObject.Find("PlayerSelectButtons");
		SinglePlayerText = GameObject.Find("SinglePlayerText").GetComponent<TextMeshProUGUI>();
		CoopText = GameObject.Find("CoopText").GetComponent<TextMeshProUGUI>();

		soundManager = FindObjectOfType<SoundManager>();

		passedValues.level = 0;
		player1or2Screen.SetActive(false);
		MainScreen.SetActive(false);
		buttonsObject.SetActive(false);
		OptionsScreen.SetActive(false);
		QuitScreen.SetActive(false);

		mainCamera = Camera.main;
		if (mainCamera) {
			cameraPos = mainCamera.transform.position;
		}


	}
	private void Start() {
		soundManager.PlayMusic("MainMenuMusic");
	}

	private void Update() {
		//textBlink(pressStartObject);
		fadeElaspedTime += Time.deltaTime;
		//StartCoroutine(TmpTextFadeIn(titleText));
		//StopCoroutine(TmpTextFadeIn(titleText));
		StartCoroutine(FadeOutImage(levelImage));
		if (fadeElaspedTime > fadeEndTime / 2) {
			StopCoroutine(FadeOutImage(levelImage));
			levelImage.gameObject.SetActive(false);
			MainScreen.SetActive(true);
			textFadeElapsedTime += Time.deltaTime;
			if (textFadeElapsedTime <= textEndTime / 2) {
				StartCoroutine(TmpTextFadeOut(pressStartText));
			}
			else if (textFadeElapsedTime > textEndTime / 2 && textFadeElapsedTime <= textEndTime) {
				StopCoroutine(TmpTextFadeOut(pressStartText));
				StartCoroutine(TmpTextFadeIn(pressStartText));
			}
			else if (textFadeElapsedTime > textEndTime) {
				StopCoroutine(TmpTextFadeIn(pressStartText));
				textFadeElapsedTime = 0;
			}
			if (Input.GetButtonDown("Submit") && onEnterScreen == true) {
				StopCoroutine(TmpTextFadeOut(pressStartText));
				StopCoroutine(TmpTextFadeIn(pressStartText));
				//pressStartObject.SetActive(false);
				//buttonsObject.SetActive(true);
				soundManager.Play("StartConfirm");
				soundManager.Play("ButtonConfirm");
				onEnterScreen = false;
				onMainScreen = true;
				//Selections();
				//startGame = true;
			}
			//else if (Input.GetButtonDown("Submit") && startGame == true) {
			//	PlayGame();
			//}
		}
		
		//if (doingSetup == false) {

		Selections();

		//	StartCoroutine(TextFlash(titleText));
		//	buttonsObject.SetActive(false);
		//	levelImage.gameObject.SetActive(false);
		//}
	}


	private void Selections() {
		if (onEnterScreen == false) {
			horizontalInput = Input.GetAxis("Horizontal");
			verticalInput = Input.GetAxis("Vertical");
			pressStartObject.SetActive(false);


		}
		if (onMainScreen == true) {
			//Debug.Log("Main button Selected: " + mainButtonSelected);
			MainScreen.SetActive(true);
			buttonsObject.SetActive(true);
			QuitScreen.SetActive(false);
			OptionsScreen.SetActive(false);
			horizontalInput = Input.GetAxis("Horizontal");
			if (horizontalInput > 0 && Input.GetButtonDown("Horizontal")) {
				//Debug.Log("Pressed Right");
				mainButtonSelected += 1;
			}
			else if (horizontalInput < 0 && Input.GetButtonDown("Horizontal")) {
				//Debug.Log("Pressed Left");
				mainButtonSelected -= 1;
			}

			switch (mainButtonSelected) {
				case 1:
					StartCoroutine(TmpTextFadeIn(playButtonText));
					StartCoroutine(TmpTextFadeIn(playButtonText));
					break;
				case 2:
					StopCoroutine(TmpTextFadeIn(playButtonText));
					StopCoroutine(TmpTextFadeIn(playButtonText));
					StartCoroutine(TmpTextFadeIn(pressOptionsText));
					StartCoroutine(TmpTextFadeIn(pressOptionsText));
					break;
				case 3:
					StopCoroutine(TmpTextFadeIn(pressOptionsText));
					StopCoroutine(TmpTextFadeIn(pressOptionsText));
					StartCoroutine(TmpTextFadeIn(pressExitText));
					StartCoroutine(TmpTextFadeIn(pressExitText));
					break;
			}


			if (mainButtonSelected < 1) {
				StopCoroutine(TmpTextFadeIn(playButtonText));
				StopCoroutine(TmpTextFadeIn(playButtonText));
				mainButtonSelected = mainSelections;
			}
			else if (mainButtonSelected > mainSelections) {
				StopCoroutine(TmpTextFadeIn(pressExitText));
				StopCoroutine(TmpTextFadeIn(pressExitText));
				mainButtonSelected = 1;
			}
			else if (mainButtonSelected == 1) {
				waitTime += Time.deltaTime;
				if (waitTime > 0.25f) {
					if (Input.GetButtonDown("Submit") && onEnterScreen == false) {
						soundManager.Play("StartConfirm");
						soundManager.Play("ButtonConfirm");
						on1or2PlayerScreen = true;
						//onPlayGameScreen = true;
						onMainScreen = false;
						waitTime = 0f;
					}
				}
			}
			else if (mainButtonSelected == 2 && Input.GetButtonDown("Submit")) {
				//Debug.Log("Options button pushed");
				if (Input.GetButtonDown("Submit") && onEnterScreen == false) {
					soundManager.Play("ButtonConfirm");
					onOptionsScreen = true;
					onMainScreen = false;
					waitTime = 0f;
				}
			}
			else if (mainButtonSelected == 3 && Input.GetButtonDown("Submit")) {
				soundManager.Play("ButtonConfirm");
				//QuitGame();
				onExitScreen = true;
				onMainScreen = false;
				waitTime = 0f;
			}

		}//EndOfMainscreenTrue option

		//Debug.Log("Main Button Selected: " + mainButtonSelected);

		//Debug.Log("Wait Time: " + waitTime);

		if (on1or2PlayerScreen == true) {

			player1or2Screen.SetActive(true);
			playGameButtonSelected = 1;

			if (horizontalInput > 0 && Input.GetButtonDown("Horizontal")) {
				//Debug.Log("Pressed Right");
				playGameButtonSelected += 1;
			}
			else if (horizontalInput < 0 && Input.GetButtonDown("Horizontal")) {
				//Debug.Log("Pressed Left");
				playGameButtonSelected -= 1;
			}

			switch (playGameButtonSelected) {
				case 1:
					StartCoroutine(TmpTextFadeIn(SinglePlayerText));
					StopCoroutine(TmpTextFadeIn(CoopText));
					break;
				case 2:
					StartCoroutine(TmpTextFadeIn(CoopText));
					break;
			}

			if (playGameButtonSelected < 1) {
				playGameButtonSelected = playGameSelections;
			}
			else if (playGameButtonSelected > playGameSelections) {
				playGameButtonSelected = 1;
			}
			else if (playGameButtonSelected == 1) {
				waitTime += Time.deltaTime;
				if (waitTime > 0.25f) {
					if (Input.GetButtonDown("Submit") && onEnterScreen == false) {
						soundManager.Play("StartConfirm");
						soundManager.Play("ButtonConfirm");
						p2Mode = false;
						var passValues = GameObject.Find("PassedValues").GetComponent<PassedValues>();
						passValues.p2Mode = false;
						startGame = true;
						PlayGame();
					}
				}
			}
			else if (playGameButtonSelected == 2 && Input.GetButtonDown("Submit")) {
				waitTime += Time.deltaTime;
				if (waitTime > 0.25f) {
					if (Input.GetButtonDown("Submit") && onEnterScreen == false) {
						soundManager.Play("StartConfirm");
						soundManager.Play("ButtonConfirm");
						p2Mode = true;
						var passValues = GameObject.Find("PassedValues").GetComponent<PassedValues>();
						passValues.p2Mode = true;
						startGame = true;
						PlayGame();
					}
				}
			}
		}
		else if (onPlayGameScreen == false) {
			player1or2Screen.SetActive(false);


		}



		if (onOptionsScreen == true) {
			OptionsScreen.SetActive(true);
			MainScreen.SetActive(false);
			if (verticalInput < 0 && Input.GetButtonDown("Vertical")) {
				optionsButtonSelected += 1;
			}
			else if (verticalInput > 0 && Input.GetButtonDown("Vertical")) {
				optionsButtonSelected -= 1;
			}
			//optionsSoundText = GameObject.Find("SoundText").GetComponent<TextMeshProUGUI>();
			//optionsGraphicsText = GameObject.Find("GraphicsText").GetComponent<TextMeshProUGUI>();
			//optionsResolutionText = GameObject.Find("ResolutionText").GetComponent<TextMeshProUGUI>();
			//optionsPassWordText = GameObject.Find("PassWordText").GetComponent<TextMeshProUGUI>();
			//optionsReturnText = GameObject.Find("ReturnText").GetComponent<TextMeshProUGUI>();
			switch (optionsButtonSelected) {
				case 1:
					StopCoroutine(TmpTextFadeIn(optionsGraphicsText));
					StartCoroutine(TmpTextFadeIn(optionsSoundText));
					break;
				case 2:
					StopCoroutine(TmpTextFadeIn(optionsSoundText));
					StopCoroutine(TmpTextFadeIn(optionsResolutionText));
					StartCoroutine(TmpTextFadeIn(optionsGraphicsText));
					break;
				case 3:
					StopCoroutine(TmpTextFadeIn(optionsPassWordText));
					StopCoroutine(TmpTextFadeIn(optionsGraphicsText));
					StartCoroutine(TmpTextFadeIn(optionsResolutionText));
					break;
				case 4:
					StopCoroutine(TmpTextFadeIn(optionsResolutionText));
					StopCoroutine(TmpTextFadeIn(optionsReturnText));
					StartCoroutine(TmpTextFadeIn(optionsPassWordText));
					break;
				case 5:
					StopCoroutine(TmpTextFadeIn(optionsPassWordText));
					StartCoroutine(TmpTextFadeIn(optionsReturnText));
					break;
			}


			if (optionsButtonSelected < 1) {
				StopCoroutine(TmpTextFadeIn(optionsSoundText));
				StopCoroutine(TmpTextFadeIn(optionsSoundText));
				optionsButtonSelected = optionsSlections;
			}
			else if (optionsButtonSelected > optionsSlections) {
				StopCoroutine(TmpTextFadeIn(optionsReturnText));
				StopCoroutine(TmpTextFadeOut(optionsReturnText));
				optionsButtonSelected = 1;
			}
			//else if (optionsButtonSelected == 1) {
			//	waitTime += Time.deltaTime;
			//	if (waitTime > 0.25f) {
			//		if (Input.GetButtonDown("Submit")) {
			//			//onPlayGameScreen = true;
			//			//onMainScreen = false;
			//			//waitTime = 0f;
			//		}
			//	}

			//}
			//else if (optionsButtonSelected == 2) {
			//	if (Input.GetButtonDown("Submit")) {
			//		onOptionsScreen = false;
			//		onMainScreen = true;
			//		//MainScreen.SetActive(true);
			//		//OptionsScreen.SetActive(false);

			//	}
			//}
			//else if (optionsButtonSelected == 3) {
			//	if (Input.GetButtonDown("Submit")) {
			//		onOptionsScreen = false;
			//		onMainScreen = true;
			//		//MainScreen.SetActive(true);
			//		//OptionsScreen.SetActive(false);

			//	}
			//}
			//else if (optionsButtonSelected == 4) {
			//	if (Input.GetButtonDown("Submit")) {
			//		onOptionsScreen = false;
			//		onMainScreen = true;
			//		//MainScreen.SetActive(true);
			//		//OptionsScreen.SetActive(false);

			//	}
			//}
			else if (optionsButtonSelected == 5) {
				waitTime += Time.deltaTime;
				if (waitTime > 0.25f) {
					if (Input.GetButtonDown("Submit")) {
						soundManager.Play("ButtonConfirm");
						onOptionsScreen = false;
						onMainScreen = true;
						optionsButtonSelected = 1;
						//MainScreen.SetActive(true);
						//OptionsScreen.SetActive(false);
					}
				}
			}

		}
		if (onExitScreen == true) {
			QuitScreen.SetActive(true);
			MainScreen.SetActive(false);
			//exQuitText exReturnText
			if (horizontalInput > 0 && Input.GetButtonDown("Horizontal")) {
				//Debug.Log("Pressed Right");
				exitButtonSelected += 1;
			}
			else if (horizontalInput < 0 && Input.GetButtonDown("Horizontal")) {
				//Debug.Log("Pressed Left");
				exitButtonSelected -= 1;
			}
			switch (exitButtonSelected) {
				case 1:
					StopCoroutine(TmpTextFadeIn(exReturnText));
					StartCoroutine(TmpTextFadeIn(exQuitText));
					break;
				case 2:
					StopCoroutine(TmpTextFadeIn(exQuitText));
					StartCoroutine(TmpTextFadeIn(exReturnText));
					break;
			}
			if (exitButtonSelected < 1) {
				exitButtonSelected = exitSelections;
			}
			else if (exitButtonSelected > exitSelections) {
				optionsButtonSelected = 1;
			}
			else if (exitButtonSelected == 1) {
				if (Input.GetButtonDown("Submit")) {
					soundManager.Play("ButtonConfirm");
					QuitGame();
				}
			}
			else if (exitButtonSelected == 2) {
				waitTime += Time.deltaTime;
				if (waitTime > 0.25f) {
					if (Input.GetButtonDown("Submit")) {
						soundManager.Play("ButtonConfirm");
						mainS();
					}
				}

			}
		}
		
		

	}

	public void PlayGame() {
		var passValues = GameObject.Find("PassedValues").GetComponent<PassedValues>();
		passValues.GameStarted = true;
		soundManager.StopSpecificSong("MainMenuMusic");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Replace inside with "level name text" or replace with buildindex number. The currently used gets next scene info
																		  //																		  //Build index is under file,build settings, drag and drop scenes

	}

	public void PlayGameCoop() {
		var passValues = GameObject.Find("PassedValues").GetComponent<PassedValues>();
		passValues.p2Mode = true;
		passValues.GameStarted = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
	}


	//private void StoryImage() {
	//	levelImage.gameObject.SetActive(true);
	//	//StopCoroutine(FadeOutImage(levelImage));
	//	//StartCoroutine(FadeInImage(levelImage));

	//	levelText.text = "This Is a story";
	//}





	public void QuitGame() {
		Debug.Log("Quit");
		Application.Quit();
	}
	void textBlink(GameObject go) {
		//float elapsedTime = 0.0f;
		//float midTime = 0.25f;
		//float endTime = 0.5f;
		textElapsedTime += Time.deltaTime;
		if (textElapsedTime < textMidTime) {
			go.SetActive(true);
		}
		else if (textElapsedTime >= textMidTime && textElapsedTime < textEndTime) {
			go.SetActive(false);
		}
		else if (textElapsedTime >= textEndTime) {

			textElapsedTime = 0f;
		}

	}

	IEnumerator TmpTextFadeOut(TextMeshProUGUI text) {
		float elapsedTime = 0.0f;
		text.color = new Color(1, 1, 1, 1);
		Color c = text.color;
		//text.color = new Color(1,1,1,1);
		while (textFadeElapsedTime <= textEndTime / 2) {
			//while (elapsedTime <= textEndTime) {
			//while (elapsedTime <= textMidTime) {
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime;
			c.a = 1.0f - Mathf.Clamp01(elapsedTime / 0.5f);
			//c.a = 1.0f - Mathf.Clamp01(elapsedTime / 1);
			text.color = c;
		}
		text.color = new Color(1, 1, 1, 0);

	}
	IEnumerator TmpTextFadeIn(TextMeshProUGUI text) {
		float elapsedTime = 0.0f;
		Color c = text.color;
		while (textFadeElapsedTime > textEndTime / 2) {
			//while (elapsedTime > textMidTime) {
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime;
			c.a = Mathf.Clamp01(elapsedTime / 0.5f);
			//c.a = Mathf.Clamp01(elapsedTime / 1);
			text.color = c;
		}
		text.color = new Color(1, 1, 1, 1);
	}



	IEnumerator FadeOutImage(Image image) { //For hurt face
		float elapsedTime = 0.0f;
		Color c = image.color;
		while (fadeElaspedTime <= fadeEndTime / 2) {
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime;
			c.a = 1.0f - Mathf.Clamp01(elapsedTime / 0.5f);
			image.color = c;
		}
	}
	IEnumerator FadeInImage(Image image) { // for the hurt face
		float elapsedTime = 0.0f;
		Color c = image.color;
		while (fadeElaspedTime > fadeEndTime) {
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime;
			c.a = Mathf.Clamp01(elapsedTime / 0.5f);
			image.color = c;
		}
	}


	private void FixedUpdate() {

		Vector3 cam = mainCamera.transform.position;
		cam.x += Time.deltaTime;
		mainCamera.transform.position = new Vector3(cam.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
	}

	//private bool onEnterScreen = true;
	//private bool onMainScreen = false;
	//private bool onPlayGameScreen = false;
	//private bool onOptionsScreen = false;
	//private bool onExitScreen = false;
	//private bool on1or2PlayerScreen = false;

	public void optionsS() {
		soundManager.Play("ButtonConfirm");
		onMainScreen = false;
		MainScreen.SetActive(false);
		onExitScreen = false;
		QuitScreen.SetActive(false);
		onOptionsScreen = true;
		onPlayGameScreen = false;
		player1or2Screen.SetActive(false);
	}
	public void exitS() {
		soundManager.Play("ButtonConfirm");
		onExitScreen = true;
		onOptionsScreen = false;
		OptionsScreen.SetActive(false);
		onMainScreen = false;
		MainScreen.SetActive(false);
		onPlayGameScreen = false;
		player1or2Screen.SetActive(false);
	}
	public void mainS() {
		soundManager.Play("ButtonConfirm");
		onEnterScreen = false;
		pressStartObject.SetActive(false);
		onMainScreen = true;
		onExitScreen = false;
		QuitScreen.SetActive(false);
		onOptionsScreen = false;
		OptionsScreen.SetActive(false);
		onPlayGameScreen = false;
		player1or2Screen.SetActive(false);
	}
	public void p1orp2S() {
		soundManager.Play("StartConfirm");
		soundManager.Play("ButtonConfirm");
		onMainScreen = false;
		onPlayGameScreen = true;
		player1or2Screen.SetActive(true);
	}
	

}
