using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData) {
        gameObject.GetComponent<ScrollRect>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        gameObject.GetComponent<ScrollRect>().enabled = false;
    }

}
