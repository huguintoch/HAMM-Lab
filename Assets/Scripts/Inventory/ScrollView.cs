using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    private void OnMouseEnter() {
        Debug.Log("Mouse is over GameObject.");
        gameObject.GetComponent<ScrollRect>().enabled = true;
    }

    private void OnMouseOver() {
        Debug.Log("Mouse is out GameObject.");
        gameObject.GetComponent<ScrollRect>().enabled = false;
    }
}
