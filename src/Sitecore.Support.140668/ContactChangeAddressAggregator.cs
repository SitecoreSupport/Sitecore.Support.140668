using System.Collections.Generic;
using Sitecore.Analytics.Model.Entities;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Models;

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
      return AggregatorHelper.ResolveAddressIndexables(contact);
    }
  }
}