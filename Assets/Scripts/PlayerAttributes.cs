using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public static PlayerAttributes Instance;

    /// <summary>
    /// ego of player
    /// </summary>
    [Range(0, 100)] public int ego;

    public int investors;

    public int cashMoney;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (Instance == null) return;
        ego = Instance.ego;
        investors = Instance.investors;
        cashMoney = Instance.cashMoney;
    }
}