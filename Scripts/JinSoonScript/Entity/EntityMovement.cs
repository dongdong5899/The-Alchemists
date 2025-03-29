using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    protected Entity _owner;
    public Rigidbody2D RigidbodyCompo { get; protected set; }
    protected float _xVelocity;
    public bool canSetVelocity;

    public void Initialize(Entity owner)
    {
        _owner = owner;
        canSetVelocity = true;
        RigidbodyCompo = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        RigidbodyCompo.velocity = new Vector2(_xVelocity, RigidbodyCompo.velocity.y);
    }

    public virtual void SetVelocity(Vector2 velocity, bool doNotFlip = false, bool withYVelocity = false)
    {
        if (canSetVelocity == false) return;

        _xVelocity = velocity.x;
        if (withYVelocity)
        {
            RigidbodyCompo.velocity = new Vector2(RigidbodyCompo.velocity.x, velocity.y);
        }
        if (!doNotFlip)
        {
            _owner.FlipController(velocity.x);
        }
    }

    public virtual void StopImmediately(bool withYAxis = false)
    {
        _xVelocity = 0;
        if (withYAxis)
        {
            RigidbodyCompo.velocity = Vector2.zero;
        }
    }
}
