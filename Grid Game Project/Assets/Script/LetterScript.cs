using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour
{
    // Start is called before the first frame update

    Color _defaultColor;
    SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        //_spriteRenderer.color = Color.blue;
    }

    private void OnMouseExit()
    {
        //_spriteRenderer.color = _defaultColor;
    }

    public void SetColor(Color color)
    {
       _spriteRenderer.color = color;
    }
}
