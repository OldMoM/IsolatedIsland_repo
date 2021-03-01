using UnityEngine;
using System.Collections.Generic;


namespace Siwei {
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class GridMesh : MonoBehaviour
    {
        public int GridSize;
        public float gridScale;
        public Vector3 offset = Vector3.zero;
        //public Vector3 worldPosition;
        public static Plane plane = new Plane(Vector3.up, 0);
        private float distance;
        public GameObject gridMesh;
        public GameObject drawObject;


        private void Update()
        {
            Vector3 worldPos;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPos = ray.GetPoint(distance);
                DrawOnMouseClicked(worldPos);
            }
            /*
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Current mouse Pos:"+"("+ worldPosition.x+","+ worldPosition.y+","+ worldPosition.z+ ")");
            }
            */


        }
        void Awake()
        {

            MeshFilter filter = gridMesh.GetComponent<MeshFilter>();
            var mesh = new Mesh();
            var verticies = new List<Vector3>();

            var indicies = new List<int>();
            for (int i = 0; i <= GridSize; i++)
            {

                verticies.Add(new Vector3(i * gridScale, 0, 0)+offset);
                verticies.Add(new Vector3(i * gridScale, 0, GridSize * gridScale) + offset);

                indicies.Add(4 * i + 0);
                indicies.Add(4 * i + 1);

                verticies.Add(new Vector3(0, 0, i * gridScale) + offset);
                verticies.Add(new Vector3(GridSize * gridScale, 0, i * gridScale) + offset);

                indicies.Add(4 * i + 2);
                indicies.Add(4 * i + 3);

            }
            //// checking drawing lines
            //for (int i = 0; i < verticies.Count; i++) {
            //    Debug.Log("(" + verticies[i].x + "," + verticies[i].z + ")");
            //}

            mesh.vertices = verticies.ToArray();
            mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
            filter.mesh = mesh;

            MeshRenderer meshRenderer = gridMesh.GetComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
            meshRenderer.material.color = Color.white;
        }


        public void DrawOnMouseClicked(Vector3 pos)
        {
            Vector2 idx = GetGridIndex(pos);

            MeshFilter filter = drawObject.GetComponent<MeshFilter>();
            var mesh = new Mesh();
            var verticies = new List<Vector3>();
            verticies.Add(new Vector3(idx.x * gridScale, 0, idx.y * gridScale) + offset);
            verticies.Add(new Vector3(idx.x * gridScale, 0, (idx.y + 1) * gridScale) + offset);
            verticies.Add(new Vector3((idx.x + 1) * gridScale, 0, (idx.y + 1) * gridScale) + offset);
            verticies.Add(new Vector3((idx.x + 1) * gridScale, 0, idx.y * gridScale) + offset);

            var indicies = new List<int>
        {
            0,1,2,
            0,2,3
        };
            mesh.vertices = verticies.ToArray();
            mesh.triangles = indicies.ToArray();
            filter.mesh = mesh;

            MeshRenderer meshRenderer = drawObject.GetComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
            meshRenderer.material.color = Color.green;
        }

        public Vector2 GetGridIndex(Vector3 pos) { 
            Vector2 idx = new Vector2(Mathf.Floor((pos.x - offset.x) / gridScale), Mathf.Floor((pos.z - offset.z) / gridScale));
            if (Input.GetMouseButtonDown(0))
                Debug.Log("Current Grid index:" + "(" + idx.x + "," + idx.y + ")");
            return idx;
        }
    }
} 
