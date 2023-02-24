using System.ComponentModel;
using Akka.Actor;
using Akka.DependencyInjection;

public class AkkaService : IHostedService, IActorBridge
{
    private ActorSystem _actorSystem;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private IActorRef _actorRef;

    private readonly IHostApplicationLifetime _applicationLifetime;

    public AkkaService(IServiceProvider serviceProvider, IHostApplicationLifetime appLifetime, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _applicationLifetime = appLifetime;
        _configuration = configuration;
    }

    public Task<T> Ask<T>(object message)
    {
        throw new NotImplementedException();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var bootstrap = BootstrapSetup.Create();

        // enable DI support inside this ActorSystem
        var diSetup = DependencyResolverSetup.Create(_serviceProvider);

        // merge this setup into ActorySystemSetup
        var actorSystemSetup = bootstrap.And(diSetup);

        // start actor system
        _actorSystem = ActorSystem.Create("akka-universe", actorSystemSetup);

        _actorRef = _actorSystem.ActorOf(Worker.Prop())
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Tell(object message)
    {
        throw new NotImplementedException();
    }
}