using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu Character", menuName = "My Stuffs/Character", order = 1)]
public class Character : ScriptableObject
{
    public string key_name;
    public Team team;
    public float summonCost;
    [Space(3)]
    [Header("Menu Info")]
    public Sprite sprite;
    public string key_description;
    [Header("In-game Behaviours")]
    public GameObject prefab;


}
