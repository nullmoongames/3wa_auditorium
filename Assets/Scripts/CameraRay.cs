using UnityEngine;

public class CameraRay : MonoBehaviour
{
    [SerializeField]
    private Texture2D _moveCursorTexture;
    [SerializeField]
    private Texture2D _resizeCursorTexture;
    [SerializeField]
    private LayerMask _interactableLayerMask;

    private Camera _camera;

    private const string EFFECTOR_CENTER_TAG_NAME = "EffectorCenter";
    private const string EFFECTOR_EDGE_TAG_NAME = "EffectorEdge";

    private Transform _currentColliderTransform;

    private bool _isMoving;
    private bool _isResizing;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D collider = CastRay();
            if (collider != null)
            {
                _currentColliderTransform = collider.transform;

                if(_currentColliderTransform.CompareTag(EFFECTOR_CENTER_TAG_NAME))
                {
                    _isMoving = true;
                }
                else if(_currentColliderTransform.CompareTag(EFFECTOR_EDGE_TAG_NAME))
                {
                    _isResizing = true;
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            // stop interaction
            _isMoving = false;
            _isResizing = false;
            _currentColliderTransform = null;
        }

        if(_isMoving)
        {
            DoMove();
        }

        if(_isResizing)
        {
            DoResize();
        }
    }

    private void FixedUpdate()
    {
        CastRay();
    }

    private Collider2D CastRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _interactableLayerMask);

        if(hit.collider != null)
        {
            if(hit.collider.CompareTag(EFFECTOR_CENTER_TAG_NAME))
            {
                // move
                Cursor.SetCursor(_moveCursorTexture, new Vector2(_moveCursorTexture.width / 2, _moveCursorTexture.height / 2), CursorMode.ForceSoftware);
            }
            else if(hit.collider.CompareTag(EFFECTOR_EDGE_TAG_NAME))
            {
                // resize
                Cursor.SetCursor(_resizeCursorTexture, new Vector2(_resizeCursorTexture.width / 2, _resizeCursorTexture.height / 2), CursorMode.ForceSoftware);
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        return hit.collider;
    }

    private void DoMove()
    {
        Vector3 screenToWorldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        screenToWorldPoint.z = 0;
        _currentColliderTransform.parent.position = screenToWorldPoint;
    }

    private void DoResize()
    {
        Vector2 transformPos = _currentColliderTransform.position;
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        CircleShape circleShape = _currentColliderTransform.GetComponent<CircleShape>();
        if (circleShape != null)
        {
            circleShape.Radius = Mathf.Clamp(Vector2.Distance(transformPos, mousePos), .65f, 3.5f);
        }
    }
}
