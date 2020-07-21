using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private GameObject playerManager;

    private void Start() {
        playerManager = GameObject.Find("Player Manager");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        gameObject.GetComponent<ScrollRect>().enabled = true;
        playerManager.GetComponent<PlayerManager>().DragFromInventory = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        gameObject.GetComponent<ScrollRect>().enabled = false;
        playerManager.GetComponent<PlayerManager>().DragFromInventory = false;
    }

}
