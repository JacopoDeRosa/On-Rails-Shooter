using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ExtendedUI
{
    public class MiniRadar : MonoBehaviour
    {
        [SerializeField]
        [InfoBox("Do not set scale to negative")]
        private int scale = 5;

        [SerializeField]
        [DetailedInfoBox("Always set Max Markers to 0 when saving as prefab", "When a markers are saved in the prefab it doesn't allow the system to delete them and you won't be able to set a different amount of max markers after")]
        private int _maxMarkers;

        [ShowInInspector]
        [ReadOnly]
        private Queue<MiniRadarMarker> _markerQueue = new Queue<MiniRadarMarker>();

        [ShowInInspector]
        [ReadOnly]
        private Queue<MiniRadarItem> _waitList = new Queue<MiniRadarItem>();

        [FoldoutGroup("Editor Components")]
        [SerializeField]
        private MiniRadarMarker _markerPrefab;

        [FoldoutGroup("Editor Components")]
        [SerializeField]
        private Transform _markersContainer;

       

        public int Scale { get => scale; }

        private void OnValidate()
        {
          if(FindObjectsOfType<MiniRadar>().Length > 1)
          {
                Debug.LogWarning("WARNING: There are 2 or more mini-radar objects in your scene, " +
                    "this is not supported, make sure to only have 1 mini-radar in each scene");
                return;
          }

          if(_markersContainer.childCount != _maxMarkers)
            {
                RegenerateMarkers();
            }
        }

        private void Awake()
        {
            BuildMarkerQueue();
        }

        private void BuildMarkerQueue()
        {
            for (int i = 0; i < _markersContainer.childCount; i++)
            {
                var marker = _markersContainer.GetChild(i).GetComponent<MiniRadarMarker>();
                if (marker != null)
                {
                    _markerQueue.Enqueue(marker);
                }
            }
        }

        [Button]
        private void RegenerateMarkers()
        {
            DeleteAllMarkers();
            InstantiateNewMarkers();
        }

        private void DeleteAllMarkers()
        {
            List<GameObject> toDestroy = new List<GameObject>();

            for (int i = 0; i < _markersContainer.childCount; i++)
            {
                var marker = _markersContainer.GetChild(i);
                toDestroy.Add(marker.gameObject);
              
            }
            foreach (var marker in toDestroy)
            {
                DestroyImmediate(marker);
            }

        }

        private void InstantiateNewMarkers()
        {
            for (int i = 0; i < _maxMarkers; i++)
            {
                var marker = Instantiate(_markerPrefab, _markersContainer);
                marker.SetParent(this);
                marker.gameObject.SetActive(false);
            }
        }

        public void Register(MiniRadarItem item)
        {
            if (item == null) return;
            if (_markerQueue.Count == 0)
            {
                _waitList.Enqueue(item);
                return;
            }
            var marker = _markerQueue.Dequeue();
            marker.gameObject.SetActive(true);
            marker.Init(item);
        }

        public void ResetMarker(MiniRadarMarker marker)
        {
            if (_waitList.Count == 0)
            {
                EnqueueMarker(marker);
            }
            else
            {
                MiniRadarItem item = null;

                while (item == null && _waitList.Count > 0)
                {
                 item = _waitList.Dequeue();
                }

                if(item == null)
                {
                    EnqueueMarker(marker);
                }

                marker.Init(item);
            }
        }

        private void EnqueueMarker(MiniRadarMarker marker)
        {
            if (marker == null) return;
            _markerQueue.Enqueue(marker);
            marker.gameObject.SetActive(false);
        }
    }
}
