using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public static PlayerAttributes Instance;

    /// <summary>
    /// ego of player
    /// 0-100
    /// </summary>
    public int Ego { get; private set; }

    public int Investors { get; private set; }
    
    public int CashMoney { get; private set; }

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
        Ego = Instance.Ego;
        Investors = Instance.Investors;
        CashMoney = Instance.CashMoney;
    }
}