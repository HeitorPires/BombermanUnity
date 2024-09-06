using System;
using System.Collections;
using System.Linq;
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
    public string DestructableTilesTag;
    public Tilemap DestructableTiles;
    public Destructable DestructablePrefab;


    private void Awake()
    {
    }

    private void Start()
    {
        Invoke(nameof(Init), .2f);
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

    void Init()
    {
        DestructableTiles = FindObjectsOfType<Tilemap>().ToList().Find(i => i.CompareTag(DestructableTilesTag));
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
        explosion.SetActiveRenderer(SpriteItemsType.START_EXPLIOSION);
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
        explosion.SetActiveRenderer(length > 1 ? SpriteItemsType.MIDDLE_EXPLIOSION : SpriteItemsType.END_EXPLIOSION);
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
        Vector3Int cell = DestructableTiles.WorldToCell(position);
        TileBase tile = DestructableTiles.GetTile(cell);
        if(tile != null)
        {
            Instantiate(DestructablePrefab, position, Quaternion.identity);
            DestructableTiles.SetTile(cell, null);
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
