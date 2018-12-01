using UnityEngine;

public class Background : MonoBehaviour {
	public float speed = 0.1f;
	MeshRenderer rend;
	float distance = 0f;

	void Awake() {
		rend = GetComponent<MeshRenderer>();
	}

	void Start() {
		var left = Camera.main.ViewportToWorldPoint(Vector2.zero);
		var right = Camera.main.ViewportToWorldPoint(Vector2.one);

		float size = (right.x - left.x) * 0.1f; // The Unity plane is 10x units in size.
		transform.localScale = new Vector3(size, transform.localScale.y, 1f);

		rend.sharedMaterial.SetTextureScale("_MainTex", new Vector2(size, 1));
		rend.sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
	}

	void Update() {
		if (Game.isPlaying) {
			rend.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(distance, 0f));
			distance += Time.deltaTime * speed;
		}
	}
}
