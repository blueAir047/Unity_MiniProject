using UnityEngine;

public class DepthSort : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    [Tooltip("체크하면 움직이지 않는 사물용(최적화), 체크 해제하면 움직이는 캐릭터용")]
    public bool isStatic = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (isStatic)
        {
            UpdateSortingOrder();
        }
    }

    void LateUpdate()
    {
        if (!isStatic)
        {
            UpdateSortingOrder();
        }
    }

    void UpdateSortingOrder()
    {
        if (spriteRenderer != null && col != null)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(col.bounds.min.y * -100f);
        }
    }
}
