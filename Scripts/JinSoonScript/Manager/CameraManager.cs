using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

//���� ī�޶� ���õȰŴ� �� �� ���⼭ ó���ϴ� ������� ������ ��
public class CameraManager : Singleton<CameraManager>
{
    //�̰� ù��°�� �־��� ī�޶� ����Ʈ�� ��������
    [SerializeField] private List<CinemachineVirtualCamera> _cameraSet; //�ó׸ӽ� �� �־���
    [SerializeField] private PlayerFollowObj _follow;            //�÷��̾� ���󰡴� ����

    private Vector2 _startingTrackedObjectOffset;   
    private Tween _panCameraTween;  
    private Sequence _shakeSeq;  

    private CinemachineVirtualCamera _currentCam;                //���� ī�޶�
    private CinemachineFramingTransposer _framingTransposer;     //ī�޶� �������ִ� ��
    private CinemachineConfiner2D _currentConfiner;              //ī�޶� ���� ����
    private CinemachineBasicMultiChannelPerlin _currentPerline;  //ī�޶� ���� �ִ� ��

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
    /// �� ī�޶� �ٲ��ִ°���
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
    /// ���� CameraFramingTransfoser�� �صθ� Offset���� �ϴ°� �ǵ鿩�� �����̴°���
    /// ī�޶� ������ ���߰� �ϴ� CameraControllerTrigger ��ũ��Ʈ���� ������ ���� �� 
    /// ī�޶� ��¦ ������ �����ִ� ���� �׷� �� ���ߴ�����
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
