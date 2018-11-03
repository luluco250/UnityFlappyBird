using UnityEngine;

public class BorderAdjuster : MonoBehaviour {
    public BoxCollider2D ceiling, ground;
    public float height = 1.0f, heightOffset = 0.5f;

    void Start() {
        var top = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1.0f));
        var bottom = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.0f));

        var pos = ceiling.offset;
        pos.x = top.x;
        pos.y = top.y + heightOffset;
        ceiling.offset = pos;

        pos = ground.offset;
        pos.x = bottom.x;
        pos.y = bottom.y - heightOffset;
        ground.offset = pos;

        var left = Camera.main.ViewportToWorldPoint(Vector2.zero);
        var right = Camera.main.ViewportToWorldPoint(Vector2.one);

        ground.size = ceiling.size = new Vector2(right.x - left.x, height);
    }
}
