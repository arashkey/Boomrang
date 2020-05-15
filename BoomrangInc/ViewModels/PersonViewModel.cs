// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using Business;
using Microsoft.Practices.Prism.Mvvm;

namespace BoomrangInc.ViewModels
{
    public class PersonViewModel : BindableBase
    {
        private PersonInfo_Person person;

        public PersonViewModel()
        {
            this.person = PersonInfo_Person.GetById(2);
            this.AllColors = new[] { "Red", "Blue", "Green" };
       
        }

        public PersonInfo_Person Person
        {
            get { return this.person; }
            set { SetProperty(ref this.person, value); }
        }

        public IEnumerable<string> AllColors { get; private set; }
      
    }
}
