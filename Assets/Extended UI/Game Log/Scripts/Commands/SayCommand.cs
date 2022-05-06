using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedUI
{
    [CreateAssetMenu(fileName = "New Say Command", menuName = "Commands/New Say Command")]
    public class SayCommand : GameLogCommand
    {
        public override void Invoke(string[] args)
        {
            if (args.Length != 2) return;

            args[1] = args[1].Replace('_', ' ');
            GameLog.SystemMessage(args[1]);
        }
    }
}
