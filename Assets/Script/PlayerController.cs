using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour {
    public float speed = 10;
    public float upForce = 20.0f;
    public float animationFrameRate = 10f;
    public float animationRate = 10.0f;

    [SerializeField] GameObject brokenParticle;
    [SerializeField] float brokenTime = 0.5f;
    [SerializeField] GameObject gameController;

    private Vector3 force = new Vector3();
    private Rigidbody rigidbody;
    private bool collisionFlag = false;
    private bool toJump = false;
    private bool isStarted = false;
    private List<int> contactGameObjectID = new List<int>();

    private void Start()
    {
        force = Vector3.zero;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        force.x = Input.GetAxis("Horizontal") * speed;
        force.z = Input.GetAxis("Vertical") * speed;

        if (Input.GetButtonDown("Jump") && collisionFlag && !toJump) {
            toJump = true;
            force.y = upForce;
        }

    }

    void FixedUpdate()
    {
        if (force != Vector3.zero & rigidbody != null) {
            rigidbody.AddForce(force * speed);
            toJump = false;
            force.y = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionFlag = true;
        if (!contactGameObjectID.Contains(collision.gameObject.GetInstanceID())) {
            contactGameObjectID.Add(collision.gameObject.GetInstanceID());
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        collisionFlag = true;
        if (!contactGameObjectID.Contains(collision.gameObject.GetInstanceID())) {
            contactGameObjectID.Add(collision.gameObject.GetInstanceID());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (contactGameObjectID.Contains(collision.gameObject.GetInstanceID())) {
            contactGameObjectID.Remove(collision.gameObject.GetInstanceID());
        }
        if (contactGameObjectID.Count == 0) {
            collisionFlag = false;
        }
    }

}
