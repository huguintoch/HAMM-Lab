using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : GridElement {
    public override void Start() {
        this.Type = Element.Treadmill;
        //base.Start();
    }

    public override void Update() {
        //base.Update();
    }

    private void OnCollisionStay(Collision collision) {
        if (!collision.collider.tag.Equals("Grid")) {
            collision.collider.GetComponent<Rigidbody>().velocity = Vector3.right * 80 * Time.deltaTime;
            //collision.collider.GetComponent<Rigidbody>().AddForce(Vector3.right*20 * Time.deltaTime);
        }
    }

}
