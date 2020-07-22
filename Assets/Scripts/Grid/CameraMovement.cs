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
    private ScrollView scroller;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool Draggable { get; set; }

    private void Start() {
        cam = Camera.main;
        scroller = GameObject.Find("Scroll View").GetComponent<ScrollView>();
        Draggable = true;
    }

    private void Update() {
        if (Draggable && !scroller.PointerOnInventory) {
            // Only works on mobile devices
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
                        fingerUp = touch.position;
                        Swipe();
                    }
                }
            }

            // Only works in the Unity editor
            if (Application.isEditor) {
                if (Input.GetMouseButtonDown(0)) {
                    StartCoroutine(RotatePlatform(transform.rotation, 1));
                } else if (Input.GetMouseButtonDown(1)) {
                    StartCoroutine(RotatePlatform(transform.rotation, -1));
                }
                Zoom(Input.GetAxis("Mouse ScrollWheel"));
            }
        }
    }

    private void Swipe() {
        float movement = Mathf.Abs(fingerUp.x - fingerDown.x);
        if (movement > minDistanceForSwipe) {
            int direction = fingerUp.x - fingerDown.x < 0? -1: 1;
            StopAllCoroutines();
            StartCoroutine(RotatePlatform(transform.rotation, direction));
            fingerDown = fingerUp;
        }
    }

    private void Zoom(float direction) {
        //TODO: Clamp zoom;
        cam.transform.Translate(cam.transform.forward * direction * zoomSpeed, Space.World);
    }

    private IEnumerator RotatePlatform(Quaternion startRotation, int direction) {
        Vector3 targetVector = Vector3.zero;
        int quadrant = ((int)startRotation.eulerAngles.y / 90) % 4 + 1;
        switch (quadrant) {
            case 1:
                if ((int)startRotation.eulerAngles.y % 90 == 0) {
                    targetVector = direction < 0 ? new Vector3(0, 270, 0) : new Vector3(0, 90, 0);
                } else {
                    targetVector = direction < 0 ? new Vector3(0, 0, 0) : new Vector3(0, 90, 0);
                }
                break;
            case 2:
                if ((int)startRotation.eulerAngles.y % 90 == 0) {
                    targetVector = direction < 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
                } else {
                    targetVector = direction < 0 ? new Vector3(0, 90, 0) : new Vector3(0, 180, 0);
                }
                break;
            case 3:
                if ((int)startRotation.eulerAngles.y % 90 == 0) {
                    targetVector = direction < 0 ? new Vector3(0, 90, 0) : new Vector3(0, 270, 0);
                } else {
                    targetVector = direction < 0 ? new Vector3(0, 180, 0) : new Vector3(0, 270, 0);
                }
                break;
            case 4:
                if ((int)startRotation.eulerAngles.y % 90 == 0) {
                    targetVector = direction < 0 ? new Vector3(0, 180, 0) : new Vector3(0, 360, 0);
                } else {
                    targetVector = direction < 0 ? new Vector3(0, 270, 0) : new Vector3(0, 360, 0);
                }
                break;
            default:
                break;
        }
        float cont = 0;
        while (cont <= 2) {
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(targetVector), cont);
            cont += Time.fixedDeltaTime*4;
            yield return new WaitForFixedUpdate();
        }
    }
}