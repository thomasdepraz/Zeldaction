using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    public bool hasHook = false;
    public bool canAttack = false;

    private void Awake()
    {
        PlayerManager.canAttack = canAttack;
        PlayerManager.hasHook = hasHook;
    }
}
