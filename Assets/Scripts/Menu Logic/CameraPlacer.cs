using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraPlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager=null,
                       CameraPoint=null;

    [SerializeField]
    private float desiredWidth = 1;

    private void Start() {
        Resize2DCamera(desiredWidth);
        transform.position = new Vector3(transform.position.x, CameraPoint.transform.position.y+Camera.main.orthographicSize, transform.position.z);
        GameManager.GetComponent<MenuInput>().camInitialY = transform.position.y;
        Destroy(CameraPoint);
    }

    private void Resize2DCamera(float desiredWidth) {
        float def = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = def * desiredWidth / 2;
    }


}
