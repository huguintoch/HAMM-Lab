using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public bool PointerOnInventory { get; private set; }

    private ScrollRect scrollRect;

    private void Awake() {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        PointerOnInventory = true;
        scrollRect.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        PointerOnInventory = false;
        scrollRect.enabled = false;
    }
}
