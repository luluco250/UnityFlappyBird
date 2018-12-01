using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("PostFX/Bloom")]
[RequireComponent(typeof(Camera))]
public class Bloom : MonoBehaviour {
	[Range(0.0f, 3.0f)]
	public float intensity = 1.0f;

	[Range(1.0f, 10.0f)]
	public float contrast = 1.0f;

	[Range(0.0f, 10.0f)]
	public float blurScale = 1.0f;

	public Shader shader;
	Material mat;

	void OnEnable() {
		if (!shader) {
			enabled = false;
			return;
		}

		mat = new Material(shader);
		mat.hideFlags = HideFlags.HideAndDontSave;
	}
	
	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		mat.SetFloat("_Intensity", intensity);
		mat.SetFloat("_Contrast", contrast);
		mat.SetFloat("_Scale", blurScale);

		Graphics.Blit(src, dest, mat);
	}
}
