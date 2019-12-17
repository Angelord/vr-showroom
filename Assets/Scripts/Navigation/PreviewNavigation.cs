using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Showroom.Navigation {
    public abstract class PreviewNavigation : MonoBehaviour {

        public abstract bool Locked { get; }
    }
}