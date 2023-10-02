using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagManager : MonoBehaviour
{
    public Dictionary<string, int> items;

    private CanvasGroup key;
    private CanvasGroup potion;
    private CanvasGroup art;

    void Start()
    {
        items = new Dictionary<string, int>();
        key = GameObject.FindWithTag("KeyUI").GetComponent<CanvasGroup>();
        potion = GameObject.FindWithTag("PotionUI").GetComponent<CanvasGroup>();
        art = GameObject.FindWithTag("ArtUI").GetComponent<CanvasGroup>();
    }

    public void UpdateUI()
    {
        key.alpha = HasItem("Key") ? 1 : 0;
        potion.alpha = HasItem("Potion") ? 1 : 0;
        art.alpha = HasItem("Art") ? 1 : 0;
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

    public bool HasItem(string tag)
    {
        if (items.ContainsKey(tag) && items[tag] > 0)
        {
            return true;
        }
        return false;
    }
}
