using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastReslectionForHabrahabr.Hydrators
{
    public class ManualContactHydrator : ContactHydratorBase
    {
        protected static readonly PropertyInfo[] _properties;
        static ManualContactHydrator()
        {
            var type = typeof(Contact);
            _properties = type.GetProperties();
        }
        public ManualContactHydrator(IRawStringParser normalizer, IStorage db) : base(normalizer, db)
        {
        }

        protected override Contact GetContact(PropertyToValueCorrelation[] correlations)
        {
            var contact = new Contact();
            foreach (var correlation in correlations)
            {
                switch(correlation.PropertyName)
                {
                    case nameof(Contact.FullName):
                        contact.FullName = correlation.Value;
                        break;
                    case nameof(Contact.Phone):
                        contact.Phone = correlation.Value;
                        break;
                    case nameof(Contact.Age):
                        contact.Age = correlation.Value;
                        break;
                }
            }
            return contact;
        }
    }
}
