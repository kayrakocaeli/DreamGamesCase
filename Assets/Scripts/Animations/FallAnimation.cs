using UnityEngine;
using DG.Tweening;

public class FallAnimation : MonoBehaviour
{
    [SerializeField] public Item item;
    [HideInInspector] public Cell TargetCell { get; private set; }

    private const float AnimationDuration = 0.35f;
    private Vector3 targetPosition;

    private void Awake()
    {
        DOTween.SetTweensCapacity(500, 50);
    }

    public void FallTo(Cell targetCell)
    {
        if (IsInvalidTargetCell(targetCell)) return;

        UpdateTargetCell(targetCell);
        AnimateFall();
    }

    private bool IsInvalidTargetCell(Cell targetCell)
    {
        return TargetCell != null && targetCell.Y >= TargetCell.Y;
    }

    private void UpdateTargetCell(Cell targetCell)
    {
        TargetCell = targetCell;
        item.Cell = TargetCell;
        targetPosition = TargetCell.transform.position;
    }

    private void AnimateFall()
    {
        item.transform.DOMoveY(targetPosition.y, AnimationDuration)
            .SetEase(Ease.InCubic)
            .OnComplete(() => TargetCell = null);
    }
}