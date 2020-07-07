using UnityEngine;
using System;

//Class to storage multiple values in the dictionary
[Serializable]
public class ItemValues {

    public Element enumElement;
    public float price;
    private string location;
    private string img;

    public ItemValues(string path, string img) {
        this.location = path;
        this.img = img;
        this.price = 0.0f;
    }

    public string getPath() {
        return this.location;
    }

    public string getImg() {
        return this.img;
    }

    public float getPrice() {
        return this.price;
    }

    public void setPrice(float price) {
        this.price = price;
    }
}
