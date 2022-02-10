using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private LayerMask _whatClikable;
    
    
    
    private Camera _camera;
    private Rigidbody _rigidbodyCube;
    private Vector3 _direction;
    private Vector3 _moveVelocity;
    private NavMeshAgent _agent;
    private bool _joystickInput;
   



    // Start is called before the first frame update
    void Start()
    {
      
        _rigidbodyCube = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _camera = FindObjectOfType<Camera>().GetComponent<Camera>();
       
    }


    private void Update()
    {
        MoveCubeVector();

        if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
        {
            _joystickInput = true;
            _agent.enabled = false;
            
        }
        else
        {
            _joystickInput = false;
            _agent.enabled = true;
           
        }

    }

    private void FixedUpdate()
    {
        CubeMove();
        TouchMovement();


    }

    private void TouchMovement()
    {


        if (Input.GetMouseButtonDown(0) && _joystickInput==false)
        {
            
            Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit _hitInfo;

            if(Physics.Raycast(_ray, out _hitInfo, 100,  _whatClikable))
            {
                _agent.SetDestination(_hitInfo.point);
                Debug.Log("Check");
            }

            
        }
    }


   

    public void MoveCubeVector()
    {
        _direction = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
        _moveVelocity = _direction.normalized * _speed;


    }

    public void CubeMove()
    {
        _rigidbodyCube.MovePosition(_rigidbodyCube.position + _moveVelocity * Time.deltaTime);
       
    }

}
