using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class module_photo : MonoBehaviour {

	WebCamTexture webCamTexture;
	public RawImage rawimage;
	public string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	public string code = "";

	void Start () {
		webCamTexture = new WebCamTexture();
		rawimage.texture = webCamTexture;
		rawimage.material.mainTexture = webCamTexture;
		GetComponent<Renderer>().material.mainTexture = webCamTexture;
		webCamTexture.Play();

		for (int i = 0; i < 20; i++) {
			int a = Random.Range(0, chars.Length);
			code = code + chars[a];
		}
	}

	public void ShootCamera(){
		StartCoroutine(TakePhoto());
	}

	IEnumerator TakePhoto()
	{
		yield return new WaitForEndOfFrame();

		Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
		photo.SetPixels(webCamTexture.GetPixels());
		photo.Apply();
		byte[] bytes = photo.EncodeToPNG();
		File.WriteAllBytes("photos/"+code+".png", bytes);

		PlayerPrefs.SetString("code", ""+code);
		webCamTexture.Stop ();
		StartCoroutine(LoadGameScene());
	}

	IEnumerator LoadGameScene() {
		yield return new WaitForSeconds(1);
		AsyncOperation async = SceneManager.LoadSceneAsync(1);
		while (!async.isDone) {
			yield return null;
		}
	}
}
