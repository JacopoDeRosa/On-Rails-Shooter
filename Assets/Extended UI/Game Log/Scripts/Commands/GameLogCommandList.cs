using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ExtendedUI
{
    [CreateAssetMenu(fileName = "New Command List", menuName = "New Command List")]
    public class GameLogCommandList : ScriptableObject
    {
        [SerializeField]
        private List<GameLogCommand> _commands;

        public void TryInvokeCommand(string name, string[] args)
        {

            var command = _commands.Find(x => x.Equals(name));
            if (command != null)
            {
                command.Invoke(args);
            }
        }
    }
}
