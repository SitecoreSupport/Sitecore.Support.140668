using System.Collections.Generic;
using Sitecore.Analytics.Model.Entities;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Models;
using System.Linq;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Analytics.Extensions;

namespace Sitecore.Support.ContentSearch.Analytics.Aggregators
{
  public class ContactChangeAddressAggregator : Sitecore.ContentSearch.Analytics.Aggregators.ContactChangeAddressAggregator
  {
    public ContactChangeAddressAggregator(string name)
      : base(name)
    {
    }

    protected override IEnumerable<IAddressIndexable> ResolveIndexables(IContact contact, ChangeEventReason reason)
    {
      if (!contact.ShouldBeIndexed())
      {
        ObservationLog.Log.Debug(() => string.Format("The contact addresses will not be indexed because contact {0} does not have an identifier and the system is not configured to index anonymous contacts and their interactions.", contact.Id), null);
        return Enumerable.Empty<IAddressIndexable>();
      }
      return AggregatorHelper.ResolveAddressIndexables(contact);
    }
  }
}