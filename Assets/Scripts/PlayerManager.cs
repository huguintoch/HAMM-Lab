using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    private GameObject elementToAdd;
    private Camera mainCamera;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start() {
        elementToAdd = null;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        
        if (elementToAdd != null) {
            elementToAdd.transform.position = GetMouseAsWorldPoint();
        }
        if (Input.GetMouseButtonDown(0)) {
            AddGridElement();
        } else if (Input.GetMouseButtonUp(0)) {
            SetGridElement();
        } else if (Input.GetMouseButtonDown(1)) {
            RemoveGridElement();
        }
    }

    public void AddGridElement() {
        if (!InvManager.instance.PlacedStructure()) {
            return;
        }
        string path = InvManager.instance.elements[InvManager.instance.Type].Location;
        elementToAdd = Instantiate((GameObject)Resources.Load(path, typeof(GameObject)), GetMouseAsWorldPoint(), Quaternion.identity);
    }

    // Method to dinamically place element on grid depending on type selected
    public void SetGridElement() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            elementToAdd.transform.position = hit.transform.position + hit.normal;
            elementToAdd = null;
        }
    }

    // Method to remove element from grid by rigth cliking it
    public void RemoveGridElement() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag != "Grid") {
                //InvManager.instance.soldStructure(hit.collider.GetComponent<>);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private Vector3 GetMouseAsWorldPoint() {
        Vector3 mInput = Input.mousePosition;
        mInput.z = 10;
        return mainCamera.ScreenToWorldPoint(mInput);
    }

}
