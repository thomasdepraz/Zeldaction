using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Sprite[] healthBarSprites;
    private Image img;

    private void Awake()
    {
        img = gameObject.GetComponent<Image>();
    }

    public void SetHealth(int health)
    {
        img.sprite = healthBarSprites[health];
    }
}
