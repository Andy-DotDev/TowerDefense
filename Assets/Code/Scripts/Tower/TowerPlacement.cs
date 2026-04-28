using System;
using UnityEngine;
public class TowerPlacement : MonoBehaviour
{

    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;

    [NonSerialized] public bool isPlacing = true;
    private bool isRestricted = false;
    void Awake()
    {
        rangeCollider.enabled = false;
    }

    void Update()
    {
        if (isPlacing) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = mousePosition;
        }
        if (Input.GetMouseButtonDown(1) && !isRestricted)
        {
            rangeCollider.enabled = true;
            isPlacing = false;

            Transform rangeObj = transform.Find("Range");
            if (rangeObj != null)
            {
                SpriteRenderer rangeSprite = rangeObj.GetComponent<SpriteRenderer>();
                if (rangeSprite != null)
                {
                    rangeSprite.enabled = false;
                }
            }

            this.enabled = false;
        }
        if (isRestricted)
        {
            rangeSprite.color = red;
        }
        else {
            rangeSprite.color = gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower" && isPlacing) {
            isRestricted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower" && isPlacing)
        {
            isRestricted = false;
        }
    }
}
