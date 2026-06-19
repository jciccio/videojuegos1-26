using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    
    [SerializeField] ParticleSystem Particles;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
        StartCoroutine(DeleteObject());
    }

    IEnumerator DeleteObject()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        Particles.Play();
        yield return new WaitForSeconds(Particles.main.startLifetime.constantMax);
        Destroy(gameObject);
    }




}
