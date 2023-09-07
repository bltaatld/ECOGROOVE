using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    public GameObject trigger;
    public LongNote longNoteSelf;
    public TriggerTracker endTrigger;
    public bool isPressing;
    public bool isHit;
    public bool isEditMode;
    public Transform target; // 따라갈 대상 오브젝트의 Transform 컴포넌트
    public BoxCollider2D boxCollider; // 따라갈 대상 오브젝트의 Transform 컴포넌트
    public string currentTimingTag;
    public float longNoteSpeed;
    public float width;
    public float height;
    public float widthDecrementRate = 50.0f; // 너비 감소 속도
    EffectManager EffectMan;

    private void Start()
    {
        EffectMan = FindObjectOfType<EffectManager>();
        width = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        height = gameObject.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void Update()
    {

        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(width, height);
        }

        if (!GameSystem.instance.isEditMode)
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

        if(currentTimingTag != null)
        {
            endTrigger.terrainTag = currentTimingTag;
        }

        if (endTrigger.triggered)
        {
            Debug.Log("Long Hit");
            if (!isPressing && isHit)
            {
                EffectMan.PerfectHitEffect();
                Debug.Log("Long Hit");
                Destroy(trigger);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        // LongNote 스크립트 인스턴스를 GameSystem의 longNotes 배열에서 제거합니다.
        GameSystem.instance.longNote = null;
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

        if (collision.CompareTag("note") && collision.GetComponent<Hit>())
        {
            target = collision.transform;

            if(GameSystem.instance.longNote == null)
            {
                GameSystem.instance.longNote = longNoteSelf;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LongNoteEnd"))
        {
            trigger = collision.gameObject;
            endTrigger = trigger.GetComponent<TriggerTracker>();
        }
    }
}