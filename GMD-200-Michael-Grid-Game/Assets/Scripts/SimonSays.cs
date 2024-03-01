using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private List<Vector2Int> correctPositions = new List<Vector2Int> ();

    private bool patternPlaying;

    private int playerPatternIndex;

    private void OnEnable()
    {
        gridManager.TileSelected += OnTileSelected;
    }

    private void OnDisable()
    {
        gridManager.TileSelected -= OnTileSelected;
    }

    private void OnTileSelected(GridTile obj)
    {
        if (patternPlaying)
        {
            return;
        }
        if (obj.gridCoords == correctPositions[playerPatternIndex])
        {
            Debug.Log("Correct!");
            StartCoroutine(Co_FlashTile(obj, Color.green, 0.25f));
            playerPatternIndex++;
            if (playerPatternIndex >= correctPositions.Count)
            {
                NextPattern();
            }
        }
        else
        {
            Debug.Log("Incorrect!");
            StartCoroutine(Co_FlashTile(obj, Color.red, 0.25f));
            correctPositions.Clear();
            NextPattern();
        }
    }
    private void Start()
    {
        NextPattern();
    }

    [ContextMenu ("Next Pattern")]

    public void NextPattern()
    {
        playerPatternIndex = 0;
        correctPositions.Add(new Vector2Int(Random.Range(0, gridManager.numCols), Random.Range(0, gridManager.numRows)));
        StartCoroutine(Co_PlayPattern(correctPositions));
    }

    private IEnumerator Co_PlayPattern(List<Vector2Int> positions)
    {
        patternPlaying = true;
        yield return new WaitForSeconds(1f);
        foreach(var pos in positions)
        {
            GridTile tile = gridManager.GetTile(pos);
            yield return Co_FlashTile(tile, Color.blue, 0.25f);
            yield return new WaitForSeconds(.5f);
        }
        patternPlaying = false;
    }

    private IEnumerator Co_FlashTile(GridTile tile, Color color, float duration)
    {
        tile.SetColor(color);
        yield return new WaitForSeconds(duration);
        tile.ResetColor();
    }


}
