using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : Probs
{
    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    public override void Interact(Entity entity)
    {
        StartCoroutine(DisableRoutine());
    }

    private IEnumerator DisableRoutine()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(0.3f);
        coll.enabled = true;
    }
}
