using System.Collections.Generic;
using Sitecore.Analytics.Aggregation.Pipeline;
using Sitecore.ContentSearch.Analytics.Models;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Analytics.Extensions;

namespace Sitecore.Support.ContentSearch.Analytics.Aggregators
{
  public class AnalyticsAddressAggregator : Sitecore.ContentSearch.Analytics.Aggregators.AnalyticsAddressAggregator
  {
    public AnalyticsAddressAggregator(string name) : base(name) { }

    protected override IEnumerable<IAddressIndexable> ResolveIndexables(AggregationPipelineArgs args)
    {
      if (args.Context.Contact == null) return new AddressIndexable[0];
      if (!args.Context.Contact.ShouldBeIndexed())
      {
        ObservationLog.Log.Debug(() => string.Format("The contact addresses will not be indexed because contact {0} does not have an identifier and the system is not configured to index anonymous contacts and their interactions.", args.Context.Contact.Id), null);
        return new AddressIndexable[0];
      }
      return AggregatorHelper.ResolveAddressIndexables(args.Context.Contact);
    }
  }
}