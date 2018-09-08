using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour {

    public static ControlPanel singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    [SerializeField] private InputField inputField;
    [SerializeField] private List<Text> outputTextFields = new List<Text>();

    [SerializeField] private List<ConsoleOutput> outputs = new List<ConsoleOutput>();
    private int consoleViewingIndex = 0;

    // Use this for initialization
    void Start() {
        inputField.onEndEdit.AddListener((e) => { OnCommandEnter(e); });
        inputField.onValueChanged.AddListener((e) => { inputField.text = e.ToUpper(); });
        UpdateConsoleOutput();
	}

    public void OnCommandEnter(string command)
    {
        foreach (Command c in CommandManager.singleton.Commands)
        {
            if (!c.CheckCommand(command)) continue;

            c.InvokeCommand(command);
            OnCommandAccepted();
            return;
        }
        
        OnCommandDeclined(new ConsoleOutput("Command Not Recognised", CustomHelper.NewColor(226, 126, 91)));
    }

    private void OnCommandAccepted()
    {        
        inputField.text = "";
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }

    private void OnCommandDeclined(ConsoleOutput output)
    {
        Output(output.Output, output.Colour);
        inputField.text = "";
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }

    public void Output(string output, Color colour)
    {
        outputs.Insert(0, new ConsoleOutput(output, colour));
        UpdateConsoleOutput();
    }

    private void UpdateConsoleOutput()
    {
        foreach (Text textField in outputTextFields)
        {
            textField.text = "";
        }

        for (int i = consoleViewingIndex; i < outputs.Count; i++)
        {
            if (i >= outputs.Count) continue;
            outputTextFields[i].color = outputs[i].Colour;
            outputTextFields[i - consoleViewingIndex].text = "[" + outputs[i].Time + "] " + outputs[i].Output;
        }
    }
}

[System.Serializable]
public class ConsoleOutput
{
    [SerializeField] private string output;
    public string Output { get { return output; } }

    [SerializeField] private Color colour;
    public Color Colour { get { return colour; } }

    [SerializeField] private string time;
    public string Time { get { return time; } }

    public ConsoleOutput(string output, Color colour)
    {
        this.output = output.ToUpper();
        this.colour = colour;
        this.time = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
    }
}
