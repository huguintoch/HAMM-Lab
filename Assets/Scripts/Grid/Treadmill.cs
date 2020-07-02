using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : GridElement {
    public override void Start() {
        //base.Start();
    }

    public override void Update() {
        //base.Update();
    }

    private void OnCollisionStay(Collision collision) {
        if (!collision.collider.tag.Equals("Grid")) {
            collision.collider.transform.Translate(Vector3.right * Time.deltaTime, Space.World);
        }
    }

}
