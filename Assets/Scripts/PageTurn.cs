using UnityEngine;
using System.Collections;

public class PageTurn : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    public bool IsRightToLeft { get; set; }

    private Vector2 r2l { get { return new Vector2(-0.9f, 1.9f); } }
    private Vector2 l2r { get { return new Vector3(-0.5f, 1.9f); } }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsRightToLeft = true;
    }

    private void Start() {
        if (IsRightToLeft) {
            transform.position = r2l;
        } else {
            transform.position = l2r;
            spriteRenderer.flipX = true;
        }
    }

}
