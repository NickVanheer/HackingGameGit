using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Destroys the attached gameobject when the trigger has a certain tag (set in the AffectedByTag), and optionally calls a callback function to trigger an event.
/// </summary>
public class DestroyOnTriggerEnter : MonoBehaviour {

    // Use this for initialization
    public int Health = 1;
    public List<string> AffectedByTag;

    public bool IsTriggerStayAlive;
    public bool IsInvincible = false;
    public bool BlinkOnTakeDamage = false;
    public Color TakeDamageColor;

    private Color defaultColor;
    Renderer RenderComponent;

    public UnityEvent OnDestroyCallback;

    void Start()
    {
        if (BlinkOnTakeDamage)
        {
            RenderComponent = GetComponentInChildren<Renderer>();

            if(RenderComponent != null)
                defaultColor = RenderComponent.material.color;
        }

        if (IsInvincible)
            Health = 999999;
    }

    void OnTriggerEnter(Collider col)
    {
        if (AffectedByTag.Contains(col.gameObject.tag))
        {
            TakeDamage(1);

            if (!IsTriggerStayAlive)
                Destroy(col.gameObject); //destroy bullet
        }

    }

    void TakeDamage(int value)
    {
        if(Health > 1 && BlinkOnTakeDamage && RenderComponent != null)
            StartCoroutine(damageFlash());

        Health -= value;

        if (Health <= 0)
        {
            Destroy(this.gameObject);

            if (OnDestroyCallback != null)
                OnDestroyCallback.Invoke();
        }

    }

    IEnumerator damageFlash()
    {
        RenderComponent.material.color = TakeDamageColor;
        yield return new WaitForSeconds(0.1f);
        RenderComponent.material.color = defaultColor;
    }
}
