using UnityEngine;
using UnityEngine.UI;

public class StatusFollow : MonoBehaviour
{
    public Vector3 offset;
    public Sprite[] sprites;
    public Image image;

    public void ChangeImage(int stateIndex)
    {
        if (stateIndex >= 0 && stateIndex < sprites.Length)
        {
            image.sprite = sprites[stateIndex];
        }
    }

    public void CloseImage()
    {
        image.enabled = false;
    }

    public void ShowImage()
    {
        image.enabled = true;
    }
}
