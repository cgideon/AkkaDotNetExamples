using System.ComponentModel;
internal class HeadlessActor : ReceiveActor
{
    public HeadlessActor()
    {

    }

    public static Props Prop() 
    {
        return Props.Create<HeadlessActor>();
    }
}