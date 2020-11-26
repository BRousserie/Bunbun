using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public static class MapGenerator
    {
        private static MapConfig config;

        private static readonly List<RoomType> RandomRoomTypes = new List<RoomType>
            {RoomType.Store, RoomType.Treasure, RoomType.MinorEnemy, RoomType.RestSite, RoomType.Mystery};

        private static List<List<Point>> paths;
        private static readonly List<List<Node>> nodesLayers = new List<List<Node>>();

        public static Map GetMap(MapConfig _config)
        {
            if (_config != null)
            {
                config = _config;

                GenerateLayers();
                GeneratePaths();
                SetUpConnections();
                RemoveCrossConnections();
                RandomizeNodePositions();

                List<Node> connectedNodes = nodesLayers.SelectMany(n => n)
                    .Where(n => n.incoming.Count > 0 || n.outgoing.Count > 0).ToList();

                string bossNodeName = config.roomPatterns.Where(b => b.type == RoomType.Boss).ToList().Random().name;
                return new Map(config.name, bossNodeName, connectedNodes, new List<Point>());
            }
            else { Debug.LogWarning("Config was null in MapGenerator.Generate()"); return null; }
        }

        private static void GenerateLayers()
        {
            nodesLayers.Clear();
            
            foreach (MapLayer layer in config.layers)
                GenerateNodesInLayer(layer);
        }

        private static void GenerateNodesInLayer(MapLayer layer)
        {
            List<Node> nodesOnThisLayer = new List<Node>();
            Vector2 layerOffset = new Vector2(
                layer.nodesApartDistance * config.GridWidth / 2f,
                layer.distanceFromPreviousLayer.GetNewValue());

            for (int nodeIndex = 0; nodeIndex < config.GridWidth; nodeIndex++)
                nodesOnThisLayer.Add(GenerateNode(layer, nodeIndex, layerOffset));

            nodesLayers.Add(nodesOnThisLayer);
        }

        private static Node GenerateNode(MapLayer layer, int nodeIndex, Vector2 layerOffset)
        {
            RoomType roomType = (Random.Range(0f, 1f) < layer.roomTypeRandomRate) ? GetRandomRoomType() : layer.roomType;
            string roomName = config.roomPatterns.Where(b => b.type == roomType).ToList().Random().name;
            
            return new Node(roomType, roomName, new Point(nodeIndex, nodesLayers.Count))
                { position = new Vector2(-layerOffset.x + nodeIndex * layer.nodesApartDistance, layerOffset.y) };
        }

        private static void GeneratePaths()
        {
            Point bossPoint = GetBossPoint();
            List<Point> preBossPoints = GeneratePreBossPoints(bossPoint);
            paths = new List<List<Point>>();
            
            foreach (Point preBossPoint in preBossPoints)
                AddPathFromFirstLayerTo(preBossPoint, bossPoint);

            int attempts = 0;
            int numOfStartingNodes = config.numOfStartingNodes.GetNewValue();
            
            while (CountPathsUniqueEnds(paths) < numOfStartingNodes && attempts++ < 100)
                AddPathFromFirstLayerTo(preBossPoints[Random.Range(0, preBossPoints.Count)], bossPoint);

            Debug.Log("Attempts to generate paths: " + attempts);
        }

        private static List<Point> GeneratePreBossPoints(Point bossPoint)
        {
            List<int> candidateXs = new List<int>();
            for (int i = 0; i < config.GridWidth; i++)
                candidateXs.Add(i);

            candidateXs.Shuffle();
            IEnumerable<int> preBossXs = candidateXs.Take(config.numOfPreBossNodes.GetNewValue());
            List<Point> preBossPoints = (from x in preBossXs select new Point(x, bossPoint.y - 1)).ToList();
            return preBossPoints;
        }

        private static void AddPathFromFirstLayerTo(Point prebossPoint, Point bossPoint)
        {
            List<Point> path = Path(prebossPoint, 0);
            path.Insert(0, bossPoint);
            paths.Add(path);
        }

        private static void SetUpConnections()
        {
            foreach (List<Point> path in paths)
            {
                for (int nodeIndex = 0; nodeIndex < path.Count; nodeIndex++)
                {
                    Node node = GetNode(path[nodeIndex]);

                    if (0 < nodeIndex) 
                        AddConnection(node, GetNode(path[nodeIndex - 1]));

                    if (nodeIndex < path.Count - 1)
                        AddConnection(GetNode(path[nodeIndex + 1]), node);
                }
            }
        }

        private static void RemoveCrossConnections()
        {
            for (int i = 0; i < config.GridWidth - 1; i++)
            {
                for (int j = 0; j < config.layers.Count - 1; j++)
                {
                    Node node = GetNode(new Point(i, j));
                    Node right = GetNode(new Point(i + 1, j));
                    Node top = GetNode(new Point(i, j + 1));
                    Node topRight = GetNode(new Point(i + 1, j + 1));

                    if (AreCrossConnected(node, right, top, topRight))
                    {
                        // 1) add direct connections:
                        AddConnection(node, top);
                        AddConnection(right, topRight);

                        float rnd = Random.Range(0f, 1f);
                        if (rnd < 0.2f) // remove both cross connections:
                        {
                            RemoveConnection(node, topRight);
                            RemoveConnection(right, top);
                        }
                        else if (rnd < 0.6f)
                            RemoveConnection(node, topRight);
                        else
                            RemoveConnection(right, top);
                    }
                }
            }
        }

        private static bool AreCrossConnected(Node node, Node right, Node top, Node topRight)
        {
            return node != null && right != null && top != null && topRight != null && 
                   !node.HasNoConnections() && !right.HasNoConnections() &&
                   !top.HasNoConnections() && !topRight.HasNoConnections() &&
                   node.outgoing.Any(element => element.Equals(topRight.point)) &&
                   right.outgoing.Any(element => element.Equals(top.point));
        }

        private static void AddConnection(Node a, Node b)
        {
            a.AddOutgoing(b.point);
            b.AddIncoming(a.point);
        }

        private static void RemoveConnection(Node a, Node b)
        {
            a.RemoveOutgoing(b.point);
            b.RemoveIncoming(a.point);
        }

        private static void RandomizeNodePositions()
        {
            for (int layerIndex = 0; layerIndex < nodesLayers.Count; layerIndex++)
            {
                List<Node> layerNodes = nodesLayers[layerIndex];
                MapLayer layer = config.layers[layerIndex];
                float distToNextLayer = (layerIndex + 1 <= config.layers.Count)
                    ? config.layers[layerIndex + 1].distanceFromPreviousLayer.GetValue() : 0f;
                float distToPreviousLayer = layer.distanceFromPreviousLayer.GetValue();

                foreach (Node node in layerNodes)
                {
                    float x = Random.Range(-1f, 1f) * layer.nodesApartDistance / 2f;
                    
                    float yRnd = Random.Range(-1f, 1f);
                    float y = yRnd < 0 ? distToPreviousLayer * yRnd / 2f : distToNextLayer * yRnd / 2f;

                    node.position += new Vector2(x, y) * layer.positionRandomRate;
                }
            }
        }

        private static Node GetNode(Point p)
        {
            if (p.y < nodesLayers.Count &&
                p.x < nodesLayers[p.y].Count) return nodesLayers[p.y][p.x];
            
            else return null;
        }

        private static Point GetBossPoint()
        {
            int y = config.layers.Count - 1;
            
            if (config.GridWidth % 2 == 1)
                return new Point(config.GridWidth / 2, y);

            else return (Random.Range(0, 2) == 0)
                ? new Point(config.GridWidth / 2, y)
                : new Point(config.GridWidth / 2 - 1, y);
        }

        private static int CountPathsUniqueEnds(IEnumerable<List<Point>> paths)
        {
            return (from path in paths select path[path.Count - 1].x).Distinct().Count();
        }

        private static List<Point> Path(Point from, int toY)
        {
            if (from.y != toY)
            {
                int direction = from.y > toY ? -1 : 1;
                List<Point> path = new List<Point> {from};
                
                while (path.Last().y != toY)
                {
                    path.Add(new Point(
                        Random.Range(
                            Mathf.Max(0, path.Last().x - 1),
                            Mathf.Max(path.Last().x + 1, config.GridWidth)),
                        path.Last().y + direction));
                }
                return path;
            }   
            else { Debug.LogError("Points are on same layers, return"); return null; }
        }

        private static RoomType GetRandomRoomType()
        {
            return RandomRoomTypes[Random.Range(0, RandomRoomTypes.Count)];
        }
    }
}