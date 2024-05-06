using System.IO;
using Grpc.Core;
using MagicOnion.Client;
using Grpc.Net.Client;
// using MagicOnion.Unity;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

    // [MagicOnionClientGeneration(typeof(ChatApp.Shared.Services.IChatService))]
    partial class MagicOnionClientInitializer {}

    class InitialSettings
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RegisterResolvers()
        {
            // // NOTE: Currently, CompositeResolver doesn't work on Unity IL2CPP build. Use StaticCompositeResolver instead of it.
            // StaticCompositeResolver.Instance.Register(
            //     MagicOnionClientInitializer.Resolver,
            //     MessagePack.Resolvers.GeneratedResolver.Instance,
            //     BuiltinResolver.Instance,
            //     PrimitiveObjectResolver.Instance
            // );

            MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions
                .WithResolver(StaticCompositeResolver.Instance);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnRuntimeInitialize()
        {
            // // Use Grpc.Net.Client instead of C-core gRPC library.
            // GrpcChannelProviderHost.Initialize(
            //     new GrpcNetClientGrpcChannelProvider(() => new GrpcChannelOptions()
            //     {
            //         HttpHandler = new Cysharp.Net.Http.YetAnotherHttpHandler()
            //         {
            //             Http2Only = true,
            //         }
            //     }));
            
            // Connect to the server using gRPC channel.
            var channel = GrpcChannel.ForAddress("http://localhost:5000",new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure
            });
            // Create a proxy to call the server transparently.
            // var client = MagicOnionClient.Create<IChatService>(channel);
            
        }
    }
