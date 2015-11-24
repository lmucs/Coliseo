using System;
using UnityEngine;
using System.Collections;
using XInputDotNetPure;

namespace Coliseo
{
	public class Vibrate
	{
		float lightMotor = 0;
		float heavyMotor = 0;
		float _lightMotorDurration = 0;
		float _heavyMotorDurration = 0;

		Controls controls;
		Controller cont;
		public static Vibrate vib;

		void Awake() {
			cont = Controller.isWindows() ? (Controller) new ControllerWin() : (Controller) new ControllerLinOSX();
			//c = Player.controls;
			vib = this;
		}

		void Update()
		{
			if (_lightMotorDurration > 0) {
				_heavyMotorDurration -= Time.deltaTime;
			}
			if (_heavyMotorDurration > 0) {
				_heavyMotorDurration -= Time.deltaTime;
			}

			cont.vibrate(Controller.HeavyMotor, heavyMotor);
			cont.vibrate(Controller.LightMotor, lightMotor);
		}


		float lightMotorDurration
		{
			get { return _lightMotorDurration; }
			set
			{
				if(_lightMotorDurration <= 0)
				{
					lightMotor = 0;
					_lightMotorDurration = 0;

				}
			}
		}

		float heavyMotorDurration
		{
			get { return _heavyMotorDurration; }
			set 
			{
				if(_heavyMotorDurration <= 0)
				{
					heavyMotor = 0;
					_lightMotorDurration = 0;
				}
			}
		}
	}
}

