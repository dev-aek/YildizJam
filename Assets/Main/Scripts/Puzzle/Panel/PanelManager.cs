using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Puzzle.Panel
{
    public class PanelManager : MonoBehaviour
    {
        private bool _isCablesConnected;
        private bool _isBoardConnected;
        
        private int _cableCount;

        private void OnEnable()
        {
            EventBus<CableConnectedEvent>.Subscribe(OnCableConnected);
            EventBus<BoardPlacedEvent>.Subscribe(OnBoardPlaced);
            EventBus<CardVerifiedEvent>.Subscribe(OnCardVerified);
        }
        
        private void OnDisable()
        {
            EventBus<CableConnectedEvent>.Unsubscribe(OnCableConnected);
            EventBus<BoardPlacedEvent>.Unsubscribe(OnBoardPlaced);
            EventBus<CardVerifiedEvent>.Unsubscribe(OnCardVerified);
        }

        private void OnCableConnected(CableConnectedEvent @event)
        {
            _cableCount++;
            if (_cableCount == 3)
            {
                _isCablesConnected = true;
            }
        }

        private void OnBoardPlaced(BoardPlacedEvent @event)
        {
            _isBoardConnected = true;
        }

        private void OnCardVerified(CardVerifiedEvent @event)
        {
            if (_isCablesConnected && _isBoardConnected)
            {
                Debug.Log("Panel is activated!");
                EventBus<PanelActivatedEvent>.Dispatch(new PanelActivatedEvent());
            }
        }
    }
}