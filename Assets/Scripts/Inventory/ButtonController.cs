using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    [SerializeField]
    private Image img = null;
    [SerializeField]
    private TextMeshProUGUI price = null;

    private Element type;

    public void setValues(string img, float price, Element type) {
        this.img.sprite = (Sprite)Resources.Load(img, typeof(Sprite));
        this.price.text = "$" + price;
        this.type = type;
    }


    public void action() {
        InvManager.instance.setType(type);
    }
}
