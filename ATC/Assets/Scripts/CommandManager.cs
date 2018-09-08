using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour {

    public static CommandManager singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    [SerializeField] private List<Command> commands = new List<Command>();
    public List<Command> Commands { get { return commands; } }

    private void Start()
    {
        commands.Add(new Command_ChangeAltitude());
    }
}
