using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOptions : MonoBehaviour
{
    Animator anim;
    float deletionTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        deletionTime = anim.GetCurrentAnimatorStateInfo(0).length;
    }

    private void CloseMenu() {
        anim.SetTrigger("Close");
        Destroy(gameObject, deletionTime);
    }

    public void CancelButton() {
        CloseMenu();
    }

    public void AcceptButton() {
        CloseMenu();
        //Accepted Code
    }

    public void RotateButton() {
        //Rotation Code
    }
}
