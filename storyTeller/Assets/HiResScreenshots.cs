using UnityEngine;
using System.Collections;

public class HiResScreenshots : MonoBehaviour {
	
	public int resWidth = 800; 
	public int resHeight = 600;
	public Camera cam;
	public Texture2D screenShot;
	int id;
	private bool takeHiResShot = false;

    private void Start()
    {
        cam = World.Instance.scenesManager.cam;
    }
    public static string ScreenShotName(int width, int height) {
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
			Application.dataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	public void TakeScreenshot(int _id) {
        if (World.Instance.timeLine.uiTimeline.state == UITimeline.states.PLAY_ALL)
            return;
		this.id = _id;
		takeHiResShot = true;
	}
	RenderTexture rt;
	void LateUpdate() {
		
		takeHiResShot |= Input.GetKeyDown("k");
		if (takeHiResShot) {
			rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
			screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            cam.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			screenShot.Apply();
            cam.targetTexture = null;
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