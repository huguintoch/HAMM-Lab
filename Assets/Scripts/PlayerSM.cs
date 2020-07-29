using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    public static PlayerSM instance;
    //0 - User is not pressing anywhere or doing anything. Will be assigned by external scripts to end an action.
    //1 - User began to drag/select an object from the inventory.
    //2 - User is dragging the selected object.
    //3 - User has selected an object, but decided not to drag it.
    //4 - The dragged/selected object is being placed.
    //5 - User is trying to rotate the camera.
    //6 - It is POSSIBLE that user is trying to click and hold to open rotation.
    //7 - User is clicking and holding an object to open rotation.
    //8 - The rotation menu is open.
    public static byte state;
    [SerializeField]
    private float CHLimit = 0.5f;

    private Camera mainCamera;
    private ScrollView scroller;

    private float timer;

    private bool clickStarted,
                 clickHolding;

    private void Awake() {
        if (!instance) {
            instance = this;
            state = 0;
        } else {
            Destroy(this);
        }

        scroller = GameObject.Find("Scroll View").GetComponent<ScrollView>();
        mainCamera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (state == 0) { //User is in default state.
                if (InvManager.instance.Type != Element.Grid) { //The user has selected an object from the inventory.
                    state = 1;
                } else if (GetTarget().TargetCollider!= null && GetTarget().TargetCollider.CompareTag("Object")) { //User is trying to click and hold an object;
                    state = 6;
                    timer = 0;
                } else if (!scroller.PointerOnInventory) { //User click outside the inventory.
                    state = 5;
                }
                return;
            }

            if (state == 3) { //User has an object selected and click.
                state = 2;
                return;
            }
        }

        if (Input.GetMouseButton(0)) {
            if (state == 6) {
                if (ClickHoldCheck()) {
                    state = 7;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (state == 2) { //User is dragging an object.
                PointerTarget target = GetTarget();
                if (scroller.PointerOnInventory) {
                    state = 3;
                } else if (target.TargetCollider == null || target.TargetCollider.tag.Equals("Grid")) {
                    state = 4;
                }
                return;
            }

            if(state == 6) { //User was trying to click and hold, but release the button.
                state = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            Debug.Log(state);
        }
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

    private bool ClickHoldCheck() {
        timer += Time.deltaTime;

        if (timer > CHLimit) {
            clickHolding = true;
            clickStarted = false;
            return true;
        }

        return false;
    }


}
