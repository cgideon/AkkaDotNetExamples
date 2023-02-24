using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization;
using System;
public sealed class AkkaService : IHostedService
{
    private IActorRef _actorRef;
    private ActorSystem _actorSystem;
    private readonly IServiceProvider _serviceProvider;

    private readonly IHostedApplicationLifetime _applicationLifetime;

    public AkkaService(IServiceProvider sp, IHostedApplicationLifetime applicationLifetime)
    {
        _serviceProvider = sp;
        _applicationLifetime = applicationLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var bootstrap = BoostrapSetup.Create();
        var diSetup = DependencyResolverSetup.Create(_serviceProvider);
        var actorSystemSetup = bootstrap.And(diSetup);

        // start ActorSystem
        _actorSystem = ActorSystem.Create("headless-service", actorSystemSetup);
        _actorRef = _actorSystem.ActorOf(HeadlessActor.Prop());


        // add a continuation task that will guarantee shutdown of application if ActorSystem terminates
        _actorSystem.WhenTerminated.ContinueWith(tr => {
            _applicationLifetime.StopApplication();
        });

        return Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        // may not strictly be necessary but guarantees that the shutdown of the cluster is graceful
        await CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
    }
}