using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player main;
    [SerializeField] private int health = 500;
    public int money = 1000;

    [SerializeField] private TextMeshProUGUI HPGui;
    [SerializeField] private TextMeshProUGUI MoneyGUI;

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        HPGui.text = "HP: " + health.ToString();
        MoneyGUI.text = "Money: " + money.ToString();
    }
}
