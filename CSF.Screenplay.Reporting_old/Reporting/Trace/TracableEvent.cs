﻿using System;
using System.Diagnostics;

namespace CSF.Screenplay.Reporting.Trace
{
  class TracableEvent
  {
    internal TraceEventType EventType { get; private set; }

    internal int EventId { get; private set; }

    internal TracableEvent(TraceEventType eventType, int eventId)
    {
      eventType.RequireDefinedValue(nameof(eventType));
      if(eventId < 1)
        throw new ArgumentOutOfRangeException(nameof(eventId),
                                              eventId,
                                              Resources.ExceptionFormats.TracableEventIdMustBePositiveInteger);

      EventType = eventType;
      EventId = eventId;
    }
  }
}
