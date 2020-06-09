using Cinemachine;
using UnityEngine;

public class ArmoredCrabProjectile : MonoBehaviour
{

    [Header("Elements")]
    public Animator anim;
    public AudioSource audioSource;

    [Header("Logic")]
    public int projectileDamage;

    [Header("Tweaks")]
    [Range(0,5)]
    public float explodeRange;


    private GameObject player;
    public CinemachineImpulseSource impulseSource;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        if ((player.transform.position - gameObject.transform.position).magnitude <= explodeRange)
        {
            player.GetComponent<PlayerHP>().TakeDamage(projectileDamage);
        }     
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRange);
        Gizmos.color = Color.red;
    }

    public void GetAnimationEvent(string parameter)
    {
        if(parameter == "explode")
        {
            anim.SetBool("Explode", true);
            Explode();
            audioSource.volume = 0.1f;
            audioSource.Play();
            impulseSource.GenerateImpulse(Vector3.up);
        }
        if(parameter =="endExplosion")
        {
            Destroy(gameObject);//POUR l'INSTANT, après détruire depuis l'anim
        }
    }
}
