namespace EsotericFiction {
    public class DescriptionEpisode : IEpisode {
        public string Description { get; }

        public DescriptionEpisode(string description) {
            Description = description;
        }

        public void Play(IEpisode.Token token) {
            token.RemoveEpisode(this);
            Act.WriteLine(Description);
        }

    }
}
