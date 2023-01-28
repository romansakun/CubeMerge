using System.Collections.Generic;

namespace BTreeUtility.Nodes
{
    public abstract class ActionWithOptions<T, O> : IAction where T: class, IAIContext
    {
        protected readonly List<IOptionScorer<T, O>> _scorers;
        
        public INode Next { get; set; }


        protected ActionWithOptions(List<IOptionScorer<T, O>> scorers)
        {
            _scorers = scorers;
        }

        public void Execute(IAIContext context)
        {
            Execute(context as T);
        }

        protected abstract void Execute(T context);

        public O GetBest(T context, IEnumerable<O> options)
        {
            O best = default(O);
            float maxScore = float.MinValue;

            foreach (var option in options)
            {
                var accumulator = 0f;
                for (int j = 0; j < _scorers.Count; j++)
                {
                    var scorer = _scorers[j];
                    accumulator += scorer.Score(context, option);
                }
                if (accumulator >= maxScore)
                {
                    best = option;
                    maxScore = accumulator;
                }
            }
            return best;
        }

        public O GetWorst(T context, IEnumerable<O> options)
        {
            O worst = default(O);
            float minScore = float.MaxValue;

            foreach (var option in options)
            {
                float accumulator = 0f;
                for (int j = 0; j < _scorers.Count; j++)
                {
                    var scorer = _scorers[j];
                    accumulator += scorer.Score(context, option);
                }
                if (accumulator < minScore)
                {
                    worst = option;
                    minScore = accumulator;
                }
            }
            return worst;
        }    
        
        public Dictionary<O, float> GetAllScores(T context, IEnumerable<O> options)
        {
            var result = new Dictionary<O, float>(_scorers.Count);
            foreach (var option in options)
            {
                var accumulator = 0f;
                for (int j = 0; j < _scorers.Count; j++)
                {
                    var scorer = _scorers[j];
                    accumulator += scorer.Score(context, option);
                }
                result.Add(option, accumulator);
            }
            return result;
        }
    }
}