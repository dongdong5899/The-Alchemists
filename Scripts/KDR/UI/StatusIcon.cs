using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    private Image _image;
    private Image _outlineImage;

    public void Init(Sprite sprite, Sprite outlineSprite)
    {
        _image = GetComponent<Image>();
        _outlineImage = transform.Find("LevelOutline").GetComponent<Image>();

        _image.sprite = sprite;
        _outlineImage.sprite = outlineSprite;
    }

    public void End()
    {
        Destroy(gameObject);
    }
}
