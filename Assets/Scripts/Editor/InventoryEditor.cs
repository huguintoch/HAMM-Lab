using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemValues))]
public class InventoryEditor : PropertyDrawer
{
    Rect drop, priceRec, priceLabel;
    SerializedProperty dropDownElements, price;
    string description;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        drop = new Rect(position.x, position.y, position.width / 2.0f, position.height);
        priceLabel = new Rect(position.x + drop.width, position.y, position.width / 8, position.height);
        priceRec = new Rect(priceLabel.position.x + priceLabel.width,
                            position.y,
                            position.width - (priceLabel.position.x + priceLabel.width),
                            position.height);

        dropDownElements = property.FindPropertyRelative("enumElement");
        price = property.FindPropertyRelative("price");
        //GUI.Label(position,label)
        EditorGUI.PropertyField(drop, dropDownElements, GUIContent.none);
        EditorGUI.LabelField(priceLabel, "Price");
        EditorGUI.PropertyField(priceRec, price, GUIContent.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
