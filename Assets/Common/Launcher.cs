using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour {
    public RawImage image;
    private Texture2D tex2D;
	// Use this for initialization
	void Awake () {
        tex2D = new Texture2D(Screen.width, Screen.height);
        tex2D.filterMode = FilterMode.Trilinear;
        tex2D.wrapMode = TextureWrapMode.Clamp;
        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        image.texture = tex2D;
        DrawScreen.instance.SetTexture(tex2D);
    }
}
