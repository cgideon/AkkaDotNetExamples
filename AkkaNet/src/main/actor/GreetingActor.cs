using Akka.Actor;
using Akka.Event;

public class GreetingActor : ReceiveActor //This is the actor basetype
{
    
    private readonly ILoggingAdapter _log = Context.GetLogger(); //Handle to built-in logging system. This is automatically thread-safe

    public GreetingActor()
    {
        
        //This is telling the actor what types of messages it can receive
        Receive<Greet>(greet => {
            //log the message being sent
            _log.Info(format: "Received {0}", greet);

            Console.WriteLine("[Thread {0}] Greeting{1}", Thread.CurrentThread.ManagedThreadId, greet.Who);
        });
    }
}