using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerAim : MonoBehaviour
    {

        [Header("Elements")]
        public GameObject crosshair;
        public GameObject aimDirectionPreview;
        [HideInInspector] public Transform crosshairTransform;
        private PlayerMovement playerMovement;

        [Header("Logic")]
        [Range(0f, 10f)]
        public float maxRange = 1f;

        private float horizontalAim;
        private float verticalAim;
        private Vector3 direction;
        [HideInInspector] public float distanceToPlayer;

        private bool coroutineCanStart = false;

        [Header("Tweak")]
        [Range(1f, 5f)]
        public float speed = 1f; //IMPORTANT : avec cette valeur on peut rajouter un slider de sensi dans les options
        [Range(0f, 5f)]
        public float range = 4f;

        void Start()
        {
            playerMovement = gameObject.GetComponent<PlayerMovement>();
        }

        void Update()
        {
            Aim();
        }

        private void Aim()
        {
            //On récupère la position des joysticks
            horizontalAim = Input.GetAxis("HorizontalAim");
            verticalAim = Input.GetAxis("VerticalAim");

            if (horizontalAim != 0 || verticalAim != 0)
            {
                coroutineCanStart = true;

                direction = new Vector3(horizontalAim, verticalAim, 0).normalized;

                aimDirectionPreview.SetActive(true); // ajout Tim: active le gameobject arrow
                aimDirectionPreview.transform.position = (Vector3)transform.position + direction.normalized * range; //The float is the distance from the player
                aimDirectionPreview.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Vector2.SignedAngle(Vector2.up, direction));

                //on ralentit le joueur       
                playerMovement.playerSpeed = 100f;
            }

            else if (horizontalAim == 0 && verticalAim == 0)//Si le joystick est à 0,0 alors la visée est désactivée après 1 sec.
            {
                if (coroutineCanStart)
                {
                    StartCoroutine("HideCrosshair");
                }
            }
        }

        private IEnumerator HideCrosshair() //Après une sec d'inactivitée on désactive le viseur
        {
            coroutineCanStart = false;
            yield return new WaitForSeconds(1f);
            if (horizontalAim == 0 && verticalAim == 0)
            {
                aimDirectionPreview.SetActive(false);
                //on reset la vitesse du joueur
                playerMovement.playerSpeed = 200f;
            }
            yield return null;
        }
    }
}
