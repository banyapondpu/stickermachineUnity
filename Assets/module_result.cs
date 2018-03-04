using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class module_result : MonoBehaviour {

	public string get_code, filePath;
	Texture2D myTexture;

	void Start () {
		get_code = PlayerPrefs.GetString ("code");
		Debug.Log ("photos/" + get_code + ".png");
		myTexture = null;
		byte[] fileData;
		filePath = "photos/" + get_code + ".png";
		if (File.Exists (filePath)) {
			Debug.Log ("get");
			fileData = File.ReadAllBytes (filePath);
			myTexture = new Texture2D (2, 2);
			myTexture.LoadImage (fileData);
			GameObject rawImage = GameObject.Find ("RawImage");
			rawImage.GetComponent<RawImage> ().texture = myTexture;
		}
	}
}
