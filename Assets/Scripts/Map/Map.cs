using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Map
{
    public class Map
    {
        public string bossNodeName;
        public string configName; // similar to the act name in Slay the Spire
        public List<Node> nodes;
        public List<Point> playerExploredPoints;

        public Map(string configName, string bossNodeName, List<Node> nodes, List<Point> playerExploredPoints)
        {
            this.configName = configName;
            this.bossNodeName = bossNodeName;
            this.nodes = nodes;
            this.playerExploredPoints = playerExploredPoints;
        }

        public Node GetBossNode()
        {
            return nodes.FirstOrDefault(n => n.roomType == RoomType.Boss);
        }

        public float DistanceBetweenFirstAndLastLayers()
        {
            Node bossNode = GetBossNode();
            Node firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);

            if (bossNode != null && firstLayerNode != null)
                return bossNode.position.y - firstLayerNode.position.y;
            else
                return 0f;
        }

        public Node GetNodeAtPoint(Point point)
        {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}