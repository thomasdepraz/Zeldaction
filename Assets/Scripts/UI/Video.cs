using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    private VideoPlayer video;
    public AudioSource audioSource;
    public RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        video.Prepare();
        while(!video.isPrepared)
        {
            Debug.Log("waiting");
            yield return new WaitForSeconds(1);
            break;
        }
        //image.texture = video.texture;
        Debug.Log("play");
        video.Play();
        audioSource.Play();
    }
}
