using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger display for sphere colliders
[RequireComponent(typeof(SphereCollider))]
[ExecuteInEditMode]
public class TriggerDisplay : MonoBehaviour {

    public Color triggerColour;
    public Color triggerWireColour;

    private SphereCollider sc;

    private void OnEnable()
    {
        sc = GetComponent<SphereCollider>();
    }

    private void OnDrawGizmos()
    {
        RedrawBox();
    }
    private void OnDrawGizmosSelected()
    {
        RedrawBox();
    }

    private void RedrawBox()
    {
        if (sc != null)
        {
            sc.isTrigger = true;
            // Debug.Log(sc.radius);
            Vector3 drawBoxScale = new Vector3(transform.lossyScale.x * sc.radius, transform.lossyScale.y * sc.radius, transform.lossyScale.z * sc.radius);
            Vector3 tempScale = transform.worldToLocalMatrix.MultiplyPoint(transform.lossyScale);
            Vector3 drawBoxPosition = transform.localToWorldMatrix.MultiplyPoint(sc.center);
            // Debug.Log(drawBoxScale);
            // Debug.Log(drawBoxPosition);

            Gizmos.matrix = Matrix4x4.TRS(drawBoxPosition, transform.rotation, drawBoxScale);
            Gizmos.color = triggerColour;
            // Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.DrawSphere(sc.center, 1);
            Gizmos.color = triggerWireColour;
            Gizmos.DrawWireSphere(sc.center, 1);
            // Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }

}

