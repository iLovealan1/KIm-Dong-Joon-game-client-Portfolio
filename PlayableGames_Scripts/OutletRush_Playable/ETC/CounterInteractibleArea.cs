using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class CounterInteractibleArea : InteractibleArea
    {
        [Header("=====CounterInteractibleArea SerializeField=====")]
        [Space]
        [SerializeField] private SpriteRenderer _playerAreaSprite = null; 

        //===============================================================
        //Properties
        //===============================================================
        public bool IsEmployeeOn 
        {
            set 
            { 
                _isEmployeeOn = value;
                _playerAreaSprite.color = Color.green;
            }
        }

        // public bool Is
        //===============================================================
        //Fields
        //===============================================================
        private bool _isEmployeeOn = false;

        //===============================================================
        //Functions
        //===============================================================
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.gameObject.layer != (int)ELayerName.Player)
                return;
            
            if (!_isEmployeeOn)
                _playerAreaSprite.color = Color.green;
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);

            if (other.gameObject.layer != (int)ELayerName.Player)
                return;

            if (!_isEmployeeOn)
                _playerAreaSprite.color = Color.white;
        }
    }

}