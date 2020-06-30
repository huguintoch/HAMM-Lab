using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridSizeAssigner : MonoBehaviour
{
    public int gridSize;

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name.Equals("Grid")) {
            //GameObject.Find("Grid").GetComponent<Grid>().gridSize = gridSize;
            Destroy(gameObject);
        }
    }
}
