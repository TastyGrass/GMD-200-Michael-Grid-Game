using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public GridManager gridManager;
    public Vector2Int gridCoords;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    private void OnMouseOver()
    {
        //Tell gridmanager that tile is hovered
        gridManager.OnTileHoverEnter(this);
        //SetColor(Color.green);
    }

    private void OnMouseDown()
    {
        gridManager.OnTileSelected(this);
    }

    private void OnMouseExit()
    {
        //Tell gridmanager tile is un hovered
        gridManager.OnTileHoverExit(this);
        //SetColor(defaultColor);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void ResetColor()
    {
        spriteRenderer.color = defaultColor;
    }
}
