using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;

    public int height = 22;
    public int width = 10;

    public int completedRows = 0;

    private Transform[,] _grid;

    private void Awake()
    {
        _grid = new Transform[width, height];
    }

    private void Start()
    {
        CreateEmptyBoard();
    }

    bool IsCellFilled(int x, int y, ShapeManager shape)
    {
        return (_grid[x, y] != null && _grid[x, y].parent != shape.transform);
    }

    bool IsInBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }

    public bool IsValidPosition(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vect2Int(child.position);

            if (!IsInBoard((int)pos.x, (int)pos.y)) return false;

            if (pos.y < height)
                if (IsCellFilled((int)pos.x, (int)pos.y, shape))
                    return false;
        }

        return true;
    }

    void CreateEmptyBoard()
    {
        if (tilePrefab == null) return;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.name = "x" + x.ToString() + ", " + "y" + y.ToString();
                tile.parent = this.transform;
            }
        }
    }

    public void AddInGrid(ShapeManager shape)
    {
        if (!shape) return;

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vect2Int(child.position);
            _grid[(int)pos.x, (int)pos.y] = child;
        }
    }

    bool IsRowFilled(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (_grid[x, y] == null) return false;
        }

        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (_grid[x, y] != null)
            {
                Destroy(_grid[x, y].gameObject);
            }

            _grid[x, y] = null;
        }
    }

    void ShiftOneDown(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (_grid[x, y] != null)
            {
                _grid[x, y - 1] = _grid[x, y];
                _grid[x, y] = null;
                _grid[x, y - 1].position += Vector3.down;
            }
        }
    }

    void ShiftAllDown(int startY)
    {
        for (int i = startY; i < height; ++i)
        {
            ShiftOneDown(i);
        }
    }

    public void ClearAllRow()
    {
        completedRows = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsRowFilled(y))
            {
                completedRows++;
                ClearRow(y);
                ShiftAllDown(y + 1);
                y--;
            }
        }
    }

    public bool IsOverflow(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y >= height - 1) return true;
        }

        return false;
    }

    static Vector2 Vect2Int(Vector2 vector)
    {
        return new Vector2(
            Mathf.Round(vector.x),
            Mathf.Round(vector.y)
        );
    }
}