using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvManager : MonoBehaviour {

    public static InvManager instance;

    public Dictionary<Element, ItemValues> elements = new Dictionary<Element, ItemValues>();
    
    [SerializeField]
    private float money = 0f;
    [SerializeField]
    private ItemValues[] objectDeclaration=null;

    //Selected element
    private Element type;
    public Element Type {
        get { return type; }
        set { type = value; }
    }

    //GUI
    private GameObject buttonPreset, background, content, moneyPanel;
    private TextMeshProUGUI moneyText;

    //Button control
    private ButtonController currentButton;
    public ButtonController CurrentButton {
        get { return currentButton; }
        set { currentButton = value; } }



    //Singleton
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    void Start() {
        //Default selected element
        type = Element.Grid;

        //Get references for the GUI elements
        //canvas = GameObject.FindObjectOfType<Canvas>();
        background = transform.Find("InvBackground").gameObject;
        content = background.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        buttonPreset = (GameObject)Resources.Load("Prefabs/GUI/InvItem", typeof(GameObject));

        //Money panel initialization
        moneyPanel = transform.Find("MoneySign").gameObject;
        moneyText = moneyPanel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = "Money: $" + money;

        InitializeDictionary();

        DeclareObjects();

        InitializeGUI();
    }


    private void InitializeDictionary() {
        elements.Add(Element.Grid, new ItemValues(null,null));
        elements.Add(Element.Hamster, new ItemValues("Prefabs/Hamster", "Images/GUI/Icon03"));
        elements.Add(Element.Treadmill, new ItemValues("Prefabs/Treadmill Model/Treadmill", "Images/GUI/Icon04"));
        elements.Add(Element.Trampoline, new ItemValues("Prefabs/Trampoline/Trampoline", "Images/GUI/Icon05"));
    }

    //Declares values from the inspector
    private void DeclareObjects() {
        foreach (ItemValues overwrite in objectDeclaration) {
            
            if (!elements.ContainsKey(overwrite.EnumElement)) {
                return;
            }

            if (overwrite.Price < 0) {//Condition to remove an element from the inventory
                elements.Remove(overwrite.EnumElement);
            } else {
                elements[overwrite.EnumElement].Price = overwrite.Price;
            }
        }
    }

    //Creates buttons and initializes them
    private void InitializeGUI() {

        RectTransform rectCont = content.GetComponent<RectTransform>();

        float distance = 0f;

        foreach (KeyValuePair<Element, ItemValues> item in elements) {
            
            if (item.Key.Equals(Element.Grid)) 
                continue;

            item.Value.EnumElement = item.Key;
            GameObject tmpBtn = Instantiate(buttonPreset, content.transform);

            RectTransform rectBtn = tmpBtn.GetComponent<RectTransform>();

            rectBtn.anchoredPosition= new Vector2(distance, 0f);
            distance +=rectBtn.sizeDelta.x+(rectBtn.sizeDelta.x/6f);

            rectCont.sizeDelta = new Vector2(distance, rectCont.sizeDelta.y);

            ButtonController tmpControl = tmpBtn.GetComponent<ButtonController>();
            tmpControl.SetValues(item.Value.Img, item.Value.Price, item.Key);
        }


    }

    public bool PlacedStructure() {
        if (money >= elements[type].Price) {
            money -= elements[type].Price;
            moneyText.text = "Money: $" + money;
            return true;
        } else {
            return false;
        }

    }

    public void SoldStructure(Element e) {
        money += elements[e].Price;
        moneyText.text = "Money: $" + money;
    }
}
