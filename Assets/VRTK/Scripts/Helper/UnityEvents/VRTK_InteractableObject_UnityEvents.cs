﻿using UnityEngine;
using UnityEngine.Events;
using VRTK;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class VRTK_InteractableObject_UnityEvents : MonoBehaviour
{
    private VRTK_InteractableObject io;

    [System.Serializable]
    public class UnityObjectEvent : UnityEvent<InteractableObjectEventArgs> { };

    /// <summary>
    /// Emits the InteractableObjectTouched class event.
    /// </summary>
    public UnityObjectEvent OnTouch;
    /// <summary>
    /// Emits the InteractableObjectUntouched class event.
    /// </summary>
    public UnityObjectEvent OnUntouch;
    /// <summary>
    /// Emits the InteractableObjectGrabbed class event.
    /// </summary>
    public UnityObjectEvent OnGrab;
    /// <summary>
    /// Emits the InteractableObjectUngrabbed class event.
    /// </summary>
    public UnityObjectEvent OnUngrab;
    /// <summary>
    /// Emits the InteractableObjectUsed class event.
    /// </summary>
    public UnityObjectEvent OnUse;
    /// <summary>
    /// Emits the InteractableObjectUnused class event.
    /// </summary>
    public UnityObjectEvent OnUnuse;

    private void SetInteractableObject()
    {
        if (io == null)
        {
            io = GetComponent<VRTK_InteractableObject>();
        }
    }

    private void OnEnable()
    {
        SetInteractableObject();
        if (io == null)
        {
            Debug.LogError("The VRTK_InteractableObject_UnityEvents script requires to be attached to a GameObject that contains a VRTK_InteractableObject script");
            return;
        }

        io.InteractableObjectTouched += Touch;
        io.InteractableObjectUntouched += UnTouch;
        io.InteractableObjectGrabbed += Grab;
        io.InteractableObjectUngrabbed += UnGrab;
        io.InteractableObjectUsed += Use;
        io.InteractableObjectUnused += Unuse;
    }

    private void Touch(object o, InteractableObjectEventArgs e)
    {
        OnTouch.Invoke(e);
    }

    private void UnTouch(object o, InteractableObjectEventArgs e)
    {
        OnUntouch.Invoke(e);
    }

    private void Grab(object o, InteractableObjectEventArgs e)
    {
        OnGrab.Invoke(e);
    }

    private void UnGrab(object o, InteractableObjectEventArgs e)
    {
        OnUngrab.Invoke(e);
    }

    private void Use(object o, InteractableObjectEventArgs e)
    {
        OnUse.Invoke(e);
    }

    private void Unuse(object o, InteractableObjectEventArgs e)
    {
        OnUnuse.Invoke(e);
    }

    private void OnDisable()
    {
        if (io == null)
        {
            return;
        }

        io.InteractableObjectTouched -= Touch;
        io.InteractableObjectUntouched -= UnTouch;
        io.InteractableObjectGrabbed -= Grab;
        io.InteractableObjectUngrabbed -= UnGrab;
        io.InteractableObjectUsed -= Use;
        io.InteractableObjectUnused -= Unuse;
    }
}