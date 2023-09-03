using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public TriggerTracker endTrigger;
    public bool isPressing;
    public bool isHit;
    public bool isEditMode;
    public Transform target; // 따라갈 대상 오브젝트의 Transform 컴포넌트
    public string currentTimingTag;
    public float longNoteSpeed;
    public float width;
    public float widthDecrementRate = 50.0f; // 너비 감소 속도
    EffectManager EffectMan;

    private void Start()
    {
        EffectMan = FindObjectOfType<EffectManager>();
        width = gameObject.GetComponent<RectTransform>().sizeDelta.x;
    }

    private void Update()
    {
        if(!isEditMode)
        {
            transform.localPosition += Vector3.right * longNoteSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            isPressing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressing = false;
        }

        endTrigger.terrainTag = currentTimingTag;
        if (endTrigger.triggered)
        {
            Debug.Log("Long Hit");
            if (!isPressing && isHit)
            {
                EffectMan.PerfectHitEffect();
                Debug.Log("Long Hit");
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(currentTimingTag))
        {
            if (isPressing && target == null)
            {
                Debug.Log("Pressed");
                isHit = true;
            }
            if(!isPressing && target == null)
            {
                Debug.Log("Not Pressed");
                Destroy(gameObject);
            }
        }
    }
}