using System;
using System.Threading.Tasks;
using Chat.Shared;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion.Client;
using UnityEngine;
using UnityEngine.UI;

public class ChatComponent : MonoBehaviour
{
    
    public Button SendMessageButton;

    public InputField Input;
    
    private ChannelBase channel;
    
    private IChatService client;
    
    async void Start()
    {
        await this.InitializeClientAsync();
        this.InitializeUi();
    }


    async void OnDestroy()
    {
        // // Clean up Hub and channel
        // shutdownCancellation.Cancel();
        //
        // if (this.streamingClient != null) await this.streamingClient.DisposeAsync();
        if (this.channel != null) await this.channel.ShutdownAsync();
    }
    
    
    private async Task InitializeClientAsync()
    {
        // Initialize the Hub
        // NOTE: If you want to use SSL/TLS connection, see InitialSettings.OnRuntimeInitialize method.
        this.channel = GrpcChannel.ForAddress(SystemConstants.ServerUrl);

        // while (!shutdownCancellation.IsCancellationRequested)
        {
            try
            {
                Debug.Log($"Connecting to the server...");
                // this.streamingClient = await StreamingHubClient.ConnectAsync<IChatHub, IChatHubReceiver>(this.channel, this, cancellationToken: shutdownCancellation.Token);
                // this.RegisterDisconnectEvent(streamingClient);
                Debug.Log($"Connection is established.");
                // break;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        
            Debug.Log($"Failed to connect to the server. Retry after 5 seconds...");
            await Task.Delay(5 * 1000);
        }

        this.client = MagicOnionClient.Create<IChatService>(this.channel);
    }
    
    private void InitializeUi()
    {
//        this.isJoin = false;

//        this.SendMessageButton.interactable = false;
//        this.ChatText.text = string.Empty;
        this.Input.text = string.Empty;
        this.Input.placeholder.GetComponent<Text>().text = "Please enter your name.";
        // this.JoinOrLeaveButtonText.text = "Enter the room";
        // this.ExceptionButton.interactable = false;
    }
    
    public async void SendMessage()
    {
        // if (!this.isJoin)
        //     return;

        var message = new ChatMessage
        {
            UserName = "TODO",
            Message = Input.text
        };
        await this.client.SendReportAsync(message);

        this.Input.text = string.Empty;
    }    

}
