using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PostProcessingManager : MonoBehaviour
{
    [Header("Elements")]
    public GameObject postProcessingManager;
    public Volume volume;
    private VolumeProfile volumeProfile;

    [Header("Bloom")]
    public float bloomIntensity;
    public float bloomScatter;
    private Bloom bloom;

    [Header("Vignette")]
    public float vignetteIntensity;
    public float vignetteSmoothness;
    private Vignette vignette;

    [Header("Color Adjustments")]
    [SerializeField] public Color filterColor;


    // Start is called before the first frame update
    void Start()
    {
        volumeProfile = volume.profile;
        if(volumeProfile.TryGet<Bloom>(out Bloom blm))
        {
            bloom = blm;
        }
        if(volumeProfile.TryGet<Vignette>(out Vignette vgn))
        {
            vignette = vgn;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        volumeProfile = volume.profile;
        if (volumeProfile.TryGet<Bloom>(out Bloom blm))
        {
            bloom = blm;
        }
        if (volumeProfile.TryGet<Vignette>(out Vignette vgn))
        {
            vignette = vgn;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(VignetteTransition(vignette.intensity.value, vignetteIntensity, vignette, true));
            StartCoroutine(VignetteTransition(vignette.smoothness.value, vignetteSmoothness, vignette, false));
            StartCoroutine(BloomTransition(bloom.intensity.value, bloomIntensity, bloom, true));
            StartCoroutine(BloomTransition(bloom.scatter.value, bloomScatter, bloom, false));
        }
    }

    private IEnumerator VignetteTransition(float defaultValue, float targetValue, Vignette vgn, bool isIntensity)
    {
            if(targetValue > defaultValue)
            {
                while (defaultValue < targetValue)
                {
                    defaultValue += 0.01f;
                    if(isIntensity)
                    {
                        vgn.intensity.Override(defaultValue);
                    }
                    else
                    {
                        vgn.smoothness.Override(defaultValue);
                    }
                    yield return new WaitForSeconds(0.005f);
                }
            }

            if (targetValue < defaultValue)
            {
                while (targetValue < defaultValue)
                {
                    defaultValue -= 0.01f;
                    if (isIntensity)
                    {
                        vgn.intensity.Override(defaultValue);
                    }
                    else
                    {
                        vgn.smoothness.Override(defaultValue);
                    }
                    yield return new WaitForSeconds(0.005f);
                }
            }
        
        yield return null;
    }

    private IEnumerator BloomTransition(float defaultValue, float targetValue, Bloom blm, bool isIntensity)
    {
        if (targetValue > defaultValue)
        {
            while (defaultValue < targetValue)
            {
                defaultValue += 0.01f;
                if (isIntensity)
                {
                    blm.intensity.Override(defaultValue);
                }
                else
                {
                    blm.scatter.Override(defaultValue);
                }
                yield return new WaitForSeconds(0.005f);
            }
        }

        if (targetValue < defaultValue)
        {
            while (targetValue < defaultValue)
            {
                defaultValue -= 0.01f;
                if (isIntensity)
                {
                    blm.intensity.Override(defaultValue);
                }
                else
                {
                    blm.scatter.Override(defaultValue);
                }
                yield return new WaitForSeconds(0.005f);
            }
        }

        yield return null;
    }
}
