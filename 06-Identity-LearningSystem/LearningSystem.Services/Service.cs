namespace LearningSystem.Services
{
    using Data;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new LearningSystemContext();
        }

        protected LearningSystemContext Context { get; }
    }
}