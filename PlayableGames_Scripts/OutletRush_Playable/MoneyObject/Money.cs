using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [Header("=====Money SerializeField=====")]
    [Space]
    [SerializeField] private Animator _anim = null;
    [SerializeField] private Vector3  _changeFormSize = Vector3.zero;
    [SerializeField] private Quaternion _changeFormRotation = Quaternion.identity;

    //===============================================================
    //Properties
    //===============================================================
    public static int Price { set => _price = value; get => _price; }
    public event Action<Money> OnRelease { add => _onRelease += value; remove => _onRelease -= value; }

    //===============================================================
    //Fields
    //===============================================================
    private static int    _price          = 10;
    private Action<Money> _onRelease       = null;
    private Vector3       _defaultSize     = Vector3.zero;
    private Quaternion    _defaultRotation = Quaternion.identity;

    //===============================================================
    //Functions
    //===============================================================
    private void Awake()     => _defaultRotation = this.transform.rotation;

    private void OnEnable()  => _anim?.Play("MoneyAppear");

    public  void Release()   => _onRelease?.Invoke(this);

    private void OnDisable() => this.transform.rotation = _defaultRotation;

    public void ChangeSize() => _anim.Play("MoneyBigToSmall");
}
