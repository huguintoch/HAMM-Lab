using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuRaiseUp : MonoBehaviour {

    private RectTransform loginBackground,
                          compContainer,
                          rectCanvas;

    private Button buttonPlay,
                   buttonExit;

    private float initialHeight;

    private bool hidingMenu,
                 showingMenu;
    public bool menuActive { get; private set; }


    private void Awake() {
        GameObject canvas = GameObject.Find("Canvas");
        rectCanvas = canvas.GetComponent<RectTransform>();
        compContainer = canvas.transform.Find("Container").GetComponent<RectTransform>();

        loginBackground = compContainer.transform.Find("LoginBackground").GetComponent<RectTransform>();
        buttonPlay = compContainer.transform.Find("ButtonPlay").GetComponent<Button>();
        buttonExit = compContainer.transform.Find("ButtonExit").GetComponent<Button>();
        
    }

    private void Start() {
        RectTransform rectLogin = loginBackground.GetComponent<RectTransform>();
        rectLogin.sizeDelta = new Vector2(rectCanvas.sizeDelta.x + 20, rectCanvas.sizeDelta.y + 20);
        initialHeight = compContainer.localPosition.y;
        menuActive = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!hidingMenu) {
                ShowMenu();
            }
        }

        if (hidingMenu) {
            MoveCanvasObejcts(1);

            if (compContainer.localPosition.y - initialHeight >= rectCanvas.sizeDelta.y+20) {
                hidingMenu = false;
                menuActive = false;
                //Debug.Log("Hidden Menu");
            }
        } else if (showingMenu) {
            MoveCanvasObejcts(-1);

            if (compContainer.localPosition.y <= initialHeight) {
                compContainer.localPosition = new Vector3(compContainer.localPosition.x, initialHeight, compContainer.localPosition.z);
                buttonPlay.interactable = true;
                buttonExit.interactable = true;
                menuActive = true;
                showingMenu = false;
                //Debug.Log("Show Menu");
            }
        }
    }

    public void RaiseMenu() {
        buttonPlay.interactable = false;
        buttonExit.interactable = false;
        hidingMenu = true;
        menuActive = false;
        showingMenu = false;
    }

    public void ShowMenu() {
        showingMenu = true;
        hidingMenu = false;
        menuActive = false;
    }

    private void MoveCanvasObejcts(int direction) {
        Vector3 trans = new Vector3(0, 1200 * Time.deltaTime * direction, 0);

        Vector3 dest = compContainer.position + trans;
        compContainer.position = Vector3.Lerp(compContainer.position, dest, 0.5f);

    }


}
