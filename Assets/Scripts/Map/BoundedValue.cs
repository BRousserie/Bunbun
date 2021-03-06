﻿using System;
using Random = UnityEngine.Random;

namespace Map
{
    [Serializable]
    public class BoundedFloat
    {
        public float min;
        public float max;
        private float value = 0.0f;

        public float GetValue()
        {
            return (value == 0.0f) ? GetNewValue() : value;
        }
        public float GetNewValue()
        {
            return value = Random.Range(min, max);
        }
    }

    [Serializable]
    public class BoundedInt
    {
        public int min;
        public int max;
        private int value;

        public int GetValue()
        {
            return (value == 0) ? GetNewValue() : value;
        }
        public int GetNewValue()
        {
            return value = Random.Range(min, max + 1);
        }
    }
}