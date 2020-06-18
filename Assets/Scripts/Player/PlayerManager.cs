using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{

    public GameObject hook;
    public GameObject aimDirectionPreview;

    [SerializeField] public static bool hasHook = true;
    [SerializeField] public static bool canAttack = false;
    public static bool unlockedAttack;
    public static bool canMove = true;
    public static bool useHook = true;
    [SerializeField] public static GameObject lastCheckpoint;


    //Player scripts 
    public HookThrow hookThrow = null;
    public PlayerAim playerAim = null;
    public PlayerAttack playerAttack= null;
    public PlayerHP playerHP = null;
    public PlayerMovement playerMovement = null;


    // Start is called before the first frame update


    private void Awake()
    {
        MakeSingleton(true);
        if (!hasHook)
        {
            hook.SetActive(false);
            aimDirectionPreview.SetActive(false);
        }
        else
        {
            hook.SetActive(true);
            aimDirectionPreview.SetActive(true);
        }
        if(SceneManager.GetActiveScene().name == "DungeonScene")
        {
            hasHook = true;
            canAttack = true;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(!hasHook)
        {
            hook.SetActive(false);
            aimDirectionPreview.SetActive(false);
        }
        else       
        {
            hook.SetActive(true);
            aimDirectionPreview.SetActive(true);
        }
    }
}
