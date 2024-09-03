using System;
using System.Collections;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Input")]
    public KeyCode KeyCodeBomb = KeyCode.Space;

    [Header("Bomb")]
    public GameObject BombPrefab;
    public float TimeToDestroy = 4f;
    public int BombAmaount = 1;
    private int BombsRemaning;

    [Header("Explosion")]
    public LayerMask ExplosionLayerMask;
    public Explosion ExplosionPrefab;
    public float ExplosionDuration = 1f;
    public int ExplosinRadius = 1;

    [Header("Destructable")]
    public Tilemap DestructableTile;
    public Destructable DestructablePrefab;

    private void OnEnable()
    {
        BombsRemaning = BombAmaount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCodeBomb) && BombsRemaning > 0)
        {
            StartCoroutine(PlaceBombCoroutine());
        }
    }



    IEnumerator PlaceBombCoroutine()
    {
        Vector2 position = RoundPosition(transform.position);

        GameObject bomb = Instantiate(BombPrefab, position, Quaternion.identity);

        BombsRemaning--;

        yield return new WaitForSeconds(TimeToDestroy);

            
        Destroy(bomb);
        BombsRemaning++;
        position = RoundPosition(bomb.transform.position);
        HandleExplosion(position);
    }

    private void HandleExplosion(Vector2 position)
    {
        Explosion explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(SpriteRendererType.START_EXPLIOSION);
        Destroy(explosion.gameObject, ExplosionDuration);

        Explode(position, Vector2.up, ExplosinRadius);
        Explode(position, Vector2.down, ExplosinRadius);
        Explode(position, Vector2.left, ExplosinRadius);
        Explode(position, Vector2.right, ExplosinRadius);

    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
            return;

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2, 0f, ExplosionLayerMask))
        {
            ClearDestructable(position);
            return;
        }

        Explosion explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? SpriteRendererType.MIDDLE_EXPLIOSION : SpriteRendererType.END_EXPLIOSION);
        explosion.SetDirection(direction);
        Destroy(explosion.gameObject, ExplosionDuration);
        Explode(position, direction, --length);
    }


    private Vector2 RoundPosition(Vector2 position)
    {
        float x = MathF.Round(position.x);
        float y = MathF.Round(position.y);
        return new Vector2(x, y);
    }

    private void ClearDestructable(Vector2 position)
    {
        Vector3Int cell = DestructableTile.WorldToCell(position);
        TileBase tile = DestructableTile.GetTile(cell);
        if(tile != null)
        {
            Instantiate(DestructablePrefab, position, Quaternion.identity);
            DestructableTile.SetTile(cell, null);
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CircleCollider2D>(out var a))
            a.isTrigger = false;
    }



}
