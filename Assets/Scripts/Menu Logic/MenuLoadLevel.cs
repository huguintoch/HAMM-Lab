using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MenuLoadLevel : MonoBehaviour
{
    private static GameObject GridCreator;

    [SerializeField]
    private int desiredGridSize=5;

    private int level;

    private void Awake() {
        try {
            level = Int32.Parse(GetComponentInChildren<TextMeshPro>().text);
        } catch (FormatException) {
            Debug.Log("The block number: " + GetComponentInChildren<TextMeshPro>().text + "couldn't be loaded.");
        }
        if (!GridCreator) {
            GridCreator = Instantiate((GameObject)Resources.Load("Prefabs/GridSizeAssigner", typeof(GameObject))) as GameObject;
            DontDestroyOnLoad(GridCreator);
        }
    }

    public void LoadLevel() {
        GridCreator.GetComponent<GridSizeAssigner>().gridSize = desiredGridSize;
        SceneManager.LoadScene("Grid");
    }
        
}
