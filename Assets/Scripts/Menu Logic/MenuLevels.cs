using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuLevels : MonoBehaviour
{
    [SerializeField]
    private int noLevels = 1;

    private void Start()
    {
        float height = 1;
        GameObject levelPicker;
        Vector3 position;
        for (int i = 0; i < noLevels; i++) {
            position = new Vector3(0, height, transform.position.z);
            levelPicker = Instantiate((GameObject)Resources.Load("Prefabs/LevelPicker", typeof(GameObject)), position, Quaternion.identity, transform) as GameObject;
            levelPicker.GetComponentInChildren<TextMeshPro>().text = (i + 1)+"";
            height += 1.5f;
        }
    }

}
