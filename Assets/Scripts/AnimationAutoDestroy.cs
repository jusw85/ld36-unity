using UnityEngine;
using System.Collections;

public class AnimationAutoDestroy : MonoBehaviour {

    public float delay = 0f;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + delay);
    }

}
