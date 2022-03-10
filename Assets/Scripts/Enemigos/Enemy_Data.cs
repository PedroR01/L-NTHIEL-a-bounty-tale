using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy", order = 1)]
public class Enemy_Data : ScriptableObject
{
    public new string name;
    public float life;
    public float armour;
    public float damage;
    public float speed;
}