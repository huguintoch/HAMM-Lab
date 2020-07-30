using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private Animator anim;
    private Camera mainCamera;
    private GameObject currentObject,
                       menuHolder;
    private Transform currentTransform;

    private float deletionTime;

    private void Awake() {
        mainCamera = Camera.main;
        menuHolder = transform.Find("ItemOptions").gameObject;
        anim = menuHolder.GetComponent<Animator>();
    }

    private void Start() {
        deletionTime = anim.GetCurrentAnimatorStateInfo(0).length;
        menuHolder.SetActive(false);
    }

    private void Update() {
        if (PlayerSM.state == 7) {
            if (GetTarget().TargetCollider.CompareTag("Object")) {
                currentObject = GetTarget().TargetCollider.gameObject;
                currentTransform = currentObject.transform;
                transform.position = mainCamera.WorldToScreenPoint(currentObject.transform.position);
                menuHolder.SetActive(true);
                PlayerSM.state = 8;
            }
        }
    }

    private void CloseMenu() {
        anim.SetTrigger("Close");
        Invoke("Deactivate", deletionTime);
    }

    public void CancelButton() {
        CloseMenu();
        currentObject.transform.position = currentTransform.position;
        currentObject.transform.rotation = currentTransform.rotation;
        PlayerSM.state = 0;
    }

    public void AcceptButton() {
        CloseMenu();
        PlayerSM.state = 0;
    }

    public void RotateButton() {
        currentObject.transform.Rotate(0, 90, 0);
    }


    private PointerTarget GetTarget() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            return new PointerTarget(
                new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z),
                hit.collider.gameObject
            );
        } else {
            return new PointerTarget(GetMouseAsWorldPoint(), null);
        }
    }

    private Vector3 GetMouseAsWorldPoint() {
        Vector3 mInput = Input.mousePosition;
        mInput.z = 10;
        return mainCamera.ScreenToWorldPoint(mInput);
    }

    private void Deactivate() {
        menuHolder.SetActive(false);
    }
}
