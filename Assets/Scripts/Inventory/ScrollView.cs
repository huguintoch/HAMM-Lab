using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public bool PointerOnInventory { get; private set; }

    public void OnPointerEnter(PointerEventData eventData) {
        PointerOnInventory = true;
        gameObject.GetComponent<ScrollRect>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        PointerOnInventory = false;
        gameObject.GetComponent<ScrollRect>().enabled = false;
    }

}
