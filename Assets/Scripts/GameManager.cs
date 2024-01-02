using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver => _ısGameOver;

    private SpawnerManager _spawner;
    private BoardManager _board;

    private ShapeManager _currentShape;

    [Header("Counters")] [Range(0.02f, 1.0f)] [SerializeField]
    private float dropTime = 0.1f;

    [Range(0.02f, 1.0f)] [SerializeField] private float leftRightClickTime = 0.25f;
    [Range(0.02f, 1.0f)] [SerializeField] private float rotationTime = 0.25f;
    [Range(0.02f, 1.0f)] [SerializeField] private float fallDownTime = 0.25f;


    private float _dropCounter;
    private float _leftRightClickCounter;
    private float _rotationCounter;
    private float _fallDownCounter;

    private bool _ısGameOver = false;

    public bool isClosckwise = true;
    public IconManager rotateIcon;


    private void Start()
    {
        _spawner = GameObject.FindObjectOfType<SpawnerManager>();
        _board = GameObject.FindObjectOfType<BoardManager>();

        if (_spawner)
            if (!_currentShape)
                _currentShape = _spawner.GenerateShape();
    }

    private void Update()
    {
        if (!_board || !_spawner || !_currentShape || _ısGameOver) return;

        StartControl();
    }

    private void StartControl()
    {
        if ((Input.GetKey("right") && Time.time > _leftRightClickCounter) || Input.GetKeyDown("right"))
        {
            _currentShape.MoveRight();
            _leftRightClickCounter = Time.time + leftRightClickTime;

            if (!_board.IsValidPosition(_currentShape))
            {
                SoundManager.instance.PlayFX(1);
                _currentShape.MoveLeft();
            }
            else
            {
                SoundManager.instance.PlayFX(3);
            }
        }
        else if ((Input.GetKey("left") && Time.time > _leftRightClickCounter) || Input.GetKeyDown("left"))
        {
            _currentShape.MoveLeft();
            _leftRightClickCounter = Time.time + leftRightClickTime;

            if (!_board.IsValidPosition(_currentShape))
            {
                SoundManager.instance.PlayFX(1);
                _currentShape.MoveRight();
            }
            else
            {
                SoundManager.instance.PlayFX(3);
            }
        }
        else if ((Input.GetKeyDown("up") && Time.time > _rotationCounter))
        {
            _currentShape.TurnRight();
            _leftRightClickCounter = Time.time + rotationTime;

            if (!_board.IsValidPosition(_currentShape))
            {
                SoundManager.instance.PlayFX(1);
                _currentShape.TurnLeft();
            }
            else
            {
                isClosckwise = !isClosckwise;
                if (rotateIcon) rotateIcon.ToggleIcon(isClosckwise);
                SoundManager.instance.PlayFX(3);
            }
        }
        else if ((Input.GetKey("down") && Time.time > _fallDownCounter) || Time.time > _dropCounter)
        {
            _fallDownCounter = Time.time + fallDownTime;
            _dropCounter = Time.time + dropTime;

            if (_currentShape)
            {
                _currentShape.MoveDown();
                if (!_board.IsValidPosition(_currentShape))
                {
                    if (_board.IsOverflow(_currentShape))
                    {
                        _currentShape.MoveUp();
                        _ısGameOver = true;
                        SoundManager.instance.PlayFX(6);
                    }
                    else
                    {
                        SpawnTile();
                    }
                }
            }
        }
    }

    private void SpawnTile()
    {
        _leftRightClickCounter = Time.time;
        _dropCounter = Time.time;
        _rotationCounter = Time.time;

        _currentShape.MoveUp();
        _board.AddInGrid(_currentShape);
        SoundManager.instance.PlayFX(5);
        _currentShape = _spawner.GenerateShape();

        _board.ClearAllRow();

        if (_board.completedRows > 0)
        {
            if (_board.completedRows > 1)
                SoundManager.instance.PlayVocal();
            SoundManager.instance.PlayFX(4);
        }
    }

    Vector2 Vect2Int(Vector2 vector)
    {
        return new Vector2(
            Mathf.Round(vector.x),
            Mathf.Round(vector.y)
        );
    }

    public void RotationIconDirection()
    {
        isClosckwise = !isClosckwise;
        _currentShape.RotateClockwise(isClosckwise);
        if (!_board.IsValidPosition(_currentShape))
        {
            _currentShape.RotateClockwise(!isClosckwise);
            SoundManager.instance.PlayFX(3);
            return;
        }

        if (rotateIcon) rotateIcon.ToggleIcon(isClosckwise);
        SoundManager.instance.PlayFX(0);
    }
}