using UnityEngine;

public class PipeSpawner : MonoBehaviour {
	private static PipeSpawner instance;
	public static PipeSpawner Get() {
		return instance;
	}

	public GameObject pipes;
	public float spawnTime = 2f;

	float lastTime;

	void Awake() {
		if (instance != null) {
            Debug.LogWarning(
                "There should be only ONE instance of " + 
                "PipeSpawner. Destroying this instance..."
            );
            DestroyImmediate(this);
            return;
        }

        instance = this;
	}

	void Start() {
		lastTime = Time.time;
	}

	void Update() {
		if (Game.isPlaying && (Time.time - lastTime > spawnTime)) {
			Instantiate(pipes);
			lastTime = Time.time;
		}
	}
}
