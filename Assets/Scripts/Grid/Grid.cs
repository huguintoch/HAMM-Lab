using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private int gridSize = 4;

    private GameObject cubePrefab;
    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
        cubePrefab = (GameObject)Resources.Load("Prefabs/GridCell", typeof(GameObject));
        for(int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                Instantiate(cubePrefab, new Vector3(i, 0, j), Quaternion.identity, this.transform);
            }
        }
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                hit.collider.gameObject.GetComponent<GridCell>().InstantiateObject(hit.normal);
            }
        }
    }
}
