

using System;

namespace Order.Framework.Core.Utility;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
