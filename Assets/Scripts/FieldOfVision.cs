using UnityEngine;

public class FieldOfVision : MonoBehaviour
{
    public LayerMask layersToRay;
    private Mesh mesh;
    private Renderer mshRender;

    private Vector3 dinVertex;
    private Vector3 origin;
    private float limitAngle;
    private float startingAngle;
    private Vector3 endPoint;

    void Start()
    {
        mesh = new Mesh();
        var filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;

        limitAngle = 110f;
        startingAngle = 0f;
        //mshRender = gameObject.GetComponent<Renderer>();
        //mshRender.sortingLayerID = 3;
    }
    void LateUpdate()
    {
        int rayCount = 40;
        float angleIncrease = limitAngle / rayCount;
        float angle = startingAngle;
        float distance = 19f;


        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexId = 1;
        int triangleId = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            //vertex = origin + vectorFromAngle(angle) * distance;

            RaycastHit2D hit = Physics2D.Raycast(origin, Utils.vectorFromAngle(angle), distance, layersToRay);
            if (hit.collider == null)
            {
                dinVertex = origin + Utils.vectorFromAngle(angle) * distance;

            }
            else
            {
                //dinVertex = hit.point;

                endPoint.x = hit.point.x;
                endPoint.y = hit.point.y;
                endPoint.z = Camera.main.nearClipPlane;
                endPoint += (Utils.vectorFromAngle(angle).normalized * 0.17f);
                dinVertex = endPoint;

            }
            vertices[vertexId] = dinVertex;

            if (i > 0)
            {
                triangles[triangleId + 0] = 0;
                triangles[triangleId + 1] = vertexId - 1;
                triangles[triangleId + 2] = vertexId;
                triangleId += 3;
            }

            vertexId++;
            angle -= angleIncrease;

        }


        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

    }

    public void setOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void setAimDirection(Vector3 direction)
    {
        startingAngle = Utils.angleFromVector(direction) + limitAngle * 0.5f;
    }
}
