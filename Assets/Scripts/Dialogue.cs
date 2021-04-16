using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string nameField;
    public Sprite icon;
    [TextArea(3, 10)] public string sentences;

}
