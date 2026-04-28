using Unity.VisualScripting;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [Header("Towers")]
    [SerializeField] private GameObject pistolTower;
    [SerializeField] private GameObject rocketTower;

    [SerializeField] private LayerMask towerLayer;
    private GameObject selectedTower;
    private GameObject placingTower;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            setTower(pistolTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setTower(rocketTower);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
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
            }
            else
            {
                // Клик в пустоту -> снимаем выделение
                if (selectedTower != null)
                {
                    Transform oldRange = selectedTower.transform.Find("Range");
                    if (oldRange != null)
                        oldRange.GetComponent<SpriteRenderer>().enabled = false;
                    selectedTower = null;
                }
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

    private void setTower(GameObject tower) {
        ClearSelected();
        placingTower = Instantiate(tower);
    }
}
