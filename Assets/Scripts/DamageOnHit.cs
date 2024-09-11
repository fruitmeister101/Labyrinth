using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] float dmg = 1.0f;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        var character = collision.gameObject.GetComponent<BasicCharacter>();
        if (character) 
        {
            character.Health -= dmg;
            character.CheckHealth();

        }
        //GameObject.Destroy(this);
    }
}
