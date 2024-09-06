using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    public SOStats SOStats;

    [Header("Input")]
    public KeyCode KeyCodeBomb = KeyCode.Space;

    [Header("Bomb")]
    public GameObject BombPrefab;
    public float TimeToDestroy = 4f;
    private int BombsRemaning;

    [Header("Explosion")]
    public LayerMask ExplosionLayerMask;
    public Explosion ExplosionPrefab;
    public float ExplosionDuration = 1f;

    [Header("Destructable")]
    public Tilemap DestructableTile;
    public Destructable DestructablePrefab;


    private void Awake()
    {
    }

    private void OnEnable()
    {
        BombsRemaning = SOStats.BombAmount;
        if (SOStats != null) SOStats.OnBombAmountIncrement += IncrementBombsRemaning;
    }

    private void OnDisable()
    {
        if (SOStats != null) SOStats.OnBombAmountIncrement -= IncrementBombsRemaning;
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
        IncrementBombsRemaning();
        position = RoundPosition(bomb.transform.position);
        HandleExplosion(position);
    }

    private void HandleExplosion(Vector2 position)
    {
        Explosion explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(SpriteRendererType.START_EXPLIOSION);
        Destroy(explosion.gameObject, ExplosionDuration);

        Explode(position, Vector2.up, SOStats.ExplosionStrenght);
        Explode(position, Vector2.down, SOStats.ExplosionStrenght);
        Explode(position, Vector2.left, SOStats.ExplosionStrenght);
        Explode(position, Vector2.right, SOStats.ExplosionStrenght);

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

    private void IncrementBombsRemaning()
    {
        BombsRemaning++;
    }

}
