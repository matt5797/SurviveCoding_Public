using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.Interact
{
    public class Door : MonoBehaviour
    {
        public Transform openTransform;  // ���� ������ ���� transform
        public Transform closedTransform;  // ���� ������ ���� transform
        public GameObject realDoor;
        public float animationDuration = 1f;  // �ִϸ��̼� ���� �ð�
        private bool isOpen = false;  // ���� ���� ����
        [SerializeField] private bool isLocked = true;  // ���� ��� ����
        private float elapsedTime = 0f;  // ��� �ð�

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

        // ���� ���� �Լ�
        public void OpenDoor()
        {
            if (!isOpen && !isLocked)
            {
                isOpen = true;
                StartCoroutine(AnimateDoor(closedTransform, openTransform));
            }
        }

        // ���� �ݴ� �Լ�
        public void CloseDoor()
        {
            if (isOpen)
            {
                isOpen = false;
                StartCoroutine(AnimateDoor(openTransform, closedTransform));
            }
        }

        // ���� ����� �����ϴ� �Լ�
        public void UnlockDoor()
        {
            isLocked = false;
        }

        // �� �ִϸ��̼� �ڷ�ƾ
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