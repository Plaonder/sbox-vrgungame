using Sandbox;
using System;
using System.Linq;

namespace VRGunGame
{
	partial class Player : Sandbox.Player
	{
		public override void Respawn()
		{
			// If not in VR just give em a regular player
			if ( !Input.VR.IsActive )
			{
				Log.Info( $"{Owner.Name}, we're not in VR." );

				Controller = new WalkController();
				Camera = new FirstPersonCamera();
				Animator = new StandardPlayerAnimator();

				SetModel( "models/citizen/citizen.vmdl" );

				EnableAllCollisions = true;
				EnableDrawing = true;
				EnableHideInFirstPerson = true;
				EnableShadowInFirstPerson = true;

				base.Respawn();
				return;
			}

			Log.Info( $"{Owner.Name}, we're in VR!" );

			Controller = new WalkController();
			Camera = new FirstPersonCamera();
			Animator = new StandardPlayerAnimator();

			SetModel( "models/citizen/citizen.vmdl" );

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = false;

			// Create our VR hands!
			CreateVRHands();

			base.Respawn();
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			
			// If you have active children (like a weapon etc) you should call this to simulate those too.
			SimulateActiveChild( cl, ActiveChild );

			if ( !Input.VR.IsActive || !IsServer ) return;
			SimulateServerHands();

		}

		public override void OnKilled()
		{
			base.OnKilled();

			Log.Error( "I'm dead!" );

			if ( RightHand != null )
				RightHand.Delete();

			if ( LeftHand != null )
				LeftHand.Delete();

			EnableDrawing = false;
		}

	}
}
