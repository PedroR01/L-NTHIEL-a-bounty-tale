using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item", order = 2)]
public class Scriptable_Item : ScriptableObject
{
    public new string name;
    public Sprite icon = null;
    public bool isKey = false;

    public virtual void Use()
    {
        Debug.Log("You used a " + name);
    }
}