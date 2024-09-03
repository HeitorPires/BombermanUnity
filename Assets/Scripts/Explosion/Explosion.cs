using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public List<AnimatedSpriteRenderer> Renderers;

    void Init()
    {
        Renderers.ForEach(i => AnimatedSpriteRendererManager.Instance.AssociateRenderes(i.SpriteRendererType, i));
    }

    private void Awake()
    {
        Init();
    }

    public void SetActiveRenderer(SpriteRendererType type)
    {
        foreach (AnimatedSpriteRenderer ar in Renderers)
        {
            if (ar.SpriteRendererType == type)
                ar.enabled = true;
            else
                ar.enabled = false;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }



}
