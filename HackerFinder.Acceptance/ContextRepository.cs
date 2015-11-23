using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace HackerFinder.Acceptance
{
    public static class ContextRepository
    {
        public static void SetInContext<T>(T target)
        {
            ScenarioContext.Current.Set<T>(target);
        }

        public static T GetFromContext<T>()
        {
            return ScenarioContext.Current.Get<T>();
        }
    }
}
