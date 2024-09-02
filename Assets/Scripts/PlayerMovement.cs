using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]private Vector2 _moveDirection;
    [SerializeField] private float _speed = 1f;
    private Rigidbody2D _rigidbody2D;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnValidate()
    {
        if(_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody2D.position;
        Vector2 translation = _speed * Time.fixedDeltaTime * _moveDirection;
        _rigidbody2D.MovePosition(position + translation);
    }

    private void GatherInput()
    {
        _moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

}
