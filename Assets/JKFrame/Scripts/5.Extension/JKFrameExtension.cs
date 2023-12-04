using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JKFrame
{
    public static class JKFrameExtension
    {
        #region GameObject
        public static bool IsNull(this GameObject obj)
        {
            return ReferenceEquals(obj, null);
        }
        #endregion
    }
}