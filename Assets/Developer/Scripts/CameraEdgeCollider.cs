using UnityEngine;

public class CameraEdgeCollider : MonoBehaviour
{
    
    void Start()
    {
        addEdgeCamera();
    }


    void addEdgeCamera()
    {
        if (Camera.main != null)
        {
            var edgeCollider = gameObject.GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : gameObject.GetComponent<EdgeCollider2D>();
            
            var leftBottomEdge = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            var leftTopEdge = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
            var rightTopEdge = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));
            var rightBottomEdge = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));

            var edgePonits = new[] { leftBottomEdge, leftTopEdge, rightTopEdge, rightBottomEdge};

            edgeCollider.points = edgePonits;
            edgeCollider.isTrigger = true;
        }
    }
}
