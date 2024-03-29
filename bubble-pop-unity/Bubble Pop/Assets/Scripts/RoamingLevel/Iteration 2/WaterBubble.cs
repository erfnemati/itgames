using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterBubble : MonoBehaviour
{
    [SerializeField] AudioClip m_collideWithBubbleSound;


    private void Start()
    {
        transform.DOScale(0.3f, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FuelBubble"))
        {
            RoamingInputHandler._instance.SetIsDragging(false);
            GameObject.FindObjectOfType<RoamingGameManager>().ReloadState();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.CompareTag("ShuttlePart"))
        {
            RoamingInputHandler._instance.SetIsDragging(false);
            RoamingSoundEffectManager._instance.PlaySound(m_collideWithBubbleSound);
            FindObjectOfType<ThirdRoamingGameManager>().RestartState();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
