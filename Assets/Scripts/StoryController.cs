using UnityEngine;
using System.Collections;

public class StoryController : MonoBehaviour {

    public TextAsset textAsset;

    private void Awake() {
        //TextMesh tm;
        //tm.text

        //page text
        //-11, 6.5, 1
        //0.25, 0.25, 0.25
        //animator = GetComponent<Animator>();
    }

    private void Start() {
        Debug.Log(textAsset.text);
        //Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + delay);
    }

}
