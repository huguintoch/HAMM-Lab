using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvManager : MonoBehaviour
{

    public static InvManager instance;

    public Dictionary<Element, ItemValues> elements = new Dictionary<Element, ItemValues>();
    public ItemValues[] objectDeclaration;
   
    //Selected element
    private Element type;

    //GUI
    private GameObject buttonPreset,background;
    private Canvas canvas;

    //Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //Default selected element
        type = Element.Hamster;

        //Get references for the GUI elements
        canvas = GameObject.FindObjectOfType<Canvas>();
        background = Instantiate((GameObject)Resources.Load("Prefabs/GUI/InvBackground", typeof(GameObject)),canvas.transform);
        buttonPreset = (GameObject)Resources.Load("Prefabs/GUI/InvItem", typeof(GameObject));

        InitializeDictionary();

        DeclareObjects();

        CreateButtons();
    }

    //Declares values from the inspector
    private void DeclareObjects()
    {
        foreach (ItemValues overwrite in objectDeclaration)
        {
            if (overwrite.getPrice() < 0)//Condition to remove an element from the inventory
            {
                elements.Remove(overwrite.enumElement);
            }
            else
            {
                elements[overwrite.enumElement].setPrice(overwrite.getPrice());
            }
        }
    }

    private void InitializeDictionary()
    {
        elements.Add(Element.Hamster, new ItemValues("Prefabs/Hamster", "Images/GUI/Icon03"));
        elements.Add(Element.Treadmill, new ItemValues("Prefabs/Treadmill Model/Treadmill", "Images/GUI/Icon04"));
        elements.Add(Element.Trampoline, new ItemValues("Prefabs/Trampoline/Trampoline", "Images/GUI/Icon05"));
    }

    //Creates buttons for each declared element
    private void CreateButtons()
    {
        float space = -1.2f;

        foreach (KeyValuePair<Element, ItemValues> item in elements)
        {
            item.Value.enumElement = item.Key;
            GameObject tmpBtn=Instantiate(buttonPreset,background.transform);
            tmpBtn.transform.position = new Vector2(tmpBtn.transform.position.x +(tmpBtn.GetComponent<RectTransform>().rect.width* space),tmpBtn.transform.position.y);

            ButtonController tmpControl = tmpBtn.GetComponent<ButtonController>();
            tmpControl.setValues(item.Value.getImg(), item.Value.getPrice(), item.Key);
            space += 1.2f;
        }


    }

    public Element getType()
    {
        return this.type;
    }

    public void setType(Element type)
    {
        this.type = type;
    }
}
