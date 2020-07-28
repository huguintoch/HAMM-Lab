﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField]
    private Material[] statusMaterial = null;

    public static PlayerManager instance;
    private Camera mainCamera;
    private PointerTarget pointerTarget;
    private Material prevMaterial;
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

    void Start() {
        mainCamera = Camera.main;
        cameraContainer = GameObject.Find("Camera Container");
        scroller = GameObject.Find("Scroll View").GetComponent<ScrollView>();
        elementToAdd = null;
        pointerTarget = new PointerTarget(new Vector3(), null);
        prevMaterial = statusMaterial[2];
    }

    void Update() {

        PointerTarget newTarget = GetTarget();

        if (elementToAdd != null) {
            DragElementToAdd();
            SetTargetColor(newTarget);
        }

        pointerTarget = newTarget;
        SetCameraDrag();

        if (Input.GetMouseButtonDown(0) && (pointerTarget.TargetCollider != null && pointerTarget.TargetCollider.CompareTag("Grid") || scroller.PointerOnInventory)) {
            AddGridElement();
        } else if (Input.GetMouseButtonUp(0)) {
            SetGridElement();
        } else if (Input.GetMouseButtonDown(1)) {
            RemoveGridElement();
        }

    }

    private void DragElementToAdd() {
        elementToAdd.SetActive(!scroller.PointerOnInventory);
        elementToAdd.transform.position = pointerTarget.TargetPosition;
    }

    private void SetTargetColor(PointerTarget newTarget) {
        if (newTarget.TargetCollider != null) {
            if (newTarget.TargetCollider.CompareTag("Grid")) {
                prevMaterial = statusMaterial[2];
                newTarget.TargetCollider.transform.Find("Top").GetComponent<Renderer>().material = statusMaterial[0];
            } else {
                prevMaterial = statusMaterial[3];
                newTarget.TargetCollider.GetComponent<Renderer>().material = statusMaterial[1];
            }
        }
        if (pointerTarget.TargetCollider != null) {
            if (!pointerTarget.TargetPosition.Equals(newTarget.TargetPosition)) {
                if (pointerTarget.TargetCollider.CompareTag("Grid")) {
                    pointerTarget.TargetCollider.transform.Find("Top").GetComponent<Renderer>().material = statusMaterial[2];
                } else {
                    pointerTarget.TargetCollider.GetComponent<Renderer>().material = statusMaterial[3];
                }
            }
        }
    }

    private void SetCameraDrag() {
        if (pointerTarget.TargetCollider == null) {
            cameraContainer.GetComponent<CameraMovement>().Draggable = true;
        } else {
            cameraContainer.GetComponent<CameraMovement>().Draggable = false;
        }
    }

    public void AddGridElement() {
        string path = InvManager.instance.elements[InvManager.instance.Type].Location;
        elementToAdd = Instantiate((GameObject)Resources.Load(path, typeof(GameObject)), GetMouseAsWorldPoint(), Quaternion.identity);
        elementToAdd.GetComponent<Collider>().enabled = false;
        elementToAdd.SetActive(false);
    }

    // Method to dinamically place element on grid depending on type selected
    public void SetGridElement() {
        if (pointerTarget.TargetCollider != null) {
            if (pointerTarget.TargetCollider.CompareTag("Grid") && elementToAdd != null || InvManager.instance.Type == Element.Hamster) {
                if (!InvManager.instance.PlacedStructure()) {
                    return;
                }
                elementToAdd.transform.position = pointerTarget.TargetPosition;
                elementToAdd.GetComponent<Collider>().enabled = true;
                pointerTarget.TargetCollider.transform.Find("Top").GetComponent<Renderer>().material = statusMaterial[2]; // [TODO]: Remove harcode
            } else {
                Destroy(elementToAdd);
                pointerTarget.TargetCollider.GetComponent<Renderer>().material = statusMaterial[3]; // [TODO]: Remove harcode
            }
        } else {
            Destroy(elementToAdd);
        }
        elementToAdd = null;
    }

    // Method to remove element from grid by rigth cliking it
    public void RemoveGridElement() {
        if (pointerTarget.TargetCollider != null && !pointerTarget.TargetCollider.CompareTag("Grid")) {
            GridElement hitElement = pointerTarget.TargetCollider.GetComponent<GridElement>();
            if (hitElement != null) {
                InvManager.instance.SoldStructure(hitElement.Type);
            }
            Destroy(pointerTarget.TargetCollider);
        }
    }

    private Vector3 GetMouseAsWorldPoint() {
        Vector3 mInput = Input.mousePosition;
        mInput.z = 10;
        return mainCamera.ScreenToWorldPoint(mInput);
    }

    private PointerTarget GetTarget() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            float yOffset = hit.collider.bounds.size.y / 2 + 0.5f; //0.5 represents the height of the object that is going to be placed.
            return new PointerTarget(
                new Vector3(hit.transform.position.x, hit.transform.position.y + yOffset, hit.transform.position.z), 
                hit.collider.gameObject
            );
        } else {
            return new PointerTarget(GetMouseAsWorldPoint(), null);
        }
    }

}
