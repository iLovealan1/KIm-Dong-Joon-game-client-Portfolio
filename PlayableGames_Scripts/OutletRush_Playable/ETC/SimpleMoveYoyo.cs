using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveYoyo : MonoBehaviour
{
    [SerializeField] private Transform targetYoyoTans = null;
    [SerializeField] private Vector3 targetPos = Vector3.zero;
    [SerializeField] private float yoyoInterval = 0f;
    Token moveYoyoToken;

    private void Awake() => moveYoyoToken = TweenUtil.LoopLocalPosition(targetYoyoTans,targetPos,true,yoyoInterval);
    private void OnDestroy() => moveYoyoToken.Stop();
}
