using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.CompareTag("Pin"))
            {
                AudioManager.Instance.PlayCollideSound();
            }
        }
    }
}
