using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager SharedInstance;

    [SerializeField] private int amount;

    public int Amount
    {
        get => amount;
        set => amount = value;
    }

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}