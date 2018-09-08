using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_ChangeAltitude : Command {

	public Command_ChangeAltitude()
    {
        identifier = "";
        identifierVaries = true;
        sections = 4;
    }

    public override bool CheckCommand(string command)
    {
        string[] sections = SplitCommand(command);
        if (sections.Length != this.sections) return false;

        Flight flight = FindFlight(sections[0]);
        if (flight == null) return false;

        if (sections[1].ToLower() != "mod") return false;
        if (sections[2].ToLower() != "alt") return false;
        float modification = 0.0f;

        if (float.TryParse(sections[3], out modification))
        {
            // TODO: Invoke Command
            Debug.Log(sections[0] + " will modify altitude " + modification);            
            return true;
        }
        else return false;       
    }

    public override void InvokeCommand(string command)
    {
        base.InvokeCommand(command);

        string[] sections = SplitCommand(command);
        Flight flight = FindFlight(sections[0]);
        AICommand_ChangeAltitude cmd = flight.gameObject.AddComponent<AICommand_ChangeAltitude>();
        cmd.altitudeChange = float.Parse(sections[3]);
    }


}
