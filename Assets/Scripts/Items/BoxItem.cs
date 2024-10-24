using System;
using UnityEngine;

public class BoxItem : Item
{
    private const int HEALTH = 1;

    public void PrepareBoxItem(ItemBase itemBase)
    {
        SoundID = SoundID.Box;
        itemBase.IsFallable = false;
        itemBase.Health = HEALTH;
        itemBase.InterectWithExplode = true;
        itemBase.Clickable = false;
        Prepare(itemBase, ItemImageLibrary.Instance.GetSpriteForItemType(itemBase.ItemType));
    }

    public override void TryExecute()
    {
        ParticleManager.Instance.PlayParticle(this);
        AudioManager.Instance.PlayEffect(SoundID);
        base.TryExecute();
    }
}
