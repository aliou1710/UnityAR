using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PLayerController : MonoBehaviour
{
   // [SerializeField] private Rigidbody rigidbody;
    //[SerializeField] private FixedJoystick _joystick;
    private float movespeed = 1f;
    private Vector3 inputvector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   /* private void FixedUpdate()
    {
        var rigibody_ = GetComponent<Rigidbody>();
        // rigibody_.velocity = new Vector3(_joystick.Horizontal * movespeed,0, _joystick.Vertical * movespeed);
        rigidbody.velocity = inputvector;
        inputvector = new Vector3(_joystick.Horizontal * 1f, 0, _joystick.Vertical * 1f);
        this.transform.LookAt(transform.position + new Vector3(inputvector.x, 0, inputvector.z));
        this.transform.position += new Vector3(rigibody_.velocity.x * 0.0015f, 0, rigibody_.velocity.z * 0.0015f);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }
    }*/
    
}
