using System;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Atmosfer
{
    public class TutorialUIManager : MonoBehaviour
    {
        [Header("Valve Elements")] 
        [SerializeField] private GameObject valveTutorial;
        
        [Header("Light Elements")]
        [SerializeField] private GameObject lightTutorial;
        
        [Header("Panel Elements")]
        [SerializeField] private GameObject panelTutorial;


        private void Start()
        {
            valveTutorial.SetActive(false);
            lightTutorial.SetActive(false);
            panelTutorial.SetActive(false);
        }

        private void OnEnable()
        {
            EventBus<InteractionTutorialEvent>.Subscribe(OnInteractionTutorial);
        }
        
        private void OnDisable()
        {
            EventBus<InteractionTutorialEvent>.Unsubscribe(OnInteractionTutorial);
        }

        private void OnInteractionTutorial(InteractionTutorialEvent @event)
        {
            if (@event.IsShow)
            {
                switch (@event.PuzzleLevel)
                {
                    case LevelEnum.Valve:
                        valveTutorial.SetActive(true);
                        break;
                    case LevelEnum.Light:
                        lightTutorial.SetActive(true);
                        break;
                    case LevelEnum.Panel:
                        panelTutorial.SetActive(true);
                        break;
                }
            }
            else
            {
                valveTutorial.SetActive(false);
                lightTutorial.SetActive(false);
                panelTutorial.SetActive(false);
            }
        }
    }
}