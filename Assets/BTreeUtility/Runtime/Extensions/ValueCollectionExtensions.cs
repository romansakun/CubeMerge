using System;
using System.Collections.Generic;

namespace BTreeUtility
{
    public static class ValueCollectionExtensions
    {
        public static V GetMaxScoreValue<V>(this Dictionary<V, float> scores)
        {
            if (scores == null || scores.Count == 0)
                return default;

            var maxValue = default(V);
            var maxScore = float.MinValue;
            var enumerator = scores.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var score = enumerator.Current.Value;
                if (score <= maxScore)
                    continue;

                maxValue = enumerator.Current.Key;
                maxScore = score;
            }
            enumerator.Dispose();
            
            return maxValue;
        }

        public static V GetMinScoreValue<V>(this Dictionary<V, float> scores)
        {
            if (scores == null || scores.Count == 0)
                return default;

            var minValue = default(V);
            var minScore = float.MaxValue;
            var enumerator = scores.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var score = enumerator.Current.Value;
                if (score > minScore)
                    continue;

                minValue = enumerator.Current.Key;
                minScore = score;
            }
            enumerator.Dispose();
            
            return minValue;
        }

        public static V GetMaxScoreWithPredicateValue<V>(this Dictionary<V, float> scores, Predicate<V> func)
        {
            if (scores == null || scores.Count == 0)
                return default;

            var maxValue = default(V);
            var maxScore = float.MinValue;
            var enumerator = scores.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current.Key;
                var score = enumerator.Current.Value;
                if (score <= maxScore || !func(value))
                    continue;

                maxValue = value;
                maxScore = score;
            }
            enumerator.Dispose();
            
            return maxValue;
        }

        public static V GetMinScoreWithPredicateValue<V>(this Dictionary<V, float> scores, Predicate<V> func)
        {
            if (scores == null || scores.Count == 0)
                return default;

            var minValue = default(V);
            var minScore = float.MaxValue;
            var enumerator = scores.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var score = enumerator.Current.Value;
                var value = enumerator.Current.Key;
                
                if (score > minScore || !func(value))
                    continue;

                minValue = value;
                minScore = score;
            }
            enumerator.Dispose();
            
            return minValue;
        }
    }
}