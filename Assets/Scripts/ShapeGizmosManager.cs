using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGizmosManager : MonoBehaviour
{
    private ShapeManager _followShape = null;
    private bool _isHitBottom = false;

    public Color color = new Color(1f, 1f, 1f, 0.2f);

    public void CreateShapeGizmos(ShapeManager shape, BoardManager board)
    {
        if (!_followShape)
        {
            _followShape = Instantiate(
                shape,
                shape.transform.position,
                shape.transform.rotation
            ) as ShapeManager;
            
            _followShape.name = shape.name + "_Gizmos";

            SpriteRenderer[] allSprites = _followShape.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in allSprites)
            {
                var spriteColor = sprite.color;
                spriteColor.a = 0.2f;
                sprite.color = spriteColor;
            }
        }
        else
        {
            _followShape.transform.position = shape.transform.position;
            _followShape.transform.rotation = shape.transform.rotation;
        }

        _isHitBottom = false;
        while (!_isHitBottom)
        {
            _followShape.MoveDown();
            if (!board.IsValidPosition(_followShape))
            {
                _followShape.MoveUp();
                _isHitBottom = true;
            }
        }
    }

    public void DestroyGizmos()
    {
        Destroy(_followShape.gameObject);
    }
}