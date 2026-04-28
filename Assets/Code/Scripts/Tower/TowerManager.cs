using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    [Header("Towers")]
    [SerializeField] private GameObject pistolTower;
    [SerializeField] private GameObject rocketTower;

    [SerializeField] private LayerMask towerLayer;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerLevel;
    [SerializeField] private TextMeshProUGUI UpgradeCost;
    [SerializeField] private TextMeshProUGUI towerTargetting;

    private GameObject selectedTower;
    private GameObject placingTower;
    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return; // Если клик по UI - выходим и не обрабатываем клик по миру
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearSelected();
        }
        if (placingTower) {
            if (!placingTower.GetComponent<TowerPlacement>().isPlacing) {
                placingTower = null;    
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos, towerLayer);

            if (hit != null)
            {
                // Скрываем радиус у ПРЕДЫДУЩЕЙ башни
                if (selectedTower != null)
                {
                    Transform oldRange = selectedTower.transform.Find("Range"); // Ищем по имени!
                    if (oldRange != null)
                        oldRange.GetComponent<SpriteRenderer>().enabled = false;
                }

                // Выбираем НОВУЮ башню
                selectedTower = hit.gameObject;

                // Показываем радиус у НОВОЙ башни
                Transform newRange = selectedTower.transform.Find("Range"); // Ищем по имени!
                if (newRange != null)
                    newRange.GetComponent<SpriteRenderer>().enabled = true;

                panel.SetActive(true);
                towerName.text = selectedTower.name.Replace("(Clone)", "").Trim();

                // ✅ Добавляем проверки на компоненты
                TowerUpgrade upgrade = selectedTower.GetComponent<TowerUpgrade>();
                if (upgrade != null)
                {
                    towerLevel.text = "Tower LVL: " + upgrade.currentLevel.ToString();
                    UpgradeCost.text = upgrade.currentCost;
                }

                Tower tower = selectedTower.GetComponent<Tower>();
                if (tower != null)
                {
                    if (tower.first)
                    {
                        towerTargetting.text = "First";
                    }
                    else if (tower.last)
                    {
                        towerTargetting.text = "Last";
                    }
                    else if (tower.strong)
                    {
                        towerTargetting.text = "Strong";
                    }
                }
            }
            else if (!EventSystem.current.IsPointerOverGameObject() && selectedTower) {
                panel.SetActive(false);
                Transform oldRange = selectedTower.transform.Find("Range"); // Ищем по имени!
                if (oldRange != null)
                    oldRange.GetComponent<SpriteRenderer>().enabled = false;
                selectedTower = null;
            }
           
           
        }

        if (Input.GetKeyDown(KeyCode.U) && selectedTower)
        {
            selectedTower.GetComponent<TowerUpgrade>().Upgrade();
        }
    }

    private void ClearSelected() {
        if (placingTower) {
            Destroy(placingTower);
            placingTower = null;
        }
    }

    public void setTower(GameObject tower) {
        ClearSelected();
        placingTower = Instantiate(tower);
    }

    public void UpgradeSelected() {
        if (selectedTower) {
            selectedTower.GetComponent<TowerUpgrade>().Upgrade();
            towerLevel.text = "Tower LVL: " + selectedTower.GetComponent<TowerUpgrade>().currentLevel.ToString();
            UpgradeCost.text = selectedTower.GetComponent<TowerUpgrade>().currentCost;
        }
    }
    public void ChangeTargetting() {
        if (selectedTower) {
            Tower tower = selectedTower.GetComponent<Tower>();

            if (tower.first)
            {
                tower.first = false;
                tower.last = true;
                tower.strong = false;
                towerTargetting.text = "Last";
            }
            else if (tower.last)
            {
                tower.first = false;
                tower.last = false;
                tower.strong = true;
                towerTargetting.text = "Strong";
            }
            else if (tower.strong)
            {
                tower.first = true;
                tower.last = false;
                tower.strong = false;
                towerTargetting.text = "First";
            }
        }
    }

}
