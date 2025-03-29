using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

//대충 카메라 관련된거는 걍 다 여기서 처리하는 방식으로 가려고 함
public class CameraManager : Singleton<CameraManager>
{
    //이거 첫번째로 넣어준 카메라를 디폴트로 세팅해줌
    [SerializeField] private List<CinemachineVirtualCamera> _cameraSet; //시네머신 다 넣어주
    [SerializeField] private PlayerFollowObj _follow;            //플레이어 따라가는 뇨속

    private Vector2 _startingTrackedObjectOffset;   
    private Tween _panCameraTween;  
    private Sequence _shakeSeq;  

    private CinemachineVirtualCamera _currentCam;                //현재 카메라
    private CinemachineFramingTransposer _framingTransposer;     //카메라 움직여주는 놈
    private CinemachineConfiner2D _currentConfiner;              //카메라 범위 제한
    private CinemachineBasicMultiChannelPerlin _currentPerline;  //카메라 흔들어 주는 놈

    private float _shakeTime = 0;
    private float _originOrthorgraphicSize = 0;
    private Transform _playerRect;

    private Vector2[] _dirArr = new Vector2[4]
    { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Awake()
    {
        if (_cameraSet.Count > 0)
            ChangeCam(_cameraSet[0]);

        _playerRect = PlayerManager.Instance.PlayerTrm;
    }

    /// <summary>
    /// 걍 카메라 바꿔주는거임
    /// </summary>
    /// <param name="activeCam"></param>
    /// <param name="changeFollowToPlayer"></param>
    public void ChangeCam(CinemachineVirtualCamera activeCam, bool changeFollowToPlayer = true)
    {
        _cameraSet.ForEach(x => x.Priority = 5);

        activeCam.Priority = 10;
        _currentCam = activeCam;

        _currentConfiner = _currentCam.GetComponent<CinemachineConfiner2D>();
        _framingTransposer = _currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _currentPerline = _currentCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (changeFollowToPlayer)
            _currentCam.Follow = _follow.transform;

        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
    }

    /// <summary>
    /// 대충 CameraFramingTransfoser로 해두면 Offset조정 하는거 건들여서 움직이는거임
    /// 카메라 연출을 원했고 일단 CameraControllerTrigger 스크립트에서 범위에 들어갔을 때 
    /// 카메라 살짝 위쪽을 보여주는 연출 그런 걸 원했던거임
    /// </summary>
    /// <param name="panDistance"></param>
    /// <param name="panTime"></param>
    /// <param name="direction"></param>
    /// <param name="panToStartingPos"></param>
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection direction, bool panToStartingPos)
    {
        Vector3 endPos = Vector3.zero;

        if (!panToStartingPos)
        {
            endPos = _dirArr[(int)direction] * panDistance + _startingTrackedObjectOffset;
        }
        else
            endPos = _startingTrackedObjectOffset;


        if (_panCameraTween != null && _panCameraTween.IsActive())
            _panCameraTween.Kill();


        _panCameraTween = DOTween.To(
            () => _framingTransposer.m_TrackedObjectOffset,
            value => _framingTransposer.m_TrackedObjectOffset = value,
            endPos, panTime);
    }

    public void ChangeConfinder(PolygonCollider2D collider)
    {
        _currentConfiner.m_BoundingShape2D = collider;
    }

    /// <summary>
    /// ZoomIn to 'size'
    /// </summary>
    /// <param name="size"></param>
    public void ZoomIn(float size)
    {
        _originOrthorgraphicSize = _currentCam.m_Lens.OrthographicSize;
        DOTween.To(() => _currentCam.m_Lens.OrthographicSize, x => _currentCam.m_Lens.OrthographicSize = x, size, 0.5f);
        //_currentCam.m_Lens.OrthographicSize = size;
    }

    /// <summary>
    /// ZoomOut to origin orthographic
    /// </summary>
    public void ZoomOut()
    {
        DOTween.To(() => _currentCam.m_Lens.OrthographicSize, x => _currentCam.m_Lens.OrthographicSize = x, _originOrthorgraphicSize, 0.5f);
    }

    public void ChangeFollow(Transform toFollow) => _currentCam.m_Follow = toFollow;

    public void ChangeFollowToPlayer() => _currentCam.m_Follow = _playerRect;

    public void ShakeCam(float amplitude, float frequency, float time, Ease ease = Ease.Linear, bool isSet = false)
    {
        if (_shakeSeq != null && _shakeSeq.IsActive()) _shakeSeq.Kill();
        _shakeSeq = DOTween.Sequence();

        float startAmplitude = isSet ? amplitude : Mathf.Max(amplitude, _currentPerline.m_AmplitudeGain);
        float startFrequency = isSet ? frequency : Mathf.Max(frequency, _currentPerline.m_FrequencyGain);

        _shakeSeq.Append(DOTween.To(() => startAmplitude, 
            value => _currentPerline.m_AmplitudeGain = value, 0, time).SetEase(ease));
        _shakeSeq.Join(DOTween.To(() => startFrequency, 
            value => _currentPerline.m_FrequencyGain = value, 0, time).SetEase(ease));
    }

    public void StartShakeCam(float amplitude, float frequency)
    {
        _currentPerline.m_AmplitudeGain = amplitude;
        _currentPerline.m_FrequencyGain = frequency;
    }

    public void StopShakeCam()
    {
        _currentPerline.m_AmplitudeGain = 0;
        _currentPerline.m_FrequencyGain = 0;
    }

    private IEnumerator DelayStopShake()
    {
        yield return new WaitForSeconds(_shakeTime);

        _currentPerline.m_AmplitudeGain = 0f;
        _currentPerline.m_FrequencyGain = 0f;
    }

}
