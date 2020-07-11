using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float dragSpeed = 1;
    [SerializeField]
    private float minZoom = 1;
    [SerializeField]
    private float maxZoom = 10;

    private Camera cam;
    private Vector2 dragOrigin;

    private bool dragging;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        
        if (Input.touchCount == 2) {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            float prevMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
            float currentMagnitude = (touch1.position - touch2.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            Zoom(difference * 0.01f);
        } else {
            if (Input.GetMouseButtonDown(0)) {
                dragOrigin = (Application.isEditor) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
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
                transform.Rotate(new Vector3(0, dir.y, 0) * dragSpeed);
            }
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment) {
        // TODO: Use Translate instead
        cam.transform.position = cam.transform.position + cam.transform.forward * increment * 5;
    }
}
