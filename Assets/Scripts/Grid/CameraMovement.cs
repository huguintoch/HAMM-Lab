using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float dragSpeed = 1;

    private Camera cam;
    private Vector2 dragOrigin;

    private bool dragging;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = (Application.isEditor) ? Input.mousePosition : (Vector3) Input.GetTouch(0).position;
            dragging = true;
        }
        if (dragging && Input.GetMouseButton(0)) {
            Vector3 pos;
            if (Application.isEditor) {
                pos = Camera.main.ScreenToViewportPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y) - dragOrigin);
            } else {
                pos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position - dragOrigin);
            }
            Vector3 dir = new Vector3(0, pos.x, 0).normalized;
            transform.Rotate(new Vector3(0, dir.y, 0)*dragSpeed);
        }
    }
}
