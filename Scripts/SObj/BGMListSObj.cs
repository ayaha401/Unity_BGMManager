using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGM
{
    [CreateAssetMenu(menuName = "BGMManager/BGMListData")]
    public class BGMListSObj : ScriptableObject
    {
        public List<BGM_SObj> bgmDatas = new List<BGM_SObj>();
    }
}

