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
    private ItemValues[] objectDeclaration;

    //Selected element
    private Element type;

    //GUI
    private GameObject buttonPreset, background, content, moneyPanel;
    private TextMeshProUGUI moneyText;
    private RectTransform rectBg;
    private Canvas canvas;



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
        type = Element.Hamster;

        //Get references for the GUI elements
        canvas = GameObject.FindObjectOfType<Canvas>();
        background = Instantiate((GameObject)Resources.Load("Prefabs/GUI/InvBackground", typeof(GameObject)), canvas.transform);
        content = background.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        buttonPreset = (GameObject)Resources.Load("Prefabs/GUI/InvItem", typeof(GameObject));
        moneyPanel = Instantiate((GameObject)Resources.Load("Prefabs/GUI/MoneySign", typeof(GameObject)), canvas.transform);
        rectBg = background.GetComponent<RectTransform>();
        moneyText = moneyPanel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = "Money: $" + money;

        InitializeDictionary();

        DeclareObjects();

        InitializeGUI();
    }


    //Declares values from the inspector
    private void DeclareObjects() {
        foreach (ItemValues overwrite in objectDeclaration) {
            //Condition to remove an element from the inventory
            if (overwrite.getPrice() < 0) {
                elements.Remove(overwrite.enumElement);
            } else {
                elements[overwrite.enumElement].setPrice(overwrite.getPrice());
            }
        }
    }

    private void InitializeDictionary() {
        elements.Add(Element.Hamster, new ItemValues("Prefabs/Hamster", "Images/GUI/Icon03"));
        elements.Add(Element.Treadmill, new ItemValues("Prefabs/Treadmill Model/Treadmill", "Images/GUI/Icon04"));
        elements.Add(Element.Trampoline, new ItemValues("Prefabs/Trampoline/Trampoline", "Images/GUI/Icon05"));
    }

    //Creates buttons for each declared element and assigns size to all GUI components
    private void InitializeGUI() {
        rectBg.sizeDelta = new Vector2(rectBg.sizeDelta.x, canvas.GetComponent<RectTransform>().sizeDelta.y / 5);

        RectTransform rectCont = content.GetComponent<RectTransform>();
        rectCont.sizeDelta = new Vector2(rectCont.sizeDelta.x, rectBg.sizeDelta.y * 0.6f);

        RectTransform rectMoney = moneyPanel.GetComponent<RectTransform>();
        rectMoney.sizeDelta = new Vector2(canvas.GetComponent<RectTransform>().sizeDelta.x / 2, rectMoney.sizeDelta.y);

        float height = Mathf.Abs(rectCont.sizeDelta.y);

        float distance = background.transform.Find("Scroll View").Find("Scrollbar Horizontal").gameObject.GetComponent<RectTransform>().offsetMin.x;
        float offset = distance / 2;

        foreach (KeyValuePair<Element, ItemValues> item in elements) {


            item.Value.enumElement = item.Key;
            GameObject tmpBtn = Instantiate(buttonPreset, content.transform);

            RectTransform rectBtn = tmpBtn.GetComponent<RectTransform>();
            rectBtn.sizeDelta = new Vector2(rectBtn.sizeDelta.x, height);

            tmpBtn.transform.position = new Vector2(distance, tmpBtn.transform.position.y);//Position
            distance += rectBtn.sizeDelta.x + offset;

            rectCont.sizeDelta = new Vector2(distance, rectCont.sizeDelta.y);

            ButtonController tmpControl = tmpBtn.GetComponent<ButtonController>();
            tmpControl.setValues(item.Value.getImg(), item.Value.getPrice(), item.Key);
        }


    }

    public bool placedStructure() {
        if (money >= elements[type].getPrice()) {
            money -= elements[type].getPrice();
            moneyText.text = "Money: $" + money;
            return true;
        } else {
            return false;
        }

    }

    public void soldStructure(Element e) {
        money += elements[e].getPrice();
        moneyText.text = "Money: $" + money;
    }

    public Element getType() {
        return this.type;
    }

    public void setType(Element type) {
        this.type = type;
    }
}
