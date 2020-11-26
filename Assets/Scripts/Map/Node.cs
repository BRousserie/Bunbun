using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Map
{
    public class Node
    {
        public readonly string roomPatternName;
        public readonly List<Point> incoming = new List<Point>();
        public readonly List<Point> outgoing = new List<Point>();
        public readonly Point point;

        [JsonConverter(typeof(StringEnumConverter))]
        public readonly RoomType roomType;

        public Vector2 position;

        public Node(RoomType roomType, string roomPatternName, Point point)
        {
            this.roomType = roomType;
            this.roomPatternName = roomPatternName;
            this.point = point;
        }

        public void AddIncoming(Point p)
        {
            if (!incoming.Any(element => element.Equals(p)))
                incoming.Add(p);
        }

        public void AddOutgoing(Point p)
        {
            if (!outgoing.Any(element => element.Equals(p)))
                outgoing.Add(p);
        }

        public void RemoveIncoming(Point p)
        {
            incoming.Remove(p);
        }

        public void RemoveOutgoing(Point p)
        {
            outgoing.Remove(p);
        }

        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }
    }
}