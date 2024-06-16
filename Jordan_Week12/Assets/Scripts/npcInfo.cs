using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC Information", fileName = "New NPC Info")]
public class npcInfo : ScriptableObject
{
    public string npcName;
    public string description;

    public Sprite npcSprite;

    public int armourLevel;
    public int age;
    public bool isFriendly;
    
}
