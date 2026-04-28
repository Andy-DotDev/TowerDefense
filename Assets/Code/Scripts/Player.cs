using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player main;
    [SerializeField] private int health = 500;
    public int money = 1000;

    [SerializeField] private TextMeshProUGUI HPGui;
    [SerializeField] private TextMeshProUGUI MoneyGUI;

    [SerializeField] private GameObject gameOverGUI;

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        HPGui.text = "HP: " + health.ToString();
        MoneyGUI.text = "Money: " + money.ToString();
    }

    public void damage(int damage) {
        health -=damage;

        if (health <= 0) {
            gameOverGUI.SetActive(true);
        }
    }

    public void restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
