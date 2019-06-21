using UnityEngine;

public class PlayerControls3D : MonoBehaviour
{
    public float Speed = 2;

    private Rigidbody componentRigidbody;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        componentRigidbody.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) componentRigidbody.velocity += Vector3.left * Speed;
        if (Input.GetKey(KeyCode.RightArrow)) componentRigidbody.velocity += Vector3.right * Speed;
        if (Input.GetKey(KeyCode.UpArrow)) componentRigidbody.velocity += Vector3.forward * Speed;
        if (Input.GetKey(KeyCode.DownArrow)) componentRigidbody.velocity += Vector3.back * Speed;
    }
}