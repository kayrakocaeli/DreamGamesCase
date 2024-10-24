using UnityEngine;
public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestruction = 2.0f;

    private void Start()
    {
        Destroy(gameObject, timeBeforeDestruction);
    }
}