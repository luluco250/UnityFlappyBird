using UnityEngine;

public class Game : MonoBehaviour {
    private static Game instance;
    public static Game Get() {
        return instance;
    }

    public static bool isPlaying = false;

    void Awake() {
        if (instance != null) {
            Debug.LogWarning(
                "There should be only ONE instance of " + 
                "Game. Destroying this instance..."
            );
            DestroyImmediate(this);
            return;
        }

        instance = this;
    }
}
