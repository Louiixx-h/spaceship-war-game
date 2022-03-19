using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ListView : MonoBehaviour
    {
        [SerializeField] private List<RoomInfo> m_Rooms;
        [SerializeField] private GameObject m_RoomItem;

        public void SetListView(List<RoomInfo> rooms)
        {
            var transforms = gameObject.GetComponentsInChildren<Transform>();
            if (transforms.Length != 0)
            {
                foreach (var tr in transforms)
                {
                    Destroy(tr.gameObject);
                }
            }
            foreach (var room in rooms)
            {
                GameObject cardObj = Instantiate(m_RoomItem, transform);
                cardObj.GetComponent<RoomItem>().SetRoomInfo(room);
            }
        }
    }
}