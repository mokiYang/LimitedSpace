using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagManager : MonoBehaviour
{
    public Dictionary<string, int> items;
    public TextMeshProUGUI bagText;

    private void Start()
    {
        items = new Dictionary<string, int>();
    }

    public void UpdateUI()
    {
        string text = "Keys: " + (items.ContainsKey("Key") ? items["Key"] : 0) + "\nPotions: " + (items.ContainsKey("Potion") ? items["Potion"] : 0) + "\nArt: " + (items.ContainsKey("Art") ? items["Art"] : 0);
        bagText.text = text;
    }

    public void AddItem(string tag)
    {
        if (items.ContainsKey(tag))
        {
            items[tag]++;
        }
        else
        {
            items.Add(tag, 1);
        }
    }

    public bool UseItem(string tag)
    {
        if (items.ContainsKey(tag) && items[tag] > 0)
        {
            items[tag]--;
            return true;
        }
        return false;
    }
}
