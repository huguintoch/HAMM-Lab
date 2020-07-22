using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    private Camera mainCamera;
    private ScrollView scroller;
    private GameObject elementToAdd,
                       cameraContainer;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start() {
        mainCamera = Camera.main;
        cameraContainer = GameObject.Find("Camera Container");
        scroller = GameObject.Find("Scroll View").GetComponent<ScrollView>();
        elementToAdd = null;
    }

    // Update is called once per frame
    void Update() {

        if (elementToAdd != null) {
            elementToAdd.transform.position = GetTargetPosition().Position;
        }
        if (Input.GetMouseButtonDown(0) && (GetTargetPosition().IsGrid || scroller.PointerOnInventory)) {
            AddGridElement();
        } else if (Input.GetMouseButtonUp(0)) {
            SetGridElement();
        } else if (Input.GetMouseButtonDown(1)) {
            RemoveGridElement();
        }
        cameraContainer.GetComponent<CameraMovement>().Draggable = !GetTargetPosition().IsGrid;

    }

    public void AddGridElement() {
        if (!InvManager.instance.PlacedStructure()) {
            return;
        }
        string path = InvManager.instance.elements[InvManager.instance.Type].Location;
        elementToAdd = Instantiate((GameObject)Resources.Load(path, typeof(GameObject)), GetMouseAsWorldPoint(), Quaternion.identity);
        elementToAdd.GetComponent<Collider>().enabled = false;
    }

    // Method to dinamically place element on grid depending on type selected
    public void SetGridElement() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Grid" || InvManager.instance.Type == Element.Hamster) && elementToAdd != null) {
            float floorHeight = 0.75f;
            elementToAdd.transform.position = hit.transform.position + hit.normal*floorHeight ;
            elementToAdd.GetComponent<Collider>().enabled = true;
        } else {
            Destroy(elementToAdd);
        }
        elementToAdd = null;
    }

    // Method to remove element from grid by rigth cliking it
    public void RemoveGridElement() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag != "Grid") {
                GridElement hitElement = hit.collider.GetComponent<GridElement>();
                if (hitElement != null) {
                    InvManager.instance.SoldStructure(hitElement.Type);
                }
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private Vector3 GetMouseAsWorldPoint() {
        Vector3 mInput = Input.mousePosition;
        mInput.z = 10;
        return mainCamera.ScreenToWorldPoint(mInput);
    }

    private TargetPosition GetTargetPosition() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            return new TargetPosition(hit.transform.position + hit.normal, true);
        } else {
            return new TargetPosition(GetMouseAsWorldPoint(), false);
        }
    }

}
