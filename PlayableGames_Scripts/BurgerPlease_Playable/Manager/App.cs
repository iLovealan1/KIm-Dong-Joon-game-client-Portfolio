using UnityEngine;
using System.Collections.Generic;

namespace LunaBurger.Playable010
{
    public partial class App : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        private void Start()
        {           
            _gameManager.Init();          
        }
    } 
}

