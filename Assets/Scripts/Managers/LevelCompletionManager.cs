using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionManager : MonoBehaviour
{
    [SerializeField] private GameGrid board;
    [SerializeField] private float delayBetweenExplosions = 0.01f;

    private void OnEnable()
    {
        GoalManager.Instance.OnGoalsCompleted += HandleGoalsCompleted;
    }

    private void OnDisable()
    {
        GoalManager.Instance.OnGoalsCompleted -= HandleGoalsCompleted;
    }

    private void HandleGoalsCompleted()
    {
        FallAndFillManager.Instance.StopFall();
        StartCoroutine(ExplodeAllCellsThenCompleteLevel());
    }

    private IEnumerator ExplodeAllCellsThenCompleteLevel()
    {
        for (var y = 0; y < board.Rows; y++)
        {
            for (var x = 0; x < board.Cols; x++)
            {
                var cell = board.Cells[x, y];
                if (cell.item != null)
                {
                    cell.item.TryExecute();

                    yield return new WaitForSeconds(delayBetweenExplosions);
                }
            }
        }
        yield return new WaitForSeconds(1f);
        UIManager.Instance.SetLevelCompletedPanel();

    }
}
