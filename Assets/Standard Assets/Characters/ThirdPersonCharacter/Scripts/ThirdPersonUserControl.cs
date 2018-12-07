using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


        Vector3 targetPosition;
        Vector3 lookatTarget;
        Quaternion playerRot;
        float rotSpeed = 5;
        float speed = 10;
        private void Start()
        {
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SettargetPosition();
            }

            m_Character.Move(lookatTarget, false, false);

        }

        void SettargetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                targetPosition = hit.point;
                //this.transform.LookAt(targetPosition);
                lookatTarget = new Vector3(targetPosition.x - transform.position.x,
                                           transform.position.y,
                                           targetPosition.z - transform.position.z);
                playerRot = Quaternion.LookRotation(lookatTarget);
            }
        }
        //void Move()
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation,
        //                                          playerRot,
        //                                          rotSpeed * Time.deltaTime);
        //    transform.position = Vector3.MoveTowards(transform.position,
        //                                             targetPosition,
        //                                             speed * Time.deltaTime);
        //}

    }
}
