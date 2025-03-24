using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        //_spriteRenderer.color = Color.red;
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
