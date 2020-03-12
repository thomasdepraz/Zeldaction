using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAim : MonoBehaviour
    {

        [Header("Elements")]
        public GameObject crosshair;
        private AimAutoLock aimAutoLock;
        public GameObject aimDirectionPreview;
        [HideInInspector] public Transform crosshairTransform;
        private Transform playerTransform;

        [Header("Logic")]
        [Range(0f, 10f)]
        public float maxRange = 1f;

        private float horizontalAim;
        private float verticalAim;
        private Vector3 direction;
        [HideInInspector] public float distanceToPlayer;

        private bool coroutineCanStart = false;
        private bool unlockCoroutineCanStart = true;

        [Header("Tweak")]
        [Range(1f, 5f)]
        public float speed = 1f; //IMPORTANT : avec cette valeur on peut rajouter un slider de sensi dans les options
        [Range(0f, 5f)]
        public float range = 4f;

        void Start()
        {
            playerTransform = gameObject.transform;
        }

        // Update is called once per frame
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
                //reset la vitesse du joueur
            }
            yield return null;
        }

        /*private IEnumerator Unlock()//Si le joueur utilise le joystick pendant plus de 0.3 sec alors que le viseur est lock, ce dernier se unlock.
        {
            unlockCoroutineCanStart = false;
            yield return new WaitForSeconds(0.3f);

            if(horizontalAim != 0 || verticalAim != 0 && aimAutoLock.locked)
            {
                aimAutoLock.locked = false;
                aimAutoLock.StartCoroutine("CanLock");
            }

            unlockCoroutineCanStart = true;
            yield return null;
        }*/
    }
}
