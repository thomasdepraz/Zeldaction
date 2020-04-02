using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D bossRb;
    void Start()
    {
        bossRb = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        bossRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        bossRb.constraints = RigidbodyConstraints2D.None;
    }
}
