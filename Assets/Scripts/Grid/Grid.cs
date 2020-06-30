using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private int gridSize = 4;
    [SerializeField]
    private GameObject cameraContainer;

    private GameObject cubePrefab;
    private Camera mainCamera;
    private int offset;

    private void Start() {
        mainCamera = Camera.main;
        cubePrefab = (GameObject)Resources.Load("Prefabs/GridCell", typeof(GameObject));
        for(int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                Instantiate(cubePrefab, new Vector3(i, 0, j), Quaternion.identity, this.transform);
            }
        }
        cameraContainer.transform.position = (gridSize % 2 == 0) ? new Vector3(gridSize/2-0.5f, 0, gridSize/2f-0.5f) : new Vector3(Mathf.Floor(gridSize / 2), 0, Mathf.Floor(gridSize / 2));

        offset = gridSize + 1;
        mainCamera.transform.position = new Vector3(gridSize+offset, offset, gridSize+offset);
        mainCamera.transform.rotation = Quaternion.Euler(25, 225, 0);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                hit.collider.gameObject.GetComponent<GridElement>().InstantiateObject(hit.normal);
            }
        } else if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag != "Grid") {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

}
