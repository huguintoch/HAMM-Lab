using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {


    [SerializeField]
    private int gridSize = 4;
    [SerializeField]
    private GameObject cameraContainer = null;

    private GameObject cubePrefab,
                       goalPrefab,
                       spawnPrefab;
    private Camera mainCamera;
    private int offset;

    private void Start() {
        // Definition of attributes
        cubePrefab = (GameObject)Resources.Load("Prefabs/GridCell", typeof(GameObject));
        goalPrefab = (GameObject)Resources.Load("Prefabs/GoalCell", typeof(GameObject));
        spawnPrefab = (GameObject)Resources.Load("Prefabs/HamsterSpawn", typeof(GameObject));
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
                    tileHeight *= -1;
                    tileHeight++;
                    tileToSpawn = goalPrefab;
                    goalReady = true;

                }else if (!playerReady && tileHeight > 50) { //Player Spawner
                    Vector3 spawnPosition = Vector3.zero;
                    Quaternion rotation = Quaternion.identity;
                    tileHeight = tileHeight - 50;
                    //Detect in which edge will the spawner will be
                    if (i==0) {
                        spawnPosition = new Vector3(i - 0.5f, (tileHeight+1)*prefabHeight, j);
                        rotation = Quaternion.Euler(0, 180, 0);
                    }else if(i== LevelsInfo.grids[currentLevel].GetLength(0) - 1) {
                        spawnPosition = new Vector3(i + 0.5f, (tileHeight+1)*prefabHeight, j);
                        rotation = Quaternion.Euler(0, 0, 0);
                    }else if (j == 0) {
                        spawnPosition = new Vector3(i, (tileHeight + 1) * prefabHeight, j-0.5f);
                        rotation = Quaternion.Euler(0, 90, 0);
                    } else if(j == LevelsInfo.grids[currentLevel].GetLength(1) - 1){
                        spawnPosition = new Vector3(i, (tileHeight + 1) * prefabHeight, j + 0.5f);
                        rotation = Quaternion.Euler(0, -90, 0);
                    }

                    Instantiate(spawnPrefab, spawnPosition, rotation, this.transform);
                    playerReady = true;
                }

                if (tileHeight != 0) { //Spawn tile
                    GameObject newCube = Instantiate(tileToSpawn, new Vector3(i, (tileHeight * prefabHeight) / 2, j), Quaternion.identity, this.transform);
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
            if (!InvManager.instance.PlacedStructure())
            {
                return;
            }
            string path = InvManager.instance.elements[InvManager.instance.Type].Location;
            Instantiate((GameObject)Resources.Load(path, typeof(GameObject)), hit.transform.position + hit.normal, Quaternion.identity);
        }
    }

    // Method to remove element from grid by rigth cliking it
    private void RemoveGridElement() {
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

}