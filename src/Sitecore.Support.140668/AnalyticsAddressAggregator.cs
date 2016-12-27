using System.Collections.Generic;
using Sitecore.Analytics.Aggregation.Pipeline;
using Sitecore.ContentSearch.Analytics.Models;

namespace Sitecore.Support.ContentSearch.Analytics.Aggregators
{
  public class AnalyticsAddressAggregator : Sitecore.ContentSearch.Analytics.Aggregators.AnalyticsAddressAggregator
  {
    public AnalyticsAddressAggregator(string name) : base(name) { }

    protected override IEnumerable<IAddressIndexable> ResolveIndexables(AggregationPipelineArgs args)
    {
      if (args.Context.Contact == null) return new AddressIndexable[0];

      return AggregatorHelper.ResolveAddressIndexables(args.Context.Contact);
    }
  }
}