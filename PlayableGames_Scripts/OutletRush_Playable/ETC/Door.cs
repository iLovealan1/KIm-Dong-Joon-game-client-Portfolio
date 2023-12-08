using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class Door : MonoBehaviour
    {
        [Header("=====Door Serialize Fields=====")]
        [Space] 
        [SerializeField] private Transform _leftDoor;
        [SerializeField] private Transform _rightDoor;
        [SerializeField] private Transform _unlockSprite;
        [SerializeField] private BoxCollider[] _doorColiderArr = null;

        //===============================================================
        //Fields
        //===============================================================
        private bool _isUnlocked = false;

        //===============================================================
        //Functions
        //===============================================================
        public void OpenDoor(Vector3 targetPos)
        {         
            if (this.transform.position.x < targetPos.x)
            {
                _leftDoor.TweenLocalRotation(Quaternion.Euler(0f, -0.01f, 0f), Quaternion.Euler(0f, -90f, 0f), false, 0.2f);
                _rightDoor.TweenLocalRotation(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), false, 0.2f);
            }
            else
            {
                _leftDoor.TweenLocalRotation(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), false, 0.2f);
                _rightDoor.TweenLocalRotation(Quaternion.Euler(0f, -0.01f, 0f), Quaternion.Euler(0f, -90f, 0f), false, 0.2f);
            }
        }

        public void CloseDoor()
        {
            if (_leftDoor.localRotation.y > 0)
            {
                _leftDoor.TweenLocalRotation(Quaternion.Euler(0f, 90, 0f), Quaternion.Euler(0f, 0f, 0f), false, 0.2f);
                _rightDoor.TweenLocalRotation(Quaternion.Euler(0f, -90f, 0f), Quaternion.Euler(0f, -0.01f, 0f), false, 0.2f);
            }
            else
            {
                _leftDoor.TweenLocalRotation(Quaternion.Euler(0f, -90f, 0f), Quaternion.Euler(0f, -0.01f, 0f), false, 0.2f);
                _rightDoor.TweenLocalRotation(Quaternion.Euler(0f, 90f, 0f), Quaternion.Euler(0f, 0, 0f), false, 0.2f);
            }
        }

        public void OpenStorage()
        {
            _isUnlocked = true;
            
            for (int i = 0; i < _doorColiderArr.Length; i++)
                _doorColiderArr[i].isTrigger = _isUnlocked;

            _unlockSprite.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isUnlocked)
                return;

            var targetLayer = (ELayerName)other.transform.gameObject.layer;

             if (targetLayer != ELayerName.Player)
                return;

            var pos = other.transform.position;
            this.OpenDoor(pos);
        }

        private void OnTriggerExit(Collider other) 
        {
            if (!_isUnlocked)
                return;
                
            var targetLayer = (ELayerName)other.transform.gameObject.layer;

            if (targetLayer != ELayerName.Player)
                return;

            this.CloseDoor();
        }
    }
}

