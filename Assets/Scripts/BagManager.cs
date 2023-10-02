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
        key.alpha = items.ContainsKey("Key") ? 1 : 0;
        potion.alpha = items.ContainsKey("Potion") ? 1 : 0;
        art.alpha = items.ContainsKey("Art") ? 1 : 0;
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
