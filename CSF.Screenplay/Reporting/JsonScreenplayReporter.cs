using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Reporting
{
    public class JsonScreenplayReporter
    {
        readonly string filePath;

        public void SubscribeTo(IHasPerformanceEvents events)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeFrom(IHasPerformanceEvents events)
        {
            throw new NotImplementedException();
        }

        public JsonScreenplayReporter(string filePath)
        {
            this.filePath = filePath ?? throw new System.ArgumentNullException(nameof(filePath));
        }
    }
}