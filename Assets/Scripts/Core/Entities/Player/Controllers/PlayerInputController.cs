using System;
using Core.Entities.Player.Movement;
using Core.SpriteControllers;
using UnityEngine;
using Zenject;

namespace Core.Entities.Player.Controllers
{
    public class PlayerInputController : MonoBehaviour
    {
    	private PlayerMover _playerMover;
        private PlayerSpriteController _playerSpriteController;
        private Camera _camera;

        [Inject]
        private void Construct(PlayerMover playerMover, PlayerSpriteController playerSpriteController)
        {
            _playerMover = playerMover;
            _playerSpriteController = playerSpriteController;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _playerMover?.Move(GetMovementInput(), Time.deltaTime); 
            _playerSpriteController?.UpdatePlayerSprite();
			CheckPlayerWorldPosition();
        }

        private void CheckPlayerWorldPosition()
        {
           var viewportPosition = _camera.WorldToViewportPoint(transform.position);
           if (viewportPosition.x < 0)
           {
               viewportPosition.x = 1;
               transform.position = _camera.ViewportToWorldPoint(viewportPosition);
           }
           // if (viewportPosition.x > 1)
           // {
           //     viewportPosition.x = 0;
           //     transform.position = _camera.ViewportToWorldPoint(viewportPosition);
           // }
           if (viewportPosition.y < 0)
           {
               viewportPosition.y = 1;
               transform.position = _camera.ViewportToWorldPoint(viewportPosition);
           } 
           if (viewportPosition.y > 1)
           {
               viewportPosition.y = 0;
               transform.position = _camera.ViewportToWorldPoint(viewportPosition);
           }
           Debug.Log(viewportPosition);
        }

        private Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis(AxisNames.Horizontal), Input.GetAxis(AxisNames.Vertical));
        }

    }
}
