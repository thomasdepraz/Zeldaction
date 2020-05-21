using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public GameObject hook;
    public GameObject aimDirectionPreview;

    [SerializeField] public static bool hasHook = true;
    [SerializeField] public static bool canAttack = true;
    public static bool canMove = true;
    public static bool useHook = true;
    [SerializeField] public static GameObject lastCheckpoint;
    // Start is called before the first frame update


    private void Awake()
    {
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

    // Update is called once per frame
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
