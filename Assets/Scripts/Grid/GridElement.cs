using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

    private GameObject objectPrefab;

    private void Start() {
        objectPrefab = (GameObject)Resources.Load("Prefabs/Object", typeof(GameObject));
    }

    public void InstantiateObject(Vector3 normal) {
        Instantiate(objectPrefab, transform.position + normal, Quaternion.identity);
    }
}