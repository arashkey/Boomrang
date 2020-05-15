// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace BoomrangInc.ViewModels
{
    public class MasterPageViewModel
    {
        public MasterPageViewModel()
        {
            this.SubmitCommand = new DelegateCommand<object>(this.OnSubmit);
            this.PersonViewModel = new PersonViewModel();
            this.ResetCommand = new DelegateCommand(this.OnReset);
        }

        public ICommand SubmitCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public PersonViewModel PersonViewModel { get; set; }

        private void OnSubmit(object obj)
        {
            Debug.WriteLine(this.BuildResultString());
        }

        private void OnReset()
        {
            this.PersonViewModel.Person= new  Business.PersonInfo_Person();
        }

        private string BuildResultString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Name: ");
            builder.Append(this.PersonViewModel.Person.Name);
            builder.Append("\nAge: ");
            builder.Append(this.PersonViewModel.Person.Family);
            builder.Append("\nFather 1: ");
            builder.Append(this.PersonViewModel.Person.Father);
            builder.Append("\nMother : ");
            builder.Append(this.PersonViewModel.Person.Mother);
            return builder.ToString();
        }
    }
}
