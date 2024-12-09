
# Unity MQTT Manager

A simple MQTT Manager for Unity that allows you to connect to an MQTT broker, publish messages, and subscribe to topics. This manager is built using the MQTTnet library and provides a straightforward way to integrate MQTT functionality into your Unity projects.

## Features

- Connect to an MQTT broker.
- Publish messages to specified topics.
- Subscribe to topics and receive messages.
- Easy integration into Unity projects.

## Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/unreliablecode/Unity-MQTT-Manager.git
   ```

2. **Open the project in Unity.**

3. **Add the MQTTnet Library:**
   - Download the latest release of the MQTTnet library from the [MQTTnet GitHub repository](https://github.com/chkr1011/MQTTnet).
   - Place the downloaded DLLs in the `Assets/Plugins` folder of your Unity project.

4. **Add the MQTTManager Script:**
   - Create a new C# script named `MQTTManager.cs` and copy the provided code into it.
   - Attach the `MQTTManager` script to an empty GameObject in your scene.

5. **Configure the Broker Address:**
   - Change the `brokerAddress` variable in the `MQTTManager` script to your MQTT broker's address if needed.

## Usage

- The MQTT Manager will automatically connect to the specified MQTT broker when the game starts.
- You can publish messages by calling the `PublishMessage("your/topic", "your message")` method from other scripts.
- Subscribe to topics using the `SubscribeToTopic("your/topic")` method to listen for incoming messages.

## Example

To publish a message, you can use the following code in another script:

```csharp
public class ExampleUsage : MonoBehaviour
{
    private MQTTManager mqttManager;

    private void Start()
    {
        mqttManager = FindObjectOfType<MQTTManager>();
        mqttManager.PublishMessage("test/topic", "Hello, MQTT!");
    }
}
```

## Important Notes

- Ensure that the MQTT broker you are connecting to is accessible from your Unity application.
- You may need to handle exceptions and errors more robustly depending on your application's requirements.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Author

[unreliablecode](https://github.com/unreliablecode)
