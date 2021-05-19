using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

    public ProgressBar Pb;

    private void Start()
    {
        Pb.BarValue = health;
    }

    public void Heal(float health)
    {
        this.health += (health * 10);
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        ClampHealth();

        if (health == 0)
        {
            GameOverMenu.Instance.Display();
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        Pb.BarValue = health;
    }
}
