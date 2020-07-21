using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerDownHandler {
    [SerializeField]
    private Image img = null;
    [SerializeField]
    private TextMeshProUGUI price = null;
    [SerializeField]
    private GameObject selectLayout = null;

    private Element type;
    private Animator anim=null;
    

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void SetValues(string img, float price, Element type) {
        this.img.sprite = (Sprite)Resources.Load(img, typeof(Sprite));
        this.price.text = "$" + price;
        this.type = type;
    }

    public void Deselect() {
        selectLayout.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (InvManager.instance.CurrentButton != null) {
            InvManager.instance.CurrentButton.Deselect();
        }

        selectLayout.SetActive(true);
        anim.SetTrigger("Select");
        InvManager.instance.CurrentButton = this;
        InvManager.instance.Type = type;
    }
}
