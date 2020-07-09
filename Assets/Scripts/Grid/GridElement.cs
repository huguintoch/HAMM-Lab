using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

    private Element type;
    public Element Type {
        get { return type; }
        set { type = value; }
    }
    // protected GameObject objectPrefab;
    public GridElement() {
        type = Element.Grid;
    }

    public virtual void Start() {
        // objectPrefab = (GameObject)Resources.Load("Prefabs/Object", typeof(GameObject));
    }

    public virtual void Update() {

    }

    /*public void InstantiateObject(Vector3 normal) {
        Instantiate(objectPrefab, transform.position + normal, Quaternion.identity);
    }*/

}