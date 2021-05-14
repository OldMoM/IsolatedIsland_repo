using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;

namespace Siwei {
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class GridMesh : MonoBehaviour
    {
        private BuildSketch sketch;

        private Vector3 offset = Vector3.zero;
        private Vector3 mousePosOffset = Vector3.zero;

        private float distance;
        private bool buildState = false;

        public int gridSize;
        public float gridScale;
        public static Plane plane = new Plane(Vector3.up, 0);
        
        public GameObject gridMesh;
        public GameObject newIsland;
        public GameObject drawCoord;

        private GameObject[] drawIslandPropsGrid;

        private void Start()
        {
            // Draw Island
            sketch.OnMouseHoverPosition
                .Where(_ => sketch.GetIslandMode())
                .Subscribe(x =>
                {
                    if(sketch.buildSeq < sketch.IslandBuildSeq.Count)
                    {
                        DrawOnIslandMode(sketch.IslandBuildSeq[sketch.buildSeq], x);
                    }
                    
                });

            // Draw Prop
            sketch.OnMouseHoverPosition
                .Where(_ => sketch.GetPropMode())
                .Subscribe(x =>
                {
                    DrawOnPropMode(sketch.islandsPosList, x);
                });

            // send build request
            sketch.OnMouseClickedPos
                .Where(_ => buildState)
                .Subscribe(x =>
                {
                    /*
                    Vector2 idx = GetGridIndex(x);
                    Debug.Log("Offset: " +offset.x+ "Clicked MousePos: " + x.x + "," + x.y + "," + x.z + " Idx: " + idx.x + "," + idx.y);
                    */


                    string buildFacility = sketch.BuildObject; // get facility name for request
                    if (sketch.CheckBuildRequirement(buildFacility))
                    {
                        sketch.buildRequest.OnNext((buildFacility, x));
                        Debug.Log("Send build request:(" + buildFacility + " " + x.x + "," + x.y + "," + x.z + ")");
                    }
                    else
                    {
                        Debug.Log("Lack of resource. Cannot build" + buildFacility);
                    }
                    
                    
                });
        }
        /*
        private void Update()
        {
            Vector3 worldPos;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPos = ray.GetPoint(distance);
                Vector2Int mouseIdx = GetGridIndex(worldPos);

                if (Input.GetMouseButtonDown(0))
                    Debug.Log("Current Grid index:" + "(" + mouseIdx.x + "," + mouseIdx.y + ")");
            }
        }
        */

        private void Awake()
        {
            if(sketch == null)
            {
                sketch = FindObjectOfType<BuildSketch>();
            }

            mousePosOffset -= gridScale * 0.5f * new Vector3(1f,0, 1f); 

            offset -= gridScale*0.5f * new Vector3(1f, 0, 1f);
            offset -= (gridSize / 2) * gridScale * new Vector3(1f, 0, 1f);
            //Debug.Log("Offset is [:" + offset.x + "," + offset.y + "," + offset.z + "]");

            MeshFilter filter = gridMesh.GetComponent<MeshFilter>();
            var mesh = new Mesh();
            var verticies = new List<Vector3>();

            var indicies = new List<int>();

            for (int i = 0; i <= gridSize; i++)
            {
                verticies.Add(new Vector3(i * gridScale, 0, 0)+offset);
                verticies.Add(new Vector3(i * gridScale, 0, gridSize * gridScale) + offset);

                indicies.Add(4 * i + 0);
                indicies.Add(4 * i + 1);

                verticies.Add(new Vector3(0, 0, i * gridScale) + offset);
                verticies.Add(new Vector3(gridSize * gridScale, 0, i * gridScale) + offset);

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


            /*
            // draw Coord for debug
            MeshFilter _filter = drawCoord.GetComponent<MeshFilter>();
            var _mesh = new Mesh();
            var _verticies = new List<Vector3>();
            var _indicies = new List<int>();
            _verticies.Add(new Vector3(-50f, 0, 0));
            _verticies.Add(new Vector3(50f, 0, 0));
            _indicies.Add(0);
            _indicies.Add(1);

            _verticies.Add(new Vector3(0, 0, 50f));
            _verticies.Add(new Vector3(0, 0, -50f));
            _indicies.Add(2);
            _indicies.Add(3);

            _mesh.vertices = _verticies.ToArray();
            _mesh.SetIndices(_indicies.ToArray(), MeshTopology.Lines, 0);
            _filter.mesh = _mesh;
            MeshRenderer _meshRenderer = drawCoord.GetComponent<MeshRenderer>();
            _meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _meshRenderer.material.color = Color.yellow;
            */
        }
        public void DrawColor(Vector2Int idx, Vector3 mousePos, GameObject drawObj, bool islandMode)
        {
            if (!sketch.CheckIslandExist(idx) && !islandMode)
            {
                return;
            }

            bool isMouseOver = GetGridIndex(mousePos) == idx ? true : false;
            //Debug.Log("isMouseOver:" + isMouseOver);
            var mesh = new Mesh();
            var verticies = new List<Vector3>();
            verticies.Add(new Vector3(idx.x * gridScale, 0, idx.y * gridScale) + mousePosOffset); //offset 
            verticies.Add(new Vector3(idx.x * gridScale, 0, (idx.y + 1) * gridScale) + mousePosOffset);
            verticies.Add(new Vector3((idx.x + 1) * gridScale, 0, (idx.y + 1) * gridScale) + mousePosOffset);
            verticies.Add(new Vector3((idx.x + 1) * gridScale, 0, idx.y * gridScale) + mousePosOffset);

            var indicies = new List<int>
            {
            0,1,2,
            0,2,3
            };

            mesh.vertices = verticies.ToArray();
            mesh.triangles = indicies.ToArray();

            MeshFilter filter = drawObj.GetComponent<MeshFilter>();
            filter.mesh = mesh;

            MeshRenderer meshRenderer = drawObj.GetComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

            if (sketch.CheckFacilityExist(idx))
            {
                meshRenderer.material.color = Color.red;
                return;
            }
            meshRenderer.material.color = isMouseOver ? Color.green : Color.white;
            buildState = isMouseOver ? true : false;
            
        }

        /// <summary>
        /// 建造岛屿模式，固定顺序新建岛屿
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mouseIdx"></param>
        public void DrawOnIslandMode(Vector2Int idx, Vector3 mousePos)
        {
            if (newIsland == null)
            {
                newIsland = new GameObject();
                newIsland.AddComponent(typeof(MeshFilter));
                newIsland.AddComponent(typeof(MeshRenderer));
                newIsland.transform.SetParent(gridMesh.transform);
            }

            DrawColor(idx, mousePos, newIsland, true);
        }

        /// <summary>
        /// 建造设施模式给每块岛屿上色
        /// </summary>
        /// <param name="islandPosList"></param>
        public void DrawOnPropMode(List<Vector2Int> islandPosList, Vector3 mousePos)
        {
            int islandNum = islandPosList.Count;
            if(drawIslandPropsGrid == null)
            {
                drawIslandPropsGrid = new GameObject[islandNum];
                for (int i = 0; i < islandNum; i++)
                {
                    drawIslandPropsGrid[i].AddComponent(typeof(MeshFilter));
                    drawIslandPropsGrid[i].AddComponent(typeof(MeshRenderer));
                }
            }

            for (int i = 0; i < islandNum; i++)
            {
                DrawColor(islandPosList[i],  mousePos, drawIslandPropsGrid[i], false);
            }

        }

       

        public Vector2Int GetGridIndex(Vector3 pos) {
            //Vector2 idx = new Vector2(Mathf.Floor((pos.x - offset.x) / gridScale), Mathf.Floor((pos.z - offset.z) / gridScale));
            Vector2 idx = new Vector2(Mathf.Floor((pos.x - mousePosOffset.x) / gridScale), Mathf.Floor((pos.z - mousePosOffset.z) / gridScale));
            return Vector2Int.RoundToInt(idx);
        }
    }
} 
