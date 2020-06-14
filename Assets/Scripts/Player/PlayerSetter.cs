using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    public bool hasHook = false;
    public bool canAttack = false;
    public bool unlockedAttack = false;

    private void Awake()
    {
        PlayerManager.canAttack = canAttack;
        PlayerManager.hasHook = hasHook;
        PlayerManager.unlockedAttack = unlockedAttack;
    }
}
