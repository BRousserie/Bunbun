using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public class MapView : Singleton<MapView>
    {
        public enum MapOrientation
        {
            BottomToTop,
            TopToBottom,
            RightToLeft,
            LeftToRight
        }

        public MapManager mapManager;
        public MapOrientation orientation;

        public GameObject nodePrefab;
        [Tooltip("Offset of both ends of the map from the edges of the screen")] public float mapEndsMargin;
        
        [Header("Background Settings")]
        [Tooltip("If null, background will not be shown")] public Sprite background;
        public Color32 backgroundColor = Color.white;
        public float xSize;
        public float yOffset;
        
        [Header("Line Settings")]
        public GameObject linePrefab;
        [Tooltip("Should be > 2 for smooth color gradients")] [Range(3, 10)] public int linePointsCount = 10;
        [Tooltip("Distance from the node to the line starting point")] public float offsetFromNodes = 0.5f;
        
        [Header("Colors")]
        [Tooltip("Visited or Attainable nodes color")] public Color32 visitedColor = Color.white;
        [Tooltip("Locked node color")] public Color32 lockedColor = Color.gray;
        [Tooltip("Visited or available path color")] public Color32 lineVisitedColor = Color.white;
        [Tooltip("Unavailable path color")] public Color32 lineLockedColor = Color.gray;

        private Camera cam;
        private GameObject firstParent, mapParent;
        private List<List<Point>> paths;
        private readonly List<MapNode> MapNodes = new List<MapNode>();
        private readonly List<LineConnection> lineConnections = new List<LineConnection>();
        private List<MapConfig> allMapConfigs;
        private ScrollNonUI scrollNonUi;


        private new void Awake()
        {
            base.Awake();
            cam = Camera.main;
            allMapConfigs = Resources.LoadAll("ScriptableObjects/Map/MapConfigs", typeof(MapConfig))
                .Cast<MapConfig>().ToList();
        }

        public void ShowMap(Map m)
        {
            if (m != null)
            {
                ClearMap();
                CreateMapParent();
                CreateNodes(m.nodes);
                DrawLines();
                SetOrientation();
                ResetNodesRotation();
                SetAttainableNodes();
                SetLineColors();
                CreateMapBackground(m);
            }
            else { Debug.LogWarning("Map was null in MapView.ShowMap()"); return; }
        }

        private void ClearMap()
        {
            if (firstParent != null)
                Destroy(firstParent);

            MapNodes.Clear();
            lineConnections.Clear();
        }

        private void CreateMapParent()
        {
            firstParent = new GameObject("OuterMapParent");
            mapParent = new GameObject("MapParentWithAScroll");
            mapParent.transform.SetParent(firstParent.transform);
            scrollNonUi = mapParent.AddComponent<ScrollNonUI>();
            scrollNonUi.freezeX = orientation == MapOrientation.BottomToTop || orientation == MapOrientation.TopToBottom;
            scrollNonUi.freezeY = orientation == MapOrientation.LeftToRight || orientation == MapOrientation.RightToLeft;
            BoxCollider boxCollider = mapParent.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(100, 100, 1);
        }


        private void CreateNodes(IEnumerable<Node> nodes)
        {
            foreach (Node node in nodes)
                MapNodes.Add(CreateMapNode(node));
        }

        private MapNode CreateMapNode(Node node)
        {
            GameObject mapNodeObject = Instantiate(nodePrefab, mapParent.transform);
            MapNode mapNode = mapNodeObject.GetComponent<MapNode>();
            RoomPattern blueprint = GetRoomPattern(node.roomPatternName);
            mapNode.SetUp(node, blueprint);
            mapNode.transform.localPosition = node.position;
            return mapNode;
        }
        
        private void DrawLines()
        {
            foreach (MapNode node in MapNodes)
                foreach (Point connection in node.Node.outgoing)
                    AddLineConnection(node, GetNode(connection));
        }

        private void AddLineConnection(MapNode from, MapNode to)
        {
            GameObject lineObject = Instantiate(linePrefab, mapParent.transform);
            LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
            Vector3 fromPoint = from.transform.position +
                                (to.transform.position - from.transform.position).normalized * offsetFromNodes;
            Vector3 toPoint = to.transform.position +
                              (from.transform.position - to.transform.position).normalized * offsetFromNodes;

            lineObject.transform.position = fromPoint;
            lineRenderer.useWorldSpace = false;
            lineRenderer.positionCount = linePointsCount;
            
            for (int i = 0; i < linePointsCount; i++)
                lineRenderer.SetPosition(i, Vector3.Lerp(
                    Vector3.zero, toPoint - fromPoint, (float) i / (linePointsCount - 1)));

            DottedLineRenderer dottedLine = lineObject.GetComponent<DottedLineRenderer>();
            if (dottedLine != null) dottedLine.ScaleMaterial();

            lineConnections.Add(new LineConnection(lineRenderer, from, to));
        }

        private void SetOrientation()
        {
            if (scrollNonUi)
            {
                float span = mapManager.CurrentMap.DistanceBetweenFirstAndLastLayers();
                float distanceLimit = span + 2f * mapEndsMargin;
                firstParent.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0f);
                switch (orientation)
                {
                    case MapOrientation.BottomToTop:
                        SetVerticalOrientation(-distanceLimit, 0, mapEndsMargin, 0);
                        break;
                    case MapOrientation.TopToBottom:
                        SetVerticalOrientation(0, distanceLimit, -mapEndsMargin, 180);
                        break;
                    case MapOrientation.RightToLeft:
                        SetHorizontalOrientation(0, distanceLimit, -mapEndsMargin * cam.aspect, 90);
                        break;
                    case MapOrientation.LeftToRight:
                        SetHorizontalOrientation(-distanceLimit, 0, mapEndsMargin * cam.aspect, -90);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        private void SetVerticalOrientation(float ymin, float ymax, float offset, float angle)
        {
            mapParent.transform.eulerAngles = new Vector3(0, 0, angle);
            firstParent.transform.localPosition += new Vector3(0, offset, 0);
            scrollNonUi.yConstraints.min = ymin;
            scrollNonUi.yConstraints.max = ymax;
        }

        private void SetHorizontalOrientation(float xmin, float xmax, float offset, float angle)
        {
            mapParent.transform.eulerAngles = new Vector3(0, 0, angle);
            float bossY = MapNodes.FirstOrDefault(n => n.Node.roomType == RoomType.Boss).transform.position.y;
            firstParent.transform.localPosition += new Vector3(offset, -bossY, 0);
            scrollNonUi.xConstraints.min = xmin;
            scrollNonUi.xConstraints.max = xmax;
        }

        private void ResetNodesRotation()
        {
            foreach (MapNode node in MapNodes)
                node.transform.rotation = Quaternion.identity;
        }
        
        public void SetAttainableNodes()
        {
            foreach (MapNode node in MapNodes)  node.SetState(NodeStates.Locked);

            List<Point> exploredPoints = mapManager.CurrentMap.playerExploredPoints;
            if (exploredPoints.Count == 0)
            {
                foreach (MapNode node in MapNodes.Where(n => n.Node.point.y == 0))
                    node.SetState(NodeStates.Attainable);
            }
            else
            {
                foreach (Point point in exploredPoints)  GetNode(point)?.SetState(NodeStates.Visited);

                Node currentNode = mapManager.CurrentMap.GetNodeAtPoint(exploredPoints[exploredPoints.Count - 1]);
                foreach (Point point in currentNode.outgoing)  GetNode(point)?.SetState(NodeStates.Attainable);
            }
        }

        public void SetLineColors()
        {
            foreach (LineConnection connection in lineConnections) connection.SetColor(lineLockedColor);

            List<Point> exploredPoints = mapManager.CurrentMap.playerExploredPoints;
            if (exploredPoints.Count != 0)
            {
                Node currentNode = mapManager.CurrentMap.GetNodeAtPoint(exploredPoints[exploredPoints.Count - 1]);
                foreach (Point outgoing in currentNode.outgoing)
                    lineConnections.FirstOrDefault(conn => 
                        conn.from.Node == currentNode && 
                        conn.to.Node.point.Equals(outgoing))?
                            .SetColor(lineVisitedColor);

                for (int point = 0; point < exploredPoints.Count - 1; point++)
                    lineConnections.FirstOrDefault(conn =>
                        conn.from.Node.point.Equals(exploredPoints[point]) &&
                        conn.to.Node.point.Equals(exploredPoints[point + 1]))?
                            .SetColor(lineVisitedColor);
            }
        }

        private void CreateMapBackground(Map m)
        {
            if (background != null)
            {
                GameObject backgroundObject = new GameObject("Background");
                backgroundObject.transform.SetParent(mapParent.transform);
                
                MapNode bossNode = MapNodes.FirstOrDefault(node => node.Node.roomType == RoomType.Boss);
                float ysize = m.DistanceBetweenFirstAndLastLayers() + yOffset * 2f;
                backgroundObject.transform.localPosition = new Vector3(
                    bossNode.transform.localPosition.x, ysize  / 2f, 0f);
                backgroundObject.transform.localRotation = Quaternion.identity;
                
                SpriteRenderer spriteRenderer = backgroundObject.AddComponent<SpriteRenderer>();
                spriteRenderer.color = backgroundColor;
                spriteRenderer.drawMode = SpriteDrawMode.Sliced;
                spriteRenderer.sprite = background;
                spriteRenderer.size = new Vector2(xSize, ysize);
            }
        }

        private MapNode GetNode(Point p)
        {
            return MapNodes.FirstOrDefault(n => n.Node.point.Equals(p));
        }

        private MapConfig GetConfig(string configName)
        {
            return allMapConfigs.FirstOrDefault(c => c.name == configName);
        }

        public RoomPattern GetRoomPattern(string name)
        {
            return GetConfig(mapManager.CurrentMap.configName)
                .roomPatterns.FirstOrDefault(n => n.name == name);
        }

        public void UpdateAttainableNodes()
        {
            List<Point> exploredPoints = mapManager.CurrentMap.playerExploredPoints; 
            if (exploredPoints.Count > 1)
                GetNode(exploredPoints[exploredPoints.Count - 2])?
                    .Node.outgoing.ForEach(p => GetNode(p)?.SetState(NodeStates.Locked));
            else
                foreach (MapNode node in MapNodes.Where(n => n.Node.point.y == 0))
                    node.SetState(NodeStates.Locked);
            
            MapNode currentNode = GetNode(exploredPoints[exploredPoints.Count - 1]);
            currentNode.SetState(NodeStates.Visited);
            currentNode.Node.outgoing.ForEach(p => GetNode(p)?.SetState(NodeStates.Attainable));
            scrollNonUi.FocusNode(currentNode);
        }

        public void SetCurrentNodeVisited()
        {
            List<Point> exploredPoints = mapManager.CurrentMap.playerExploredPoints;
            GetNode(exploredPoints[exploredPoints.Count - 1]).SetState(NodeStates.Visited);
        }

        public void SetVisible(bool value) 
        {
            firstParent.SetActive(value);
        }
    }
}