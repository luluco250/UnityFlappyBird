﻿using UnityEngine;

using static UnityEngine.Mathf;

public class Bird : MonoBehaviour {
	public float jumpVelocity = 5f;
	public float rotationVelocity = 3f;

	[Range(0f, 180f)]
	public float rotationTopAngle = 35f;
	[Range(-180f, 0f)]
	public float rotationBottomAngle = -70f;

	float rotation;
	float rotVel;
	
	public Vector2 startViewportPos = new Vector2(0.5f, 0.5f);
	Vector3 startPos;
	Rigidbody2D rb2d;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Start() {
		startPos = Camera.main.ViewportToWorldPoint(startViewportPos);
		startPos.z = transform.position.z;
		
		transform.position = startPos;
		rotation = transform.rotation.eulerAngles.z;
	}

	void Update() {
		//rotVel += rotationVelocity * Time.deltaTime;
		//rotation = Max(rotationBottomAngle, rotation - rotVel);

		rotation -= (rotation - rotationTopAngle) * Time.deltaTime; //(rotationBottomAngle - rotation) * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space)) {
			Game.isPlaying = rb2d.simulated = true;

			rb2d.velocity = Vector2.up * jumpVelocity;
			rotation = rotationTopAngle;
			rotVel = 0f;
		} else if (!Game.isPlaying) {
			var pos = transform.position;
			pos.y = Sin(Time.time * 4f) * 0.15f;
			transform.position = pos;
			rotation = 0f;
		}

		var rot = transform.rotation;
		rot.eulerAngles = new Vector3(0f, 0f, rotation);
		transform.rotation = rot;
	}

	void OnCollisionEnter2D(Collision2D c) {
		return;
		if (c.transform.tag == "Pipe") {
			// Resetar estado do jogo.
			Game.isPlaying = false;

			// Destruir todos os canos.
			var pipes = FindObjectsOfType<Pipe>();
			for (int i = 0; i < pipes.Length; ++i)
				Destroy(pipes[i].gameObject);
			
			// Desligar física e tirar velocidade.
			rb2d.simulated = false;
			rb2d.velocity = Vector2.zero;
			
			// Resetar posições x e y.
			transform.position = startPos;

			// Resetar pontuação.
			ScoreSystem.ResetScore();
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.transform.tag == "Score")
			ScoreSystem.AddPoint();
	}
}