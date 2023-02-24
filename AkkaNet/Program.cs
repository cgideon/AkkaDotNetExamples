using Akka.Actor;

// Create actor system to allow actors to talk in-memory. Usually there's only one actor in a process
var actorSystem = ActorSystem.Create("MySystem");

// Props == formula used to start an actor
// Akka.Remote will allow you to deploy an actor from one process to another
var greetingActorProps = Props.Create(factory: () => new GreetingActor());


// Mailboxes hold messages destined for an actor
Props.Create<GreetingActor>().WithMailbox("my-custom-mailbox");

// start greetingActor and get actor reference (IActorRef)
var greeter = actorSystem.ActorOf<GreetingActor>("greeter");

// tell greetingActor a message
// Actor will run its Receive code when it receives this message (it's asynchronous)
// If you need to wait for a response, you use .Ask(...)
greeter.Tell(new Greet("Hello World!"));
