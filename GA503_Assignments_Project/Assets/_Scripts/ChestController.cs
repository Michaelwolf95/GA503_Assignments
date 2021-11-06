using System;
using System.Collections;
using UnityEngine;

namespace GA503
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private bool isOpen = false;
        private bool isInteractable = true;

        private int IsOpenParamID = Animator.StringToHash("IsOpen");
        private const float openAnimDuration = 2.1f;
        private const float closeAnimDuration = 1.1f;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryInteract();
            }
        }

        public void TryInteract()
        {
            if (!isInteractable)
            {
                return;
            }
            
            // Toggle interact.
            if (isOpen)
            {
                CloseChest();
            }
            else
            {
                OpenChest();
            }
        }
        
        public void OpenChest()
        {
            if (!isInteractable || isOpen)
            {
                return;
            }

            isOpen = true;
            animator.SetBool(IsOpenParamID, true);
            isInteractable = false;
            WaitForAction((() =>
            {
                isInteractable = true;
            }), openAnimDuration);
        }
        
        public void CloseChest()
        {
            if (!isInteractable || !isOpen)
            {
                return;
            }

            isOpen = false;
            animator.SetBool(IsOpenParamID, false);
            isInteractable = false;
            WaitForAction((() =>
            {
                isInteractable = true;
            }), closeAnimDuration);
        }

        private void WaitForAction(Action action, float duration)
        {
            StartCoroutine(CoWaitForAction(action, duration));
        }

        private IEnumerator CoWaitForAction(Action action, float duration)
        {
            yield return new WaitForSeconds(duration);
            action?.Invoke();
        }
        
    }
}