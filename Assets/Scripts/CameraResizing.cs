using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraResizing : MonoBehaviour
{
    [SerializeField]
    private float desiredWidth = 1;

    private void Start() {
        float def = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = def * desiredWidth / 2;
    }


}
