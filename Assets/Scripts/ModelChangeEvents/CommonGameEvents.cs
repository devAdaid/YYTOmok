using AY.Core;

namespace ModelChangeEvents
{
    public class CommonGameEvents
    {
        public class ActorChanged : ModelChangeEvent
        {
            public readonly ActorType CurrentActor;

            public ActorChanged(ActorType currentActor)
            {
                CurrentActor = currentActor;
            }
        }
    }
}