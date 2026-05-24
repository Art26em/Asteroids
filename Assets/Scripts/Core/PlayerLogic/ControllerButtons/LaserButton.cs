using Core.WeaponsLogic;
using Zenject;

namespace Core.PlayerLogic.ControllerButtons
{
    public class LaserButton : HoldingButton
    {
        private LaserWeapon _laserWeapon;

        [Inject]
        private void Construct(LaserWeapon laserWeapon)
        {
            _laserWeapon = laserWeapon;
        }

        private void Update()
        {
            if (IsHolding)
            {
                _laserWeapon.Shoot();
            }
        }    
    }
}