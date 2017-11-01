using UnityEngine;
using System.Collections;

public class HiResScreenshots : MonoBehaviour {
	
	public int resWidth = 800; 
	public int resHeight = 600;
	public Camera camera;
	public Texture2D screenShot;
	int id;
	private bool takeHiResShot = false;

	public static string ScreenShotName(int width, int height) {
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
			Application.dataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	public void TakeScreenshot(int _id) {
		this.id = _id;
		takeHiResShot = true;
	}
	RenderTexture rt;
	void LateUpdate() {
		
		takeHiResShot |= Input.GetKeyDown("k");
		if (takeHiResShot) {
			rt = new RenderTexture(resWidth, resHeight, 24);
			camera.targetTexture = rt;
			screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			camera.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			screenShot.Apply();
			camera.targetTexture = null;
			byte[] bytes = screenShot.EncodeToPNG();
//			string filename = ScreenShotName(resWidth, resHeight);
//			System.IO.File.WriteAllBytes(filename, bytes);
//			Debug.Log(string.Format("Took screenshot to: {0}", filename));
			takeHiResShot = false;
			Events.UpdateThumbButton (id);
		}
	}
	public void ResetImage()
	{
		RenderTexture.active = null;
		Destroy(rt);
		rt = null;
		screenShot = null;
	}
}