using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Exceptions;

public class MQTTManager : MonoBehaviour
{
    private IMqttClient mqttClient;
    private string brokerAddress = "broker.hivemq.com"; // Change to your broker address
    private int brokerPort = 1883; // Default MQTT port

    private void Start()
    {
        InitializeMQTTClient();
    }

    private async void InitializeMQTTClient()
    {
        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithClientId("UnityClient_" + Guid.NewGuid())
            .WithTcpServer(brokerAddress, brokerPort)
            .Build();

        try
        {
            await mqttClient.ConnectAsync(options);
            Debug.Log("Connected to MQTT broker.");
            SubscribeToTopic("test/topic"); // Change to your topic
        }
        catch (MqttProtocolViolationException ex)
        {
            Debug.LogError($"Protocol error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Could not connect to MQTT broker: {ex.Message}");
        }

        mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Debug.Log($"Received message: {message} from topic: {e.ApplicationMessage.Topic}");
        });
    }

    public async void PublishMessage(string topic, string message)
    {
        if (mqttClient.IsConnected)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await mqttClient.PublishAsync(mqttMessage);
            Debug.Log($"Published message: {message} to topic: {topic}");
        }
        else
        {
            Debug.LogWarning("MQTT client is not connected.");
        }
    }

    public async void SubscribeToTopic(string topic)
    {
        if (mqttClient.IsConnected)
        {
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
            Debug.Log($"Subscribed to topic: {topic}");
        }
        else
        {
            Debug.LogWarning("MQTT client is not connected.");
        }
    }

    private async void OnApplicationQuit()
    {
        if (mqttClient != null)
        {
            await mqttClient.DisconnectAsync();
            Debug.Log("Disconnected from MQTT broker.");
        }
    }
}
