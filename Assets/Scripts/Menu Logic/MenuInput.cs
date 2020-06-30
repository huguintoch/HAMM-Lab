using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour {
    [SerializeField]
    private float dragSpeed=1;

    private MenuRaiseUp menuRaise;
    private Camera cam;
    private Vector2 dragOrigin;

    public float camInitialY;

    private bool dragging;

    private void Awake() {
        menuRaise = GameObject.Find("GameManager").GetComponent<MenuRaiseUp>();
        cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !menuRaise.menuActive) {
            if (Application.isEditor) {
                dragOrigin = Input.mousePosition;
            } else {
                dragOrigin = Input.GetTouch(0).position;
            }
            dragging = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20f)) {
                hit.collider.gameObject.GetComponent<MenuLoadLevel>().LoadLevel();
            }
            return;
        }

        if (dragging && Input.GetMouseButton(0)) {
            Vector3 pos;
            if (Application.isEditor) {
                pos = Camera.main.ScreenToViewportPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y) - dragOrigin);
            } else {
                pos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position - dragOrigin);
            }

            Vector3 move = new Vector3(0, -pos.y * dragSpeed,0);
            cam.transform.Translate(move);

            if (cam.transform.position.y < camInitialY) {
                cam.transform.position = new Vector3(0, camInitialY, 0);
            }
        }

        if(dragging && Input.GetMouseButtonUp(0)) {
            dragging = false;
        }
    }
}
