using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public enum eScreenState
        {
            NONE = 0,
            PORTRAIT = 1,
            LANDSCAPE = 2,
            UPGRADE = 3,
        }
        
        public enum eCamState
        {
            None = 0,
            SDIE = 1,
            STRAIT = 2,
        }
    public class MainCamController : MonoBehaviour
    {
        
        
        //---------------------------------------------------------
        //프리셋 스크린 사이즈 데이터
        //---------------------------------------------------------
        [SerializeField] private eCamState _camState = eCamState.SDIE; 

        [System.Serializable]
        private class MainCamData
        {
            public EGameState GameState;

            public int _portaraitOrthoSize;
            public int _landScapeOrthoSize;
            public Vector3 _portraitCamPos;
            public Vector3 _landScapeCamPos;
            public Vector3 _rotaiton;

            [Header("Strait Mode")]
            [Space]

            public int _STRPortraitOrthoSize;
            public int _STRLandScapeOrthoSize;
            public Vector3 _STRPortraitCamPos;
            public Vector3 _STRLandScapeCamPos;
            public Vector3 _STRRotaiton;

        }
        [SerializeField] private List<MainCamData> _mainCamDataList;
        [SerializeField] private RectTransform     _canvasRect;
        [SerializeField] private AnimationCurve    _animCurve;

        //---------------------------------------------------------
        // 로직용 전역변수
        //---------------------------------------------------------
        private int           _currLandScapeOrthoSize;
        private int           _currPortraitOrthoSize;
        private Vector3       _currPortraitCamPos;
        private Vector3       _currLandScapeCamPos;
        private Camera        _mainCamera;
        private eScreenState  _currScreenState;
        private eScreenState  _lastScreenState;  
        private float         _screenWidth;
        private float         _screenHeight;
        private EGameState    _currGameState;
        private float         _timer = 3.0f;
        private Coroutine     _coZoomOut;
  
        public void Init()
        {
            _mainCamera = Camera.main;

            if(_camState == eCamState.SDIE)
            {
                var screenState = CheckScreenState();
                var defaultScreenData = _mainCamDataList[0];
                _mainCamera.transform.position = defaultScreenData._portraitCamPos;
                _mainCamera.transform.eulerAngles = defaultScreenData._rotaiton;    

                if (screenState == eScreenState.PORTRAIT)
                {
                    _mainCamera.orthographicSize = defaultScreenData._portaraitOrthoSize;
                    UIManager.OnChangeUIPos(false,EGameState.STATE1,_camState);
                } 
                else if (screenState == eScreenState.LANDSCAPE)
                {
                    _mainCamera.orthographicSize = defaultScreenData._landScapeOrthoSize;
                    UIManager.OnChangeUIPos(true,EGameState.STATE1,_camState);
                }
                            
                StartCoroutine(CoCamSideViewCheck());
            }
            else if(_camState == eCamState.STRAIT)
            {
                var screenState = CheckScreenState();
                var defaultScreenData = _mainCamDataList[0];
                _mainCamera.transform.position = defaultScreenData._STRPortraitCamPos;
                _mainCamera.transform.eulerAngles = defaultScreenData._STRRotaiton;    

                if (screenState == eScreenState.PORTRAIT)
                {
                    _mainCamera.orthographicSize = defaultScreenData._STRPortraitOrthoSize;
                    UIManager.OnChangeUIPos(false,EGameState.STATE1,_camState);
                } 
                else if (screenState == eScreenState.LANDSCAPE)
                {
                    _mainCamera.orthographicSize = defaultScreenData._STRLandScapeOrthoSize;
                    UIManager.OnChangeUIPos(true,EGameState.STATE1,_camState);
                }
                            
                StartCoroutine(CoCamStraitViewCheck());
            }
        }

        private IEnumerator CoCamSideViewCheck() 
        {
            while (true)
            {
                _currScreenState = CheckScreenState();

                if (_currScreenState == _lastScreenState)
                {
                    yield return CoroutineUtil.WaitForEndOfFrame;
                } 
                else 
                {
                    switch (_currScreenState)
                    {
                        case eScreenState.PORTRAIT:
                            if(_lastScreenState == eScreenState.UPGRADE)
                            {
                                if(_coZoomOut == null)
                                {
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currPortraitOrthoSize, _currPortraitCamPos));
                                }
                                else
                                {
                                    StopCoroutine(_coZoomOut);
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currPortraitOrthoSize, _currPortraitCamPos));
                                }
                            }
                            else
                            {
                                _mainCamera.orthographicSize = _currPortraitOrthoSize;
                                this.transform.position = _currPortraitCamPos;  
                                UIManager.OnChangeUIPos(false,_currGameState,_camState);
                            }
                            _lastScreenState = _currScreenState;
                            break;

                        case eScreenState.LANDSCAPE:
                            if(_lastScreenState == eScreenState.UPGRADE)
                            {
                                if(_coZoomOut == null)
                                {
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currLandScapeOrthoSize, _currLandScapeCamPos));
                                }
                                else
                                {
                                    StopCoroutine(_coZoomOut);
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currLandScapeOrthoSize, _currLandScapeCamPos));
                                }
                            }
                            else
                            {
                                _mainCamera.orthographicSize = _currLandScapeOrthoSize;
                                this.transform.position = _currLandScapeCamPos;  
                                UIManager.OnChangeUIPos(false,_currGameState,_camState);
                            }
                            _lastScreenState = _currScreenState;
                            break;
                    }
                }

                yield return CoroutineUtil.WaitForEndOfFrame;
            }           
        }

        private IEnumerator CoCamStraitViewCheck() 
        {
            while (true)
            {
                _currScreenState = CheckScreenState();

                if (_currScreenState == _lastScreenState)
                {
                    yield return CoroutineUtil.WaitForEndOfFrame;
                } 
                else 
                {
                    switch (_currScreenState)
                    {
                        case eScreenState.PORTRAIT:
                            if(_lastScreenState == eScreenState.UPGRADE)
                            {
                                if(_coZoomOut == null)
                                {
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currPortraitOrthoSize, _currPortraitCamPos));
                                }
                                else
                                {
                                    StopCoroutine(_coZoomOut);
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currPortraitOrthoSize, _currPortraitCamPos));
                                }
                            }
                            else
                            {
                                _mainCamera.orthographicSize = _currPortraitOrthoSize;
                                this.transform.position = _currPortraitCamPos;  
                                UIManager.OnChangeUIPos(false,_currGameState,_camState);
                            }
                            _lastScreenState = _currScreenState;
                            break;

                        case eScreenState.LANDSCAPE:
                            if(_lastScreenState == eScreenState.UPGRADE)
                            {
                                if(_coZoomOut == null)
                                {
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currLandScapeOrthoSize, _currLandScapeCamPos));
                                }
                                else
                                {
                                    StopCoroutine(_coZoomOut);
                                    _coZoomOut = StartCoroutine(CoStartCamAway(_currLandScapeOrthoSize, _currLandScapeCamPos));
                                }
                            }
                            else
                            {
                                _mainCamera.orthographicSize = _currLandScapeOrthoSize;
                                this.transform.position = _currLandScapeCamPos; 
                                UIManager.OnChangeUIPos(true,_currGameState,_camState);
                            }
                            _lastScreenState = _currScreenState;
                            break;
                    }
                }

                yield return CoroutineUtil.WaitForEndOfFrame;
            }           
        }

        private eScreenState CheckScreenState()
        {
            var currScreenState = eScreenState.NONE;
            _screenWidth = _canvasRect.sizeDelta.x;
            _screenHeight = _canvasRect.sizeDelta.y;

            if(_screenWidth > _screenHeight) 
                currScreenState = eScreenState.LANDSCAPE;
            else if(_screenWidth < _screenHeight) 
                currScreenState = eScreenState.PORTRAIT;

            return currScreenState;
        }

        public void ChangeCamState(EGameState state)
        {
            var idx = (int)state;

            var camData = _mainCamDataList[idx];

            if(_camState == eCamState.SDIE)
            {
                _currLandScapeOrthoSize = camData._landScapeOrthoSize;
                _currLandScapeCamPos = camData._landScapeCamPos;
                _currPortraitOrthoSize = camData._portaraitOrthoSize;
                _currPortraitCamPos = camData._portraitCamPos;
            }
            else if (_camState == eCamState.STRAIT)
            {
                _currLandScapeOrthoSize = camData._STRLandScapeOrthoSize;
                _currLandScapeCamPos = camData._STRLandScapeCamPos;
                _currPortraitOrthoSize = camData._STRPortraitOrthoSize;
                _currPortraitCamPos = camData._STRPortraitCamPos;
            }  

            _currGameState = state;
            _lastScreenState = eScreenState.UPGRADE;            
        }

        private IEnumerator CoStartCamAway(int targetOrtho, Vector3 targetPos)
        {
            var sec = _timer;
            var startSec = Time.time;
            var endSec = startSec + sec;
            var startPos = this.transform.position;
            var currState = _currScreenState;
            var isLandScape = _currScreenState == eScreenState.LANDSCAPE;

           
            while(Time.time < endSec)
            {
                UIManager.OnChangeUIPos(isLandScape,_currGameState,_camState);
                if(currState != _currScreenState)
                {
                    _lastScreenState = eScreenState.NONE;
                    yield break;
                } 

                var ratio = (Time.time - startSec) * 10;
                this.transform.position = Vector3.Lerp(startPos, targetPos, _animCurve.Evaluate(ratio));

                if(targetOrtho > _mainCamera.orthographicSize)
                {
                    _mainCamera.orthographicSize += Time.fixedDeltaTime * 14;
                }   
                else if (targetOrtho < _mainCamera.orthographicSize)
                {
                    _mainCamera.orthographicSize = targetOrtho;
                }

                yield return CoroutineUtil.WaitForFixedUpdate;
            }
            
            this.transform.position = targetPos;
        }
    }
}    

