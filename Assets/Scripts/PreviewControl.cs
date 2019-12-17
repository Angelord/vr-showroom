using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Showroom {
    public abstract class PreviewControl : MonoBehaviour {

        public abstract bool Locked { get; }
        
    }
}