using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Linq;

public class WordManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int numRows = 6;
    public int numCols = 5;

    public float xPadding = 0.1f;
    public float yPadding = 0.1f;

    public Color oddColor;
    public Color evenColor;

    public GameObject GM;

    [SerializeField] private LetterScript letterPrefab;
    //[SerializeField] 

    public void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    public void InitGrid()
    {
        for (int x = 0; x < numCols; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                LetterScript letter = Instantiate(letterPrefab, transform);
                Vector2 letterPos = new Vector2(x + (x*xPadding), y + (y*yPadding));
                letter.transform.localPosition = letterPos;
                letter.name = $"Letter_{x}_{y}";
                if ((x + y) % 2 == 0)
                    letter.SetColor(evenColor);
                else
                    letter.SetColor(oddColor);
            }
        }
    }
}
