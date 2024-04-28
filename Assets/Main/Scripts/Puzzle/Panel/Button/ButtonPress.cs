using System;
using DG.Tweening;
using UnityEngine;

namespace Puzzle.Panel.Button
{
    public class ButtonPress : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Debug.Log("Button pressed!");
            transform.DOMoveY(-0.3f, 0.3f);
        }
    }
}