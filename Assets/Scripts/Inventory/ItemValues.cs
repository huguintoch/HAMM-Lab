using UnityEngine;
using System;

//Class to storage multiple values in the dictionary
[Serializable]
public class ItemValues {

    [SerializeField]
    private Element enumElement;
    public Element EnumElement {
        get { return enumElement; }
        set { enumElement = value; }
    }

    [SerializeField]
    private float price;
    public float Price {
        get { return price; }
        set { price = value; }
    }

    private string location;
    public string Location {
        get { return location; }
    }

    private string img;
    public string Img {
        get { return img; }
    }


    public ItemValues(string path, string img) {
        this.location = path;
        this.img = img;
        this.price = 0.0f;
    }
}
