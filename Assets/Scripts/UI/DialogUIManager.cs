using UnityEngine;
using UnityEngine.Rendering;

public class DialogUIManager : MonoBehaviour
{
    public UIAudioManager audioManager;
    public Volume postProcessingManager;
    public VolumeProfile defaultProfile;
    public VolumeProfile dialogProfile;
  
  

    private void OnEnable()
    {
        postProcessingManager.profile = dialogProfile;
        audioManager.PlayClip(audioManager.soundSource,audioManager.openDialog,1, audioManager.dialog); 
    }
    private void OnDisable()
    {
        postProcessingManager.profile = defaultProfile;
        audioManager.PlayClip(audioManager.soundSource, audioManager.closeDialog, 1, audioManager.dialog);
    }
}
