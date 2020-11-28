using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public static MapPlayerTracker Instance;
        public float enterNodeDelay = 1f;
        public bool lockAfterSelecting;
        public MapManager mapManager;
        public MapView view;
        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (!Locked)
            {
                if (mapManager.CurrentMap.playerExploredPoints.Count == 0)
                {
                    if (mapNode.Node.point.y == 0)
                        SendPlayerToNode(mapNode);
                    else
                        PlayWarningThatNodeCannotBeAccessed();
                }
                else
                {
                    Point currentPoint = mapManager.CurrentMap.playerExploredPoints[mapManager.CurrentMap.playerExploredPoints.Count - 1];
                    Node currentNode = mapManager.CurrentMap.GetNodeAtPoint(currentPoint);

                    if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                        SendPlayerToNode(mapNode);
                    else
                        PlayWarningThatNodeCannotBeAccessed();
                }
            }        
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.playerExploredPoints.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.UpdateAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            Debug.Log("Entering node: " + mapNode.Node.roomPatternName + " of type: " + mapNode.Node.roomType);
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map: 
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false
            switch (mapNode.Node.roomType)
            {
                case RoomType.MinorEnemy:
                    break;
                case RoomType.EliteEnemy:
                    break;
                case RoomType.RestSite:
                    break;
                case RoomType.Treasure:
                    break;
                case RoomType.Store:
                    break;
                case RoomType.Boss:
                    break;
                case RoomType.Mystery:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}