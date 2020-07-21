using UnityEngine;

public class TargetPosition {

    public Vector3 Position { get; }
    public bool IsGrid { get; }

    public TargetPosition (Vector3 position, bool isGrid) {
        Position = position;
        IsGrid = isGrid;
    }

}
