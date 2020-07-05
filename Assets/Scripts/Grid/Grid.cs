using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Grid : MonoBehaviour {


    [SerializeField]
    private int gridSize = 4;
    [SerializeField]
    private GameObject cameraContainer = null;

    private GameObject cubePrefab;
    private Camera mainCamera;
    private int offset;

    private void Start() {
        // Definition of attributes
        cubePrefab = (GameObject)Resources.Load("Prefabs/GridCell", typeof(GameObject));
        mainCamera = Camera.main;
        offset = gridSize + 1;

        // Initial setups
        InitializeCamera();
        InitializeGrid();
        InitializeCamera();
        
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            AddGridElement();
        } else if (Input.GetMouseButtonDown(1)) {
            RemoveGridElement();
        }
    }

    // Method to build the grid on which elements are placed
    private void InitializeGrid() {
        int currentLevel = LevelsInfo.currentLevel;
        gridSize = LevelsInfo.grids[currentLevel].GetLength(0);
        
        float prefabHeight = cubePrefab.transform.localScale.y;
        bool goalReady = false,
             playerReady = false;
        for (int i = 0; i < LevelsInfo.grids[currentLevel].GetLength(0); i++) {
            for (int j = 0; j < LevelsInfo.grids[currentLevel].GetLength(1); j++) {
                int tileHeight = LevelsInfo.grids[currentLevel][i, j];
                GameObject tileToSpawn = cubePrefab;

                if (!goalReady && tileHeight < 0) { //Goal Tile
                    //tileToSpawn = goalPrefab;
                    tileHeight *= -1;
                    goalReady = true;
                }else if (!playerReady && tileHeight > 50) { //Player Position
                    //*** Missing logic to spawn player with initial speed. ***
                    tileHeight = tileHeight - 50;
                    playerReady = true;
                }

                if (tileHeight != 0) { //Spawn floor in tile;
                    GameObject newCube = Instantiate(tileToSpawn, new Vector3(i, 0, j), Quaternion.identity, this.transform);
                    newCube.transform.Translate(0, (tileHeight*prefabHeight) / 2, 0);
                    newCube.transform.localScale = new Vector3(1, tileHeight * prefabHeight, 1);
                }

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
            if (!InvManager.instance.placedStructure())
            {
                return;
            }
            string path = InvManager.instance.elements[InvManager.instance.getType()].getPath();
            Instantiate((GameObject)Resources.Load(path, typeof(GameObject)), hit.transform.position + hit.normal, Quaternion.identity);
        }
    }

    // Method to remove element from grid by rigth cliking it
    private void RemoveGridElement() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag != "Grid") {
                //InvManager.instance.soldStructure(hit.collider.GetComponent<>);
                Destroy(hit.collider.gameObject);
            }
        }
    }

}