using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Grid : MonoBehaviour {

    public static Dictionary<Element, string> gridElements = new Dictionary<Element, string>();

    [SerializeField]
    private int gridSize = 4;
    [SerializeField]
    private GameObject cameraContainer = null;

    private GameObject cubePrefab;
    private Camera mainCamera;
    private Element type;
    private int offset;

    private void Start() {
        // Definition of attributes
        cubePrefab = (GameObject)Resources.Load("Prefabs/GridCell", typeof(GameObject));
        mainCamera = Camera.main;
        offset = gridSize + 1;
        type = Element.Hamster;

        // Initial setups
        InitializeDictionary();
        InitializeCamera();
        InitializeGrid();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            AddGridElement();
        } else if (Input.GetMouseButtonDown(1)) {
            RemoveGridElement();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            type = Element.Hamster;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            type = Element.Treadmill;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            type = Element.Trampoline;
        }
    }

    private void InitializeDictionary() {
        gridElements.Add(Element.Hamster, "Prefabs/Hamster");
        gridElements.Add(Element.Treadmill, "Prefabs/Treadmill Model/Treadmill");
        gridElements.Add(Element.Trampoline, "Prefabs/Trampoline/Trampoline");
    }

    // Method to build the grid on which elements are placed
    private void InitializeGrid() {
        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                Instantiate(cubePrefab, new Vector3(i, 0, j), Quaternion.identity, this.transform);
            }
        }
    }

    // Method to initialize camera position
    private void InitializeCamera() {
        cameraContainer.transform.position = (gridSize % 2 == 0) ? new Vector3(gridSize / 2 - 0.5f, 0, gridSize / 2f - 0.5f) : new Vector3(Mathf.Floor(gridSize / 2), 0, Mathf.Floor(gridSize / 2));
        mainCamera.transform.position = new Vector3(gridSize + offset, offset, gridSize + offset);
        mainCamera.transform.rotation = Quaternion.Euler(25, 225, 0);
    }

    // Method to dinamically place element on grid depending on type selected
    private void AddGridElement() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            string path = gridElements[type];
            Instantiate((GameObject)Resources.Load(path, typeof(GameObject)), hit.transform.position + hit.normal, Quaternion.identity);
        }
    }

    // Method to remove element from grid by rigth cliking it
    private void RemoveGridElement() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag != "Grid") {
                Destroy(hit.collider.gameObject);
            }
        }
    }

}