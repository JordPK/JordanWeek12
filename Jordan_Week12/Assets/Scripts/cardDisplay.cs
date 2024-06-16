using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class cardDisplay : MonoBehaviour
{
    public GameObject statsCard;

    public TMP_Text npcName, description, armor, age, friendliness;

    public Image artwork;

    public void ShowCharacterCard(npcInfo stats)
    {
        statsCard.SetActive(true);
        npcName.text = "My Name is " + stats.name;
        description.text = stats.description;
        armor.text = "I have an armor level of " + stats.armourLevel.ToString();
        age.text = "I am " + stats.age + "Years Old";
        artwork.sprite = stats.npcSprite;

        if (stats.isFriendly)
            friendliness.text = "Let's be friends!";
        else friendliness.text = "What are you doing here?";
    }

}
    
