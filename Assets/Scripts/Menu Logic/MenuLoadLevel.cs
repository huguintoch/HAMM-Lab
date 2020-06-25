using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuLoadLevel : MonoBehaviour
{
    

    private int level;

    private void Awake() {
        try {
            level = Int32.Parse(GetComponentInChildren<TextMeshPro>().text);
        } catch (FormatException) {
            Debug.Log("The block number: " + GetComponentInChildren<TextMeshPro>().text + "couldn't be loaded.");
        }
        
    }

    

    public void LoadLevel() {
        Debug.Log("Missing loading level logic");
    }
        
}
