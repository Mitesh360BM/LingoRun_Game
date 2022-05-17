using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Player player;
    public AudioClip crashClip, deathClip;
    public AudioSource source;
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    float shakeDura;
    bool shake;

    private void Start()
    {
        EventController.instance.CameraShakeEvent += Instance_CameraShakeEvemt;
    }

    private void Instance_CameraShakeEvemt()
    {

        shakeDuration = shakeDura;
        InvokeRepeating("startCameraShake", 0, 0.01f);

    }

    private void startCameraShake()
    {

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            CancelInvoke("startCameraShake");
        }



    }

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
        shakeDura = shakeDuration;
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
    }
}