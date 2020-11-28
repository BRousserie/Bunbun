using Newtonsoft.Json;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapView view;

        public Map CurrentMap { get; private set; }

        private void Start()
        {
            if (PlayerPrefs.HasKey("Map"))
            {
                string mapJson = PlayerPrefs.GetString("Map");
                Map map = JsonConvert.DeserializeObject<Map>(mapJson);
                // TODO: Compare performance of Contains() with Any(p => p.Equals(map.GetBossNode().point))
                if (!map.playerExploredPoints.Contains(map.GetBossNode().point))
                {
                    CurrentMap = map;
                    view.ShowMap(map);
                } 
                else GenerateNewMap();
            }
            else GenerateNewMap();
        }

        public void GenerateNewMap()
        {
            CurrentMap = MapGenerator.GetMap(config);
            Debug.Log(CurrentMap.ToJson());
            view.ShowMap(CurrentMap);
        }

        public void SaveMap()
        {
            if (CurrentMap != null)
            {
                PlayerPrefs.SetString("Map", JsonConvert.SerializeObject(CurrentMap));
                PlayerPrefs.Save();
            }
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }
    }
}