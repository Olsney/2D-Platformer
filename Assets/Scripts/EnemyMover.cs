using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Transform _path;

    private Rigidbody2D _rigidbody;
    private bool _isLookingRight;
    private Transform[] _points;
    private int _currentPointIndex;

    private void Start()
    {
        _isLookingRight = true;
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i);
    }

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        TryRotate();
    }

    private void Move()
    {
        Transform target = _points[_currentPointIndex];
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        if ((transform.position - target.position).magnitude < 1f)
            ChangePoint();
    }

    private void ChangePoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _points.Length)
            _currentPointIndex = 0;
    }

    private void TryRotate()
    {
        float leftRotation = -180f;
        float rightRotation = 180f;

        if (IsMovingRight() == false && _isLookingRight)
        {
            transform.Rotate(new Vector3(0, leftRotation, 0));
            _isLookingRight = false;
        }
        
        if (IsMovingRight() && _isLookingRight == false)
        {
            transform.Rotate(new Vector3(0, rightRotation, 0));
            _isLookingRight = true;
        }
    }
    
    private bool IsMovingRight() =>
        _rigidbody.velocity.x > 0;
}
