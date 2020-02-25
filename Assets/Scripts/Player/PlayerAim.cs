using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAim : MonoBehaviour
    {

        [Header("Elements")]
        public GameObject crosshair;
        [HideInInspector] public Transform crosshairTransform;
        private Transform playerTransform;

        [Header("Logic")]
        [Range(0f, 10f)]
        public float maxRange = 1f;

        private float horizontalAim;
        private float verticalAim;
        private Vector3 direction;
        private float distanceToPlayer;

        private bool coroutineCanStart = false;

        [Header("Tweak")]
        [Range(1f, 5f)]
        public float speed = 1f; //IMPORTANT : avec cette valeur on peut rajouter un slider de sensi dans les options

        void Start()
        {
            crosshairTransform = crosshair.transform;
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

            distanceToPlayer = (playerTransform.position - crosshairTransform.position).magnitude;//La distance entre player et viseur

            if (horizontalAim != 0 || verticalAim != 0)
            {
                coroutineCanStart = true;
                crosshair.SetActive(true);
                direction = new Vector3(horizontalAim, verticalAim, 0).normalized;
                //on ralentit le joueur

                if (distanceToPlayer <= maxRange) //On peut bouger le viseur jusqu'a maxRange.
                {
                    crosshairTransform.position += direction * Time.deltaTime * speed;//on déplace le viseur                                                              
                }
            }

            else if (horizontalAim == 0 && verticalAim == 0)//Si le joystick est à 0,0 alors la visée est désactivée après 1 sec.
            {    
                if(coroutineCanStart)
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
                crosshair.SetActive(false);
                //reset la vitesse du joueur
            }
            yield return null;
        }
    }
}
