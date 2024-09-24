using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.Interact
{
    public class Door : MonoBehaviour
    {
        public Transform openTransform;  // 문이 열렸을 때의 transform
        public Transform closedTransform;  // 문이 닫혔을 때의 transform
        public GameObject realDoor;
        public float animationDuration = 1f;  // 애니메이션 지속 시간
        private bool isOpen = false;  // 문의 현재 상태
        [SerializeField] private bool isLocked = true;  // 문의 잠금 상태
        private float elapsedTime = 0f;  // 경과 시간

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (!isOpen)
                {
                    Debug.Log("Door Open");
                    OpenDoor();
                }
                else
                {
                    Debug.Log("Door Close");
                    CloseDoor();
                }
            }
        }

        // 문을 여는 함수
        public void OpenDoor()
        {
            if (!isOpen && !isLocked)
            {
                isOpen = true;
                StartCoroutine(AnimateDoor(closedTransform, openTransform));
            }
        }

        // 문을 닫는 함수
        public void CloseDoor()
        {
            if (isOpen)
            {
                isOpen = false;
                StartCoroutine(AnimateDoor(openTransform, closedTransform));
            }
        }

        // 문의 잠금을 해제하는 함수
        public void UnlockDoor()
        {
            isLocked = false;
        }

        // 문 애니메이션 코루틴
        private IEnumerator AnimateDoor(Transform startTransform, Transform endTransform)
        {
            elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                float t = elapsedTime / animationDuration;
                realDoor.transform.localPosition = Vector3.Lerp(startTransform.localPosition, endTransform.localPosition, t);
                realDoor.transform.localRotation = Quaternion.Lerp(startTransform.localRotation, endTransform.localRotation, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            realDoor.transform.localPosition = endTransform.localPosition;
            realDoor.transform.localRotation = endTransform.localRotation;
        }
    }
}