using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class WorldObjectField : MonoBehaviour
    {

        [Header("======Player SerializeField======")]
        [Space]
        [SerializeField] private Player _player = null;
        public Player Player => _player;
        [SerializeField] private PlayerCamera _playerCamera = null;
        public PlayerCamera PlayerCamera => _playerCamera;


        [Header("======Upgrade Stool SerializeField======")]
        [Space]
        [SerializeField] private UpgradeStool _counterUpdradeStool = null;
        public UpgradeStool CounterUpdradeStool => _counterUpdradeStool;
        [SerializeField] private UpgradeStool _dipslay_Shoe1_UpgradeStool = null;
        public UpgradeStool DipslayShoe_1_UpgradeStool => _dipslay_Shoe1_UpgradeStool;
        [SerializeField] private UpgradeStool _dipslay_Shoe2_UpgradeStool = null;
        public UpgradeStool DipslayShoe2_UpgradeStool => _dipslay_Shoe2_UpgradeStool;
        [SerializeField] private UpgradeStool _storage_Shoe_UpgradeStool = null;
        public UpgradeStool StorageShoe_UpgradeStool => _storage_Shoe_UpgradeStool;
        [SerializeField] private UpgradeStool _storage_Clothes_UpgradeStool = null;
        public UpgradeStool StorageClothes_UpgradeStool => _storage_Clothes_UpgradeStool;
        [SerializeField] private UpgradeStool _dipslay_Clothes_UpgradeStool = null;
        public UpgradeStool DipslayClothes_UpgradeStool => _dipslay_Clothes_UpgradeStool;
        [SerializeField] private UpgradeStool _counter_Employee_UpgradeStool = null;
        public UpgradeStool CounterEmployee_UpgradeStool => _counter_Employee_UpgradeStool;


        [Header("======Shop Object SerializeField======")]
        [Space]
        [SerializeField] private MoneyStacker _moneyStacker = null;
        public MoneyStacker MoneyStacker => _moneyStacker;

        [SerializeField] private Counter _counter = null;
        public Counter Counter => _counter;
        [SerializeField] private DisplayShelf _shoeDisplayShelf_1 = null;
        public DisplayShelf DisplayShoe_1 => _shoeDisplayShelf_1;

        [SerializeField] private DisplayShelf _shoeDisplayShelf_2 = null;
        public DisplayShelf DisplayShoe_2 => _shoeDisplayShelf_2;

        [SerializeField] private DisplayShelf _clothesDisplayShelf = null;
        public DisplayShelf DisplayClothes => _clothesDisplayShelf;      


        [Header("======Storage Object SerializeField======")]
        [Space]
        [SerializeField] private StorageShelf _shoeStorageShelf = null;
        public StorageShelf StorageShoe => _shoeStorageShelf;

        [SerializeField] private StorageShelf _clothesStorageShelf = null;
        public StorageShelf StorageClothes => _clothesStorageShelf;


        [Header("======ETC Object SerializeField======")]
        [Space]
        [SerializeField] private GuideArrow _guideArrow = null;
        public GuideArrow GuideArrow => _guideArrow;
        [SerializeField] private Door _door = null;
        public Door Door => _door;
    }
}