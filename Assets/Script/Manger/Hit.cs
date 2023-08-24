using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hit : MonoBehaviour
{
    public float noteSpeed = 1;
    public bool isEditMode;
    public bool isSpike;
    public GameObject note;
    public TMP_InputField inputNoteSpeed;
    UnityEngine.UI.Image noteImage;

    void Start()
    {
        noteImage = GetComponent<UnityEngine.UI.Image>();
    }

    void Update()
    {
        if(!isEditMode && !isSpike)
        {
            transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
        }
        if (!isEditMode && isSpike)
        {
            transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime;
        }
    }

    public void HideNote()
    {
        noteImage.enabled = false;
        Destroy(note);
    }

    public void SetSpeed()
    {
        if(!isSpike)
        {
            if (float.TryParse(inputNoteSpeed.text, out noteSpeed))
            {
                Debug.Log("speed set to " + noteSpeed.ToString());
            }
            else
            {
                Debug.Log("speed set failed");
            }
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameSystem.instance.playerControl.isClick)
        {
            if (collision.CompareTag("Pure"))
            {
                GameSystem.instance.timing.EffectMan.PerfectHitEffect();
                Destroy(gameObject);
            }
            if (collision.CompareTag("Neat"))
            {
                GameSystem.instance.timing.EffectMan.GoodHitEffect();
                Destroy(gameObject);
            }
            if (collision.CompareTag("Clean"))
            {
                GameSystem.instance.timing.EffectMan.BadHitEffect();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Miss");
                Destroy(gameObject);
            }
        }
    }*/
}
