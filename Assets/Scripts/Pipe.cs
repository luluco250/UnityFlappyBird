using UnityEngine;

public class Pipe : MonoBehaviour {
	public float speed = 1f;

	void Start() {
		var startPos = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0.5f));
		var pos = transform.position;
		pos.x = startPos.x + 10f;
		pos.y = startPos.y + Random.Range(-2, 2);
		transform.position = pos;
	}

	void FixedUpdate() {
		transform.Translate(-speed, 0f, 0f);

		var killPos = Camera.main.ViewportToWorldPoint(new Vector2(-0.25f, 0f));
		if (transform.position.x < killPos.x)
			Destroy(this.gameObject);
	}
}
