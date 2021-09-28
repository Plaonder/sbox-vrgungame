using Sandbox;
using System;
using System.Linq;

namespace VRGunGame
{
	partial class Player : Sandbox.Player
	{
		[Net, Predicted] AnimEntity LeftHand { get; set; }
		[Net, Predicted] AnimEntity RightHand { get; set; }

		public override void Respawn()
		{
			// If not in VR just give em a regular player
			if ( !Input.VR.IsActive )
			{
				Log.Info( "Not in VR." );

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

			Log.Info( "We're in VR!" );
			
			Controller = new WalkController();
			Camera = new FirstPersonCamera(); 
			Animator = new StandardPlayerAnimator();

			SetModel( "models/citizen/citizen.vmdl" );

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = false;

			base.Respawn();
		}

		/// <summary>
		/// Called every tick!!!
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( !Input.VR.IsActive || !IsServer ) return;

			if (LeftHand == null)
			{
				LeftHand = new AnimEntity();
				LeftHand.SetModel( "models/hands/alyx_hand_left.vmdl" );
			}

			if ( RightHand == null )
			{
				RightHand = new AnimEntity();
				RightHand.SetModel( "models/hands/alyx_hand_right.vmdl" );
			}

			LeftHand.Transform = Input.VR.LeftHand.Transform;
			RightHand.Transform = Input.VR.RightHand.Transform;

			LeftHand.SetAnimFloat( "Thumb", Input.VR.LeftHand.GetFingerValue( FingerValue.ThumbCurl ) );
			LeftHand.SetAnimFloat( "Index", Input.VR.LeftHand.GetFingerValue( FingerValue.IndexCurl ) );
			LeftHand.SetAnimFloat( "Middle", Input.VR.LeftHand.GetFingerValue( FingerValue.MiddleCurl ) );
			LeftHand.SetAnimFloat( "Ring", Input.VR.LeftHand.GetFingerValue( FingerValue.RingCurl ) );

			RightHand.SetAnimFloat( "Thumb", Input.VR.RightHand.GetFingerValue( FingerValue.ThumbCurl ) );
			RightHand.SetAnimFloat( "Index", Input.VR.RightHand.GetFingerValue( FingerValue.IndexCurl ) );
			RightHand.SetAnimFloat( "Middle", Input.VR.RightHand.GetFingerValue( FingerValue.MiddleCurl ) );
			RightHand.SetAnimFloat( "Ring", Input.VR.RightHand.GetFingerValue( FingerValue.RingCurl ) );

			// If you have active children (like a weapon etc) you should call this to simulate those too.
			SimulateActiveChild( cl, ActiveChild );
		}

		public override void OnKilled()
		{
			base.OnKilled();

			if ( RightHand != null )
				RightHand.Delete();

			if ( LeftHand != null )
				LeftHand.Delete();

			EnableDrawing = false;
		}
	}
}
