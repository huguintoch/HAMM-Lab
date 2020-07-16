using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : GridElement
{
    

    public override void Start() {
        this.Type = Element.Trampoline;
        //base.Start();
    }

    public override void Update() {
        //base.Update();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag.Equals("Player")) {
            Vector3 direction = collision.transform.position - transform.position;
            if (direction.y > 0.45) {
                Vector3 jumpForce = new Vector3(0, 200, 0);
                collision.collider.GetComponent<Rigidbody>().AddForce(jumpForce);
            }
            
        }
    }
}
