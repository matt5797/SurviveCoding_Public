using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.UI
{
    public class Billboard : MonoBehaviour
    {
        private Transform mainCam;

        private void Start()
        {
            mainCam = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + mainCam.rotation * Vector3.forward,
                mainCam.rotation * Vector3.up);
        }
    }
}