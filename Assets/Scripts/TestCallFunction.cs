using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LLMUnity;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine.Events;

public class TestCallFunction : MonoBehaviour
{
    public LLMCharacter MyCharacter;
    public TMP_InputField MyInputField;
    public TMP_Text TextResponse;
    public AiMultipleChoiceEvent[] MultipleChoiceEvents;
    string aiResponse;

    private void Start()
    {
        MyInputField.onEndEdit.AddListener(TriggerLLM);
        MyCharacter.grammarString = MultipleChoiceGrammar();
        print(ConstructPrompt("piedi"));
    }
    public void TriggerLLM(string inputString)
    {
        _ = MyCharacter.Chat(ConstructPrompt(inputString), AiResponse, ResponseCompleted);
        MyInputField.text = string.Empty;
    }

    void AiResponse(string inResponse)
    {
        print(inResponse);
        TextResponse.text = inResponse;
        aiResponse = inResponse;
    }

    void ResponseCompleted()
    {
        print("risposta completa, risultato:" + aiResponse);
        GetEventFromTopic(aiResponse).Invoke();

    }

    UnityEvent GetEventFromTopic(string topic)
    {
        for (int i = 0; i < MultipleChoiceEvents.Length; i++)
        {
            if (topic == MultipleChoiceEvents[i].TopicString)
            {
                return MultipleChoiceEvents[i].AiTriggerEvent;
                break;
            }
        }
        return null;
    }
    string MultipleChoiceGrammar()
    {
        return "root ::= (\"" + string.Join("\" | \"", GetFunctionNames()) + "\")";
    }

    string[] GetFunctionNames()
    {
        string[] multipleChoiceNames = new string[MultipleChoiceEvents.Length];
        for (int i = 0; i < MultipleChoiceEvents.Length; i++)
        {
            multipleChoiceNames[i] = MultipleChoiceEvents[i].TopicString;
        }

        return multipleChoiceNames;
    }

    string ConstructPrompt(string message)
    {
        string prompt = "Which of the following choices matches best the input?\n\n";
        prompt += "Input:" + message + "\n\n";
        prompt += "Choices:\n";

        for (int i = 0; i < MultipleChoiceEvents.Length; i++)
        {
            prompt += "- ";
            prompt += MultipleChoiceEvents[i].TopicString;
            prompt += "\n";
        }


        foreach (string functionName in GetFunctionNames()) prompt += $"- {functionName}\n";
        prompt += "\nAnswer directly with the choice";
        return prompt;
    }

    [System.Serializable]
    public class AiMultipleChoiceEvent
    {
        public string TopicString;
        public UnityEvent AiTriggerEvent;
    }
}