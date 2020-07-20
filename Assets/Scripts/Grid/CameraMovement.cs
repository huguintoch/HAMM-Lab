using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [Header("Zoom Settings")]
    [SerializeField]
    private float minZoom = 1;
    [SerializeField]
    private float maxZoom = 10;
    [SerializeField]
    private float zoomSpeed = 5;
    
    [Header("Swipe Settings")]
    [SerializeField]
    private float minDistanceForSwipe = 20f;

    private Camera cam;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private Vector3 newRotation;

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
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                }
                if (touch.phase == TouchPhase.Ended) {
                    fingerDown = touch.position;
                    Swipe();
                }
            }
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Swipe() {
        float movement = Mathf.Abs(fingerDown.x - fingerUp.x);
        if (movement > minDistanceForSwipe) {
            var direction = fingerDown.x - fingerUp.x > 0 ? 1 : -1;
            transform.Rotate(new Vector3(0, 90 * direction, 0));
            fingerUp = fingerDown;
        }
    }

    private void Zoom(float direction) {
        //TODO: Clamp zoom;
        cam.transform.Translate(cam.transform.forward * direction * zoomSpeed, Space.World);
    }
}