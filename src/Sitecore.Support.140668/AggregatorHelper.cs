using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Analytics.Model.Entities;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Analytics.Models;

namespace Sitecore.Support.ContentSearch.Analytics.Aggregators
{
  public static class AggregatorHelper
  {
    [NotNull]
    public static IEnumerable<IAddressIndexable> ResolveAddressIndexables([NotNull] IContact contact)
    {
      if (contact == null)
        throw new ArgumentNullException("contact");

      var contactId = contact.Id.Guid;
      Dictionary<string, IAddress> dbAddresses = ResolveContactAddressesFromDB(contact);
      var indexedAddresses = ResolveContactAddressesFromIndex(contact);

      foreach (var addressKey in dbAddresses.Keys)
      {
        yield return new AddressIndexable(addressKey, contactId, dbAddresses[addressKey]);
      }

      foreach (var address in indexedAddresses)
      {
        if (!dbAddresses.ContainsKey(address.Key))
        {
          yield return new AddressChangeIndexable(new AddressIndexable(address.Key, contactId, null), ChangeEventReason.Deleted);
        }
      }
    }

    [NotNull]
    private static IEnumerable<IndexedAddress> ResolveContactAddressesFromIndex(IContact contact)
    {
      try
      {
        using (var context = ContentSearchManager.GetAnalyticsIndex().CreateSearchContext())
        {
          var addressesQuery = context.GetQueryable<IndexedAddress>().Where(address => address.ContactId == contact.Id.ToGuid());

          var count = addressesQuery.Count();

          if (count == 0)
          {
            return new List<IndexedAddress>();
          }

          return addressesQuery.Take(count + 1).ToList();
        }
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("Cannot get addresses from index for the contact {0}", contact.Id), ex);
      }
    }

    [NotNull]
    private static Dictionary<string, IAddress> ResolveContactAddressesFromDB(IContact contact)
    {
      var dbAddresses = new Dictionary<string, IAddress>();
      var addressesFacet = contact.Facets.FirstOrDefault(kvp => kvp.Value is IContactAddresses).Value;
      if (addressesFacet != null)
      {
        var addresses = (IContactAddresses)addressesFacet;
        foreach (var key in addresses.Entries.Keys)
        {
          dbAddresses.Add(key, addresses.Entries[key]);
        }
      }

      return dbAddresses;
    }
  }
}