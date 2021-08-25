using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour {


	public AudioMixer audioMixer;

	Resolution[] resolutions;
	//public Dropdown resolutionDropdown;
	public TMP_Dropdown resolutionDropdown;

	//private void Awake() {
	//	resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<TMP_Dropdown>();
	//}

	private void Start() {

		resolutions = Screen.resolutions;

		resolutionDropdown.ClearOptions();

		List<string> possResolutions = new List<string>();

		int currentResolutionIndex = 0;
		for(int i = 0; i < resolutions.Length; i++) {
			string possResolution = resolutions[i].width + "x" + resolutions[i].height;
			possResolutions.Add(possResolution);

			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
				currentResolutionIndex = i;
			}

		}

		resolutionDropdown.AddOptions(possResolutions);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();

	}

	public void SetVolume(float volume) {
		//Debug.Log(volume);
		audioMixer.SetFloat("Volume", volume);
	}

	public void SetQuality(int qualityIndex) {
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetResolution(int resolutionIndex) {


		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
	}

	public void SetFullscreen (bool isFullscreen) {
		Screen.fullScreen = isFullscreen;
	}


	//public void Password(GameObject passwordGameObject) {
	//	var pw = "";

	//	if (passwordGameObject.GetComponentInChildren<TextMeshProUGUI>() != null) {
	//		pw = passwordGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
	//	}
	//	else {
	//		Debug.LogError("Password textmesh pro gui text not found");
	//	}

	//	if(pw == "poop") {

	//	}

	//}


}
