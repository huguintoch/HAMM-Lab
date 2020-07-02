using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

    // protected GameObject objectPrefab;

    public virtual void Start() {
        // objectPrefab = (GameObject)Resources.Load("Prefabs/Object", typeof(GameObject));
    }

    public virtual void Update() {

    }

    /*public void InstantiateObject(Vector3 normal) {
        Instantiate(objectPrefab, transform.position + normal, Quaternion.identity);
    }*/

}