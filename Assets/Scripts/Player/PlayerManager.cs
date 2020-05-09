using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject hook;
    public GameObject aimDirectionPreview;

    [SerializeField] public static bool hasHook = false;
    [SerializeField] public static bool canAttack =false;
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
