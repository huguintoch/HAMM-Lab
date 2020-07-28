using UnityEngine;

public class PointerTarget {

    public Vector3 TargetPosition { get; }
    public GameObject TargetCollider { get; }

    public PointerTarget (Vector3 targetPosition, GameObject targetCollider) {
        TargetPosition = targetPosition;
        TargetCollider = targetCollider;
    }

}
