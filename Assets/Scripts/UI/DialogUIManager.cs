
using UnityEngine;
using UnityEngine.Rendering;

public class DialogUIManager : MonoBehaviour
{
    public Volume postProcessingManager;
    public VolumeProfile defaultProfile;
    public VolumeProfile dialogProfile;
  
  

    private void OnEnable()
    {
        postProcessingManager.profile = dialogProfile;
    }
    private void OnDisable()
    {
        postProcessingManager.profile = defaultProfile;
    }
}
