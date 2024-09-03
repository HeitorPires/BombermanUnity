using System;
using System.Collections;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public KeyCode KeyCodeBomb = KeyCode.Space;

    public GameObject bombPrefab;
    public float timeToDestroy = 4f;
    public int bombAmaount = 1;

    private int bombsRemaning;


    private void OnEnable()
    {
        bombsRemaning = bombAmaount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCodeBomb) && bombsRemaning > 0)
        {
            StartCoroutine(PlaceBombCoroutine());
        }
    }

    private void PlaceBomb()
    {

    }

    IEnumerator PlaceBombCoroutine()
    {
        Vector2 position = transform.position;
        position.x = MathF.Round(position.x);
        position.y = MathF.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

        bombsRemaning--;

        yield return new WaitForSeconds(timeToDestroy);

        Destroy(bomb);
        bombsRemaning++;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CircleCollider2D>(out var a))
            a.isTrigger = false;
    }

}
