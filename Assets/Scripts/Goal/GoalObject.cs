using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalObject : MonoBehaviour
{
    [SerializeField] private Image goalImage;
    [SerializeField] private Image completedMarkImage;
    [SerializeField] private TextMeshProUGUI goalCountText;
    [SerializeField] private ParticleSystem completionEffect;

    private int remainingCount;
    private LevelGoal levelGoal;

    public LevelGoal LevelGoal => levelGoal;

    public void Prepare(LevelGoal goal)
    {
        levelGoal = goal;
        goalImage.sprite = ItemImageLibrary.Instance.GetSpriteForItemType(levelGoal.ItemType);

        remainingCount = levelGoal.Count;
        UpdateGoalCountText();
    }

    public void DecreaseCount()
    {
        if (remainingCount <= 0) return;

        remainingCount--;
        UpdateGoalState();
    }

    private void UpdateGoalState()
    {
        if (remainingCount <= 0)
        {
            MarkGoalAsCompleted();
        }
        else
        {
            UpdateGoalCountText();
            PlayDecreaseEffect();
        }
    }

    private void UpdateGoalCountText()
    {
        goalCountText.text = remainingCount.ToString();
    }

    private void MarkGoalAsCompleted()
    {
        remainingCount = 0;
        goalCountText.gameObject.SetActive(false);
        completedMarkImage.gameObject.SetActive(true);
        PlayCompletionEffect();
    }

    private void PlayDecreaseEffect()
    {
        if (completionEffect != null)
        {
            completionEffect.Play();
        }
    }

    private void PlayCompletionEffect()
    {
        if (completionEffect != null)
        {
            completionEffect.Play();
        }
    }

    public bool IsCompleted()
    {
        return remainingCount <= 0;
    }
}