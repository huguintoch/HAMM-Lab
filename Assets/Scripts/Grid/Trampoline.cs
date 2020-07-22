using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : GridElement { 

    public override void Start() {
        this.Type = Element.Trampoline;
        //base.Start();
    }

    public override void Update() {
        //base.Update();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            Vector3 direction = other.transform.position - transform.position;
            if (direction.y > 0.45) {
                Rigidbody rg = other.GetComponent<Rigidbody>();
                rg.velocity = Vector3.zero;
                Vector3 jumpForce = new Vector3(105, 250, 0);
                other.GetComponent<Rigidbody>().AddForce(jumpForce);
            }

        }
    }
}
