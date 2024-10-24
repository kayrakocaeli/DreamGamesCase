using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private const int BaseSortingOrder = 10;
    private static int childSpriteOrder;
    public SpriteRenderer SpriteRenderer;

    public ItemType ItemType;
    public bool Clickable;
    public bool InterectWithExplode;
    public bool IsFallable;
    public int Health;

    public FallAnimation FallAnimation;
    public ParticleSystem Particle;
    private Cell cell;
    public SoundID SoundID = SoundID.None;
    public Cell Cell
    {
        get { return cell; }
        set
        {
            if (cell == value) return;

            var oldCell = cell;
            cell = value;

            if (oldCell != null && oldCell.item == this)
                oldCell.item = null;

            if (value != null)
            {
                value.item = this;
                gameObject.name = cell.gameObject.name + " " + GetType().Name;
            }
        }
    }


    public void Prepare(ItemBase itemBase, Sprite sprite)
    {
        SpriteRenderer = AddSprite(sprite);

        ItemType = itemBase.ItemType;
        Clickable = itemBase.Clickable;
        InterectWithExplode = itemBase.InterectWithExplode;
        IsFallable = itemBase.IsFallable;
        FallAnimation = itemBase.FallAnimation;
        Health = itemBase.Health;
        FallAnimation.item = this;
    }

    public SpriteRenderer AddSprite(Sprite sprite)
    {
        var spriteRenderer = new GameObject("Sprite_" + childSpriteOrder).AddComponent<SpriteRenderer>();
        spriteRenderer.transform.SetParent(transform);
        spriteRenderer.transform.localPosition = Vector3.zero;
        spriteRenderer.transform.localScale = new Vector2(0.7f, 0.7f);
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerID = SortingLayer.NameToID("Cell");
        spriteRenderer.sortingOrder = BaseSortingOrder + childSpriteOrder++;

        return spriteRenderer;
    }

    public virtual MatchType GetMatchType() { return MatchType.None; }

    public virtual void TryExecute()
    {
        GoalManager.Instance.UpdateLevelGoal(ItemType);
        RemoveItem();
    }
    public void RemoveItem()
    {
        Cell.item = null;
        Cell = null;
        Destroy(gameObject);
    }

    public void UpdateSprite(Sprite sprite)
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public virtual void HintUpdateToSprite(ItemType itemType)
    {
        return;
    }
    public void Fall()
    {
        if (!this.IsFallable) return;

        FallAnimation.FallTo(cell.GetFallTarget());
    }
}
