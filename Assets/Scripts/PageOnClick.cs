using UnityEngine;
using System.Collections;

public class PageOnClick : MonoBehaviour {

    public GameObject pageTurnPrefab;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseDown() {
        Instantiate(pageTurnPrefab);
    }

}
