using System;
using UnityEngine;

namespace Runtime.StateMachine.Utilities
{
	[AttributeUsage(AttributeTargets.Field)]
	public class InitOnlyAttribute : PropertyAttribute { }
}
