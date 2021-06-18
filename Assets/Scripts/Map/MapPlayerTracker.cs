using System;
using System.Linq;
using DG.Tweening;
using Room;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Map
{
    public class MapPlayerTracker : Singleton<MapPlayerTracker>
    {
        public float enterNodeDelay = 1f;
        public bool lockAfterSelecting;
        public MapManager mapManager;
        public MapView view;
        public bool Locked { get; set; }

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

            view.SetCurrentNodeVisited();
            mapNode.ShowSwirlAnimation();
            
            DG.Tweening.DOTween.Sequence().AppendInterval(enterNodeDelay)
                .OnComplete(() =>
                {
                    RoomManager.Instance.EnterRoom(mapNode.Node.roomType);
                    view.SetVisible(false);
                    view.UpdateAttainableNodes();
                    view.SetLineColors();
                });
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}